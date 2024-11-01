using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories;

namespace Application.Services
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        public CartService(IProductRepository productRepository, IUserRepository userRepository, ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }

        public ServiceResult<ProductWithCountDTO> GetCartContent(int userid)
        {
            var productSelectionList = new List<ProductWithCountDTO>();

            var cartId = _cartRepository.GetCartByUserId(userid);
            //var cartItemList = _cartRepository.GetCartItemByCartId(userid);

            //foreach (var cartItem in cartItemList) 
            //{
            //   var product = _productRepository.GetProductById(cartItem.ProductId);

            //    productSelectionList.Add(new ProductWithCountDTO
            //    { 
            //        Product = product,
            //        Count =cartItem.Quantity
            //    });
            //}

            //return productSelectionList;
            throw new NotImplementedException();
        }


    }
}
