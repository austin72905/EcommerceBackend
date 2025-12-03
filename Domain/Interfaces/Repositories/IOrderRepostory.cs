using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IOrderRepostory
    {
        public Task<IEnumerable<Order>> GetOrdersByUserId(int userid, string? query = null);

        public Task<Order?> GetOrderInfoById(int orderId);

        /// <summary>
        /// 獲取訂單（帶追蹤，用於更新狀態）
        /// </summary>
        public Task<Order?> GetOrderInfoByIdForUpdate(int orderId);

        public Task<Order?> GetOrderInfoByUserId(int userid, string recordCode);

        public Task<Order?> GetOrderInfoByRecordCode(string recordCode);

        /// <summary>
        /// 獲取訂單（帶追蹤，用於更新狀態）
        /// </summary>
        public Task<Order?> GetOrderInfoByRecordCodeForUpdate(string recordCode);

        public Task GenerateOrder(Order order);

        
        public Task UpdateOrderStatusAsync(string recordCode, int status);

        public Task SaveChangesAsync();
    }
}
