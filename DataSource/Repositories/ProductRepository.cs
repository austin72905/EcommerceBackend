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

        public async Task<IEnumerable<Product>> GetProductsByQuery(string keyword)
        {
            return await _dbSet
                 .AsNoTracking()
                 .Include(p => p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                        .ThenInclude(pkd => pkd.Discount)
                .Where(p => p.Title.Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBasicInfoByQuery(string keyword)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(p => p.Title.Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByKind(string kind, string? query = null)
        {
            var queryable = _dbSet
                 .AsNoTracking()
                 .Include(p => p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                        .ThenInclude(pkd => pkd.Discount)
                .Include(p => p.ProductKinds)
                    .ThenInclude(pkt => pkt.Kind)
                .Where(p => p.ProductKinds.Any(pkt => pkt.Kind.Name == kind));

            // 如果有額外的查詢條件，在資料庫層過濾
            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(p => p.Title.Contains(query));
            }

            return await queryable.ToListAsync();
        }

        /// <summary>
        /// 返回商品基本資訊的列表
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="query">可選的額外查詢條件</param>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProductsBasicInfoByKind(string kind, string? query = null)
        {
            var queryable = _dbSet
                 .AsNoTracking()
                 .Include(p => p.ProductKinds)
                     .ThenInclude(pkt => pkt.Kind)
                 .Where(p => p.ProductKinds.Any(pkt => pkt.Kind.Name == kind));

            // 如果有額外的查詢條件，在資料庫層過濾
            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(p => p.Title.Contains(query));
            }

            return await queryable.ToListAsync();
        }



        public async Task<IEnumerable<Product>> GetProductsByTag(string tag, string? query = null)
        {
            var queryable = _dbSet
                .AsNoTracking()
                 .Include(p => p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                        .ThenInclude(pkd => pkd.Discount)
                .Include(p => p.ProductTags)
                    .ThenInclude(pkt => pkt.Tag)
                .Where(p => p.ProductTags.Any(pkt => pkt.Tag.Name == tag));

            // 如果有額外的查詢條件，在資料庫層過濾
            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(p => p.Title.Contains(query));
            }

            return await queryable.ToListAsync();
        }

        /// <summary>
        /// 返回商品基本資訊的列表
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="query">可選的額外查詢條件</param>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProductsBasicInfByTag(string tag, string? query = null)
        {
            var queryable = _dbSet
                .AsNoTracking()
               .Include(p => p.ProductTags)
                   .ThenInclude(pkt => pkt.Tag)
               .Where(p => p.ProductTags.Any(pkt => pkt.Tag.Name == tag));

            // 如果有額外的查詢條件，在資料庫層過濾
            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(p => p.Title.Contains(query));
            }

            return await queryable.ToListAsync();
        }

        public async Task<Product?> GetProductById(int productId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                        .ThenInclude(pkd => pkd.Discount)
                .Include(p => p.ProductImages)
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 返回商品基本資料
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<Product?> GetProductBasicInfoById(int productId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.ProductImages)
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<ProductVariant>> GetProductVariantsByProductId(int productId)
        {
            return await _context.ProductVariants
                .AsNoTracking()
                .Include(pv => pv.Size)
                .Include(pv => pv.ProductVariantDiscounts)
                    .ThenInclude(pvd => pvd.Discount)
                .Where(pv => pv.ProductId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetRecommendationProduct(int userid, int productId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                            .ThenInclude(pkd => pkd.Discount)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetRecommendationProductBasicInfo(int userid, int productId)
        {
            return await _dbSet
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetfavoriteProducts(int userid)
        {
            return await _dbSet
                .AsNoTracking()
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
                .AsNoTracking()
                .Include(pv => pv.Product)
                .Include(pv => pv.Size)
                .Include(pv => pv.ProductVariantDiscounts)
                    .ThenInclude(pvd => pvd.Discount)
                .Where(pv => variantIds.Contains(pv.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductVariant>> GetProductVariantsByProductIdList(IEnumerable<int> productIdList)
        {
            return await _context.ProductVariants
                .AsNoTracking()
                .Include(pv => pv.Size)
                .Include(pv => pv.ProductVariantDiscounts)
                    .ThenInclude(pvd => pvd.Discount)
                .Where(pv => productIdList.Contains(pv.ProductId))
                .ToListAsync();
        }

       
    }
}
