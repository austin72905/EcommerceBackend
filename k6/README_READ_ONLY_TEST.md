# 純讀取效能測試說明

## 概述

`read-only-test.js` 是一個專門測試系統讀取效能的 k6 測試腳本，用於評估資料庫索引優化效果。

## 測試場景

此腳本只包含讀取操作，不包含任何寫入操作：

1. **商品列表查詢** (`GetProductList`)
   - 複雜查詢，包含多層 Include
   - 測試資料庫 JOIN 效能

2. **商品基本信息列表** (`GetProductBasicInfoList`)
   - 輕量查詢，只返回基本信息
   - 測試簡單查詢效能

3. **單個商品詳情** (`GetProductById`)
   - 單筆查詢，測試主鍵索引效能

4. **訂單列表查詢** (`GetOrders`)
   - 需要認證
   - 測試 `UserId` 索引效能

5. **訂單詳情查詢** (`GetOrderInfo`)
   - 需要認證
   - 測試 `RecordCode` 索引效能

6. **商品基本信息詳情** (`GetProductBasicInfoById`)
   - 輕量單筆查詢

## 執行方式

### 基本執行

```bash
k6 run k6/scripts/read-only-test.js
```

### 自定義參數

```bash
# 設定最大並發用戶數和測試持續時間
MAX_VUS=500 DURATION=3m k6 run k6/scripts/read-only-test.js

# 指定 API 基礎 URL
BASE_URL=http://localhost:5025 MAX_VUS=300 DURATION=2m k6 run k6/scripts/read-only-test.js

# 使用測試帳號（用於訂單相關 API）
TEST_USERNAME=testuser TEST_PASSWORD=Test123456! MAX_VUS=300 DURATION=2m k6 run k6/scripts/read-only-test.js
```

### 完整範例

```bash
# Windows PowerShell
$env:MAX_VUS=300; $env:DURATION="2m"; $env:BASE_URL="http://localhost:5025"; k6 run k6/scripts/read-only-test.js

# Windows CMD
set MAX_VUS=300 && set DURATION=2m && set BASE_URL=http://localhost:5025 && k6 run k6/scripts/read-only-test.js

# Linux/Mac
MAX_VUS=300 DURATION=2m BASE_URL=http://localhost:5025 k6 run k6/scripts/read-only-test.js
```

## 環境變數

| 變數名稱 | 預設值 | 說明 |
|---------|--------|------|
| `MAX_VUS` | 300 | 最大並發虛擬用戶數 |
| `DURATION` | 2m | 測試持續時間（格式：30s, 1m, 5m30s） |
| `BASE_URL` | http://localhost:5025 | API 基礎 URL |
| `TEST_USERNAME` | testuser | 測試帳號用戶名（用於訂單 API） |
| `TEST_PASSWORD` | Test123456! | 測試帳號密碼 |

## 測試結果解讀

### 關鍵指標

1. **請求速率 (RPS)**
   - 讀取操作應該比混合讀寫測試有更高的 RPS
   - 如果 RPS 仍然很低，可能是資料庫查詢瓶頸

2. **響應時間百分位數**
   - P50（中位數）：應該 < 1000ms
   - P95：應該 < 3000ms
   - P99：應該 < 5000ms

3. **各 API 響應時間**
   - 商品列表：P95 < 2000ms
   - 商品基本信息列表：P95 < 1000ms
   - 商品詳情：P95 < 500ms
   - 訂單列表：P95 < 2000ms（測試 UserId 索引）
   - 訂單詳情：P95 < 1000ms（測試 RecordCode 索引）

### 索引優化效果評估

測試結果會顯示：
- ✓ 訂單列表查詢（UserId 索引）效能良好
- ✓ 訂單詳情查詢（RecordCode 索引）效能良好

如果顯示 ⚠，表示可能需要：
1. 確認索引是否已建立（執行 `dotnet ef database update`）
2. 檢查資料庫查詢計劃（使用 `EXPLAIN ANALYZE`）
3. 檢查是否有 N+1 查詢問題

## 與混合測試的比較

執行此純讀取測試後，可以與 `stress-test.js` 的結果比較：

| 指標 | 純讀取測試 | 混合測試 | 說明 |
|------|-----------|---------|------|
| RPS | 應該更高 | 較低 | 讀取操作比寫入快 |
| P50 | 應該更低 | 較高 | 沒有寫入操作延遲 |
| P95 | 應該更低 | 較高 | 沒有資料庫鎖競爭 |

如果純讀取測試的 RPS 仍然很低，問題可能在：
- 資料庫查詢效能（索引未生效、N+1 問題）
- 序列化效能（JSON 序列化）
- 網路延遲
- 應用層邏輯

## 注意事項

1. **認證問題**
   - 訂單相關 API 需要認證
   - 如果未提供有效的測試帳號，這些 API 會返回 401，這是正常的
   - 401 不會被計入錯誤率

2. **資料準備**
   - 確保資料庫中有足夠的測試資料
   - 商品 ID 範圍假設在 1-100，可根據實際情況調整

3. **測試環境**
   - 建議在測試環境執行，避免影響生產環境
   - 確保資料庫連線池設定足夠（已調整為 MaxPoolSize=200）

## 下一步

1. 執行純讀取測試，記錄基準結果
2. 確認資料庫索引已建立
3. 再次執行測試，比較優化前後的差異
4. 如果效能仍不理想，檢查：
   - EF Core 查詢日誌（啟用 `LogTo`）
   - 資料庫慢查詢日誌
   - 應用層序列化效能

