using System.Text;
using System.Text.Json;
using Common.Interfaces.Infrastructure;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace Infrastructure.MQ
{
    public class PaymentCompletedProducer : IPaymentCompletedProducer
    {
        private readonly RabbitMqConnectionManager _connectionManager;
        private readonly ILogger<PaymentCompletedProducer> _logger;

        public PaymentCompletedProducer(
            RabbitMqConnectionManager connectionManager,
            ILogger<PaymentCompletedProducer> logger)
        {
            _connectionManager = connectionManager;
            _logger = logger;
        }

        public async Task SendMessage(object message)
        {
            IChannel? channel = null;
            try
            {
                channel = await _connectionManager.CreateChannelAsync();

                const string exchange = "payment.completed";
                const string queue = "payment_completed_queue";
                const string routingKey = "payment.completed";
                const string dlx = "dead.letter.exchange";
                const string dlq = "payment_completed_queue.dlq";
                const string dlqRoutingKey = "payment.completed.dlq";

                await channel.ExchangeDeclareAsync(
                    exchange: exchange,
                    type: "direct",
                    durable: true,
                    autoDelete: false);

                // dead-letter exchange/queue for manual inspection
                await channel.ExchangeDeclareAsync(
                    exchange: dlx,
                    type: "direct",
                    durable: true,
                    autoDelete: false);

                await channel.QueueDeclareAsync(
                    queue: dlq,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                await channel.QueueBindAsync(
                    queue: dlq,
                    exchange: dlx,
                    routingKey: dlqRoutingKey);

                var queueArgs = new Dictionary<string, object>
                {
                    { "x-dead-letter-exchange", dlx },
                    { "x-dead-letter-routing-key", dlqRoutingKey }
                };

                await channel.QueueDeclareAsync(
                    queue: queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: queueArgs);

                await channel.QueueBindAsync(
                    queue: queue,
                    exchange: exchange,
                    routingKey: routingKey);

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                await channel.BasicPublishAsync(
                    exchange: exchange,
                    routingKey: routingKey,
                    body: body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "發送支付完成事件失敗");
                throw;
            }
            finally
            {
                if (channel != null && channel.IsOpen)
                {
                    try
                    {
                        await channel.CloseAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "關閉 payment completed channel 時發生錯誤");
                    }
                }
            }
        }
    }
}
