using Domain.Entities;
using Domain.Enums;
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

        /// <summary>
        /// 獲取訂單（僅載入 OrderProducts，用於庫存更新等輕量級操作）
        /// </summary>
        /// <param name="recordCode">訂單編號</param>
        /// <returns>訂單實體（僅包含 OrderProducts）</returns>
        public Task<Order?> GetOrderWithProductsOnlyForUpdate(string recordCode);

        /// <summary>
        /// 獲取訂單（僅載入 OrderSteps，用於狀態同步等輕量級操作）
        /// </summary>
        /// <param name="recordCode">訂單編號</param>
        /// <returns>訂單實體（僅包含 OrderSteps）</returns>
        Task<Order?> GetOrderForStatusUpdateAsync(string recordCode);

        public Task GenerateOrder(Order order);

        
        public Task UpdateOrderStatusAsync(string recordCode, int status);

        /// <summary>
        /// 單一 SQL 嘗試更新訂單狀態（帶狀態條件，避免重複處理）
        /// </summary>
        /// <param name="recordCode">訂單編號</param>
        /// <param name="fromStatus">預期原狀態</param>
        /// <param name="toStatus">更新目標狀態</param>
        /// <returns>成功更新回傳 true，未命中回傳 false</returns>
        public Task<bool> TryUpdateOrderStatusAsync(string recordCode, OrderStatus fromStatus, OrderStatus toStatus);

        public Task SaveChangesAsync();
    }
}
