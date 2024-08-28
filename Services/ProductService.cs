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

        public ProductResponse GetProductById(string userid, string productId)
        {
            var product = _repository.GetProductById(productId);

            if (product == null) 
            {
                return new ProductResponse { Product = new ProductWithFavoriteStatus() };
            }


            //有登陸的情況
            if (!string.IsNullOrEmpty(userid))
            {
                var favoriteProductIds = _userRepository.GetFavoriteProductIdsByUser(userid);
                
                return new ProductResponse 
                { 
                    Product = new ProductWithFavoriteStatus { product= product,IsFavorite = favoriteProductIds.Contains(product.ProductId) } 
                };

               
            }
            else
            {
                
                return new ProductResponse
                {
                    Product = new ProductWithFavoriteStatus { product = product }
                };
            }
        }

        public ProductListResponse GetProducts(string userid, string kind, string tag)
        {
            List<ProductInfomation> products = new List<ProductInfomation>();

            if (string.IsNullOrEmpty(tag) && string.IsNullOrEmpty(kind))
            {
                return new ProductListResponse { Products= products.Select(p => new ProductWithFavoriteStatus { product = p }) };
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

                return new ProductListResponse { Products = productWithFavorite };
            }
            else
            {
                var productWithFavorite = products.Select(p => new ProductWithFavoriteStatus { product = p });

                return new ProductListResponse { Products = productWithFavorite };
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
