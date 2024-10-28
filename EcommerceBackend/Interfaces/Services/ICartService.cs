using EcommerceBackend.Models;
using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Interfaces.Services
{
    public interface ICartService
    {
        public List<ProductWithCountDTO> GetCartContent(int userid);
    }
}
