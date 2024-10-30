using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace DataSource.Repositories
{
    public class ProductRepository: IProductRepository
    {
        public IEnumerable<Product> GetProductsByKind(string kind)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByTag(string tag)
        {
            throw new NotImplementedException();
        }

        public Product? GetProductById(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
