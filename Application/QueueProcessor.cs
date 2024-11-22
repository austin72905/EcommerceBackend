using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public class QueueProcessor : IQueueProcessor
    {
        private readonly List<Queue<Shipment>> _queues = new List<Queue<Shipment>>();
        private readonly object _lock = new object();
        private bool _isProcessing = false;

        private readonly IServiceScopeFactory _scopeFactory;

        public QueueProcessor(IServiceScopeFactory serviceScopeFactory)
        {
            // 初始化時不啟動處理

            _scopeFactory = serviceScopeFactory;
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


            // 動態建立 Scope
            using (var scope = _scopeFactory.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<IShipmentRepository>();
                await scopedService.AddAsync(new Domain.Entities.Shipment
                {
                    OrderId = item.OrderId,
                    ShipmentStatus = item.ShipmentStatus,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,

                });
                await scopedService.SaveChangesAsync();
            }

            using (var scope = _scopeFactory.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<IOrderRepostory>();
                var order = await scopedService.GetOrderInfoById(item.OrderId);
                if (item.ShipmentStatus == (int)ShipmentStatus.InTransit)
                {
                    order.Status = (int)OrderStatus.InTransit;
                    order.OrderSteps.Add(new OrderStep
                    {
                        OrderId = item.OrderId,
                        StepStatus = (int)OrderStepStatus.ShipmentCompleted,
                        CreatedAt= DateTime.Now,
                        UpdatedAt= DateTime.Now,
                    });

                }
                else if (item.ShipmentStatus == (int)ShipmentStatus.Delivered)
                {
                    order.Status = (int)OrderStatus.WaitPickup;

                }
                else if (item.ShipmentStatus == (int)ShipmentStatus.PickedUpByCustomer)
                {
                    order.Status = (int)OrderStatus.Completed;
                    order.OrderSteps.Add(new OrderStep
                    {
                        OrderId = item.OrderId,
                        StepStatus = (int)OrderStepStatus.OrderCompleted,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    });

                }
                
                await scopedService.SaveChangesAsync();
            }

        }


    }
}
