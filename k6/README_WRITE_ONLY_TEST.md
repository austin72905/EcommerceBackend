# 專業寫入效能測試說明

## 概述

`write-only-test.js` 是一個專業的寫入效能測試腳本，採用分階段測試策略，用於評估資料庫寫入操作、事務處理、鎖競爭和系統極限。

## 測試策略

與讀取測試不同，寫入測試不能暴力測試，需要分階段進行：

### 第 1 階段：10-50 VU（Deadlock 檢測）

**目的**：檢測是否會出現 Deadlock

**執行方式**：
```bash
TEST_MODE=stage1 k6 run k6/scripts/write-only-test.js
```

**檢測重點**：
- Deadlock 錯誤
- 基本寫入效能
- 系統穩定性

### 第 2 階段：100-200 VU（Transaction Latency 檢測）

**目的**：檢測 transaction latency、row lock、Redis Lua + DB 一致性

**執行方式**：
```bash
TEST_MODE=stage2 MAX_VUS=200 DURATION=3m k6 run k6/scripts/write-only-test.js
```

**檢測重點**：
- Transaction latency 是否飆高
- 是否產生 row lock（長時間事務）
- Redis Lua + DB 是否一致性問題
- 連線池是否足夠

### 第 3 階段：Spike Test（瞬間暴增）

**目的**：測試系統極限，檢測瞬間高負載下的表現

**執行方式**：
```bash
TEST_MODE=spike SPIKE_VUS=1000 SPIKE_DURATION=10s k6 run k6/scripts/write-only-test.js
```

**檢測重點**：
- API timeout
- DB 出現 "Too many clients"
- Redis Lua script 堆積
- MQ 出現瓶頸
- 是否有訂單重複產生

### 完整測試（所有階段）

**執行方式**：
```bash
TEST_MODE=full DURATION=5m k6 run k6/scripts/write-only-test.js
```

## 測試場景

此腳本只包含寫入操作，不包含任何讀取操作：

1. **用戶註冊** (`UserRegister`)
   - 寫入 `Users` 表
   - 涉及密碼加密（BCrypt）
   - 涉及唯一性檢查（Email/Username 索引）

2. **用戶登入** (`UserLogin`)
   - 可能涉及 Session 寫入（Redis）
   - 涉及密碼驗證

3. **購物車操作** (`MergeCartContent`)
   - 寫入 `Cart` 表
   - 寫入 `CartItem` 表
   - 可能涉及庫存檢查（Redis）

4. **提交訂單** (`SubmitOrder`)
   - 寫入多張表：`Order`、`OrderProduct`、`OrderStep`、`Shipment`、`Payment`
   - 涉及事務處理
   - 涉及庫存扣減（Redis）
   - 可能涉及 RabbitMQ 訊息發送

## 執行方式

### 測試模式選擇

腳本支援 4 種測試模式：

1. **stage1**：第 1 階段測試（10-50 VU）
2. **stage2**：第 2 階段測試（100-200 VU）
3. **spike**：Spike Test（瞬間暴增）
4. **full**：完整測試（所有階段）

### 基本執行

```bash
# 預設執行第 1 階段
k6 run k6/scripts/write-only-test.js

# 執行第 1 階段
TEST_MODE=stage1 k6 run k6/scripts/write-only-test.js

# 執行第 2 階段
TEST_MODE=stage2 k6 run k6/scripts/write-only-test.js

# 執行 Spike Test
TEST_MODE=spike k6 run k6/scripts/write-only-test.js

# 執行完整測試
TEST_MODE=full k6 run k6/scripts/write-only-test.js
```

### 自定義參數

```bash
# 第 1 階段自定義
TEST_MODE=stage1 MAX_VUS=50 DURATION=2m k6 run k6/scripts/write-only-test.js

# 第 2 階段自定義
TEST_MODE=stage2 MAX_VUS=200 DURATION=3m k6 run k6/scripts/write-only-test.js

# Spike Test 自定義
TEST_MODE=spike SPIKE_VUS=1000 SPIKE_DURATION=10s k6 run k6/scripts/write-only-test.js

# 指定 API 基礎 URL
TEST_MODE=stage2 BASE_URL=http://localhost:5025 k6 run k6/scripts/write-only-test.js
```

### 完整範例

```bash
# Windows CMD - 第 1 階段
set TEST_MODE=stage1 && k6 run k6/scripts/write-only-test.js

# Windows CMD - Spike Test
set TEST_MODE=spike && set SPIKE_VUS=1000 && set SPIKE_DURATION=10s && k6 run k6/scripts/write-only-test.js

# Linux/Mac - 第 2 階段
TEST_MODE=stage2 MAX_VUS=200 DURATION=3m k6 run k6/scripts/write-only-test.js
```

## 環境變數

