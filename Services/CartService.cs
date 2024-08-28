using EcommerceBackend.Interfaces.Repositorys;
using EcommerceBackend.Interfaces.Services;
using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public class CartService: ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        public CartService(IProductRepository productRepository, IUserRepository userRepository, ICartRepository cartRepository) 
        {
            _productRepository= productRepository;
            _userRepository= userRepository;
            _cartRepository= cartRepository;
        }

        public List<ProductSelection> GetCartContent(int userid)
        {
            var productSelectionList = new List<ProductSelection>();

            var cartId=_cartRepository.GetCartByUserId(userid);
            var cartItemList = _cartRepository.GetCartItemByCartId(userid);

            foreach (var cartItem in cartItemList) 
            {
               var product = _productRepository.GetProductById(cartItem.ProductId);

                productSelectionList.Add(new ProductSelection 
                { 
                    Product = product,
                    SelectColor= cartItem.SelectColor,
                    SelectSize= cartItem.SelectSize,
                    Count =cartItem.Quantity
                });
            }

            return productSelectionList;
        }


    }
}
