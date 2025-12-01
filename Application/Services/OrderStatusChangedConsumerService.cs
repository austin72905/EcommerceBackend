using Common.Interfaces.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class OrderStatusChangedConsumerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OrderStatusChangedConsumerService> _logger;

        public OrderStatusChangedConsumerService(
            IServiceScopeFactory scopeFactory,
            ILogger<OrderStatusChangedConsumerService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var serviceId = Guid.NewGuid().ToString("N")[..8];
            _logger.LogInformation("[{ServiceId}] OrderStatusChangedConsumerService 正在啟動...", serviceId);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var consumer = scope.ServiceProvider.GetRequiredService<IOrderStatusChangedConsumer>();

                            _logger.LogInformation("[{ServiceId}] 正在啟動 OrderStatusChangedConsumer...", serviceId);

                            if (consumer is Infrastructure.MQ.OrderStatusChangedConsumer orderStatusChangedConsumer)
                            {
                                await orderStatusChangedConsumer.StartListening(stoppingToken);
                            }
                            else
                            {
                                await consumer.StartListening();
                            }

                            _logger.LogInformation("[{ServiceId}] OrderStatusChangedConsumer 已正常結束", serviceId);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.LogInformation("[{ServiceId}] OrderStatusChangedConsumerService 被取消", serviceId);
                        break;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "[{ServiceId}] OrderStatusChangedConsumerService 發生錯誤，5秒後重試", serviceId);
                        // 等待一段時間後重試
                        try
                        {
                            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                        }
                        catch (OperationCanceledException)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{ServiceId}] OrderStatusChangedConsumerService 發生致命錯誤: {Message}", serviceId, ex.Message);
            }
            finally
            {
                _logger.LogInformation("[{ServiceId}] OrderStatusChangedConsumerService 已停止", serviceId);
            }
        }
    }
}

