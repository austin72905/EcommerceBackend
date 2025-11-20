namespace Domain.Entities
{
    /// <summary>
    /// 購物車項目實體
    /// </summary>
    public class CartItem
    {
        // 私有構造函數
        private CartItem() { }

        // 工廠方法：創建購物車項目
        public static CartItem Create(int productVariantId, int quantity, ProductVariant productVariant = null)
        {
            if (productVariantId <= 0)
                throw new ArgumentException("商品變體 ID 必須大於 0", nameof(productVariantId));

            if (quantity <= 0)
                throw new ArgumentException("數量必須大於 0", nameof(quantity));

            return new CartItem
            {
                ProductVariantId = productVariantId,
                Quantity = quantity,
                ProductVariant = productVariant
            };
        }

        // 只讀屬性或私有 setter
        public int Id { get; private set; }
        public int CartId { get; private set; }
        public int ProductVariantId { get; private set; }
        public int Quantity { get; private set; }

        // 導航屬性
        public Cart Cart { get; private set; }
        public ProductVariant ProductVariant { get; private set; }

        // ============ 業務邏輯方法 ============

        /// <summary>
        /// 更新數量
        /// </summary>
        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("數量必須大於 0", nameof(newQuantity));

            Quantity = newQuantity;
        }

        /// <summary>
        /// 增加數量
        /// </summary>
        public void IncreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("增加數量必須大於 0", nameof(amount));

            Quantity += amount;
        }

        /// <summary>
        /// 減少數量
        /// </summary>
        public void DecreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("減少數量必須大於 0", nameof(amount));

            if (Quantity - amount < 0)
                throw new InvalidOperationException("數量不能小於 0");

            Quantity -= amount;
        }
    }
}