| 變數名稱 | 預設值 | 說明 |
|---------|--------|------|
| `TEST_MODE` | stage1 | 測試模式：stage1, stage2, spike, full |
| `MAX_VUS` | 依 TEST_MODE | 最大並發虛擬用戶數（stage1: 50, stage2: 200, spike: 200） |
| `DURATION` | 依 TEST_MODE | 測試持續時間（stage1/stage2: 2m, spike: 30s） |
| `SPIKE_VUS` | 1000 | Spike Test 的瞬間 VU 數 |
| `SPIKE_DURATION` | 10s | Spike Test 的持續時間 |
| `BASE_URL` | http://localhost:5025 | API 基礎 URL |

## 測試結果解讀

### 關鍵指標

1. **請求速率 (RPS)**
   - 寫入操作的 RPS 通常比讀取操作低，這是正常的
   - 如果 RPS < 50，可能是資料庫寫入瓶頸

2. **響應時間百分位數**
   - **第 1 階段**：P50 < 2000ms, P95 < 5000ms, P99 < 10000ms
   - **第 2 階段**：P50 < 3000ms, P95 < 8000ms, P99 < 15000ms
   - **Spike Test**：P95 < 20000ms, P99 < 30000ms（允許較高延遲）

3. **各 API 響應時間**
   - 用戶註冊：P95 < 3000ms
   - 用戶登入：P95 < 2000ms
   - 購物車操作：P95 < 3000ms
   - 訂單提交：P95 < 5000ms（第 1 階段）或 < 8000ms（第 2 階段）

### 特定錯誤檢測

測試結果會顯示以下關鍵指標：

1. **Deadlock 錯誤**
   - 應該 < 1%（第 1 階段）或 < 2%（第 2 階段）
   - 如果 > 0，需要檢查事務順序和鎖定範圍

2. **Timeout 錯誤**
   - 應該 < 1%（第 1 階段）或 < 5%（第 2 階段）
   - 如果過多，檢查連線池和慢查詢

3. **連線錯誤（Too many clients）**
   - 應該 = 0%
   - 如果 > 0，需要增加 PostgreSQL max_connections

4. **訂單重複錯誤**
   - 應該 < 5%
   - 如果過多，需要檢查冪等性機制

5. **長時間事務（> 3秒）**
   - 可能是 row lock 等待
   - 需要檢查事務隔離級別和鎖定範圍

### 效能預估

測試結果會提供：
- **當前訂單提交速率**：實際測量的 orders/s
- **預估最大可支撐寫入量**：基於 P95 響應時間計算
- **建議最大並發訂單數**：保守估計（保留 30% 緩衝）

### 寫入效能分析

測試結果會顯示：
- ✓ 用戶註冊效能良好
- ✓ 購物車操作效能良好
- ✓ 訂單提交效能良好
- ✓ 未檢測到 Deadlock 錯誤
- ✓ 未檢測到 Timeout 錯誤
- ✓ 未檢測到連線錯誤

如果顯示 ⚠ 或 ✗，表示可能需要：
1. 檢查資料庫寫入效能（I/O、事務日誌）
2. 檢查鎖競爭情況
3. 檢查事務隔離級別
4. 檢查是否有不必要的鎖定
5. 檢查 Redis Lua script 效能
6. 檢查 MQ 訊息堆積

## 測試階段建議流程

### 建議執行順序

1. **先執行第 1 階段**
   ```bash
   TEST_MODE=stage1 k6 run k6/scripts/write-only-test.js
   ```
   - 如果未發現 Deadlock，繼續下一步
   - 如果發現 Deadlock，先修復問題

2. **執行第 2 階段**
   ```bash
   TEST_MODE=stage2 k6 run k6/scripts/write-only-test.js
   ```
   - 如果 transaction latency 可接受，繼續下一步
   - 如果 latency 過高，優化後再測試

3. **執行 Spike Test**
   ```bash
   TEST_MODE=spike k6 run k6/scripts/write-only-test.js
   ```
   - 檢查系統是否能在瞬間高負載下恢復
   - 檢查是否有訂單重複產生

4. **（可選）執行完整測試**
   ```bash
   TEST_MODE=full k6 run k6/scripts/write-only-test.js
   ```
   - 綜合分析所有階段的結果

## 與讀取測試的比較

執行此寫入測試後，可以與 `read-only-test.js` 的結果比較：

| 指標 | 寫入測試 | 讀取測試 | 說明 |
|------|---------|---------|------|
| RPS | 較低（正常） | 較高 | 寫入操作比讀取慢 |
| P50 | 較高 | 較低 | 寫入涉及更多處理 |
| P95 | 較高 | 較低 | 寫入涉及事務和鎖 |
| 錯誤率 | 可能較高 | 較低 | 寫入可能出現衝突（409、400） |
| Deadlock | 可能出現 | 不會出現 | 寫入涉及鎖競爭 |
| Timeout | 可能出現 | 較少 | 寫入涉及長時間事務 |

