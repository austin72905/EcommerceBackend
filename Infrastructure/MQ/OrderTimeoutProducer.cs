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
    public class OrderTimeoutProducer : IOrderTimeoutProducer
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConfiguration _configuration;

        public OrderTimeoutProducer(IConfiguration configuration)
        {
            _configuration = configuration;

            _connectionFactory = new ConnectionFactory
            {
                HostName = _configuration["AppSettings:RabbitMqHostName"],
            };
        }

        public async Task SendOrderTimeoutMessageAsync(int userId, string recordCode, int delayMinutes = 10)
        {
            var connection = await _connectionFactory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            try
            {
                // 宣告延遲交換器 (使用 x-delayed-message 插件)
                var exchangeArgs = new Dictionary<string, object>
                {
                    { "x-delayed-type", "direct" }
                };

                await channel.ExchangeDeclareAsync(
                    exchange: "order.timeout.delayed",
                    type: "x-delayed-message", // 使用 x-delayed-message 插件
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

                // 創建訊息
                var message = new OrderTimeoutMessage
                {
                    UserId = userId,
                    RecordCode = recordCode,
                    CreatedAt = DateTime.UtcNow
                };

                var messageJson = JsonSerializer.Serialize(message);
                var messageBody = Encoding.UTF8.GetBytes(messageJson);

                // 設定延遲時間 (毫秒)
                var delayMilliseconds = delayMinutes * 60 * 1000;

                // 設定訊息屬性，包含延遲時間
                var basicProperties = new BasicProperties();
                basicProperties.Headers = new Dictionary<string, object>
                {
                    { "x-delay", delayMilliseconds }
                };
                basicProperties.Persistent = true; // 持久化訊息

                // 發送延遲訊息
                await channel.BasicPublishAsync(
                    exchange: "order.timeout.delayed",
                    routingKey: "order.timeout",
                    mandatory: false,
                    basicProperties: basicProperties,
                    body: messageBody
                );

                var currentTime = DateTime.Now;
                var expectedTime = currentTime.AddMinutes(delayMinutes);
                Console.WriteLine($"[x] Sent delayed order timeout message for order {recordCode}, delay: {delayMinutes} minutes");
                Console.WriteLine($"[x] Message sent at: {currentTime:HH:mm:ss}, expected processing at: {expectedTime:HH:mm:ss}");
            }
            finally
            {
                await channel.CloseAsync();
                await connection.CloseAsync();
            }
        }

        /// <summary>
        /// 發送以秒為單位的延遲訊息（用於測試）
        /// </summary>
        public async Task SendOrderTimeoutMessageWithSecondsAsync(int userId, string recordCode, int delaySeconds)
        {
            var connection = await _connectionFactory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            try
            {
                // 宣告延遲交換器 (使用 x-delayed-message 插件)
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

                // 創建訊息
                var message = new OrderTimeoutMessage
                {
                    UserId = userId,
                    RecordCode = recordCode,
                    CreatedAt = DateTime.UtcNow
                };

                var messageJson = JsonSerializer.Serialize(message);
                var messageBody = Encoding.UTF8.GetBytes(messageJson);

                // 設定延遲時間 (毫秒)
                var delayMilliseconds = delaySeconds * 1000;

                // 設定訊息屬性，包含延遲時間
                var basicProperties = new BasicProperties();
                basicProperties.Headers = new Dictionary<string, object>
                {
                    { "x-delay", delayMilliseconds }
                };
                basicProperties.Persistent = true; // 持久化訊息

                // 發送延遲訊息
                await channel.BasicPublishAsync(
                    exchange: "order.timeout.delayed",
                    routingKey: "order.timeout",
                    mandatory: false,
                    basicProperties: basicProperties,
                    body: messageBody
                );

                var currentTime = DateTime.Now;
                var expectedTime = currentTime.AddSeconds(delaySeconds);
                Console.WriteLine($"[x] Sent delayed order timeout message for order {recordCode}, delay: {delaySeconds} seconds");
                Console.WriteLine($"[x] Message sent at: {currentTime:HH:mm:ss.fff}, expected processing at: {expectedTime:HH:mm:ss.fff}");
            }
            finally
            {
                await channel.CloseAsync();
                await connection.CloseAsync();
            }
        }
    }

    public class OrderTimeoutMessage
    {
        public int UserId { get; set; }
        public string RecordCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}