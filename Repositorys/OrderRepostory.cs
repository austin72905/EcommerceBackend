using EcommerceBackend.Interfaces.Repositorys;
using EcommerceBackend.Models;

namespace EcommerceBackend.Repositorys
{
    public class OrderRepostory : IOrderRepostory
    {
        public List<OrderInfomation> GetOrdersByUserId(string userid)
        {
            var orders = new List<OrderInfomation>()
            {

            };
            return orders;
        }
    }
}
