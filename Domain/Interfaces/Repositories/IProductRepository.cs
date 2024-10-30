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
        public IEnumerable<Product> GetProductsByKind(string kind);

        public IEnumerable<Product> GetProductsByTag(string tag);

        public Product? GetProductById(int productId);

    }
}
