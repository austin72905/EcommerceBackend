using EcommerceBackend.Models;

namespace EcommerceBackend.Interfaces.Repositorys
{
    public interface ICartRepository
    {
       public Cart GetCartByUserId(int userId);

        public List<CartItem> GetCartItemByCartId(int cartId);
    }
}
