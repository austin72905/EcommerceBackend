using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataSource.Repositories
{
    public class CartRepository: Repository<Cart>,ICartRepository
    {
        public CartRepository(EcommerceDBContext context, EcommerceReadOnlyDBContext? readContext = null) 
            : base(context, readContext)
        {
        }

        public async Task<Cart?> GetCartByUserId(int userId)
        {
            // 讀取操作使用讀取 DbContext（從庫）
            return await ReadContext.Carts
                .AsNoTracking()
                .Include(c => c.CartItems)
                    .ThenInclude(ct => ct.ProductVariant)
                        .ThenInclude(pv => pv.Product)
                .Include(c => c.CartItems)
                    .ThenInclude(ct => ct.ProductVariant)
                        .ThenInclude(pv => pv.Size)
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();

        }

        /// <summary>
        /// 批次更新購物車項目（使用 ExecuteDelete + AddRange，效能更佳）
        /// 先刪除指定購物車的所有項目，然後批次插入新項目
        /// </summary>
        /// <param name="cartId">購物車 ID</param>
        /// <param name="newItems">新的購物車項目列表</param>
        public async Task UpdateCartItemsBatchAsync(int cartId, IEnumerable<CartItem> newItems)
        {
            // 使用 ExecuteDeleteAsync 批次刪除舊的購物車項目（不需要追蹤實體）
            await _context.CartItems
                .Where(ci => ci.CartId == cartId)
                .ExecuteDeleteAsync();

            // 如果有新項目，批次插入
            if (newItems != null && newItems.Any())
            {
                // 創建新的 CartItem 實體（只包含必要的欄位，避免導航屬性追蹤問題）
                var itemsToAdd = newItems.Select(item => new CartItem_Internal
                {
                    CartId = cartId,
                    ProductVariantId = item.ProductVariantId,
                    Quantity = item.Quantity
                }).ToList();

                // 使用原生 SQL 批次插入（避免 ChangeTracker 追蹤）
                foreach (var item in itemsToAdd)
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "INSERT INTO \"CartItems\" (\"CartId\", \"ProductVariantId\", \"Quantity\") VALUES ({0}, {1}, {2})",
                        item.CartId, item.ProductVariantId, item.Quantity);
                }
            }

            // 更新購物車的 UpdatedAt
            await _context.Carts
                .Where(c => c.Id == cartId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c => c.UpdatedAt, DateTime.UtcNow));
        }

        /// <summary>
        /// 創建新購物車並批次插入項目（使用事務）
        /// </summary>
        /// <param name="userId">用戶 ID</param>
        /// <param name="items">購物車項目列表</param>
        /// <returns>創建的購物車</returns>
        public async Task<Cart> CreateCartWithItemsAsync(int userId, IEnumerable<CartItem> items)
        {
            // 使用原生 SQL 插入購物車並返回 ID
            var now = DateTime.UtcNow;
            
            // 插入購物車
            var cart = Cart.CreateForUser(userId);
            await _dbSet.AddAsync(cart);
            await _context.SaveChangesAsync();

            // 如果有項目，批次插入
            if (items != null && items.Any())
            {
                foreach (var item in items)
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "INSERT INTO \"CartItems\" (\"CartId\", \"ProductVariantId\", \"Quantity\") VALUES ({0}, {1}, {2})",
                        cart.Id, item.ProductVariantId, item.Quantity);
                }
            }

            return cart;
        }

        /// <summary>
        /// 簡化後的 SaveChangesAsync - 直接標記非購物車相關實體為 Unchanged
        /// </summary>
        public new async Task SaveChangesAsync()
        {
            // 簡化處理：只處理 Added 狀態的非購物車實體，標記為 Unchanged
            // 這樣可以避免 EF Core 嘗試插入已存在的相關實體
            var entriesToFix = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && 
                           !(e.Entity is Cart) && 
                           !(e.Entity is CartItem))
                .ToList();
            
            foreach (var entry in entriesToFix)
            {
                entry.State = EntityState.Unchanged;
            }
            
            await _context.SaveChangesAsync();
        }

        public new async Task AddAsync(Cart entity)
        {
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// 內部類別，用於批次插入時避免 ChangeTracker 追蹤
        /// </summary>
        private class CartItem_Internal
        {
            public int CartId { get; set; }
            public int ProductVariantId { get; set; }
            public int Quantity { get; set; }
        }

    }
}
