using EcommerceBackend.Interfaces.Repositorys;
using EcommerceBackend.Interfaces.Services;
using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepostory _orderRepostory;
        public OrderService(IOrderRepostory orderRepostory) 
        {
            _orderRepostory = orderRepostory;
        }
        public ServiceResult<List<OrderInfomation>>  GetOrders(string userid)
        {
            var orderList=_orderRepostory.GetOrdersByUserId(userid);

            return new ServiceResult<List<OrderInfomation>>()
            {
                IsSuccess = true,
                Data = orderList
            };

        }
    }
}
