
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IProductService
    {
        public ServiceResult<ProductListResponse>  GetProducts(int userid,string kind,string tag);

        public ServiceResult<ProductResponse> GetProductById(int userid, int productId);
        public ServiceResult<List<ProductInfomationDTO>> GetProductsByKind(string kind);

        public ServiceResult<List<ProductInfomationDTO>> GetProductsByTag(string tag);

        public ServiceResult<List<ProductInfomationDTO>> GetRecommendationProduct(int userid, int productId);
    }
}
