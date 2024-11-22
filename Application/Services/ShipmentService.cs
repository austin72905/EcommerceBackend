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
                // 模擬處理物流訂單的過程
                // 以一個定時器10秒新增一次
                var shipments = new Queue<Shipment>
                    (
                        new[]
                        {
                            //new Shipment{ ShipmentStatus= (int)ShipmentStatus.Pending,OrderId=orderId },
                            new Shipment{ ShipmentStatus= (int)ShipmentStatus.Shipped,OrderId=orderId },
                            new Shipment{ ShipmentStatus= (int)ShipmentStatus.InTransit,OrderId=orderId },
                            new Shipment{ ShipmentStatus= (int)ShipmentStatus.OutForDelivery,OrderId=orderId },
                            new Shipment{ ShipmentStatus= (int)ShipmentStatus.Delivered,OrderId=orderId },
                            new Shipment{ ShipmentStatus= (int)ShipmentStatus.PickedUpByCustomer,OrderId=orderId },

                        }
                    );
                _queueProcessor.AddQueue(shipments);
            }


        }
    }
}
