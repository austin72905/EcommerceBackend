using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataSource.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(EcommerceDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByKind(string kind)
        {
            return await  _dbSet
                .Include(p => p.ProductKindTags)
                    .ThenInclude(pkt => pkt.Kind)
                .Where(p => p.ProductKindTags.Any(pkt => pkt.Kind.Name == kind))
                .ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetProductsByTag(string tag)
        {
            return await _dbSet
                .Include(p => p.ProductKindTags)
                    .ThenInclude(pkt => pkt.Kind)
                .Where(p => p.ProductKindTags.Any(pkt => pkt.Tag.Name == tag))
                .ToListAsync();
        }

        public async Task<Product?> GetProductById(int productId)
        {
            return await _dbSet
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();
        }
    }
}
