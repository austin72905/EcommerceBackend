using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataSource.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(EcommerceDBContext context, ILogger<ProductRepository> logger) : base(context)
        {
            _logger = logger;
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
            IQueryable<Product> queryable = _dbSet
                 .AsNoTracking()
                 .Include(p => p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                        .ThenInclude(pkd => pkd.Discount)
                .Include(p => p.ProductKinds)
                    .ThenInclude(pkt => pkt.Kind);

            // 如果 kind 不是 "all"，則按類別過濾
            if (!string.Equals(kind, "all", StringComparison.OrdinalIgnoreCase))
            {
                queryable = queryable.Where(p => p.ProductKinds.Any(pkt => pkt.Kind.Name == kind));
            }

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
            IQueryable<Product> queryable = _dbSet
                 .AsNoTracking()
                 .Include(p => p.ProductKinds)
                     .ThenInclude(pkt => pkt.Kind);

            // 如果 kind 不是 "all"，則按類別過濾
            if (!string.Equals(kind, "all", StringComparison.OrdinalIgnoreCase))
            {
                queryable = queryable.Where(p => p.ProductKinds.Any(pkt => pkt.Kind.Name == kind));
            }

            // 如果有額外的查詢條件，在資料庫層過濾
            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(p => p.Title.Contains(query));
            }

            return await queryable.ToListAsync();
        }



        public async Task<IEnumerable<Product>> GetProductsByTag(string tag, string? query = null)
        {
            IQueryable<Product> queryable = _dbSet
                .AsNoTracking()
                 .Include(p => p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                        .ThenInclude(pkd => pkd.Discount)
                .Include(p => p.ProductTags)
                    .ThenInclude(pkt => pkt.Tag);

            // 如果 tag 不是 "all"，則按標籤過濾
            if (!string.Equals(tag, "all", StringComparison.OrdinalIgnoreCase))
            {
                queryable = queryable.Where(p => p.ProductTags.Any(pkt => pkt.Tag.Name == tag));
            }

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
            IQueryable<Product> queryable = _dbSet
                .AsNoTracking()
               .Include(p => p.ProductTags)
                   .ThenInclude(pkt => pkt.Tag);

            // 如果 tag 不是 "all"，則按標籤過濾
            if (!string.Equals(tag, "all", StringComparison.OrdinalIgnoreCase))
            {
                queryable = queryable.Where(p => p.ProductTags.Any(pkt => pkt.Tag.Name == tag));
            }

            // 如果有額外的查詢條件，在資料庫層過濾
            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(p => p.Title.Contains(query));
            }

            return await queryable.ToListAsync();
        }

        public async Task<Product?> GetProductById(int productId)
        {
            // 註解：已改為使用順序查詢（GetProductBasicInfoById + GetProductDynamicInfoById）
            // 此方法保留以備向後兼容，但實際不再使用
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

        /// <summary>
        /// 新增商品
        /// </summary>
        public async Task<Product> AddProductAsync(Product product)
        {
            await _dbSet.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// 根據名稱查找或創建 Tag
        /// </summary>
        public async Task<Tag> GetOrCreateTagAsync(string tagName)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName);
            if (tag == null)
            {
                tag = new Tag { Name = tagName };
                await _context.Tags.AddAsync(tag);
                await _context.SaveChangesAsync();
            }
            return tag;
        }

        /// <summary>
        /// 根據名稱查找或創建 Kind
        /// </summary>
        public async Task<Kind> GetOrCreateKindAsync(string kindName)
        {
            var kind = await _context.Kinds.FirstOrDefaultAsync(k => k.Name == kindName);
            if (kind == null)
            {
                kind = new Kind { Name = kindName };
                await _context.Kinds.AddAsync(kind);
                await _context.SaveChangesAsync();
            }
            return kind;
        }

        /// <summary>
        /// 根據 SizeValue 查找或創建 Size
        /// </summary>
        public async Task<Size> GetOrCreateSizeAsync(string sizeValue)
        {
            var size = await _context.Sizes.FirstOrDefaultAsync(s => s.SizeValue == sizeValue);
            if (size == null)
            {
                size = new Size { SizeValue = sizeValue };
                await _context.Sizes.AddAsync(size);
                await _context.SaveChangesAsync();
            }
            return size;
        }

        #region 分頁查詢方法實現

        /// <summary>
        /// 根據 Kind 分頁查詢商品（完整資訊）
        /// </summary>
        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsByKindPagedAsync(string kind, string? query, int page, int pageSize)
        {
            IQueryable<Product> queryable = _dbSet
                 .AsNoTracking()
                 .Include(p => p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                        .ThenInclude(pkd => pkd.Discount)
                .Include(p => p.ProductKinds)
                    .ThenInclude(pkt => pkt.Kind);

            // 如果 kind 不是 "all"，則按類別過濾
            if (!string.Equals(kind, "all", StringComparison.OrdinalIgnoreCase))
            {
                queryable = queryable.Where(p => p.ProductKinds.Any(pkt => pkt.Kind.Name == kind));
            }

            // 如果有額外的查詢條件，在資料庫層過濾
            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(p => p.Title.Contains(query));
            }

            // 計算總數
            var totalCount = await queryable.CountAsync();

            // 分頁查詢
            var products = await queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        /// <summary>
        /// 根據 Tag 分頁查詢商品（完整資訊）
        /// </summary>
        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsByTagPagedAsync(string tag, string? query, int page, int pageSize)
        {
            IQueryable<Product> queryable = _dbSet
                .AsNoTracking()
                 .Include(p => p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                        .ThenInclude(pkd => pkd.Discount)
                .Include(p => p.ProductTags)
                    .ThenInclude(pkt => pkt.Tag);

            // 如果 tag 不是 "all"，則按標籤過濾
            if (!string.Equals(tag, "all", StringComparison.OrdinalIgnoreCase))
            {
                queryable = queryable.Where(p => p.ProductTags.Any(pkt => pkt.Tag.Name == tag));
            }

            // 如果有額外的查詢條件，在資料庫層過濾
            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(p => p.Title.Contains(query));
            }

            // 計算總數
            var totalCount = await queryable.CountAsync();

            // 分頁查詢
            var products = await queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        /// <summary>
        /// 根據關鍵字分頁查詢商品（完整資訊）
        /// </summary>
        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsByQueryPagedAsync(string keyword, int page, int pageSize)
        {
            var queryable = _dbSet
                 .AsNoTracking()
                 .Include(p => p.ProductVariants)
                    .ThenInclude(pkt => pkt.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantDiscounts)
                        .ThenInclude(pkd => pkd.Discount)
                .Where(p => p.Title.Contains(keyword));

            // 計算總數
            var totalCount = await queryable.CountAsync();

            // 分頁查詢
            var products = await queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        /// <summary>
        /// 根據 Kind 分頁查詢商品（基本資訊）
        /// </summary>
        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsBasicInfoByKindPagedAsync(string kind, string? query, int page, int pageSize)
        {
            IQueryable<Product> queryable = _dbSet
                 .AsNoTracking()
                 .Include(p => p.ProductImages)  // 基本資訊需要圖片
                 .Include(p => p.ProductKinds)
                     .ThenInclude(pkt => pkt.Kind);

            // 如果 kind 不是 "all"，則按類別過濾
            if (!string.Equals(kind, "all", StringComparison.OrdinalIgnoreCase))
            {
                queryable = queryable.Where(p => p.ProductKinds.Any(pkt => pkt.Kind.Name == kind));
            }

            // 如果有額外的查詢條件，在資料庫層過濾
            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(p => p.Title.Contains(query));
            }

            // 計算總數
            var totalCount = await queryable.CountAsync();

            // 分頁查詢
            var products = await queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        /// <summary>
        /// 根據 Tag 分頁查詢商品（基本資訊）
        /// </summary>
        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsBasicInfoByTagPagedAsync(string tag, string? query, int page, int pageSize)
        {
            IQueryable<Product> queryable = _dbSet
                .AsNoTracking()
                .Include(p => p.ProductImages)  // 基本資訊需要圖片
                .Include(p => p.ProductTags)
                   .ThenInclude(pkt => pkt.Tag);

            // 如果 tag 不是 "all"，則按標籤過濾
            if (!string.Equals(tag, "all", StringComparison.OrdinalIgnoreCase))
            {
                queryable = queryable.Where(p => p.ProductTags.Any(pkt => pkt.Tag.Name == tag));
            }

            // 如果有額外的查詢條件，在資料庫層過濾
            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(p => p.Title.Contains(query));
            }

            // 計算總數
            var totalCount = await queryable.CountAsync();

            // 分頁查詢
            var products = await queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        /// <summary>
        /// 根據關鍵字分頁查詢商品（基本資訊）
        /// </summary>
        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsBasicInfoByQueryPagedAsync(string keyword, int page, int pageSize)
        {
            var queryable = _dbSet
                .AsNoTracking()
                .Include(p => p.ProductImages)  // 基本資訊需要圖片
                .Where(p => p.Title.Contains(keyword));

            // 計算總數
            var totalCount = await queryable.CountAsync();

            // 分頁查詢
            var products = await queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        #endregion

        /// <summary>
        /// 批量更新商品變體庫存（使用原始 SQL 進行高效批量更新）
        /// </summary>
        public async Task UpdateProductVariantStocksAsync(Dictionary<int, int> variantStocks)
        {
            if (variantStocks == null || !variantStocks.Any())
            {
                return;
            }

            // 使用原始 SQL 進行批量更新，使用 CASE WHEN 語句
            // 這比逐個更新更高效，特別是在更新大量記錄時
            // 生成的 SQL 類似：
            // UPDATE "ProductVariants"
            // SET "Stock" = CASE 
            //     WHEN "Id" = {0} THEN {1}
            //     WHEN "Id" = {2} THEN {3}
            //     ...
            // END
            // WHERE "Id" IN ({4}, {5}, ...)
            
            // 構建 CASE WHEN 語句和參數列表
            var caseWhenParts = new List<string>();
            var parameters = new List<object>();
            var paramIndex = 0;

            // 構建 CASE WHEN 子句
            foreach (var item in variantStocks)
            {
                // 構建 WHEN ... THEN ... 語句，使用參數占位符
                caseWhenParts.Add($"WHEN \"Id\" = {{{paramIndex}}} THEN {{{paramIndex + 1}}}");
                parameters.Add(item.Key);   // variantId
                parameters.Add(item.Value); // stock
                paramIndex += 2;
            }

            // 構建 WHERE 子句的參數
            var variantIds = variantStocks.Keys.ToList();
            var wherePlaceholders = new List<string>();
            for (int i = 0; i < variantIds.Count; i++)
            {
                wherePlaceholders.Add($"{{{paramIndex}}}");
                parameters.Add(variantIds[i]);
                paramIndex++;
            }

            // 構建完整的 SQL 語句
            // 注意：這裡使用字符串插值，但需要確保 {paramIndex} 等占位符被正確處理
            // 我們先構建 caseWhenClause 和 whereClause，然後手動組裝 SQL
            var caseWhenClause = string.Join(" ", caseWhenParts);
            var whereClause = string.Join(", ", wherePlaceholders);
            
            // 手動構建 SQL，避免 string.Format 處理占位符
            var sqlBuilder = new System.Text.StringBuilder();
            sqlBuilder.AppendLine("UPDATE \"ProductVariants\"");
            sqlBuilder.AppendLine("SET \"Stock\" = CASE ");
            sqlBuilder.AppendLine(caseWhenClause);
            sqlBuilder.AppendLine("END");
            sqlBuilder.Append("WHERE \"Id\" IN (");
            sqlBuilder.Append(whereClause);
            sqlBuilder.Append(")");
            
            var sql = sqlBuilder.ToString();

            // 使用 ExecuteSqlRawAsync 執行參數化 SQL（EF Core 會自動處理參數化，防止 SQL 注入）
            // 參數會按照 {0}, {1}, {2}... 的順序對應到 parameters 數組
            await _context.Database.ExecuteSqlRawAsync(sql, parameters.ToArray());
        }

        /// <summary>
        /// 批量扣除商品變體庫存（直接在 SQL 中計算，減少查詢）
        /// </summary>
        public async Task DeductProductVariantStocksAsync(Dictionary<int, int> variantQuantities)
        {
            if (variantQuantities == null || !variantQuantities.Any())
            {
                return;
            }

            // 使用原始 SQL 進行批量扣除，直接在 SQL 中計算：Stock = Stock - Quantity
            // 這比先查詢再更新更高效，減少一次資料庫查詢
            // 生成的 SQL 類似：
            // UPDATE "ProductVariants"
            // SET "Stock" = "Stock" - CASE 
            //     WHEN "Id" = {0} THEN {1}
            //     WHEN "Id" = {2} THEN {3}
            //     ...
            // END
            // WHERE "Id" IN ({4}, {5}, ...)
            
            // 構建 CASE WHEN 語句和參數列表
            var caseWhenParts = new List<string>();
            var parameters = new List<object>();
            var paramIndex = 0;

            // 構建 CASE WHEN 子句（扣除數量）
            foreach (var item in variantQuantities)
            {
                // 構建 WHEN ... THEN ... 語句，使用參數占位符
                caseWhenParts.Add($"WHEN \"Id\" = {{{paramIndex}}} THEN {{{paramIndex + 1}}}");
                parameters.Add(item.Key);      // variantId
                parameters.Add(item.Value);     // quantity (扣除數量)
                paramIndex += 2;
            }

            // 構建 WHERE 子句的參數
            var variantIds = variantQuantities.Keys.ToList();
            var wherePlaceholders = new List<string>();
            for (int i = 0; i < variantIds.Count; i++)
            {
                wherePlaceholders.Add($"{{{paramIndex}}}");
                parameters.Add(variantIds[i]);
                paramIndex++;
            }

            // 構建完整的 SQL 語句
            var caseWhenClause = string.Join(" ", caseWhenParts);
            var whereClause = string.Join(", ", wherePlaceholders);
            
            // 手動構建 SQL，直接在 SQL 中計算：Stock = Stock - CASE WHEN ...
            var sqlBuilder = new System.Text.StringBuilder();
            sqlBuilder.AppendLine("UPDATE \"ProductVariants\"");
            sqlBuilder.AppendLine("SET \"Stock\" = GREATEST(0, \"Stock\" - CASE ");
            sqlBuilder.AppendLine(caseWhenClause);
            sqlBuilder.AppendLine("END)");
            sqlBuilder.Append("WHERE \"Id\" IN (");
            sqlBuilder.Append(whereClause);
            sqlBuilder.Append(")");
            
            var sql = sqlBuilder.ToString();

            // 使用 ExecuteSqlRawAsync 執行參數化 SQL（EF Core 會自動處理參數化，防止 SQL 注入）
            // GREATEST(0, ...) 確保庫存不會變成負數
            await _context.Database.ExecuteSqlRawAsync(sql, parameters.ToArray());
        }
       
    }
}
