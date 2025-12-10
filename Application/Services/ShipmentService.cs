using Common.Interfaces.Application.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Application.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository _shipmentRepository;

        public ShipmentService(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task UpdateShipmentStatus(int status, string recordCode, int orderId)
        {
            var shipmentList = await _shipmentRepository.GetShipmentsByOrderId(orderId);

            if (shipmentList.ToList().Count == 1)
            {
                // 模擬處理物流訂單的過程 - 使用富領域模型工廠方法
                // 以一個定時器10秒新增一次
                // 物流佇列流程已暫停，僅保留查詢條件避免未來誤觸
            }


        }
    }
}
