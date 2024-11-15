
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IProductService
    {
        public Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetProducts(string kind, string tag);
        //public Task<ServiceResult<ProductListResponse>> GetProducts(string kind, string tag);

        public Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetProductsForUser(int userid, string kind, string tag);
        //public Task<ServiceResult<ProductListResponse>>  GetProductsForUser(int userid,string kind,string tag);

        public Task<ServiceResult<ProductWithFavoriteStatusDTO>> GetProductById(int productId);
        //public Task<ServiceResult<ProductResponse>> GetProductById(int productId);
        //public Task<ServiceResult<ProductResponse>> GetProductByIdForUser(int userid, int productId);

        public  Task<ServiceResult<ProductWithFavoriteStatusDTO>> GetProductByIdForUser(int userid, int productId);
        //public ServiceResult<List<ProductInfomationDTO>> GetProductsByKind(string kind);

        //public ServiceResult<List<ProductInfomationDTO>> GetProductsByTag(string tag);

        public  Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetRecommendationProduct(int userid, int productId);
        //public  Task<ServiceResult<ProductListResponse>> GetRecommendationProduct(int userid, int productId);

        public Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetfavoriteList(int userid);
    }
}
