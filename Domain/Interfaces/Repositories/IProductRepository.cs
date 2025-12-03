using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        public  Task<IEnumerable<Product>> GetProductsByQuery(string keyword);

        public Task<IEnumerable<Product>> GetProductsBasicInfoByQuery(string keyword);

        public Task<IEnumerable<Product>> GetProductsByKind(string kind, string? query = null);

        public Task<IEnumerable<Product>> GetProductsBasicInfoByKind(string kind, string? query = null);

        public Task<IEnumerable<Product>> GetProductsByTag(string tag, string? query = null);

        public Task<IEnumerable<Product>> GetProductsBasicInfByTag(string tag, string? query = null);

        public Task<Product?> GetProductById(int productId);

        public Task<Product?> GetProductBasicInfoById(int productId);

        public Task<IEnumerable<Product>> GetRecommendationProduct(int userid, int productId);

        public  Task<IEnumerable<Product>> GetRecommendationProductBasicInfo(int userid, int productId);

        public Task<IEnumerable<Product>> GetfavoriteProducts(int userid);

        public Task<IEnumerable<ProductVariant>> GetProductVariants(IEnumerable<int> variantIds);

        public Task<IEnumerable<ProductVariant>> GetProductVariantsByProductId(int productId);

        public Task<IEnumerable<ProductVariant>> GetProductVariantsByProductIdList(IEnumerable<int> productIdList);

    }
}
