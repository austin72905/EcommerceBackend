using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataSource.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(EcommerceDBContext context, EcommerceReadOnlyDBContext? readContext = null) 
            : base(context, readContext)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByQuery(string keyword)
        {
            // 讀取操作使用讀取 DbContext（從庫）
            return await ReadContext.Products
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
            // 讀取操作使用讀取 DbContext（從庫）
            return await ReadContext.Products
                .AsNoTracking()
                .Where(p => p.Title.Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByKind(string kind, string? query = null)
        {
            // 讀取操作使用讀取 DbContext（從庫）
            var queryable = ReadContext.Products
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
            // 讀取操作使用讀取 DbContext（從庫）
            var queryable = ReadContext.Products
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
            // 讀取操作使用讀取 DbContext（從庫）
            var queryable = ReadContext.Products
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
            // 讀取操作使用讀取 DbContext（從庫）
            var queryable = ReadContext.Products
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
            // 讀取操作使用讀取 DbContext（從庫）
            return await ReadContext.Products
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
            // 讀取操作使用讀取 DbContext（從庫）
            return await ReadContext.Products
                .AsNoTracking()
                .Include(p => p.ProductImages)
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<ProductVariant>> GetProductVariantsByProductId(int productId)
        {
            // 讀取操作使用讀取 DbContext（從庫）
            return await ReadContext.ProductVariants
                .AsNoTracking()
                .Include(pv => pv.Size)
                .Include(pv => pv.ProductVariantDiscounts)
                    .ThenInclude(pvd => pvd.Discount)
                .Where(pv => pv.ProductId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetRecommendationProduct(int userid, int productId)
        {
            // 讀取操作使用讀取 DbContext（從庫）
            return await ReadContext.Products
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
            // 讀取操作使用讀取 DbContext（從庫）
            return await ReadContext.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetfavoriteProducts(int userid)
        {
            // 讀取操作使用讀取 DbContext（從庫）
            return await ReadContext.Products
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
            // 讀取操作使用讀取 DbContext（從庫）
            return await ReadContext.ProductVariants
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
            // 讀取操作使用讀取 DbContext（從庫）
            return await ReadContext.ProductVariants
                .AsNoTracking()
                .Include(pv => pv.Size)
                .Include(pv => pv.ProductVariantDiscounts)
                    .ThenInclude(pvd => pvd.Discount)
                .Where(pv => productIdList.Contains(pv.ProductId))
                .ToListAsync();
        }

       
    }
}
