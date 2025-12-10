using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Common.Interfaces.Infrastructure;
using Common.Interfaces.Application.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Infrastructure.MQ
{
    public class OrderStatusChangedConsumer : IOrderStatusChangedConsumer
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrderStatusChangedConsumer> _logger;

        public OrderStatusChangedConsumer(
            IServiceScopeFactory serviceScopeFactory,
            IConfiguration configuration,
            ILogger<OrderStatusChangedConsumer> logger)
        {
            _configuration = configuration;
            _connectionFactory = new ConnectionFactory
            {
                HostName = _configuration["AppSettings:RabbitMqHostName"],
            };
            _scopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task StartListening()
        {
            await StartListening(CancellationToken.None);
        }

        public async Task StartListening(CancellationToken cancellationToken)
        {
            IConnection? connection = null;
            IChannel? channel = null;

            try
            {
                var instanceId = Guid.NewGuid().ToString("N")[..8];
                _logger.LogInformation("[{InstanceId}] [訂單狀態同步] 正在建立 RabbitMQ 連線...", instanceId);

                connection = await _connectionFactory.CreateConnectionAsync();
                channel = await connection.CreateChannelAsync();

                _logger.LogInformation("[{InstanceId}] [訂單狀態同步] RabbitMQ 連線建立成功，設定 Exchange 和 Queue...", instanceId);

                // 確保交換器存在
                await channel.ExchangeDeclareAsync(
                    exchange: "order.state.update",
                    type: "direct",
                    durable: true,
                    autoDelete: false
                );

                // 確保隊列存在
                const string dlx = "dead.letter.exchange";
                const string dlq = "order_state_changed_queue.dlq";
                const string dlqRoutingKey = "order.state.changed.dlq";

                await channel.ExchangeDeclareAsync(
                    exchange: dlx,
                    type: "direct",
                    durable: true,
                    autoDelete: false
                );

                await channel.QueueDeclareAsync(
                    queue: dlq,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                await channel.QueueBindAsync(
                    queue: dlq,
                    exchange: dlx,
                    routingKey: dlqRoutingKey
                );

                await channel.QueueDeclareAsync(
                    queue: "order_state_changed_queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: new Dictionary<string, object>
                    {
                        { "x-dead-letter-exchange", dlx },
                        { "x-dead-letter-routing-key", dlqRoutingKey }
                    }
                );

                // 綁定交換器與隊列
                await channel.QueueBindAsync(
                    queue: "order_state_changed_queue",
                    exchange: "order.state.update",
                    routingKey: "order.state.status.changed"
                );

                // 設置公平分發
                await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var messageJson = Encoding.UTF8.GetString(body);

                    _logger.LogInformation("[訂單狀態同步] 收到訊息: {Message}", messageJson);

                    try
                    {
                        // 解析消息並處理
                        var statusChangedMessage = JsonSerializer.Deserialize<OrderStatusChangedMessage>(messageJson);

                        if (statusChangedMessage == null)
                        {
                            _logger.LogWarning("[訂單狀態同步] 訊息解析失敗，訊息為 null");
                            await channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                            return;
                        }

                        // 處理消息
                        await ProcessOrderStatusChangedAsync(statusChangedMessage);

                        // 消息處理成功，發送 ACK
                        await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                        _logger.LogInformation("[訂單狀態同步] 訂單 {RecordCode} 狀態已同步: {FromStatus} -> {ToStatus}",
                            statusChangedMessage.OrderID, statusChangedMessage.FromStatus, statusChangedMessage.ToStatus);
                    }
                    catch (Exception ex)
                    {
                        // 消息處理失敗，發送 NACK
                        _logger.LogError(ex, "[訂單狀態同步] 處理訊息失敗: {Message}", messageJson);
                        await channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                    }
                };

                // 開始消費
                await channel.BasicConsumeAsync(
                    queue: "order_state_changed_queue",
                    autoAck: false,
                    consumer: consumer
                );

                _logger.LogInformation("[{InstanceId}] [訂單狀態同步] OrderStatusChangedConsumer 已啟動，等待訊息...", instanceId);

                // 保持消費者運行，直到取消
                try
                {
                    await Task.Delay(Timeout.Infinite, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("[{InstanceId}] [訂單狀態同步] OrderStatusChangedConsumer 收到取消信號，正在關閉...", instanceId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[訂單狀態同步] OrderStatusChangedConsumer 發生錯誤");
                throw;
            }
            finally
            {
                if (channel != null)
                {
                    try
                    {
                        await channel.CloseAsync();
                        _logger.LogDebug("[訂單狀態同步] RabbitMQ Channel 已關閉");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "[訂單狀態同步] 關閉 RabbitMQ Channel 時發生錯誤");
                    }
                }

                if (connection != null)
                {
                    try
                    {
                        await connection.CloseAsync();
                        _logger.LogDebug("[訂單狀態同步] RabbitMQ Connection 已關閉");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "[訂單狀態同步] 關閉 RabbitMQ Connection 時發生錯誤");
                    }
                }
            }
        }

        private async Task ProcessOrderStatusChangedAsync(OrderStatusChangedMessage message)
        {
            // 冪等性檢查：避免重複處理相同訊息
            var messageKey = $"{message.OrderID}:{message.FromStatus}:{message.ToStatus}:{message.Timestamp}";
            
            // 分散式鎖的 key 和 value
            var lockKey = $"order_status_sync:{message.OrderID}";
            var lockValue = Guid.NewGuid().ToString();
            bool lockAcquired = false;
            
            // 動態建立 Scope
            using (var scope = _scopeFactory.CreateScope())
            {
                var redisService = scope.ServiceProvider.GetRequiredService<IRedisService>();
                
                // 檢查訊息是否已處理（第一次檢查，快速失敗）
                var isProcessed = await redisService.IsMessageProcessedAsync(messageKey);
                if (isProcessed)
                {
                    _logger.LogInformation("[訂單狀態同步] 訊息已處理，跳過: MessageKey={MessageKey}, OrderID={OrderID}", 
                        messageKey, message.OrderID);
                    return;
                }
                
                // 嘗試獲取分散式鎖（防止並發處理同一訂單）
                lockAcquired = await redisService.TryAcquireLockAsync(lockKey, lockValue, TimeSpan.FromSeconds(10));
                if (!lockAcquired)
                {
                    _logger.LogWarning("[訂單狀態同步] 無法獲取分散式鎖，可能正在被其他消費者處理: OrderID={OrderID}, MessageKey={MessageKey}", 
                        message.OrderID, messageKey);
                    return; // 其他消費者正在處理，直接返回（避免重複處理）
                }
                
                try
                {
                    // 雙重檢查：在獲取鎖的過程中，訊息可能已被其他消費者處理
                    isProcessed = await redisService.IsMessageProcessedAsync(messageKey);
                    if (isProcessed)
                    {
                        _logger.LogInformation("[訂單狀態同步] 訊息已在獲取鎖的過程中處理，跳過: MessageKey={MessageKey}, OrderID={OrderID}", 
                            messageKey, message.OrderID);
                        return;
                    }
                    
                    var orderStatusSyncService = scope.ServiceProvider.GetRequiredService<IOrderStatusSyncService>();
                    await orderStatusSyncService.SyncOrderStatusFromStateServiceAsync(message.OrderID, message.FromStatus, message.ToStatus);
                    
                    // 處理成功後標記為已處理
                    // 冪等性檢查，避免重複處理相同訊息
                    await redisService.MarkMessageAsProcessedAsync(messageKey);
                    _logger.LogInformation("[訂單狀態同步] 訊息處理成功並標記: MessageKey={MessageKey}, OrderID={OrderID}", 
                        messageKey, message.OrderID);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "[訂單狀態同步] 訊息處理失敗: MessageKey={MessageKey}, OrderID={OrderID}", 
                        messageKey, message.OrderID);
                    throw; // 重新拋出異常，讓 RabbitMQ 重試
                }
                finally
                {
                    // 釋放分散式鎖
                    if (lockAcquired)
                    {
                        try
                        {
                            await redisService.ReleaseLockAsync(lockKey, lockValue);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "[訂單狀態同步] 釋放分散式鎖失敗: OrderID={OrderID}，鎖會自動過期", 
                                message.OrderID);
                        }
                    }
                }
            }
        }

        public class OrderStatusChangedMessage
        {
            [JsonPropertyName("eventType")]
            public string EventType { get; set; }
            
            [JsonPropertyName("orderId")]
            public string OrderID { get; set; } // 使用 RecordCode 作為 orderId
            
            [JsonPropertyName("fromStatus")]
            public int FromStatus { get; set; } // 統一使用數字 enum
            
            [JsonPropertyName("toStatus")]
            public int ToStatus { get; set; } // 統一使用數字 enum
            
            [JsonPropertyName("timestamp")]
            public string Timestamp { get; set; }
        }
    }
}

