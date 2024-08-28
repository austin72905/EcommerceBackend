using EcommerceBackend.Interfaces.Repositorys;
using EcommerceBackend.Models;

namespace EcommerceBackend.Repositorys
{
    public class CartRepository: ICartRepository
    {
        public Cart GetCartByUserId(int userId)
        {
            return new Cart 
            {
                UserId = userId,
                CartId = 1,
                
            
            };
            throw new NotImplementedException();
        }

        public List<CartItem> GetCartItemByCartId(int cartId)
        {
            return new List<CartItem>
            {
                new CartItem
                {
                    ProductId="26790367",
                    CartId=cartId,
                    CartItemId=1,
                    Quantity=5,
                    SelectColor="黑",
                    SelectSize="標準"
                },
                new CartItem
                {
                    ProductId="26790368",
                    CartId=cartId,
                    CartItemId=3,
                    Quantity=2,
                    SelectColor="黑",
                    SelectSize="標準"
                },
                new CartItem
                {
                    ProductId="13790367",
                    CartId=cartId,
                    CartItemId=5,
                    Quantity=4,
                    SelectColor="黑",
                    SelectSize="標準"
                }
            };
            
        }

      
    }
}
