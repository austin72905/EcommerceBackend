using Common.Interfaces.Application.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Application.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IQueueProcessor _queueProcessor;

        public ShipmentService(IShipmentRepository shipmentRepository, IQueueProcessor queueProcessor)
        {
            _shipmentRepository = shipmentRepository;
            _queueProcessor = queueProcessor;
        }

        public async Task UpdateShipmentStatus(int status, string recordCode, int orderId)
        {
            var shipmentList = await _shipmentRepository.GetShipmentsByOrderId(orderId);

            if (shipmentList.ToList().Count == 1)
            {
                // 模擬處理物流訂單的過程 - 使用富領域模型工廠方法
                // 以一個定時器10秒新增一次
                var shipments = new Queue<Shipment>
                    (
                        new[]
                        {
                            //Shipment.CreateForOrder(orderId, (int)ShipmentStatus.Pending),
                            Shipment.CreateForOrder(orderId, (int)ShipmentStatus.Shipped),
                            Shipment.CreateForOrder(orderId, (int)ShipmentStatus.InTransit),
                            Shipment.CreateForOrder(orderId, (int)ShipmentStatus.OutForDelivery),
                            Shipment.CreateForOrder(orderId, (int)ShipmentStatus.Delivered),
                            Shipment.CreateForOrder(orderId, (int)ShipmentStatus.PickedUpByCustomer),
                        }
                    );
                _queueProcessor.AddQueue(shipments);
            }


        }
    }
}
