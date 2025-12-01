# k6 壓力測試

本目錄包含使用 k6 進行壓力測試的腳本和配置。

## 📁 目錄結構

```
k6/
├── scripts/          # 測試腳本
│   ├── smoke-test.js    # 冒煙測試（輕量驗證）
│   ├── load-test.js     # 正常負載測試
│   ├── stress-test.js   # 壓力測試（超過正常負載）
│   └── spike-test.js    # 峰值測試（突然增加負載）
├── utils/            # 共用工具函數
│   └── helpers.js
├── data/             # 測試數據
│   └── test-data.json
└── README.md         # 本文件
```

## 🚀 快速開始

### 1. 安裝 k6

**Windows:**
```powershell
# 使用 Chocolatey
choco install k6

# 或使用 Scoop
scoop install k6
```

**macOS:**
```bash
brew install k6
```

**Linux:**
```bash
# Ubuntu/Debian
sudo gpg -k
sudo gpg --no-default-keyring --keyring /usr/share/keyrings/k6-archive-keyring.gpg --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys C5AD17C747E3415A3642D57D77C6C491D6AC1D6F
echo "deb [signed-by=/usr/share/keyrings/k6-archive-keyring.gpg] https://dl.k6.io/deb stable main" | sudo tee /etc/apt/sources.list.d/k6.list
sudo apt-get update
sudo apt-get install k6
```

### 2. 配置測試環境

編輯測試腳本中的 `BASE_URL` 變數，設定為您的 API 基礎 URL：

```javascript
const BASE_URL = __ENV.BASE_URL || 'http://localhost:5025';
```

### 3. 執行測試

```bash
# 從專案根目錄執行
k6 run k6/scripts/smoke-test.js

# 或指定環境變數
BASE_URL=http://localhost:5025 k6 run k6/scripts/load-test.js

# 執行壓力測試
k6 run k6/scripts/stress-test.js

# 執行峰值測試
k6 run k6/scripts/spike-test.js
```

## 📊 測試類型說明

### 1. Smoke Test (冒煙測試)
- **目的**: 驗證系統基本功能是否正常
- **負載**: 1 個虛擬用戶，持續 1 分鐘
- **用途**: 快速驗證 API 是否可訪問

### 2. Load Test (負載測試)
- **目的**: 測試系統在正常預期負載下的表現
- **負載**: 10 個虛擬用戶，持續 5 分鐘
- **用途**: 評估系統在正常使用情況下的性能

### 3. Stress Test (壓力測試)
- **目的**: 測試系統在超過正常負載下的表現
- **負載**: 逐步增加到 50 個虛擬用戶
- **用途**: 找出系統的極限和瓶頸

### 4. Spike Test (峰值測試)
- **目的**: 測試系統處理突然增加的負載
- **負載**: 從 10 個用戶突然增加到 100 個用戶
- **用途**: 模擬促銷活動或突發流量

## 🔧 環境變數

可以通過環境變數自定義測試參數：

```bash
# 設定 API 基礎 URL
export BASE_URL=http://localhost:5025

# 設定測試持續時間（秒）
export DURATION=300

# 設定虛擬用戶數
export VUS=50

# 執行測試
k6 run k6/scripts/load-test.js
```

## 📈 查看測試結果

k6 會在終端顯示實時統計信息，包括：
- 請求速率 (req/s)
- 響應時間 (平均、最小、最大、p95、p99)
- 錯誤率
- 數據傳輸量

### 輸出到文件

```bash
# 輸出 JSON 格式結果
k6 run --out json=results.json k6/scripts/load-test.js

# 輸出 InfluxDB（需要先啟動 InfluxDB）
k6 run --out influxdb=http://localhost:8086/k6 k6/scripts/load-test.js

# 輸出到 Grafana Cloud
k6 run --out cloud k6/scripts/load-test.js
```

## 🔐 認證

如果 API 需要認證，請在 `utils/helpers.js` 中設定：

```javascript
export function getAuthHeaders() {
    return {
        'Authorization': 'Bearer YOUR_TOKEN_HERE',
        'Content-Type': 'application/json',
    };
}
```

## 📝 注意事項

1. **測試環境**: 建議在測試環境或開發環境進行壓力測試，避免影響生產環境
2. **數據庫**: 確保測試數據庫有足夠的測試數據
3. **依賴服務**: 確保 RabbitMQ、Redis 等依賴服務正常運行
4. **監控**: 測試時監控服務器資源使用情況（CPU、記憶體、網路）

## 🐛 故障排除

### 連接被拒絕
- 檢查 API 服務是否正在運行
- 確認 BASE_URL 設定正確
- 檢查防火牆設定

### 認證失敗
- 檢查 JWT Token 是否有效
- 確認 Token 未過期
- 檢查認證中間件配置

### 高錯誤率
- 檢查服務器日誌
- 確認數據庫連接正常
- 檢查依賴服務（RabbitMQ、Redis）狀態

## 📚 更多資源

- [k6 官方文檔](https://k6.io/docs/)
- [k6 最佳實踐](https://k6.io/docs/using-k6/best-practices/)
- [k6 測試腳本示例](https://k6.io/docs/examples/)

