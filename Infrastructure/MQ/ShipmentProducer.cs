using Common.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.MQ
{
    public class ShipmentProducer: IShipmentProducer
    {
        private readonly RabbitMqConnectionManager _connectionManager;
        private readonly ILogger<ShipmentProducer> _logger;

        public ShipmentProducer(RabbitMqConnectionManager connectionManager, ILogger<ShipmentProducer> logger)
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

                const string dlx = "dead.letter.exchange";
                const string dlq = "shipment_queue.dlq";
                const string dlqRoutingKey = "shipment.dlq";

                // 確保交換器存在
                await channel.ExchangeDeclareAsync(
                    exchange: "shipment.status.update",
                    type: "direct", // 使用 Direct 類型交換器
                    durable: true,
                    autoDelete: false
                );

                // DLX/DLQ
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


                // 確保隊列存在並綁定
                // (當你使用 QueueDeclare 或 QueueDeclareAsync 方法時，RabbitMQ 會自動幫你創建隊列（如果該隊列不存在）。這是 RabbitMQ 的一個很方便的特性，讓應用程序可以在需要時動態創建資源，而無需提前手動配置。)
                await channel.QueueDeclareAsync(
                    queue: "shipment_queue",
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
                    queue: "shipment_queue",
                    exchange: "shipment.status.update",
                    routingKey: "shipment.cansend"
                );




                //發送消息
                var messageJson = JsonSerializer.Serialize(message);
                var messageBody = Encoding.UTF8.GetBytes(messageJson);

                await channel.BasicPublishAsync
                    (
                        exchange: "shipment.status.update",
                        routingKey: "shipment.cansend",
                        body: messageBody
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "發送物流消息時發生錯誤");
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
