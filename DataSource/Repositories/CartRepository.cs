using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace DataSource.Repositories
{
    public class CartRepository: ICartRepository
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
