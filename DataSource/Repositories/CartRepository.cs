using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

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
            await _context.SaveChangesAsync();
        }


        public async Task AddAsync(Cart entity)
        {
            await _dbSet.AddAsync(entity);
        }

    }
}
