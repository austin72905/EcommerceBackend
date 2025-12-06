using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        public  Task<IEnumerable<Product>> GetProductsByQuery(string keyword);

        public Task<IEnumerable<Product>> GetProductsBasicInfoByQuery(string keyword);

        public Task<IEnumerable<Product>> GetProductsByKind(string kind, string? query = null);

        public Task<IEnumerable<Product>> GetProductsBasicInfoByKind(string kind, string? query = null);

        public Task<IEnumerable<Product>> GetProductsByTag(string tag, string? query = null);

        public Task<IEnumerable<Product>> GetProductsBasicInfByTag(string tag, string? query = null);

        public Task<Product?> GetProductById(int productId);

        public Task<Product?> GetProductBasicInfoById(int productId);

        public Task<IEnumerable<Product>> GetRecommendationProduct(int userid, int productId);

        public  Task<IEnumerable<Product>> GetRecommendationProductBasicInfo(int userid, int productId);

        public Task<IEnumerable<Product>> GetfavoriteProducts(int userid);

        public Task<IEnumerable<ProductVariant>> GetProductVariants(IEnumerable<int> variantIds);

        public Task<IEnumerable<ProductVariant>> GetProductVariantsByProductId(int productId);

        public Task<IEnumerable<ProductVariant>> GetProductVariantsByProductIdList(IEnumerable<int> productIdList);

        /// <summary>
        /// 新增商品
        /// </summary>
        public Task<Product> AddProductAsync(Product product);

        /// <summary>
        /// 根據名稱查找或創建 Tag
        /// </summary>
        public Task<Tag> GetOrCreateTagAsync(string tagName);

        /// <summary>
        /// 根據名稱查找或創建 Kind
        /// </summary>
        public Task<Kind> GetOrCreateKindAsync(string kindName);

        /// <summary>
        /// 根據 SizeValue 查找或創建 Size
        /// </summary>
        public Task<Size> GetOrCreateSizeAsync(string sizeValue);

        #region 分頁查詢方法

        /// <summary>
        /// 根據 Kind 分頁查詢商品（完整資訊）
        /// </summary>
        public Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsByKindPagedAsync(string kind, string? query, int page, int pageSize);

        /// <summary>
        /// 根據 Tag 分頁查詢商品（完整資訊）
        /// </summary>
        public Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsByTagPagedAsync(string tag, string? query, int page, int pageSize);

        /// <summary>
        /// 根據關鍵字分頁查詢商品（完整資訊）
        /// </summary>
        public Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsByQueryPagedAsync(string keyword, int page, int pageSize);

        /// <summary>
        /// 根據 Kind 分頁查詢商品（基本資訊）
        /// </summary>
        public Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsBasicInfoByKindPagedAsync(string kind, string? query, int page, int pageSize);

        /// <summary>
        /// 根據 Tag 分頁查詢商品（基本資訊）
        /// </summary>
        public Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsBasicInfoByTagPagedAsync(string tag, string? query, int page, int pageSize);

        /// <summary>
        /// 根據關鍵字分頁查詢商品（基本資訊）
        /// </summary>
        public Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsBasicInfoByQueryPagedAsync(string keyword, int page, int pageSize);

        #endregion

        /// <summary>
        /// 批量更新商品變體庫存（設置為指定值）
        /// </summary>
        /// <param name="variantStocks">商品變體 ID 與庫存數量的對應關係</param>
        /// <returns>更新結果</returns>
        public Task UpdateProductVariantStocksAsync(Dictionary<int, int> variantStocks);

        /// <summary>
        /// 批量扣除商品變體庫存（直接在 SQL 中計算，減少查詢）
        /// </summary>
        /// <param name="variantQuantities">商品變體 ID 與扣除數量的對應關係</param>
        /// <returns>更新結果</returns>
        public Task DeductProductVariantStocksAsync(Dictionary<int, int> variantQuantities);

    }
}
