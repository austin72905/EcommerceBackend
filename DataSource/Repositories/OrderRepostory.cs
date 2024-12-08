using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

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
        /// <returns></returns>
        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userid)
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
                .Where(o => o.UserId == userid)
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

        public async Task GenerateOrder(Order order)
        {
            await _dbSet.AddAsync(order);
            await _context.SaveChangesAsync();

        }

        
    }
}
