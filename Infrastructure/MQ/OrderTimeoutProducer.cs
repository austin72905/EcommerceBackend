using Common.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.MQ
{
    /// <summary>
    /// 訂單超時訊息生產者
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
    public class OrderTimeoutProducer : IOrderTimeoutProducer
    {
        private readonly ConnectionFactory _connectionFactory;
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

        public OrderTimeoutProducer(IConfiguration configuration)
        {
            _configuration = configuration;
            var rabbitMqUri = _configuration["AppSettings:RabbitMqUri"] ?? "amqp://guest:guest@localhost:5672/";
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(rabbitMqUri),
            };
        }

        public async Task SendOrderTimeoutMessageAsync(int userId, string recordCode, int delayMinutes = 10)
        {
            var delayMilliseconds = delayMinutes * 60 * 1000;
            await SendDelayedMessageAsync(userId, recordCode, delayMilliseconds, delayMinutes, "minutes");
        }

        /// <summary>
        /// 發送以秒為單位的延遲訊息（用於測試）
        /// </summary>
        public async Task SendOrderTimeoutMessageWithSecondsAsync(int userId, string recordCode, int delaySeconds)
        {
            var delayMilliseconds = delaySeconds * 1000;
            await SendDelayedMessageAsync(userId, recordCode, delayMilliseconds, delaySeconds, "seconds");
        }

        /// <summary>
        /// 核心方法：使用 TTL + DLX 模式發送延遲訊息
        /// </summary>
        private async Task SendDelayedMessageAsync(int userId, string recordCode, int delayMilliseconds, int delayValue, string delayUnit)
        {
            var connection = await _connectionFactory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            try
            {
                // 設定 Exchange 和 Queue（使用 TTL + DLX 模式）
                await SetupDelayedQueueInfrastructure(channel, delayMilliseconds);

                // 創建訊息
                var message = new OrderTimeoutMessage
                {
                    UserId = userId,
                    RecordCode = recordCode,
                    CreatedAt = DateTime.UtcNow
                };

                var messageJson = JsonSerializer.Serialize(message);
                var messageBody = Encoding.UTF8.GetBytes(messageJson);

                // 設定訊息屬性
                var basicProperties = new BasicProperties
                {
                    Persistent = true,
                    // 動態延遲改用 per-message TTL，避免 Queue 被不同 TTL 重新宣告
                    Expiration = delayMilliseconds.ToString()
                };

                // 發送訊息到延遲交換器
                await channel.BasicPublishAsync(
                    exchange: DelayExchange,
                    routingKey: DelayRoutingKey,
                    mandatory: false,
                    basicProperties: basicProperties,
                    body: messageBody
                );

                var currentTime = DateTime.Now;
                var expectedTime = delayUnit == "seconds" 
                    ? currentTime.AddSeconds(delayValue) 
                    : currentTime.AddMinutes(delayValue);
                    
                Console.WriteLine($"[x] Sent delayed order timeout message for order {recordCode}, delay: {delayValue} {delayUnit}");
                Console.WriteLine($"[x] Message sent at: {currentTime:HH:mm:ss.fff}, expected processing at: {expectedTime:HH:mm:ss.fff}");
                Console.WriteLine($"[x] Using TTL + DLX mode (Amazon MQ compatible)");
            }
            finally
            {
                await channel.CloseAsync();
                await connection.CloseAsync();
            }
        }

        /// <summary>
        /// 設定 TTL + DLX 延遲訊息基礎架構
        /// </summary>
        private async Task SetupDelayedQueueInfrastructure(IChannel channel, int delayMilliseconds)
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

            // 5. 宣告延遲隊列 - 僅設定 DLX，TTL 交由 per-message Expiration 控制
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
        }
    }

    public class OrderTimeoutMessage
    {
        public int UserId { get; set; }
        public string RecordCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
