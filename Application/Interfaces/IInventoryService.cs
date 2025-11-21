using Application.DTOs;

namespace Application.Interfaces
{
    /// <summary>
    /// 庫存管理服務介面
    /// </summary>
    public interface IInventoryService
    {
        /// <summary>
        /// 檢查並預扣庫存
        /// </summary>
        /// <param name="orderId">訂單編號</param>
        /// <param name="variantInventory">商品變體與數量的對應關係</param>
        /// <returns>庫存檢查結果</returns>
        Task<ServiceResult<InventoryCheckResult>> CheckAndHoldInventoryAsync(
            string orderId, 
            Dictionary<int, int> variantInventory);

        /// <summary>
        /// 確認庫存（付款成功後，正式扣除庫存）
        /// </summary>
        /// <param name="orderId">訂單編號</param>
        /// <returns>確認結果</returns>
        Task<ServiceResult<bool>> ConfirmInventoryAsync(string orderId);

        /// <summary>
        /// 回滾庫存（訂單取消或付款失敗時，將預扣的庫存回補）
        /// </summary>
        /// <param name="orderId">訂單編號</param>
        /// <returns>回滾結果</returns>
        Task<ServiceResult<bool>> RollbackInventoryAsync(string orderId);

        /// <summary>
        /// 獲取單個商品變體的庫存
        /// </summary>
        /// <param name="variantId">商品變體 ID</param>
        /// <returns>庫存數量</returns>
        Task<ServiceResult<int?>> GetProductInventoryAsync(int variantId);

        /// <summary>
        /// 批量獲取商品變體的庫存
        /// </summary>
        /// <param name="variantIds">商品變體 ID 陣列</param>
        /// <returns>商品變體與庫存的對應關係</returns>
        Task<ServiceResult<Dictionary<int, int>>> GetProductInventoriesAsync(int[] variantIds);
    }
}

