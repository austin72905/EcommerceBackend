

using Application.DTOs;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        public Task<ServiceResult<List<OrderInfomationDTO>>> GetOrders(int userid);

        public Task<ServiceResult<OrderInfomationDTO>> GetOrderInfo(int userid,string recordCode);


        public Task<ServiceResult<PaymentRequestDataWithUrl>> GenerateOrder(OrderInfo info);



    }
}
