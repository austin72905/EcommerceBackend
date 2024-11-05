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
        public Task<IEnumerable<Product>> GetProductsByKind(string kind);

        public Task<IEnumerable<Product>> GetProductsByTag(string tag);

        public Task<Product?> GetProductById(int productId);

        public Task<IEnumerable<Product>> GetRecommendationProduct(int userid, int productId);

        public Task<IEnumerable<Product>> GetfavoriteProducts(int userid);

    }
}