## 常見問題與解決方案

### 1. 檢測到 Deadlock 錯誤

**症狀**：測試結果顯示 Deadlock 錯誤 > 0

**可能原因**：
- 事務順序不一致
- 鎖定範圍過大
- 索引使用不當

**解決方案**：
1. 檢查資料庫日誌確認 Deadlock 詳情
2. 統一事務順序（例如：總是先鎖定 User，再鎖定 Order）
3. 減少鎖定範圍（使用 SELECT FOR UPDATE 時只鎖定必要的行）
4. 檢查索引是否正確使用

### 2. 大量 Timeout 錯誤

**症狀**：測試結果顯示 Timeout 錯誤 > 5%

**可能原因**：
- 資料庫連線池不足
- 事務超時設定過短
- 慢查詢導致阻塞

**解決方案**：
1. 增加 CommandTimeout（已設定為 60 秒）
2. 檢查並優化慢查詢
3. 檢查 Redis/MQ 連線是否正常
4. 增加連線池大小

### 3. 連線錯誤（Too many clients）

**症狀**：測試結果顯示連線錯誤 > 0

**可能原因**：
- PostgreSQL max_connections 設定過低
- 連線池 MaxPoolSize 設定過高

**解決方案**：
1. 增加 PostgreSQL max_connections（建議 300-400）
2. 調整連線池 MaxPoolSize（已設定為 200）
3. 檢查是否有連線洩漏

### 4. 訂單重複產生

**症狀**：測試結果顯示訂單重複錯誤 > 5%

**可能原因**：
- 缺乏冪等性機制
- 併發控制不當
- RecordCode 生成邏輯有問題

**解決方案**：
1. 實作訂單唯一性檢查（使用 RecordCode 唯一索引）
2. 使用分散式鎖（Redis）
3. 檢查 RecordCode 生成邏輯（確保唯一性）

### 5. 長時間事務（可能是 row lock）

**症狀**：測試結果顯示大量長時間事務（> 3秒）

**可能原因**：
- 事務隔離級別過高
- 鎖定範圍過大
- 索引使用不當

**解決方案**：
1. 檢查資料庫鎖等待情況（`pg_locks` 視圖）
2. 優化事務範圍（減少事務時間）
3. 考慮降低隔離級別（如果業務允許）
4. 確保索引正確使用

### 6. 訂單提交特別慢

訂單提交涉及：
- 多張表的寫入（Order、OrderProduct、OrderStep、Shipment、Payment）
- 事務處理
- 庫存扣減（Redis Lua script）
- RabbitMQ 訊息發送

如果特別慢，建議檢查：
- 資料庫事務日誌寫入效能
- 是否有鎖競爭
- Redis Lua script 執行時間
- RabbitMQ 訊息發送是否阻塞

## 注意事項

1. **測試資料**
   - 此測試會產生大量測試資料（用戶、購物車、訂單）
   - 建議在測試環境執行
   - 測試後可能需要清理測試資料

2. **資料庫連線**
   - 寫入操作可能比讀取操作消耗更多連線
   - 確保 PostgreSQL `max_connections` 設定足夠

3. **資源使用**
   - 寫入操作會消耗更多 CPU 和 I/O
   - 監控資料庫 CPU、記憶體和磁碟 I/O

4. **測試環境**
   - 建議在測試環境執行，避免影響生產環境
   - 確保資料庫有足夠的空間和效能

## 效能預估說明

### 如何解讀「預估最大可支撐寫入量」

測試結果會提供：
```
當前訂單提交速率: X orders/s
預估最大可支撐寫入量: Y orders/s (基於 P95 響應時間 Zms)
建議最大並發訂單數: W orders/s (保守估計，保留 30% 緩衝)
```

**計算方式**：
- 基於 P95 響應時間和最大 VU 數進行簡化計算
- 公式：`(VU數 * 1000ms) / P95響應時間`
- 這是一個理論值，實際值可能因系統負載、資料庫狀態等因素而異

**使用建議**：
- 將「建議最大並發訂單數」作為生產環境的參考值
- 實際部署時建議保留 50% 以上的緩衝空間
- 定期執行測試以更新預估值

## 下一步

1. **執行第 1 階段測試**，記錄基準結果
2. **如果未發現 Deadlock**，執行第 2 階段測試
3. **如果 transaction latency 可接受**，執行 Spike Test
4. **與讀取測試結果比較**，了解讀寫效能差異
5. **如果寫入效能不理想**，檢查：
   - 資料庫慢查詢日誌（寫入操作）
   - 事務日誌寫入效能
   - 鎖競爭情況（`pg_locks` 視圖）
   - Redis Lua script 執行時間
   - RabbitMQ 訊息堆積情況
   - 資料庫連線使用情況

