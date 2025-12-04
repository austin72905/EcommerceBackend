using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ICartRepository
    {
        public Task<Cart?> GetCartByUserId(int userId);
        public Task SaveChangesAsync();

        public Task AddAsync(Cart entity);

        /// <summary>
        /// 批次更新購物車項目（使用 ExecuteDelete + AddRange，效能更佳）
        /// 先刪除指定購物車的所有項目，然後批次插入新項目
        /// </summary>
        /// <param name="cartId">購物車 ID</param>
        /// <param name="newItems">新的購物車項目列表</param>
        public Task UpdateCartItemsBatchAsync(int cartId, IEnumerable<CartItem> newItems);

        /// <summary>
        /// 創建新購物車並批次插入項目（使用事務）
        /// </summary>
        /// <param name="userId">用戶 ID</param>
        /// <param name="items">購物車項目列表</param>
        /// <returns>創建的購物車</returns>
        public Task<Cart> CreateCartWithItemsAsync(int userId, IEnumerable<CartItem> items);
    }
}
