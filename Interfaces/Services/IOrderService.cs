using EcommerceBackend.Models;

namespace EcommerceBackend.Interfaces.Services
{
    public interface IOrderService
    {
        public ServiceResult<List<OrderInfomation>> GetOrders(string userid);
    }
}
