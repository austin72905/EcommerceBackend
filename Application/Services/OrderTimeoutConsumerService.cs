using Common.Interfaces.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Infrastructure.MQ;

namespace Application.Services
{
    /// <summary>
    /// 訂單超時消費者背景服務 - 使用正確的服務生命週期管理
    /// </summary>
    public class OrderTimeoutConsumerService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<OrderTimeoutConsumerService> _logger;

        public OrderTimeoutConsumerService(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<OrderTimeoutConsumerService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var serviceId = Guid.NewGuid().ToString("N")[..8];
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  [{serviceId}] OrderTimeoutConsumerService 正在啟動...");
            _logger.LogInformation("[{ServiceId}] OrderTimeoutConsumerService 正在啟動...", serviceId);
            
            try
            {
                // 等待應用程式完全啟動
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  [{serviceId}] 等待 5 秒...");
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                
                // 只啟動一次消費者，讓它持續運行直到取消
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  [{serviceId}] 建立 ServiceScope...");
                using var scope = _serviceScopeFactory.CreateScope();
                var consumer = scope.ServiceProvider.GetRequiredService<IOrderTimeoutConsumer>();
                
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  [{serviceId}] 正在啟動 OrderTimeoutConsumer...");
                _logger.LogInformation("[{ServiceId}] 正在啟動 OrderTimeoutConsumer...", serviceId);
                
                // 使用新的 StartListening 方法，傳入 CancellationToken
                if (consumer is OrderTimeoutConsumer orderTimeoutConsumer)
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  [{serviceId}] 呼叫 StartListening...");
                    await orderTimeoutConsumer.StartListening(stoppingToken);
                }
                else
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  [{serviceId}] 呼叫預設 StartListening...");
                    await consumer.StartListening();
                }
                
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  [{serviceId}] OrderTimeoutConsumer 已正常結束");
                _logger.LogInformation("[{ServiceId}] OrderTimeoutConsumer 已正常結束", serviceId);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  [{serviceId}] OrderTimeoutConsumerService 被取消");
                _logger.LogInformation("[{ServiceId}] OrderTimeoutConsumerService 被取消", serviceId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  [{serviceId}] OrderTimeoutConsumerService 發生致命錯誤: {ex.Message}");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  [{serviceId}] 錯誤詳情: {ex}");
                _logger.LogError(ex, "[{ServiceId}] OrderTimeoutConsumerService 發生致命錯誤", serviceId);
                throw;
            }
            finally
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  [{serviceId}] OrderTimeoutConsumerService 已停止");
                _logger.LogInformation("[{ServiceId}] OrderTimeoutConsumerService 已停止", serviceId);
            }
        }
    }
}