using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Common.Interfaces.Application.Services;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Common.Interfaces.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Infrastructure.MQ
{
    /// <summary>
    /// 訂單超時消費者 - 重新設計，支援 CancellationToken 和正確的生命週期管理
    /// </summary>
    public class OrderTimeoutConsumer : IOrderTimeoutConsumer
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OrderTimeoutConsumer> _logger;

        public OrderTimeoutConsumer(
            IServiceScopeFactory serviceScopeFactory, 
            IConfiguration configuration,
            ILogger<OrderTimeoutConsumer> logger)
        {
            var rabbitMqUri = configuration["AppSettings:RabbitMqUri"] ?? "amqp://guest:guest@localhost:5672/";
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(rabbitMqUri),
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                RequestedHeartbeat = TimeSpan.FromSeconds(30)
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
                _logger.LogInformation("[{InstanceId}] 正在建立 RabbitMQ 連線...", instanceId);
                
                connection = await _connectionFactory.CreateConnectionAsync();
                channel = await connection.CreateChannelAsync();

                _logger.LogInformation("[{InstanceId}] RabbitMQ 連線建立成功，設定 Exchange 和 Queue...", instanceId);

                await SetupExchangeAndQueue(channel);
                
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    //Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 消費者事件被觸發！");
                    //Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 訊息大小: {ea.Body.Length} bytes");
                    //Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] DeliveryTag: {ea.DeliveryTag}");
                    await ProcessMessage(channel, ea);
                };

                var consumerTag = await channel.BasicConsumeAsync(
                    queue: "order_timeout_queue",
                    autoAck: false,
                    consumer: consumer
                );
                
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  消費者標籤: {consumerTag}");
                
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  消費者已註冊到 queue: order_timeout_queue");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  等待延遲訊息中...");
                
                _logger.LogInformation("[{InstanceId}] OrderTimeoutConsumer 啟動成功，開始監聽延遲訊息...", instanceId);
                
                // 保持消費者運行，直到取消
                try
                {
                    await Task.Delay(Timeout.Infinite, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("[{InstanceId}] OrderTimeoutConsumer 收到取消信號，正在關閉...", instanceId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OrderTimeoutConsumer 發生錯誤");
                throw;
            }
            finally
            {
                if (channel != null)
                {
                    try
                    {
                        await channel.CloseAsync();
                        _logger.LogDebug("RabbitMQ Channel 已關閉");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "關閉 RabbitMQ Channel 時發生錯誤");
                    }
                }

                if (connection != null)
                {
                    try
                    {
                        await connection.CloseAsync();
                        _logger.LogDebug("RabbitMQ Connection 已關閉");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "關閉 RabbitMQ Connection 時發生錯誤");
                    }
                }
            }
        }

        private async Task SetupExchangeAndQueue(IChannel channel)
        {
            // 宣告延遲交換器
            var exchangeArgs = new Dictionary<string, object>
            {
                { "x-delayed-type", "direct" }
            };

            await channel.ExchangeDeclareAsync(
                exchange: "order.timeout.delayed",
                type: "x-delayed-message",
                durable: true,
                autoDelete: false,
                arguments: exchangeArgs
            );

            // 宣告隊列
            await channel.QueueDeclareAsync(
                queue: "order_timeout_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            // 綁定交換器與隊列
            await channel.QueueBindAsync(
                queue: "order_timeout_queue",
                exchange: "order.timeout.delayed",
                routingKey: "order.timeout"
            );

            // 設置公平分發
            await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);
        }



        private async Task ProcessMessage(IChannel channel, BasicDeliverEventArgs ea)
        {
            try
            {
                var body = ea.Body.ToArray();
                var messageJson = Encoding.UTF8.GetString(body);

                // 強制輸出到 Console，確保能看到
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 🔥 收到延遲訊息: {messageJson}");
                _logger.LogInformation("收到延遲訊息: {Message}", messageJson);

                // 使用動態反序列化，避免類型不匹配問題
                var timeoutMessage = JsonSerializer.Deserialize<dynamic>(messageJson);
                
                var userId = timeoutMessage.GetProperty("UserId").GetInt32();
                var recordCode = timeoutMessage.GetProperty("RecordCode").GetString();
                
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  開始處理訂單超時: {recordCode}");
                
                // 使用 scoped 服務處理訊息
                using var scope = _scopeFactory.CreateScope();
                var orderTimeoutHandler = scope.ServiceProvider.GetRequiredService<IOrderTimeoutHandler>();
                
                await orderTimeoutHandler.HandleOrderTimeoutAsync(userId, recordCode);

                // 處理成功，發送 ACK
                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 訂單超時處理完成: {recordCode}");

                //_logger.LogInformation("✅訂單超時 {RecordCode} 處理完成", recordCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  處理延遲訊息失敗: {ex.Message}");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  錯誤詳情: {ex}");
                _logger.LogError(ex, " 處理延遲訊息失敗");
                
                // 處理失敗，發送 NACK (不重新排隊，避免無限循環)
                await channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
            }
        }


    }
}