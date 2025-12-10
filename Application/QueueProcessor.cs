using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class QueueProcessor : IQueueProcessor
    {
        private readonly List<Queue<Shipment>> _queues = new List<Queue<Shipment>>();
        private readonly object _lock = new object();
        private bool _isProcessing = false;

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<QueueProcessor> _logger;

        public QueueProcessor(IServiceScopeFactory serviceScopeFactory, ILogger<QueueProcessor> logger)
        {
            // 初始化時不啟動處理

            _scopeFactory = serviceScopeFactory;
            _logger = logger;
        }


        public Task AddQueueAsync(Queue<Shipment> queue, string recordCode)
        {
            _logger.LogInformation("新增物流佇列，訂單編號: {RecordCode}", recordCode);
            AddQueue(queue);
            return Task.CompletedTask;
        }


        // 新增 Queue
        public void AddQueue(Queue<Shipment> queue)
        {
            lock (_lock)
            {
                _queues.Add(queue);
                if (!_isProcessing)
                {
                    _isProcessing = true;
                    StartProcessing();
                }
            }
        }


        // 處理邏輯
        private void StartProcessing()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    Queue<Shipment> currentQueue = null;

                    lock (_lock)
                    {
                        if (_queues.Count > 0)
                        {
                            currentQueue = _queues[0];
                        }
                    }

                    if (currentQueue != null)
                    {
                        // 處理當前的 Queue
                        await ProcessQueue(currentQueue);

                        lock (_lock)
                        {
                            if (currentQueue.Count == 0)
                            {
                                _queues.Remove(currentQueue); // 處理完成後移除
                            }
                        }
                    }
                    else
                    {
                        lock (_lock)
                        {
                            // 沒有任何 Queue 時停止處理
                            _isProcessing = false;
                            break;
                        }
                    }

                    // 可選：添加處理間隔，避免無限循環佔用過多資源
                    Thread.Sleep(10000);
                }
            });
        }


        // 處理單個 Queue 的邏輯
        private async Task ProcessQueue(Queue<Shipment> queue)
        {
            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                await ProcessItem(item);
                // 每次处理一个项后，等待 15 秒
                Thread.Sleep(15000);
            }
        }


        // 處理單個項目
        private async Task ProcessItem(Shipment item)
        {
            Console.WriteLine($"Processing item: {item}");
            // 添加具體的業務邏輯


            // 動態建立 Scope - 使用富領域模型工廠方法
            using (var scope = _scopeFactory.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<IShipmentRepository>();
                // 使用 Shipment 工廠方法創建
                var shipment = Domain.Entities.Shipment.CreateForOrder(item.OrderId, item.ShipmentStatus);
                await scopedService.AddAsync(shipment);
                await scopedService.SaveChangesAsync();
            }

            using (var scope = _scopeFactory.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<IOrderRepostory>();
                // 使用帶追蹤的方法獲取訂單，以便 EF Core 可以追蹤變更
                var order = await scopedService.GetOrderInfoByIdForUpdate(item.OrderId);
                
                if (order == null)
                {
                    _logger.LogWarning("找不到訂單: OrderId={OrderId}", item.OrderId);
                    return;
                }
                
                // 使用 Order 的業務方法更新狀態
                if (item.ShipmentStatus == (int)ShipmentStatus.InTransit)
                {
                    order.UpdateStatus(OrderStatus.InTransit);
                    // UpdateStatus 會自動添加 OrderStep，但這裡需要特定的 ShipmentCompleted 步驟
                    // 如果需要不同的步驟狀態，可以單獨添加
                    // 注意：OrderStep 應該由 Order 管理，這裡暫時保持原邏輯
                }
                else if (item.ShipmentStatus == (int)ShipmentStatus.Delivered)
                {
                    order.UpdateStatus(OrderStatus.WaitPickup);
                }
                else if (item.ShipmentStatus == (int)ShipmentStatus.PickedUpByCustomer)
                {
                    order.Complete(); // 使用 Complete 方法完成訂單
                }
                
                await scopedService.SaveChangesAsync();
            }

        }


    }
}
