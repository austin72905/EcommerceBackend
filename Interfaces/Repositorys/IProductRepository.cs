using EcommerceBackend.Models;
using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Interfaces.Repositorys
{
    public interface IProductRepository
    {
        public List<ProductInfomationDTO> GetProductsByKind(string kind);

        public List<ProductInfomationDTO> GetProductsByTag(string tag);

        public ProductInfomationDTO? GetProductById(int productId);
    }
}
