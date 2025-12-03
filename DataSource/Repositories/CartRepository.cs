using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataSource.Repositories
{
    public class CartRepository: Repository<Cart>,ICartRepository
    {
        public CartRepository(EcommerceDBContext context) : base(context)
        {
        }

        public async Task<Cart?> GetCartByUserId(int userId)
        {
            return await _dbSet
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



        public async Task SaveChangesAsync()
        {
            // 在保存之前，先處理重複追蹤的問題
            HandleDuplicateTracking();
            
            // 在保存之前，先分離所有相關實體，防止重複追蹤
            DetachExistingEntities();
            
            // 在保存之前，確保所有相關實體（ProductVariant、Product 等）都被標記為已存在
            // 這可以防止在並發情況下出現主鍵衝突
            var trackedEntities = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .ToList();
            
            foreach (var entry in trackedEntities)
            {
                // 如果是 Product 實體，標記為已存在（不應插入新產品）
                if (entry.Entity is Product)
                {
                    entry.State = EntityState.Unchanged;
                }
                // 如果是 ProductVariant 實體，標記為已存在（不應插入新變體）
                else if (entry.Entity is ProductVariant)
                {
                    entry.State = EntityState.Unchanged;
                }
                // 如果是 ProductVariantDiscount 實體，標記為已存在（不應插入新折扣關聯）
                else if (entry.Entity is ProductVariantDiscount)
                {
                    entry.State = EntityState.Unchanged;
                }
                // 如果是 Size 實體，標記為已存在（不應插入新尺寸）
                else if (entry.Entity is Size)
                {
                    entry.State = EntityState.Unchanged;
                }
                // 如果是 Discount 實體，標記為已存在（不應插入新折扣）
                else if (entry.Entity is Discount)
                {
                    entry.State = EntityState.Unchanged;
                }
            }
            
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 處理重複追蹤問題：如果發現相同 ID 的實體被多次追蹤，分離重複的實體
        /// </summary>
        private void HandleDuplicateTracking()
        {
            // 按實體類型和 ID 分組，找出重複追蹤的實體
            var productGroups = _context.ChangeTracker.Entries<Product>()
                .Where(e => e.State != EntityState.Detached)
                .GroupBy(e => e.Entity.Id)
                .Where(g => g.Count() > 1)
                .ToList();

            foreach (var group in productGroups)
            {
                // 保留第一個，分離其他的
                var entries = group.ToList();
                for (int i = 1; i < entries.Count; i++)
                {
                    entries[i].State = EntityState.Detached;
                }
            }

            var sizeGroups = _context.ChangeTracker.Entries<Size>()
                .Where(e => e.State != EntityState.Detached)
                .GroupBy(e => e.Entity.Id)
                .Where(g => g.Count() > 1)
                .ToList();

            foreach (var group in sizeGroups)
            {
                var entries = group.ToList();
                for (int i = 1; i < entries.Count; i++)
                {
                    entries[i].State = EntityState.Detached;
                }
            }

            var variantGroups = _context.ChangeTracker.Entries<ProductVariant>()
                .Where(e => e.State != EntityState.Detached)
                .GroupBy(e => e.Entity.Id)
                .Where(g => g.Count() > 1)
                .ToList();

            foreach (var group in variantGroups)
            {
                var entries = group.ToList();
                for (int i = 1; i < entries.Count; i++)
                {
                    entries[i].State = EntityState.Detached;
                }
            }
        }

        /// <summary>
        /// 分離 ChangeTracker 中已存在的實體，防止 EF Core 重複追蹤
        /// </summary>
        private void DetachExistingEntities()
        {
            // 分離所有相關實體，防止重複追蹤
            var entriesToDetach = _context.ChangeTracker.Entries()
                .Where(e => (e.Entity is Product || 
                            e.Entity is ProductVariant || 
                            e.Entity is ProductVariantDiscount ||
                            e.Entity is Size || 
                            e.Entity is Discount) && 
                           e.State != EntityState.Detached)
                .ToList();

            foreach (var entry in entriesToDetach)
            {
                entry.State = EntityState.Detached;
            }
        }


        public async Task AddAsync(Cart entity)
        {
            // 在附加購物車之前，先清理導航屬性，防止重複追蹤
            // 只保留外鍵 ID，讓 EF Core 根據外鍵建立關聯，而不追蹤整個物件圖
            ClearNavigationProperties(entity);
            
            // 在附加購物車之前，先分離所有相關實體，防止重複追蹤
            DetachExistingEntities();
            
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// 清理購物車中所有 CartItem 的導航屬性，防止 EF Core 追蹤整個物件圖
        /// 注意：由於 CartItem.ProductVariant 的 setter 是 private，這個方法可能無法直接清理
        /// 但我們會在 HandleDuplicateTracking 中處理重複追蹤的問題
        /// </summary>
        private void ClearNavigationProperties(Cart cart)
        {
            // 由於導航屬性的 setter 是 private，我們無法直接清理
            // 但這沒關係，因為我們會在 SaveChangesAsync 中通過 HandleDuplicateTracking 處理重複追蹤的問題
            // 這個方法保留作為佔位符，以便將來如果需要可以實現
        }

    }
}
