

using Application;
using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepostory _orderRepostory;
        public OrderService(IOrderRepostory orderRepostory) 
        {
            _orderRepostory = orderRepostory;
        }

        public  async Task<ServiceResult<OrderInfomationDTO>> GetOrderInfo(int userid, string recordCode)
        {
            var orderInfo =await _orderRepostory.GetOrderInfoByUserId(userid, recordCode);

            return new ServiceResult<OrderInfomationDTO>()
            {
                IsSuccess = true,
                Data = new OrderInfomationDTO()
            };
        }

        public async Task<ServiceResult<List<OrderInfomationDTO>>>  GetOrders(int userid)
        {
            var orderList=await _orderRepostory.GetOrdersByUserId(userid);

            return new ServiceResult<List<OrderInfomationDTO>>()
            {
                IsSuccess = true,
                Data = new List<OrderInfomationDTO>()
            };

        }


        public async Task<ServiceResult<PaymentRequestDataWithUrl>> GenerateOrder()
        {
            await _orderRepostory.GenerateOrder();

            return new ServiceResult<PaymentRequestDataWithUrl>
            {
                IsSuccess = true,
                Data = new PaymentRequestDataWithUrl() { Amount="",RecordNo="",PaymentUrl="",PayType=""}
            };
        }
    }
}
