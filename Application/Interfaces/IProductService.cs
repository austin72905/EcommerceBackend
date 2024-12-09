
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IProductService
    {
        public Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetProducts(string? kind, string? tag, string? query=null);

        public Task<ServiceResult<List<ProductBasicDTO>>> GetProductsBasicInfo(string? kind, string? tag, string? query = null);

        public Task<ServiceResult<List<ProductDynamicDTO>>> GetProductsDynamicInfo(List<int> productIdList);

        public Task<ServiceResult<List<ProductDynamicDTO>>> GetProductsDynamicInfoForUser(List<int> productIdList, int userid);


        public Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetProductsForUser(int userid, string? kind, string? tag);
        

        public Task<ServiceResult<ProductWithFavoriteStatusDTO>> GetProductById(int productId);

        public Task<ServiceResult<ProductBasicDTO>> GetProductBasicInfoById(int productId);

        public  Task<ServiceResult<List<ProductDynamicDTO>>> GetProductDynamicInfoById(int productId);


        public  Task<ServiceResult<ProductWithFavoriteStatusDTO>> GetProductByIdForUser(int userid, int productId);
        

        public  Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetRecommendationProduct(int userid, int productId);

        public Task<ServiceResult<List<ProductBasicDTO>>> GetRecommendationProductBasicInfo(int userid, int productId);



        public Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetfavoriteList(int userid);
    }
}
