namespace Application.DTOs
{
    /// <summary>
    /// 庫存檢查結果
    /// </summary>
    public class InventoryCheckResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 訂單編號
        /// </summary>
        public string OrderId { get; set; } = string.Empty;

        /// <summary>
        /// 失敗的商品變體 ID 列表（庫存不足時）
        /// </summary>
        public List<int>? FailedVariantIds { get; set; }
    }
}

