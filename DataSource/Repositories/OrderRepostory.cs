using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataSource.Repositories
{
    public class OrderRepostory : Repository<Order>, IOrderRepostory
    {
        public OrderRepostory(EcommerceDBContext context) : base(context)
        {
        }

        /// <summary>
        /// 獲取用戶的所有訂單
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="query">可選的查詢條件（用於過濾訂單）</param>
        /// <returns></returns>
        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userid, string? query = null)
        {
            var queryable = _dbSet
                .AsNoTracking()
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                           .ThenInclude(pv => pv.Size)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                           .ThenInclude(pv => pv.Product)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                           .ThenInclude(pv => pv.ProductVariantDiscounts)
                                .ThenInclude(pvd => pvd.Discount)
                .Include(o => o.OrderSteps)
                .Include(o => o.Shipments)
                .Where(o => o.UserId == userid);

            // 如果有查詢條件，在資料庫層過濾（避免載入所有訂單到記憶體）
            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(o => 
                    o.RecordCode.Contains(query) || 
                    o.OrderProducts.Any(op => op.ProductVariant.Product.Title.Contains(query))
                );
            }

            // 在資料庫層排序（使用 UpdatedAt 索引）
            return await queryable
                .OrderByDescending(o => o.UpdatedAt)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderInfoById(int orderId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                        .ThenInclude(pv => pv.Size)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                           .ThenInclude(pv => pv.Product)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                           .ThenInclude(pv => pv.ProductVariantDiscounts)
                                .ThenInclude(pvd => pvd.Discount)
                .Include(o => o.OrderSteps)
                .Include(o => o.Shipments)
                .Include(o => o.Address)
                .Where(o => o.Id==orderId)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 獲取訂單（帶追蹤，用於更新狀態）
        /// </summary>
        public async Task<Order?> GetOrderInfoByIdForUpdate(int orderId)
        {
            return await _dbSet
                .TagWith("GetOrderInfoByIdForUpdate")  // 添加查詢標記，用於日誌過濾
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                        .ThenInclude(pv => pv.Size)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                           .ThenInclude(pv => pv.Product)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                           .ThenInclude(pv => pv.ProductVariantDiscounts)
                                .ThenInclude(pvd => pvd.Discount)
                .Include(o => o.OrderSteps)
                .Include(o => o.Shipments)
                .Include(o => o.Address)
                .Where(o => o.Id==orderId)
                .FirstOrDefaultAsync();
        }

        public async Task<Order?> GetOrderInfoByUserId(int userid, string recordCode)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                        .ThenInclude(pv => pv.Size)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                           .ThenInclude(pv => pv.Product)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                           .ThenInclude(pv => pv.ProductVariantDiscounts)
                                .ThenInclude(pvd => pvd.Discount)
                .Include(o => o.OrderSteps)
                .Include(o => o.Shipments)
                .Include(o => o.Address)
                .Where(o => o.UserId == userid && o.RecordCode == recordCode)
                .FirstOrDefaultAsync();
        }

        public async Task<Order?> GetOrderInfoByRecordCode(string recordCode)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                        .ThenInclude(pv => pv.Size)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                           .ThenInclude(pv => pv.Product)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                           .ThenInclude(pv => pv.ProductVariantDiscounts)
                                .ThenInclude(pvd => pvd.Discount)
                .Include(o => o.OrderSteps)
                .Include(o => o.Shipments)
                .Include(o => o.Address)
                .Where(o => o.RecordCode == recordCode)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 獲取訂單（帶追蹤，用於更新狀態）
        /// </summary>
        public async Task<Order?> GetOrderInfoByRecordCodeForUpdate(string recordCode)
        {
            return await _dbSet
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                        .ThenInclude(pv => pv.Size)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                           .ThenInclude(pv => pv.Product)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                           .ThenInclude(pv => pv.ProductVariantDiscounts)
                                .ThenInclude(pvd => pvd.Discount)
                .Include(o => o.OrderSteps)
                .Include(o => o.Shipments)
                .Include(o => o.Address)
                .Where(o => o.RecordCode == recordCode)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 獲取訂單（僅載入 OrderProducts，用於庫存更新等輕量級操作）
        /// 相比 GetOrderInfoByRecordCodeForUpdate，此方法只載入必要的 OrderProducts，
        /// 大幅減少資料庫查詢負載和記憶體使用
        /// 使用 AsNoTracking 避免 EF Core 追蹤實體，提升性能
        /// </summary>
        public async Task<Order?> GetOrderWithProductsOnlyForUpdate(string recordCode)
        {
            return await _dbSet
                .AsNoTracking()  // 不追蹤實體，減少記憶體使用和性能開銷
                .Include(o => o.OrderProducts)  // 只載入 OrderProducts，不載入相關的 ProductVariant、Size 等
                .Where(o => o.RecordCode == recordCode)
                .FirstOrDefaultAsync();
        }

        public async Task GenerateOrder(Order order)
        {
            // 在添加訂單之前，先清理 ChangeTracker 中可能存在的相關實體
            // 這可以防止 EF Core 追蹤並嘗試插入已存在的實體
            DetachExistingEntities();
            
            // 添加訂單到追蹤器
            await _dbSet.AddAsync(order);
            
            // 在保存之前，確保所有相關實體（ProductVariant、Product 等）都被標記為已存在
            // 這可以防止在並發情況下出現主鍵衝突
            // 遍歷所有被追蹤的實體，將任何被誤標記為 Added 的 Product、ProductVariant 等實體標記為 Unchanged
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
        /// 分離 ChangeTracker 中已存在的實體，防止 EF Core 嘗試插入它們或重複追蹤
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


        public async Task UpdateOrderStatusAsync(string recordcode,int status)
        {
            await _dbSet
                    .Where(o => o.RecordCode == recordcode)
                    .ExecuteUpdateAsync(set => set
                    .SetProperty(prop => prop.Status, status)
                    .SetProperty(prop => prop.UpdatedAt, DateTime.UtcNow));
        }

        
    }
}
