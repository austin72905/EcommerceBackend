using EcommerceBackend.Models;

namespace EcommerceBackend.Interfaces.Services
{
    public interface IOrderService
    {
        public List<OrderInfomation> GetOrders(string userid);
    }
}
