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
    }
}
