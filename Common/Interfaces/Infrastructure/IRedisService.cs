using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Infrastructure
{
    public interface IRedisService
    {
        public Task SetUserInfoAsync(string sessonId, string userInfo);

        public Task DelUserInfoAsync(string sessonId);

        public Task<string?> GetUserInfoAsync(string sessonId);

        public Task SetWrongPasswordTimeAsync(string username, bool keepTtl = false);

        public  Task LockUserAsync(string username);
        public  Task<int?> GetWrongPasswordTimeAsync(string username);

        // 商品庫存相關方法
        public Task SetProductStockAsync(int variantId, int stock);
        public Task<int?> GetProductStockAsync(int variantId);

        /// <summary>
        /// 查詢多個variant 的 庫存
        /// </summary>
        /// <param name="variantIds"></param>
        /// <returns></returns>
        public Task<Dictionary<int, int>> GetProductStocksAsync(int[] variantIds);
        public Task SetProductStocksAsync(Dictionary<string, int> stockData);

        /// <summary>
        /// 獲取所有product variant 的 庫存
        /// </summary>
        /// <returns></returns>
        public Task<Dictionary<string, int>> GetAllProductStocksAsync();

        /// <summary>
        /// 檢查庫存，並將庫存預先扣除
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="variantStock"></param>
        /// <returns></returns>
        public Task<object> CheckAndHoldStockAsync(string orderId,Dictionary<int,int> variantStock);

        /// <summary>
        /// 回補庫存到redis
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task<object?> RollbackStockAsync(string recordcode);


        /// <summary>
        /// 檢查訊息是否已處理（冪等性檢查）
        /// </summary>
        /// <param name="messageKey">訊息唯一鍵</param>
        /// <returns>true 表示已處理，false 表示未處理</returns>
        public Task<bool> IsMessageProcessedAsync(string messageKey);

        /// <summary>
        /// 標記訊息為已處理（冪等性處理）
        /// </summary>
        /// <param name="messageKey">訊息唯一鍵</param>
        /// <param name="ttl">過期時間（預設 24 小時）</param>
        public Task MarkMessageAsProcessedAsync(string messageKey, TimeSpan? ttl = null);

        /// <summary>
        /// api 限流 token bucket ， 每個用戶都有自己的token bucket，是針對單個用戶的限流
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public Task<bool> IsRateLimitExceededAsync(string userId, string apiKey);

        #region 商品緩存相關方法

        /// <summary>
        /// 緩存單個商品基本資訊
        /// </summary>
        /// <param name="productId">商品 ID</param>
        /// <param name="productJson">商品 JSON 字串</param>
        /// <param name="ttl">過期時間（預設 4 小時）</param>
        Task SetProductBasicInfoAsync(int productId, string productJson, TimeSpan? ttl = null);

        /// <summary>
        /// 獲取單個商品基本資訊緩存
        /// </summary>
        /// <param name="productId">商品 ID</param>
        /// <returns>商品 JSON 字串，若無緩存則為 null</returns>
        Task<string?> GetProductBasicInfoAsync(int productId);

        /// <summary>
        /// 批量緩存商品基本資訊
        /// </summary>
        /// <param name="products">商品 ID 與 JSON 字串的字典</param>
        /// <param name="ttl">過期時間（預設 4 小時）</param>
        Task SetProductBasicInfoBatchAsync(Dictionary<int, string> products, TimeSpan? ttl = null);

        /// <summary>
        /// 批量獲取商品基本資訊緩存
        /// </summary>
        /// <param name="productIds">商品 ID 列表</param>
        /// <returns>商品 ID 與 JSON 字串的字典</returns>
        Task<Dictionary<int, string>> GetProductBasicInfoBatchAsync(IEnumerable<int> productIds);

        /// <summary>
        /// 緩存商品列表（按分類）
        /// </summary>
        /// <param name="kind">商品種類</param>
        /// <param name="tag">商品標籤</param>
        /// <param name="query">搜尋關鍵字</param>
        /// <param name="productsJson">商品列表 JSON 字串</param>
        /// <param name="ttl">過期時間（預設 10 分鐘）</param>
        Task SetProductListCacheAsync(string? kind, string? tag, string? query, string productsJson, TimeSpan? ttl = null);

        /// <summary>
        /// 獲取商品列表緩存（按分類）
        /// </summary>
        /// <param name="kind">商品種類</param>
        /// <param name="tag">商品標籤</param>
        /// <param name="query">搜尋關鍵字</param>
        /// <returns>商品列表 JSON 字串，若無緩存則為 null</returns>
        Task<string?> GetProductListCacheAsync(string? kind, string? tag, string? query);

        /// <summary>
        /// 清除單個商品緩存
        /// </summary>
        /// <param name="productId">商品 ID</param>
        Task InvalidateProductCacheAsync(int productId);

        /// <summary>
        /// 清除所有商品列表緩存（商品變更時使用）
        /// </summary>
        Task InvalidateAllProductListCacheAsync();

        /// <summary>
        /// 緩存分頁商品列表（基本資訊）
        /// </summary>
        /// <param name="kind">商品種類</param>
        /// <param name="tag">商品標籤</param>
        /// <param name="query">搜尋關鍵字</param>
        /// <param name="page">頁碼</param>
        /// <param name="pageSize">每頁數量</param>
        /// <param name="productsJson">商品列表 JSON 字串</param>
        /// <param name="ttl">過期時間（預設 10 分鐘）</param>
        Task SetProductListPagedCacheAsync(string? kind, string? tag, string? query, int page, int pageSize, string productsJson, TimeSpan? ttl = null);

        /// <summary>
        /// 獲取分頁商品列表緩存（基本資訊）
        /// </summary>
        /// <param name="kind">商品種類</param>
        /// <param name="tag">商品標籤</param>
        /// <param name="query">搜尋關鍵字</param>
        /// <param name="page">頁碼</param>
        /// <param name="pageSize">每頁數量</param>
        /// <returns>商品列表 JSON 字串，若無緩存則為 null</returns>
        Task<string?> GetProductListPagedCacheAsync(string? kind, string? tag, string? query, int page, int pageSize);

        /// <summary>
        /// 緩存商品動態資訊（變體、價格、折扣等）
        /// </summary>
        /// <param name="productId">商品 ID</param>
        /// <param name="dynamicInfoJson">動態資訊 JSON 字串</param>
        /// <param name="ttl">過期時間（預設 10 分鐘）</param>
        Task SetProductDynamicInfoCacheAsync(int productId, string dynamicInfoJson, TimeSpan? ttl = null);

        /// <summary>
        /// 獲取商品動態資訊緩存（變體、價格、折扣等）
        /// </summary>
        /// <param name="productId">商品 ID</param>
        /// <returns>動態資訊 JSON 字串，若無緩存則為 null</returns>
        Task<string?> GetProductDynamicInfoCacheAsync(int productId);

        /// <summary>
        /// 緩存分頁商品列表（完整資訊，包含變體和折扣）
        /// </summary>
        /// <param name="kind">商品種類</param>
        /// <param name="tag">商品標籤</param>
        /// <param name="query">搜尋關鍵字</param>
        /// <param name="page">頁碼</param>
        /// <param name="pageSize">每頁數量</param>
        /// <param name="productsJson">商品列表 JSON 字串</param>
        /// <param name="ttl">過期時間（預設 10 分鐘）</param>
        Task SetProductListFullPagedCacheAsync(string? kind, string? tag, string? query, int page, int pageSize, string productsJson, TimeSpan? ttl = null);

        /// <summary>
        /// 獲取分頁商品列表緩存（完整資訊，包含變體和折扣）
        /// </summary>
        /// <param name="kind">商品種類</param>
        /// <param name="tag">商品標籤</param>
        /// <param name="query">搜尋關鍵字</param>
        /// <param name="page">頁碼</param>
        /// <param name="pageSize">每頁數量</param>
        /// <returns>商品列表 JSON 字串，若無緩存則為 null</returns>
        Task<string?> GetProductListFullPagedCacheAsync(string? kind, string? tag, string? query, int page, int pageSize);

        #endregion

        #region 分散式鎖相關方法

        /// <summary>
        /// 嘗試獲取分散式鎖
        /// </summary>
        /// <param name="lockKey">鎖的鍵</param>
        /// <param name="lockValue">鎖的值（用於安全釋放，建議使用 GUID）</param>
        /// <param name="expiry">鎖的過期時間（預設 30 秒）</param>
        /// <returns>true 表示成功獲取鎖，false 表示鎖已被其他進程持有</returns>
        Task<bool> TryAcquireLockAsync(string lockKey, string lockValue, TimeSpan? expiry = null);

        /// <summary>
        /// 釋放分散式鎖
        /// </summary>
        /// <param name="lockKey">鎖的鍵</param>
        /// <param name="lockValue">鎖的值（必須與獲取時的值相同才能釋放）</param>
        /// <returns>true 表示成功釋放，false 表示鎖不存在或值不匹配</returns>
        Task<bool> ReleaseLockAsync(string lockKey, string lockValue);

        #endregion

        #region 通用緩存方法

        /// <summary>
        /// 設置緩存值
        /// </summary>
        /// <param name="key">緩存鍵</param>
        /// <param name="value">緩存值</param>
        /// <param name="ttl">過期時間（預設 1 小時）</param>
        Task SetCacheAsync(string key, string value, TimeSpan? ttl = null);

        /// <summary>
        /// 獲取緩存值
        /// </summary>
        /// <param name="key">緩存鍵</param>
        /// <returns>緩存值，若無緩存則為 null</returns>
        Task<string?> GetCacheAsync(string key);

        #endregion
    }
}
