
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

        /// <summary>
        /// 獲取完整的商品資訊（包含基本資訊和動態資訊）
        /// </summary>
        public Task<ServiceResult<ProductCompleteDTO>> GetProductCompleteInfoById(int productId);

        /// <summary>
        /// 獲取完整的商品資訊（包含基本資訊和動態資訊）- 已登入用戶版本
        /// </summary>
        public Task<ServiceResult<ProductCompleteDTO>> GetProductCompleteInfoByIdForUser(int userId, int productId);


        public  Task<ServiceResult<ProductWithFavoriteStatusDTO>> GetProductByIdForUser(int userid, int productId);
        

        public  Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetRecommendationProduct(int userid, int productId);

        public Task<ServiceResult<List<ProductBasicDTO>>> GetRecommendationProductBasicInfo(int userid, int productId);



        public Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetfavoriteList(int userid);

        /// <summary>
        /// 新增商品
        /// </summary>
        public Task<ServiceResult<AddProductResponseDTO>> AddProduct(AddProductRequestDTO request);

        /// <summary>
        /// 分頁查詢商品列表（完整資訊）
        /// </summary>
        public Task<ServiceResult<PagedResponseDTO<ProductWithFavoriteStatusDTO>>> GetProductsPaged(string? kind, string? tag, string? query, int page, int pageSize);

        /// <summary>
        /// 分頁查詢商品列表（完整資訊）- 已登入用戶
        /// </summary>
        public Task<ServiceResult<PagedResponseDTO<ProductWithFavoriteStatusDTO>>> GetProductsPagedForUser(int userid, string? kind, string? tag, int page, int pageSize);

        /// <summary>
        /// 分頁查詢商品基本資訊列表
        /// </summary>
        public Task<ServiceResult<PagedResponseDTO<ProductBasicDTO>>> GetProductsBasicInfoPaged(string? kind, string? tag, string? query, int page, int pageSize);
    }
}
