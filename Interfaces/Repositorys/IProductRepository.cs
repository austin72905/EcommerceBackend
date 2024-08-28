using EcommerceBackend.Models;

namespace EcommerceBackend.Interfaces.Repositorys
{
    public interface IProductRepository
    {
        public List<ProductInfomation> GetProductsByKind(string kind);

        public List<ProductInfomation> GetProductsByTag(string tag);

        public ProductInfomation? GetProductById(string productId);
    }
}
