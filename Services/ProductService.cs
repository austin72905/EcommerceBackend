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

        public ServiceResult<ProductResponse> GetProductById(string userid, int productId)
        {
            var product = _repository.GetProductById(productId);

            if (product == null) 
            {
                return new ServiceResult<ProductResponse>()
                {
                    IsSuccess = false,
                    ErrorMessage="產品不存在"
                };
                
            }


            //有登陸的情況
            if (!string.IsNullOrEmpty(userid))
            {
                var favoriteProductIds = _userRepository.GetFavoriteProductIdsByUser(userid);

                return new ServiceResult<ProductResponse>()
                {
                    IsSuccess = true,
                    Data = new ProductResponse
                    {
                        Product = new ProductWithFavoriteStatus { product = product, IsFavorite = favoriteProductIds.Contains(product.ProductId) }
                    }
                };
                
               
            }
            else
            {
                return new ServiceResult<ProductResponse>()
                {
                    IsSuccess = true,
                    Data = new ProductResponse
                    {
                        Product = new ProductWithFavoriteStatus { product = product }
                    }
                };
                
            }
        }

        public ServiceResult<ProductListResponse> GetProducts(string userid, string kind, string tag)
        {
            List<ProductInfomation> products = new List<ProductInfomation>();

            //if (string.IsNullOrEmpty(tag) && string.IsNullOrEmpty(kind))
            //{
            //    return new ProductListResponse { Products= products.Select(p => new ProductWithFavoriteStatus { product = p }) };
            //}


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

                return new ServiceResult<ProductListResponse>
                {
                    IsSuccess = true,
                    Data = new ProductListResponse { Products = productWithFavorite }
                };

  
            }
            else
            {
                var productWithFavorite = products.Select(p => new ProductWithFavoriteStatus { product = p });
                return new ServiceResult<ProductListResponse>
                {
                    IsSuccess = true,
                    Data = new ProductListResponse { Products = productWithFavorite },
                };

            }


        }

        public ServiceResult<List<ProductInfomation>> GetProductsByKind(string kind)
        {
            var products = _repository.GetProductsByKind(kind);

            return new ServiceResult<List<ProductInfomation>> 
            { 
                IsSuccess=true,
                Data= products

            };

        }

        public ServiceResult<List<ProductInfomation>> GetProductsByTag(string tag)
        {
            var products = _repository.GetProductsByTag(tag);


            return new ServiceResult<List<ProductInfomation>>
            {
                IsSuccess = true,
                Data = products

            };

        }

        public ServiceResult<List<ProductInfomation>> GetRecommendationProduct(string? userid, int productId)
        {
            List<ProductInfomation> products = new List<ProductInfomation>();
            // 目前假設是返回新品上市
            // 之後可以改成 用戶瀏覽紀錄 or 依照 productId 找出先關聯的產品
            products = _repository.GetProductsByKind("new-arrival");


            return new ServiceResult<List<ProductInfomation>>
            {
                IsSuccess = true,
                Data = products

            };

        }
    }
}
