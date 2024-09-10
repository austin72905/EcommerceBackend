using EcommerceBackend.Models;

namespace EcommerceBackend.Interfaces.Services
{
    public interface IProductService
    {
        public ServiceResult<ProductListResponse>  GetProducts(string? userid,string kind,string tag);

        public ServiceResult<ProductResponse> GetProductById(string? userid, string productId);
        public ServiceResult<List<ProductInfomation>> GetProductsByKind(string kind);

        public ServiceResult<List<ProductInfomation>> GetProductsByTag(string tag);

        public ServiceResult<List<ProductInfomation>> GetRecommendationProduct(string? userid,string productId);
    }
}
