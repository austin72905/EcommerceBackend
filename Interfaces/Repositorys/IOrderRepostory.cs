using EcommerceBackend.Models;

namespace EcommerceBackend.Interfaces.Repositorys
{
    public interface IOrderRepostory
    {
        public List<OrderInfomation> GetOrdersByUserId(string userid);
    }
}
