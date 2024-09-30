using EcommerceBackend.Interfaces.Repositorys;
using EcommerceBackend.Interfaces.Services;
using EcommerceBackend.Models;
using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepostory _orderRepostory;
        public OrderService(IOrderRepostory orderRepostory) 
        {
            _orderRepostory = orderRepostory;
        }
        public ServiceResult<List<OrderInfomationDTO>>  GetOrders(string userid)
        {
            var orderList=_orderRepostory.GetOrdersByUserId(userid);

            return new ServiceResult<List<OrderInfomationDTO>>()
            {
                IsSuccess = true,
                Data = orderList
            };

        }
    }
}
