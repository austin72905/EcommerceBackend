# RabbitMQ 延遲訊息 - 訂單超時處理

## 功能概述

此功能實現了使用 RabbitMQ 的 **TTL + Dead-Letter Exchange (DLX)** 模式來處理訂單超時的延遲訊息機制。當用戶創建訂單後，系統會自動發送一個延遲 10 分鐘的訊息，如果訂單在此期間未付款，則自動取消訂單並回滾庫存。

> **注意**: 本實現使用 TTL + DLX 模式，適用於 Amazon MQ 等不支援 `x-delayed-message` 插件的環境。

## 架構說明

### TTL + DLX 延遲訊息模式

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                           TTL + DLX 延遲訊息架構                             │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                             │
│   Producer                                                                  │
│      │                                                                      │
│      ▼                                                                      │
│   ┌──────────────────────┐                                                  │
│   │ order.timeout.exchange│  (入口交換器 - Direct)                          │
│   └──────────────────────┘                                                  │
│      │                                                                      │
│      │ routing_key: order.timeout.delay                                     │
│      ▼                                                                      │
│   ┌──────────────────────────────────────────┐                              │
│   │     order_timeout_delay_queue            │  (延遲隊列)                   │
│   │  ┌─────────────────────────────────────┐ │                              │
│   │  │ x-message-ttl: 600000 (10分鐘)       │ │                              │
│   │  │ x-dead-letter-exchange: order.timeout.dlx │                          │
│   │  │ x-dead-letter-routing-key: order.timeout  │                          │
│   │  └─────────────────────────────────────┘ │                              │
│   └──────────────────────────────────────────┘                              │
│      │                                                                      │
│      │ (TTL 到期後自動轉發)                                                  │
│      ▼                                                                      │
│   ┌──────────────────────┐                                                  │
│   │   order.timeout.dlx   │  (死信交換器 DLX - Direct)                       │
│   └──────────────────────┘                                                  │
│      │                                                                      │
│      │ routing_key: order.timeout                                           │
│      ▼                                                                      │
│   ┌──────────────────────┐                                                  │
│   │  order_timeout_queue  │  (處理隊列 - Consumer 監聽此隊列)                │
│   └──────────────────────┘                                                  │
│      │                                                                      │
│      ▼                                                                      │
│   Consumer                                                                  │
│                                                                             │
└─────────────────────────────────────────────────────────────────────────────┘
```

### 訊息流程

1. **Producer** 發送訊息到 `order.timeout.exchange`
2. 訊息被路由到 `order_timeout_delay_queue`（延遲隊列）
3. 訊息在延遲隊列中等待 TTL 到期（預設 10 分鐘）
4. TTL 到期後，訊息被轉發到 `order.timeout.dlx`（死信交換器）
5. 死信交換器將訊息路由到 `order_timeout_queue`（處理隊列）
6. **Consumer** 從處理隊列接收並處理訊息

## 前置需求

### Amazon MQ / 標準 RabbitMQ

此實現**不需要**安裝任何額外插件，使用 RabbitMQ 原生支援的功能：
- Dead-Letter Exchange (DLX)
- Message TTL (Time-To-Live)

## 實作架構

### 1. 介面設計

```csharp
// Producer 介面
public interface IOrderTimeoutProducer
{
    Task SendOrderTimeoutMessageAsync(int userId, string recordCode, int delayMinutes = 10);
    Task SendOrderTimeoutMessageWithSecondsAsync(int userId, string recordCode, int delaySeconds);
}

