namespace Domain.Entities
{
    /// <summary>
    /// 訂單商品實體
    /// </summary>
    public class OrderProduct
    {
        // 私有構造函數
        private OrderProduct() { }

        // 內部工廠方法（僅供 Order 使用）
        // 注意：不保存 productVariant 導航屬性，避免 EF Core 嘗試插入相關實體（導致主鍵衝突）
        internal static OrderProduct Create(int productVariantId, int productPrice, int count, ProductVariant productVariant = null)
        {
            if (productVariantId <= 0)
                throw new ArgumentException("商品變體 ID 必須大於 0", nameof(productVariantId));

            if (productPrice < 0)
                throw new ArgumentException("商品價格不能為負數", nameof(productPrice));

            if (count <= 0)
                throw new ArgumentException("商品數量必須大於 0", nameof(count));

            return new OrderProduct
            {
                ProductVariantId = productVariantId,
                ProductPrice = productPrice,
                Count = count,
                // 不設定 ProductVariant 導航屬性，EF Core 會根據 ProductVariantId 自動建立關聯
                // 避免將整個物件圖（包含 Product）插入資料庫
                // ProductVariant = productVariant // ❌ 不設定，避免 EF Core 嘗試插入 Product
            };
        }

        // 只讀屬性或私有 setter
        public int Id { get; private set; }
        public int OrderId { get; private set; }
        public int ProductVariantId { get; private set; }
        public int ProductPrice { get; private set; }
        public int Count { get; private set; }

        // 導航屬性
        public Order Order { get; private set; }
        public ProductVariant ProductVariant { get; private set; }

        // ============ 業務邏輯方法 ============

        /// <summary>
        /// 計算商品小計
        /// </summary>
        public int CalculateSubtotal()
        {
            return ProductPrice * Count;
        }

        /// <summary>
        /// 更新數量
        /// </summary>
        internal void UpdateCount(int newCount)
        {
            if (newCount <= 0)
                throw new ArgumentException("數量必須大於 0", nameof(newCount));

            Count = newCount;
        }
    }
}
