using System.Text;
using System.Text.Json;
using Application.DTOs;
using Common.Interfaces.Infrastructure;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.MQ;
using System.Collections.Generic;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Application.Services
{
    /// <summary>
    /// 消費支付完成事件：建立 OrderStep、確認庫存、發送 MQ
    /// </summary>
    public class PaymentCompletedConsumerService : BackgroundService
    {
        private readonly RabbitMqConnectionManager _connectionManager;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<PaymentCompletedConsumerService> _logger;

        public PaymentCompletedConsumerService(
            RabbitMqConnectionManager connectionManager,
            IServiceScopeFactory scopeFactory,
            ILogger<PaymentCompletedConsumerService> logger)
        {
            _connectionManager = connectionManager;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            const string exchange = "payment.completed";
            const string queue = "payment_completed_queue";
            const string routingKey = "payment.completed";

            while (!stoppingToken.IsCancellationRequested)
            {
                IChannel? channel = null;
                try
                {
                    channel = await _connectionManager.CreateChannelAsync();

                    await channel.ExchangeDeclareAsync(
                        exchange: exchange,
                        type: "direct",
                        durable: true,
                        autoDelete: false);

                    const string dlx = "dead.letter.exchange";
                    const string dlq = "payment_completed_queue.dlq";
                    const string dlqRoutingKey = "payment.completed.dlq";

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

                    await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 5, global: false);

                    var consumer = new AsyncEventingBasicConsumer(channel);
                    consumer.ReceivedAsync += async (_, ea) =>
                    {
                        var payload = Encoding.UTF8.GetString(ea.Body.ToArray());
                        try
                        {
                            var evt = JsonSerializer.Deserialize<PaymentCompletedEvent>(payload);
                            if (evt == null)
                            {
                                throw new InvalidOperationException("無法解析支付完成事件");
                            }

                            await HandleEventAsync(evt, stoppingToken);
                            await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "處理支付完成事件失敗，payload: {Payload}", payload);
                            await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
                        }
                    };

                    await channel.BasicConsumeAsync(
                        queue: queue,
                        autoAck: false,
                        consumer: consumer);

                    // block until cancelled
                    await Task.Delay(Timeout.Infinite, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "PaymentCompletedConsumerService 發生錯誤，5 秒後重試");
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
                finally
                {
                    if (channel != null)
                    {
                        try { await channel.CloseAsync(); } catch { /* ignore */ }
                    }
                }
            }
        }

        private async Task HandleEventAsync(PaymentCompletedEvent evt, CancellationToken ct)
        {

            // 這裡是有使用連線池的嗎? 整個EF Core有使用連線持嗎?
            // 連線池在 Program.cs 透過 NpgsqlConnectionStringBuilder 設定
            // 連線池不是由 EF Core 管理，而是由 Npgsql ADO.NET Provider 管理。
            // 並在 AddDbContext + UseNpgsql 時傳遞給 Npgsql Provider。Npgsql 負責管理連線池，EF Core 透過 Npgsql 間接使用它
            using var scope = _scopeFactory.CreateScope();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepostory>();
            var inventoryService = scope.ServiceProvider.GetRequiredService<IInventoryService>();
            var shipmentProducer = scope.ServiceProvider.GetRequiredService<IShipmentProducer>();
            var orderStateProducer = scope.ServiceProvider.GetRequiredService<IOrderStateProducer>();

            var order = await orderRepository.GetOrderInfoByIdForUpdate(evt.OrderId);
            if (order == null)
            {
                _logger.LogWarning("支付完成事件找不到訂單，RecordCode={RecordCode}, OrderId={OrderId}", evt.RecordCode, evt.OrderId);
                return;
            }

            // 幂等：若已有 PaymentReceived 步驟就跳過建立
            var hasPaymentStep = order.OrderSteps.Any(s => s.StepStatus == (int)OrderStepStatus.PaymentReceived);
            if (!hasPaymentStep)
            {
                order.OrderSteps.Add(OrderStep.CreateForOrder(order.Id, (int)OrderStepStatus.PaymentReceived));
                order.OrderSteps.Add(OrderStep.CreateForOrder(order.Id, (int)OrderStepStatus.WaitingForShipment));
                await orderRepository.SaveChangesAsync();
                _logger.LogInformation("已為訂單建立支付/待出貨步驟，RecordCode={RecordCode}", evt.RecordCode);
            }

            // 前面下單時在 Redis 中預扣庫存，防止超賣
            // 付款成功後（資料庫正式扣除）
            var confirmResult = await inventoryService.ConfirmInventoryAsync(evt.RecordCode);
            if (!confirmResult.IsSuccess)
            {
                _logger.LogWarning("付款成功後確認庫存失敗，訂單編號: {RecordCode}, 錯誤: {Error}", evt.RecordCode, confirmResult.ErrorMessage);
                throw new InvalidOperationException($"庫存確認失敗: {confirmResult.ErrorMessage}");
            }

            var shipmentMessage = new
            {
                Status = (int)ShipmentStatus.Pending,
                OrderId = order.Id,
                RecordCode = order.RecordCode
            };

            var orderStateMessage = new
            {
                eventType = "PaymentCompleted",
                orderId = order.RecordCode,
                timestamp = evt.OccurredAtUtc
            };
            // 目前用不到，因為都是統一用 orderStateProducer 去模擬整個流程
            // 之後可以加上try catch
            await shipmentProducer.SendMessage(shipmentMessage);
            // 同步訂單狀態到外部 Go 微服務（ec-order-state-service）
            // 之後可以加上try catch 處理失敗
            await orderStateProducer.SendMessage(orderStateMessage);
            _logger.LogInformation("支付完成事件已處理並發送 MQ，RecordCode={RecordCode}", evt.RecordCode);
        }
    }
}
