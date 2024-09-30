using EcommerceBackend.Models;
using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Interfaces.Repositorys
{
    public interface IOrderRepostory
    {
        public List<OrderInfomationDTO> GetOrdersByUserId(string userid);
    }
}
