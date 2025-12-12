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
    /// 訂單超時消費者
    /// 使用 TTL + Dead-Letter Exchange (DLX) 模式實現延遲訊息
    /// 適用於 Amazon MQ 等不支援 x-delayed-message 插件的環境
    /// 
    /// 架構說明：
    /// 1. order.timeout.exchange (direct) - 接收訊息的入口交換器
    /// 2. order_timeout_delay_queue - 延遲隊列，設定 TTL 和 DLX
    /// 3. order.timeout.dlx (direct) - 死信交換器，接收過期訊息
    /// 4. order_timeout_queue - 真正的處理隊列，消費者監聽此隊列
    /// 
    /// 訊息流程：
    /// Producer → order.timeout.exchange → order_timeout_delay_queue 
    ///          → (TTL 到期) → order.timeout.dlx → order_timeout_queue → Consumer
    /// </summary>
    public class OrderTimeoutConsumer : IOrderTimeoutConsumer
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OrderTimeoutConsumer> _logger;
        private readonly IConfiguration _configuration;

        // Exchange 名稱常數
        private const string DelayExchange = "order.timeout.exchange";      // 入口交換器
        private const string DeadLetterExchange = "order.timeout.dlx";      // 死信交換器 (DLX)
        
        // Queue 名稱常數
        private const string DelayQueue = "order_timeout_delay_queue";      // 延遲隊列
        private const string ProcessingQueue = "order_timeout_queue";       // 處理隊列
        
        // Routing Key 常數
        private const string DelayRoutingKey = "order.timeout.delay";       // 延遲隊列路由鍵
        private const string ProcessingRoutingKey = "order.timeout";        // 處理隊列路由鍵

        // 預設延遲時間（毫秒）- 10 分鐘
        private const int DefaultDelayMilliseconds = 10 * 60 * 1000;

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
            _configuration = configuration;
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

                _logger.LogInformation("[{InstanceId}] RabbitMQ 連線建立成功，設定 Exchange 和 Queue (TTL + DLX 模式)...", instanceId);

                await SetupExchangeAndQueue(channel);
                
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    await ProcessMessage(channel, ea);
                };

                var consumerTag = await channel.BasicConsumeAsync(
                    queue: ProcessingQueue,
                    autoAck: false,
                    consumer: consumer
                );
                
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 消費者標籤: {consumerTag}");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 消費者已註冊到 queue: {ProcessingQueue}");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 使用 TTL + DLX 模式 (Amazon MQ 相容)");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 等待延遲訊息中...");
                
                _logger.LogInformation("[{InstanceId}] OrderTimeoutConsumer 啟動成功，開始監聽延遲訊息 (TTL + DLX 模式)...", instanceId);
                
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

        /// <summary>
        /// 設定 TTL + DLX 延遲訊息基礎架構
        /// </summary>
        private async Task SetupExchangeAndQueue(IChannel channel)
        {
            // 1. 宣告死信交換器 (DLX) - 接收過期訊息
            await channel.ExchangeDeclareAsync(
                exchange: DeadLetterExchange,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                arguments: null
            );

            // 2. 宣告處理隊列 - 消費者實際監聽的隊列
            await channel.QueueDeclareAsync(
                queue: ProcessingQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            // 3. 綁定處理隊列到死信交換器
            await channel.QueueBindAsync(
                queue: ProcessingQueue,
                exchange: DeadLetterExchange,
                routingKey: ProcessingRoutingKey
            );

            // 4. 宣告入口交換器 - 接收生產者訊息
            await channel.ExchangeDeclareAsync(
                exchange: DelayExchange,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                arguments: null
            );

            // 5. 宣告延遲隊列 - 僅設定 DLX，延遲時間改用 per-message TTL
            var delayQueueArgs = new Dictionary<string, object>
            {
                // 訊息過期後轉發到死信交換器
                { "x-dead-letter-exchange", DeadLetterExchange },
                // 訊息過期後使用的路由鍵
                { "x-dead-letter-routing-key", ProcessingRoutingKey }
            };

            await channel.QueueDeclareAsync(
                queue: DelayQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: delayQueueArgs
            );

            // 6. 綁定延遲隊列到入口交換器
            await channel.QueueBindAsync(
                queue: DelayQueue,
                exchange: DelayExchange,
                routingKey: DelayRoutingKey
            );

            // 設置公平分發
            await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

            _logger.LogInformation("TTL + DLX 延遲訊息架構設定完成，延遲時間改由每則訊息的 Expiration 控制");
        }

        private async Task ProcessMessage(IChannel channel, BasicDeliverEventArgs ea)
        {
            try
            {
                var body = ea.Body.ToArray();
                var messageJson = Encoding.UTF8.GetString(body);

                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 🔥 收到延遲訊息: {messageJson}");
                _logger.LogInformation("收到延遲訊息: {Message}", messageJson);

                // 使用動態反序列化，避免類型不匹配問題
                var timeoutMessage = JsonSerializer.Deserialize<dynamic>(messageJson);
                
                var userId = timeoutMessage.GetProperty("UserId").GetInt32();
                var recordCode = timeoutMessage.GetProperty("RecordCode").GetString();
                
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 開始處理訂單超時: {recordCode}");
                
                // 使用 scoped 服務處理訊息
                using var scope = _scopeFactory.CreateScope();
                var orderTimeoutHandler = scope.ServiceProvider.GetRequiredService<IOrderTimeoutHandler>();
                
                await orderTimeoutHandler.HandleOrderTimeoutAsync(userId, recordCode);

                // 處理成功，發送 ACK
                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 訂單超時處理完成: {recordCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 處理延遲訊息失敗: {ex.Message}");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 錯誤詳情: {ex}");
                _logger.LogError(ex, "處理延遲訊息失敗");
                
                // 處理失敗，發送 NACK (不重新排隊，避免無限循環)
                await channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
            }
        }
    }
}
