using Infrastructure.Interfaces;
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
    public class ShipmentProducer: IShipmentProducer
    {
        private readonly ConnectionFactory _connectionFactory;

        private readonly IConfiguration _configuration;

        public ShipmentProducer(IConfiguration configuration)
        {
            _configuration= configuration;

            _connectionFactory =new ConnectionFactory 
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
                    exchange: "shipment.status.update",
                    type: "direct", // 使用 Direct 類型交換器
                    durable: true,
                    autoDelete: false
                );



                // 確保隊列存在並綁定
                // (當你使用 QueueDeclare 或 QueueDeclareAsync 方法時，RabbitMQ 會自動幫你創建隊列（如果該隊列不存在）。這是 RabbitMQ 的一個很方便的特性，讓應用程序可以在需要時動態創建資源，而無需提前手動配置。)
                await channel.QueueDeclareAsync(
                    queue: "shipment_queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
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
    }
}