// Consumer 介面
public interface IOrderTimeoutConsumer
{
    Task StartListening();
    Task StartListening(CancellationToken cancellationToken);
}
```

### 2. 訊息結構

```csharp
public class OrderTimeoutMessage
{
    public int UserId { get; set; }
    public string RecordCode { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### 3. Exchange 和 Queue 設定

| 名稱 | 類型 | 用途 |
|------|------|------|
| `order.timeout.exchange` | Direct Exchange | 入口交換器，接收生產者訊息 |
| `order.timeout.dlx` | Direct Exchange | 死信交換器，接收過期訊息 |
| `order_timeout_delay_queue` | Queue | 延遲隊列，設定 TTL 和 DLX |
| `order_timeout_queue` | Queue | 處理隊列，消費者監聽 |

### 4. 延遲隊列參數

```csharp
var delayQueueArgs = new Dictionary<string, object>
{
    // 訊息過期後轉發到死信交換器
    { "x-dead-letter-exchange", "order.timeout.dlx" },
    // 訊息過期後使用的路由鍵
    { "x-dead-letter-routing-key", "order.timeout" },
    // 隊列級別的 TTL（毫秒）
    { "x-message-ttl", 600000 }  // 10 分鐘
};
```

## 使用流程

### 1. 訂單創建時發送延遲訊息

```csharp
// 在 OrderService.CreateOrderAsync 中
await _orderTimeoutProducer.SendOrderTimeoutMessageAsync(userid, order.RecordCode, 10);
```

### 2. 10分鐘後自動處理超時

Consumer 會自動接收延遲訊息並呼叫：

```csharp
await orderTimeoutHandler.HandleOrderTimeoutAsync(timeoutMessage.UserId, timeoutMessage.RecordCode);
```

### 3. 超時處理邏輯

```csharp
public async Task HandleOrderTimeoutAsync(int userId, string recordcode)
{
    var order = await _orderRepostory.GetOrderInfoByUserId(userId, recordcode);

    // 只有未付款的訂單才需要處理
    if (order == null || order.Status != (int)OrderStatus.Created)
    {
        return;
    }

    // 回滾庫存
    await _redisService.RollbackStockAsync(recordcode);

    // 更新訂單狀態為取消
    await _orderRepostory.UpdateOrderStatusAsync(recordcode, (int)OrderStatus.Canceled);
}
```

## 配置設定

### appsettings.json

```json
{
  "AppSettings": {
    "RabbitMqUri": "amqp://guest:guest@localhost:5672/",
    "OrderTimeoutMinutes": 10
  }
}
```

### 環境變數 (Production)

```bash
export AppSettings__RabbitMqUri=amqp://user:password@your-rabbitmq-server:5672/
export AppSettings__OrderTimeoutMinutes=10
```

## 服務註冊

在 `Program.cs` 中已自動註冊相關服務：

```csharp
// MQ 服務
builder.Services.AddSingleton<IOrderTimeoutProducer, OrderTimeoutProducer>();
builder.Services.AddSingleton<IOrderTimeoutConsumer, OrderTimeoutConsumer>();

// 背景服務
builder.Services.AddHostedService<OrderTimeoutConsumerService>();
```

## 監控和日誌

### 1. 發送訊息日誌

```
[x] Sent delayed order timeout message for order ORD20241124001, delay: 10 minutes
[x] Message sent at: 10:00:00.000, expected processing at: 10:10:00.000
[x] Using TTL + DLX mode (Amazon MQ compatible)
```

### 2. 接收處理日誌

```
[2024-11-24 10:10:00.123] 🔥 收到延遲訊息: {"UserId":1,"RecordCode":"ORD20241124001","CreatedAt":"2024-11-24T10:00:00Z"}
[2024-11-24 10:10:00.125] 開始處理訂單超時: ORD20241124001
[2024-11-24 10:10:00.200] 訂單超時處理完成: ORD20241124001
```

### 3. 啟動日誌

```
[a1b2c3d4] RabbitMQ 連線建立成功，設定 Exchange 和 Queue (TTL + DLX 模式)...
[a1b2c3d4] TTL + DLX 延遲訊息架構設定完成，延遲時間: 10 分鐘
[a1b2c3d4] OrderTimeoutConsumer 啟動成功，開始監聯延遲訊息 (TTL + DLX 模式)...
```

## 測試方式

### 1. 功能測試

1. 創建一個訂單
2. 觀察延遲訊息是否正確發送
3. 等待 10 分鐘（或使用測試方法調整時間）
4. 確認訂單狀態是否變為取消
5. 確認庫存是否正確回滾

### 2. 使用秒級延遲進行快速測試

```csharp
// 使用秒級延遲進行測試（例如 30 秒）
await _orderTimeoutProducer.SendOrderTimeoutMessageWithSecondsAsync(userid, order.RecordCode, 30);
```

## 注意事項

1. **FIFO 限制**: 使用 per-message TTL 時，如果先發送長延遲訊息再發送短延遲訊息，短延遲訊息會被阻塞。建議使用隊列級別的 TTL（統一延遲時間）。

2. **性能考量**: 延遲訊息會佔用 RabbitMQ 記憶體，大量延遲訊息可能影響性能

3. **故障恢復**: RabbitMQ 重啟後，延遲訊息仍會正確執行（因為使用了持久化）

4. **冪等性**: `HandleOrderTimeoutAsync` 方法具有冪等性，重複執行不會造成問題

5. **Amazon MQ 相容性**: 此實現完全相容 Amazon MQ，不需要任何額外插件

## 與 x-delayed-message 插件的比較

| 特性 | TTL + DLX 模式 | x-delayed-message 插件 |
|------|----------------|----------------------|
| Amazon MQ 支援 | ✅ 支援 | ❌ 不支援 |
| 動態延遲時間 | ⚠️ 需要額外處理 FIFO 問題 | ✅ 完全支援 |
| 安裝需求 | 無需安裝 | 需要安裝插件 |
| 性能 | 良好 | 良好 |
| 持久化 | ✅ 支援 | ✅ 支援 |

## 故障排除

### 1. 訊息未被延遲

檢查延遲隊列的 `x-message-ttl` 參數是否正確設定：

```bash
# 使用 RabbitMQ Management API 檢查
curl -u guest:guest http://localhost:15672/api/queues/%2F/order_timeout_delay_queue
```

### 2. 訊息未到達處理隊列

確認 DLX 設定正確：
- `x-dead-letter-exchange` 指向正確的死信交換器
- `x-dead-letter-routing-key` 設定正確的路由鍵
- 處理隊列已正確綁定到死信交換器

### 3. Consumer 未接收到訊息

確認：
- Consumer 監聽的是 `order_timeout_queue`（處理隊列），而不是延遲隊列
- RabbitMQ 連線正常
- Queue 綁定正確

## 擴展建議

1. **多級延遲隊列**: 為不同延遲時間創建多個延遲隊列（如 1分鐘、5分鐘、10分鐘）
2. **取消訂單時清理**: 當訂單已付款時，可考慮從延遲隊列清理對應訊息
3. **重試機制**: 對於處理失敗的超時訊息，實作重試邏輯
4. **監控告警**: 建立監控系統追蹤延遲訊息的處理狀況
