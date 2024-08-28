using EcommerceBackend.Controllers;
using EcommerceBackend.Interfaces.Repositorys;
using EcommerceBackend.Interfaces.Services;
using EcommerceBackend.Models;
using EcommerceBackend.Repositorys;

namespace EcommerceBackend.Services
{
    public class ProductService: IProductService
    {
        public readonly IProductRepository _repository;
        private readonly IUserRepository _userRepository;
        public ProductService(IProductRepository repository, IUserRepository userRepository) 
        {
            _repository = repository;
            _userRepository= userRepository;
        }

        public ProductResponse GetProducts(string userid, string kind, string tag)
        {
            List<ProductInfomation> products = new List<ProductInfomation>();

            if (string.IsNullOrEmpty(tag) && string.IsNullOrEmpty(kind))
            {
                return new ProductResponse { Products= products.Select(p => new ProductWithFavoriteStatus { product = p }) };
            }


            if (!string.IsNullOrEmpty(tag))
            {
                products = _repository.GetProductsByTag(tag);

            }

            if (!string.IsNullOrEmpty(kind))
            {
                products = _repository.GetProductsByKind(kind);

            }

            //有登陸的情況
            if (!string.IsNullOrEmpty(userid))
            {
                var favoriteProductIds = _userRepository.GetFavoriteProductIdsByUser(userid);
                var productWithFavorite = products.Select(p => new ProductWithFavoriteStatus 
                { 
                    product = p,
                    IsFavorite= favoriteProductIds.Contains(p.ProductId) 
                });

                return new ProductResponse { Products = productWithFavorite };
            }
            else
            {
                var productWithFavorite = products.Select(p => new ProductWithFavoriteStatus { product = p });

                return new ProductResponse { Products = productWithFavorite };
            }


        }

        public List<ProductInfomation> GetProductsByKind(string kind)
        {
            var products = _repository.GetProductsByKind(kind);

            return products;
        }

        public List<ProductInfomation> GetProductsByTag(string tag)
        {
            var products = _repository.GetProductsByTag(tag);

            return products;
        }
    }
}
