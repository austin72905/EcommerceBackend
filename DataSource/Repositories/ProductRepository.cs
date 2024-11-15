using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
                 .Include(p => p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                        .ThenInclude(pkd => pkd.Discount)
                .Include(p => p.ProductKinds)
                    .ThenInclude(pkt => pkt.Kind)
                .Where(p => p.ProductKinds.Any(pkt => pkt.Kind.Name == kind))
                .ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetProductsByTag(string tag)
        {
            return await _dbSet
                 .Include(p => p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                        .ThenInclude(pkd => pkd.Discount)
                .Include(p => p.ProductTags)
                    .ThenInclude(pkt => pkt.Tag)
                .Where(p => p.ProductTags.Any(pkt => pkt.Tag.Name == tag))
                .ToListAsync();
        }

        public async Task<Product?> GetProductById(int productId)
        {
            return await _dbSet
                .Include(p=>p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                        .ThenInclude(pkd => pkd.Discount)
                .Include(p => p.ProductImages)
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetRecommendationProduct(int userid, int productId)
        {
            return await _dbSet
                .Include(p => p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                            .ThenInclude(pkd => pkd.Discount)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetfavoriteProducts(int userid)
        {
            return await _dbSet
                .Include(p => p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                        .ThenInclude(pkd => pkd.Discount)
                .Include(p => p.FavoriteProducts)
                .Where(p => p.FavoriteProducts.Any(fp => fp.UserId == userid))
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductVariant>> GetProductVariants(IEnumerable<int> variantIds)
        {
            return await _context.ProductVariants
                .Include(pv => pv.Product)
                .Include(pv => pv.Size)
                .Include(pv => pv.ProductVariantDiscounts)
                    .ThenInclude(pvd => pvd.Discount)
                .Where(pv => variantIds.Contains(pv.Id))
                .ToListAsync();
        }

     

    }
}
