using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Common.Interfaces.Application.Services;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Common.Interfaces.Infrastructure;
using System.Collections.Generic;

namespace Infrastructure.MQ
{
    public class ShipmentConsumer: IShipmentConsumer
    {
        private readonly ConnectionFactory _connectionFactory;

        private readonly IServiceScopeFactory _scopeFactory;

        private readonly IConfiguration _configuration;

        public ShipmentConsumer(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _configuration = configuration;

            // 初始化 RabbitMQ 連接工廠
            _connectionFactory = new ConnectionFactory
            {
                HostName = _configuration["AppSettings:RabbitMqHostName"],

            };

            _scopeFactory = serviceScopeFactory;
        }

        public async Task StartListening()
        {
            var connection = await _connectionFactory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();
            

                // 確保交換器存在
                await channel.ExchangeDeclareAsync(
                    exchange: "shipment.status.update",
                    type: "direct",
                    durable: true,
                    autoDelete: false
                );

                // 確保隊列存在
                const string dlx = "dead.letter.exchange";
                const string dlq = "shipment_queue.dlq";
                const string dlqRoutingKey = "shipment.dlq";

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
                    arguments: null);

                await channel.QueueBindAsync(
                    queue: dlq,
                    exchange: dlx,
                    routingKey: dlqRoutingKey);

                await channel.QueueDeclareAsync(queue: "shipment_queue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: new Dictionary<string, object>
                                     {
                                         { "x-dead-letter-exchange", dlx },
                                         { "x-dead-letter-routing-key", dlqRoutingKey }
                                     });

                await channel.QueueBindAsync(
                   queue: "shipment_queue",
                   exchange: "shipment.status.update",
                   routingKey: "shipment.cansend"
               );


                // 設置公平分發
                await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var messageJson = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"[x] Received message: {messageJson}");

                    try
                    {
                        // 解析消息並處理
                        var shipmentMessage = JsonSerializer.Deserialize<ShipmentMessage>(messageJson);

                        // 處理消息
                        await ProcessShipmentMessageAsync(shipmentMessage);

                        // 消息處理成功，發送 ACK
                        await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                        Console.WriteLine($"[x] Shipment {shipmentMessage.RecordCode} processed successfully.");
                    }
                    catch (Exception ex)
                    {
                        // 消息處理失敗，發送 NACK
                        Console.WriteLine($"[!] Error processing message: {ex.Message}");
                        await channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                    }
                };

                // 開始消費
                await channel.BasicConsumeAsync(queue: "shipment_queue",
                                                 autoAck: false,
                                                 consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");

            
        }

        private async Task ProcessShipmentMessageAsync(ShipmentMessage shipmentMessage)
        {
            
            // 動態建立 Scope
            using (var scope = _scopeFactory.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<IShipmentService>();
                await scopedService.UpdateShipmentStatus(status:shipmentMessage.Status,recordCode:shipmentMessage.RecordCode,orderId:shipmentMessage.OrderId);
                
            }
        }


        public class ShipmentMessage
        {
            public int Status { get; set; }         // 使用 int 對應於枚舉值
            public int OrderId { get; set; }
            public string RecordCode { get; set; }
        }
    }
}
