using System.Text;
using System.Text.Json;
using System.Threading;
using System.Collections.Generic;
using Common.Interfaces.Infrastructure;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Infrastructure.MQ
{
    public class PaymentCompletedProducer : IPaymentCompletedProducer
    {
        private readonly RabbitMqConnectionManager _connectionManager;
        private readonly ILogger<PaymentCompletedProducer> _logger;
        private readonly SemaphoreSlim _channelLock = new(1, 1);
        private IChannel? _channel;

        private const string Exchange = "payment.completed";
        private const string Queue = "payment_completed_queue";
        private const string RoutingKey = "payment.completed";
        private const string Dlx = "dead.letter.exchange";
        private const string Dlq = "payment_completed_queue.dlq";
        private const string DlqRoutingKey = "payment.completed.dlq";

        public PaymentCompletedProducer(
            RabbitMqConnectionManager connectionManager,
            ILogger<PaymentCompletedProducer> logger)
        {
            _connectionManager = connectionManager;
            _logger = logger;
        }

        public async Task SendMessage(object message)
        {
            try
            {
                var channel = await GetOrCreateChannelAsync();
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                await channel.BasicPublishAsync(
                    exchange: Exchange,
                    routingKey: RoutingKey,
                    body: body);
            }
            catch (Exception ex)
            {
                // 發送失敗時重置 channel，下次會重新建立
                _channel = null;
                _logger.LogError(ex, "發送支付完成消息時發生錯誤");
                throw;
            }
        }

        /// <summary>
        /// 取得或建立可重用的 Channel，並確保 Exchange/Queue/Binding 已宣告。
        /// </summary>
        private async Task<IChannel> GetOrCreateChannelAsync()
        {
            if (_channel?.IsOpen == true)
            {
                return _channel;
            }

            await _channelLock.WaitAsync();
            try
            {
                if (_channel?.IsOpen == true)
                {
                    return _channel;
                }

                if (_channel != null)
                {
                    try { await _channel.CloseAsync(); } catch { /* ignore */ }
                }

                var channel = await _connectionManager.CreateChannelAsync();

                // 宣告主交換器
                await channel.ExchangeDeclareAsync(
                    exchange: Exchange,
                    type: "direct",
                    durable: true,
                    autoDelete: false);

                // 宣告 DLX/DLQ
                await channel.ExchangeDeclareAsync(
                    exchange: Dlx,
                    type: "direct",
                    durable: true,
                    autoDelete: false);

                await channel.QueueDeclareAsync(
                    queue: Dlq,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                await channel.QueueBindAsync(
                    queue: Dlq,
                    exchange: Dlx,
                    routingKey: DlqRoutingKey);

                var queueArgs = new Dictionary<string, object>
                {
                    { "x-dead-letter-exchange", Dlx },
                    { "x-dead-letter-routing-key", DlqRoutingKey }
                };

                await channel.QueueDeclareAsync(
                    queue: Queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: queueArgs);

                await channel.QueueBindAsync(
                    queue: Queue,
                    exchange: Exchange,
                    routingKey: RoutingKey);

                _channel = channel;
                return channel;
            }
            finally
            {
                _channelLock.Release();
            }
        }
    }
}
