using DataSource.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Repositories
{
    public class CartRepository
    {
        public Cart GetCartByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartItem> GetCartItemByCartId(int cartId)
        {
            throw new NotImplementedException();
        }
    }
}
