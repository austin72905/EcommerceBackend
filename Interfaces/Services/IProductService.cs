using EcommerceBackend.Models;
using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Interfaces.Services
{
    public interface IProductService
    {
        public ServiceResult<ProductListResponse>  GetProducts(string? userid,string kind,string tag);

        public ServiceResult<ProductResponse> GetProductById(string? userid, int productId);
        public ServiceResult<List<ProductInfomationDTO>> GetProductsByKind(string kind);

        public ServiceResult<List<ProductInfomationDTO>> GetProductsByTag(string tag);

        public ServiceResult<List<ProductInfomationDTO>> GetRecommendationProduct(string? userid, int productId);
    }
}
