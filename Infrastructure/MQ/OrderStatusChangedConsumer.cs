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
                await channel.QueueDeclareAsync(
                    queue: "order_state_changed_queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
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
                        await channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
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
            // 動態建立 Scope
            using (var scope = _scopeFactory.CreateScope())
            {
                var orderStatusSyncService = scope.ServiceProvider.GetRequiredService<IOrderStatusSyncService>();
                await orderStatusSyncService.SyncOrderStatusFromStateServiceAsync(message.OrderID, message.FromStatus, message.ToStatus);
            }
        }

        public class OrderStatusChangedMessage
        {
            [JsonPropertyName("eventType")]
            public string EventType { get; set; }
            
            [JsonPropertyName("orderId")]
            public string OrderID { get; set; } // 使用 RecordCode 作為 orderId
            
            [JsonPropertyName("fromStatus")]
            public string FromStatus { get; set; }
            
            [JsonPropertyName("toStatus")]
            public string ToStatus { get; set; }
            
            [JsonPropertyName("timestamp")]
            public string Timestamp { get; set; }
        }
    }
}

