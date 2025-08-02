# RabbitMQ 延遲訊息 - 訂單超時處理

## 功能概述

此功能實現了使用 RabbitMQ 的 `x-delayed-message` 插件來處理訂單超時的延遲訊息機制。當用戶創建訂單後，系統會自動發送一個延遲 10 分鐘的訊息，如果訂單在此期間未付款，則自動取消訂單並回滾庫存。

## 前置需求

### 1. 安裝 x-delayed-message 插件

```bash
# 下載插件 (根據你的 RabbitMQ 版本選擇合適的版本)
wget https://github.com/rabbitmq/rabbitmq-delayed-message-exchange/releases/download/3.12.0/rabbitmq_delayed_message_exchange-3.12.0.ez

# 複製到 RabbitMQ 插件目錄
cp rabbitmq_delayed_message_exchange-3.12.0.ez $RABBITMQ_HOME/plugins/

# 啟用插件
rabbitmq-plugins enable rabbitmq_delayed_message_exchange

# 重啟 RabbitMQ
sudo systemctl restart rabbitmq-server
```

### 2. 驗證插件安裝

```bash
# 檢查插件是否已啟用
rabbitmq-plugins list | grep delayed
```

應該會看到類似這樣的輸出：
```
[E*] rabbitmq_delayed_message_exchange   3.12.0
```

## 實作架構

### 1. 介面設計

```csharp
// Producer 介面
public interface IOrderTimeoutProducer
{
    Task SendOrderTimeoutMessageAsync(int userId, string recordCode, int delayMinutes = 10);
}

// Consumer 介面
public interface IOrderTimeoutConsumer
{
    Task StartListening();
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

- **Exchange**: `order.timeout.delayed`
  - Type: `x-delayed-message`
  - x-delayed-type: `direct`
  - Durable: `true`

- **Queue**: `order_timeout_queue`
  - Durable: `true`
  - Routing Key: `order.timeout`

## 使用流程

### 1. 訂單創建時發送延遲訊息

```csharp
// 在 OrderService.CreateOrderAsync 中
await _orderTimeoutProducer.SendOrderTimeoutMessageAsync(userid, order.RecordCode, 10);
```

### 2. 10分鐘後自動處理超時

Consumer 會自動接收延遲訊息並呼叫：

```csharp
await orderService.HandleOrderTimeoutAsync(timeoutMessage.UserId, timeoutMessage.RecordCode);
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
    "RabbitMqHostName": "localhost"
  }
}
```

### 環境變數 (Production)

```bash
export RABBITMQ_HOSTNAME=your-rabbitmq-server
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
```

### 2. 接收處理日誌

```
[x] Received delayed order timeout message: {"UserId":1,"RecordCode":"ORD20241124001","CreatedAt":"2024-11-24T10:00:00Z"}
[x] Order timeout ORD20241124001 processed successfully.
```

### 3. 錯誤處理日誌

```
[!] Error processing order timeout message: Order not found
```

## 測試方式

### 1. 功能測試

1. 創建一個訂單
2. 觀察延遲訊息是否正確發送
3. 等待 10 分鐘（或調整測試時間）
4. 確認訂單狀態是否變為取消
5. 確認庫存是否正確回滾

### 2. 手動測試延遲時間

可以在測試環境中將延遲時間調整為較短的時間（如 30 秒）：

```csharp
await _orderTimeoutProducer.SendOrderTimeoutMessageAsync(userid, order.RecordCode, 0.5); // 30秒
```

## 注意事項

1. **插件版本兼容性**: 確保 x-delayed-message 插件版本與 RabbitMQ 版本兼容
2. **性能考量**: 延遲訊息會佔用 RabbitMQ 記憶體，大量延遲訊息可能影響性能
3. **故障恢復**: RabbitMQ 重啟後，延遲訊息仍會正確執行（因為使用了持久化）
4. **冪等性**: `HandleOrderTimeoutAsync` 方法具有冪等性，重複執行不會造成問題

## 故障排除

### 1. 插件未安裝

```
Error: exchange type 'x-delayed-message' not found
```

**解決方案**: 按照前置需求重新安裝插件

### 2. 延遲訊息未執行

檢查：
- RabbitMQ 服務是否正常運行
- Consumer 是否正常啟動
- 插件是否正確啟用

### 3. 記憶體使用過高

考慮：
- 調整延遲時間
- 實作訂單狀態檢查機制
- 使用外部排程系統替代超長延遲訊息

## 擴展建議

1. **動態延遲時間**: 根據訂單金額或用戶等級調整延遲時間
2. **取消延遲訊息**: 當訂單已付款時，取消對應的延遲訊息
3. **重試機制**: 對於處理失敗的超時訊息，實作重試邏輯
4. **監控告警**: 建立監控系統追蹤延遲訊息的處理狀況