using DataSource.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Repositories
{
    public class ProductRepository
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
