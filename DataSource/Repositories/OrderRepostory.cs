using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataSource.Repositories
{
    public class OrderRepostory: Repository<Order>,IOrderRepostory
    {
        public OrderRepostory(EcommerceDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userid)
        {
            return await _dbSet
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                .Include(o => o.OrderSteps)
                .Where(o => o.UserId == userid)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order?> GetOrderInfoByUserId(int userid, string recordCode)
        {
            return await _dbSet
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductVariant)
                .Include(o => o.OrderSteps)
                .Include(o => o.Shipments)
                .Include(o => o.Address)
                .Where(o => o.UserId == userid && o.RecordCode == recordCode)
                .FirstOrDefaultAsync();
        }

        public async Task GenerateOrder()
        {
            await _dbSet.AddAsync(new Order());
            await _context.SaveChangesAsync();

        }
    }
}
