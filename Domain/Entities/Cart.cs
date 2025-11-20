using Domain.Interfaces;

namespace Domain.Entities
{
    /// <summary>
    /// 購物車聚合根 - 富領域模型
    /// 
    /// 聚合邊界：
    /// - Cart（聚合根）
    /// - CartItem（聚合內實體）
    /// 
    /// 不變性（Invariants）：
    /// 1. 購物車必須屬於一個用戶（UserId > 0）
    /// 2. 購物車項目數量必須大於 0
    /// 3. 同一商品變體（ProductVariant）在購物車中只能有一個 CartItem（透過 AddItem 自動合併）
    /// 4. CartItem 的數量必須大於 0
    /// </summary>
    public class Cart : IAggregateRoot
    {
        // 私有構造函數
        private Cart()
        {
            CartItems = new List<CartItem>();
        }

        // 工廠方法：為用戶創建購物車
        public static Cart CreateForUser(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("用戶 ID 必須大於 0", nameof(userId));

            return new Cart
            {
                UserId = userId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        // 只讀屬性或私有 setter
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // 導航屬性
        public User User { get; private set; }
        public ICollection<CartItem> CartItems { get; private set; }

        // ============ 業務邏輯方法 ============

        /// <summary>
        /// 添加商品到購物車
        /// </summary>
        public void AddItem(ProductVariant productVariant, int quantity)
        {
            if (productVariant == null)
                throw new ArgumentNullException(nameof(productVariant));

            if (quantity <= 0)
                throw new ArgumentException("數量必須大於 0", nameof(quantity));

            // 檢查是否已存在相同商品
            var existingItem = CartItems.FirstOrDefault(ci => ci.ProductVariantId == productVariant.Id);

            if (existingItem != null)
            {
                // 已存在，更新數量
                existingItem.UpdateQuantity(existingItem.Quantity + quantity);
            }
            else
            {
                // 新增商品
                var cartItem = CartItem.Create(productVariant.Id, quantity, productVariant);
                CartItems.Add(cartItem);
            }

            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// 更新商品數量
        /// </summary>
        public void UpdateItemQuantity(int productVariantId, int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("數量必須大於 0", nameof(newQuantity));

            var item = CartItems.FirstOrDefault(ci => ci.ProductVariantId == productVariantId);
            
            if (item == null)
                throw new InvalidOperationException("購物車中不存在此商品");

            item.UpdateQuantity(newQuantity);
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// 移除商品
        /// </summary>
        public void RemoveItem(int productVariantId)
        {
            var item = CartItems.FirstOrDefault(ci => ci.ProductVariantId == productVariantId);
            
            if (item == null)
                throw new InvalidOperationException("購物車中不存在此商品");

            CartItems.Remove(item);
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// 清空購物車
        /// </summary>
        public void Clear()
        {
            CartItems.Clear();
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// 合併購物車商品（用於登入後合併前端購物車）
        /// </summary>
        public void MergeItems(List<CartItem> itemsToMerge, IEnumerable<ProductVariant> productVariants)
        {
            if (itemsToMerge == null || !itemsToMerge.Any())
                return;

            foreach (var itemToMerge in itemsToMerge)
            {
                var productVariant = productVariants.FirstOrDefault(pv => pv.Id == itemToMerge.ProductVariantId);
                
                if (productVariant == null)
                    continue;

                var existingItem = CartItems.FirstOrDefault(ci => ci.ProductVariantId == itemToMerge.ProductVariantId);

                if (existingItem != null)
                {
                    // 取較大的數量
                    var maxQuantity = Math.Max(existingItem.Quantity, itemToMerge.Quantity);
                    existingItem.UpdateQuantity(maxQuantity);
                }
                else
                {
                    // 新增商品
                    var newItem = CartItem.Create(itemToMerge.ProductVariantId, itemToMerge.Quantity, productVariant);
                    CartItems.Add(newItem);
                }
            }

            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// 重建購物車（清空後重新添加）
        /// </summary>
        public void Rebuild(List<CartItem> newItems, IEnumerable<ProductVariant> productVariants)
        {
            CartItems.Clear();

            foreach (var item in newItems)
            {
                var productVariant = productVariants.FirstOrDefault(pv => pv.Id == item.ProductVariantId);
                
                if (productVariant != null)
                {
                    var newItem = CartItem.Create(item.ProductVariantId, item.Quantity, productVariant);
                    CartItems.Add(newItem);
                }
            }

            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// 計算購物車總金額
        /// </summary>
        public int CalculateTotalAmount(Func<CartItem, int> getPriceForItem)
        {
            return CartItems.Sum(item => getPriceForItem(item) * item.Quantity);
        }

        /// <summary>
        /// 獲取購物車商品數量
        /// </summary>
        public int GetTotalItemCount()
        {
            return CartItems.Sum(ci => ci.Quantity);
        }
    }
}
