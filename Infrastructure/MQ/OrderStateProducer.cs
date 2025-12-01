using Common.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.MQ
{
    public class OrderStateProducer : IOrderStateProducer
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConfiguration _configuration;

        public OrderStateProducer(IConfiguration configuration)
        {
            _configuration = configuration;

            _connectionFactory = new ConnectionFactory
            {
                HostName = _configuration["AppSettings:RabbitMqHostName"],
            };
        }

        public async Task SendMessage(object message)
        {
            var connection = await _connectionFactory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            // 確保交換器存在
            await channel.ExchangeDeclareAsync(
                exchange: "order.state.update",
                type: "direct",
                durable: true,
                autoDelete: false
            );

            // 確保隊列存在並綁定
            await channel.QueueDeclareAsync(
                queue: "order_state_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
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

            await channel.CloseAsync();
            await connection.CloseAsync();
        }
    }
}

