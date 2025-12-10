

using Application.DTOs;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        public Task<ServiceResult<List<OrderInfomationDTO>>> GetOrders(int userid,string query);

        public Task<ServiceResult<OrderInfomationDTO>> GetOrderInfo(int userid,string recordCode);


        public Task<ServiceResult<PaymentRequestDataWithUrl>> GenerateOrder(OrderInfo info);


        /// <summary>
        /// 檢查DB支付狀態，如果未支付 or 支付失敗 就回補庫存到 redis
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task HandleOrderTimeoutAsync(int userId, string recordcode);

        /// <summary>
        /// 從訂單狀態服務同步訂單狀態（統一使用數字 enum）
        /// </summary>
        /// <param name="recordCode">訂單編號</param>
        /// <param name="fromStatus">來源狀態（數字 enum 值）</param>
        /// <param name="toStatus">目標狀態（數字 enum 值）</param>
        /// <returns></returns>
        public Task SyncOrderStatusFromStateServiceAsync(string recordCode, int fromStatus, int toStatus);

    }
}
