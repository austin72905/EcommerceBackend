using EcommerceBackend.Models;

namespace EcommerceBackend.Interfaces.Services
{
    public interface IProductService
    {
        public ProductListResponse GetProducts(string userid,string kind,string tag);

        public ProductResponse GetProductById(string userid, string productId);
        public List<ProductInfomation> GetProductsByKind(string kind);

        public List<ProductInfomation> GetProductsByTag(string tag);

        public List<ProductInfomation> GetRecommendationProduct(string? userid,string productId);
    }
}
