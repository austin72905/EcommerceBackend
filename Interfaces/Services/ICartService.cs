using EcommerceBackend.Models;

namespace EcommerceBackend.Interfaces.Services
{
    public interface ICartService
    {
        public List<ProductSelection> GetCartContent(int userid);
    }
}
