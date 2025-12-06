using Common.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Infrastructure.MQ
{
    public class OrderStateProducer : IOrderStateProducer
    {
        private readonly RabbitMqConnectionManager _connectionManager;
        private readonly ILogger<OrderStateProducer> _logger;

        public OrderStateProducer(RabbitMqConnectionManager connectionManager, ILogger<OrderStateProducer> logger)
        {
            _connectionManager = connectionManager;
            _logger = logger;
        }

        public async Task SendMessage(object message)
        {
            IChannel? channel = null;
            try
            {
                // 使用連接管理器獲取 channel（重用連接）
                channel = await _connectionManager.CreateChannelAsync();

            // 確保交換器存在
            await channel.ExchangeDeclareAsync(
                exchange: "order.state.update",
                type: "direct",
                durable: true,
                autoDelete: false
            );

            const string dlx = "dead.letter.exchange";
            const string dlq = "order_state_queue.dlq";
            const string dlqRoutingKey = "order.state.dlq";

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

            var queueArgs = new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", dlx },
                { "x-dead-letter-routing-key", dlqRoutingKey }
            };

            await channel.QueueDeclareAsync(
                queue: "order_state_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: queueArgs
            );

            // 綁定交換器與隊列
            await channel.QueueBindAsync(
                queue: "order_state_queue",
                exchange: "order.state.update",
                routingKey: "order.state.payment.completed"
            );

            // 發送消息
            var messageJson = JsonSerializer.Serialize(message);
            var messageBody = Encoding.UTF8.GetBytes(messageJson);

                await channel.BasicPublishAsync(
                    exchange: "order.state.update",
                    routingKey: "order.state.payment.completed",
                    body: messageBody
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "發送訂單狀態消息時發生錯誤");
                throw;
            }
            finally
            {
                // 只關閉 channel，連接由連接管理器管理
                if (channel != null && channel.IsOpen)
                {
                    try
                    {
                        await channel.CloseAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "關閉 channel 時發生錯誤");
                    }
                }
            }
        }
    }
}

