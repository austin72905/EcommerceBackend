using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using static Application.Extensions.ProductExtensions;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _repository;
        private readonly IUserRepository _userRepository;
        public ProductService(IProductRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

       

        public async Task<ServiceResult<ProductResponse>> GetProductById(int productId)
        {
            var product =await _repository.GetProductById(productId);

            if (product == null)
            {
                return new ServiceResult<ProductResponse>()
                {
                    IsSuccess = false,
                    ErrorMessage = "產品不存在"
                };

            }

            var productDto = product.ToProductInformationDTO();

            //productDto = fakeProductList.FirstOrDefault(p => p.ProductId == productId);

            return new ServiceResult<ProductResponse>()
            {
                IsSuccess = true,
                Data = new ProductResponse
                {
                    Product = new ProductWithFavoriteStatusDTO { Product = productDto }
                }
            };


        }

        public async Task<ServiceResult<ProductResponse>> GetProductByIdForUser(int userid, int productId)
        {
            var product =await _repository.GetProductById(productId);

            if (product == null)
            {
                return new ServiceResult<ProductResponse>()
                {
                    IsSuccess = false,
                    ErrorMessage = "產品不存在"
                };

            }

            var productDto = product.ToProductInformationDTO();

            //productDto = fakeProductList.FirstOrDefault(p => p.ProductId == productId);

            var favoriteProductIds =await _userRepository.GetFavoriteProductIdsByUser(userid);

            return new ServiceResult<ProductResponse>()
            {
                IsSuccess = true,
                Data = new ProductResponse
                {
                    Product = new ProductWithFavoriteStatusDTO { Product = productDto, IsFavorite = favoriteProductIds.Contains(productDto.ProductId) }
                }
            };
        }

       
        public async Task<ServiceResult<ProductListResponse>> GetProducts(string kind, string tag)
        {
            IEnumerable<Domain.Entities.Product> products;
            if (!string.IsNullOrEmpty(tag))
            {
                products = await _repository.GetProductsByTag(tag);

            }
            else if (!string.IsNullOrEmpty(kind))
            {
                products = await _repository.GetProductsByKind(kind);

            }
            else
            {
                products = Enumerable.Empty<Product>(); // 如果沒有指定 tag 或 kind，返回空集合
            }

            var productsDtos = products.ToProductInformationDTOs();

            var productWithFavorite = productsDtos.Select(p => new ProductWithFavoriteStatusDTO { Product = p });

            return new ServiceResult<ProductListResponse>
            {
                IsSuccess = true,
                Data = new ProductListResponse { Products = productWithFavorite }
            };


        }

        public async Task<ServiceResult<ProductListResponse>> GetProductsForUser(int userid, string kind, string tag)
        {
            IEnumerable<Domain.Entities.Product> products;
            if (!string.IsNullOrEmpty(tag))
            {
                products = await _repository.GetProductsByTag(tag);

            }
            else if (!string.IsNullOrEmpty(kind))
            {
                products = await _repository.GetProductsByKind(kind);

            }
            else
            {
                products = Enumerable.Empty<Product>(); // 如果沒有指定 tag 或 kind，返回空集合
            }

            var productsDtos = products.ToProductInformationDTOs();
            var favoriteProductIds =await _userRepository.GetFavoriteProductIdsByUser(userid);
            var productWithFavorite = productsDtos.Select(p => new ProductWithFavoriteStatusDTO
            {
                Product = p,
                IsFavorite = favoriteProductIds.Contains(p.ProductId)
            });

 
            return new ServiceResult<ProductListResponse>
            {
                IsSuccess = true,
                Data = new ProductListResponse { Products = productWithFavorite }
            };
        }

        public ServiceResult<List<ProductInfomationDTO>> GetProductsByKind(string kind)
        {
            var products = _repository.GetProductsByKind(kind);

            var productsDto =new List<ProductInfomationDTO>();

            return new ServiceResult<List<ProductInfomationDTO>>
            {
                IsSuccess = true,
                Data = productsDto

            };

        }

        public ServiceResult<List<ProductInfomationDTO>> GetProductsByTag(string tag)
        {
            var products = _repository.GetProductsByTag(tag);

            var productsDto = new List<ProductInfomationDTO>();

            return new ServiceResult<List<ProductInfomationDTO>>
            {
                IsSuccess = true,
                Data = productsDto

            };

        }

       

        public async Task<ServiceResult<ProductListResponse>> GetRecommendationProduct(int userid, int productId)
        {
            var products =await _repository.GetRecommendationProduct(userid, productId);
            var productsDto = products.ToProductInformationDTOs();

            var favoriteProductIds =await _userRepository.GetFavoriteProductIdsByUser(userid);
            var productWithFavorite = productsDto.Select(p => new ProductWithFavoriteStatusDTO
            {
                Product = p,
                IsFavorite = favoriteProductIds.Contains(p.ProductId)
            });

            return new ServiceResult<ProductListResponse>
            {
                IsSuccess = true,
                Data = new ProductListResponse { Products = productWithFavorite }
            };

        }

        public async Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetfavoriteList(int userid)
        {
            var products = await _repository.GetfavoriteProducts(userid);

            var productsDto = products.ToProductInformationDTOs();

            var productWithFavorite = productsDto.Select(p => new ProductWithFavoriteStatusDTO
            {
                Product = p,
                IsFavorite = true
            }).ToList();

            return new ServiceResult<List<ProductWithFavoriteStatusDTO>>
            {
                IsSuccess = true,
                Data = productWithFavorite
            };

        }



        public static List<ProductInfomationDTO> fakeProductList = new List<ProductInfomationDTO>()
            {
                new ProductInfomationDTO
                {
                    Title="超時尚流蘇几皮外套",
                    ProductId=26790367,
                    //Stock=60,
                    //Price=150,
                    //DiscountPrice=100,
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    Variants=new List<ProductVariantDTO>
                    {
                       new ProductVariantDTO
                        {
                            VariantID=1,
                            Color="黑",
                            Size="S",
                            SKU="BLACK-S",
                            Stock=2,
                            Price=99
                        },
                        new ProductVariantDTO
                        {
                            VariantID=2,
                            Color="黑",
                            Size="L",
                            SKU="BLACK-L",
                            Stock=16,
                            Price=283
                        },
                        new ProductVariantDTO
                        {
                            VariantID=3,
                            Color="米",
                            Size="L",
                            SKU="WHEAT-L",
                            Stock=3,
                            Price=150
                        },
                        new ProductVariantDTO
                        {
                            VariantID=4,
                            Color="咖啡",
                            Size="M",
                            SKU="BROWN-M",
                            Stock=17,
                            Price=199
                        },
                        new ProductVariantDTO
                        {
                            VariantID=5,
                            Color="咖啡",
                            Size="L",
                            SKU="BROWN-L",
                            Stock=20,
                            Price=211
                        }
                    },
                    CoverImg="http://localhost:9000/coat1.jpg"
                },
                new ProductInfomationDTO
                {
                    Title="紫色格紋大衣",
                    ProductId=26790368,
                    //Stock=5,
                    //Price=598,
                    //DiscountPrice=500,
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    Variants=new List<ProductVariantDTO>
                    {
                        new ProductVariantDTO
                        {
                            VariantID=1,
                            Color="黑",
                            Size="S",
                            SKU="BLACK-S",
                            Stock=2,
                            Price=99
                        },
                        new ProductVariantDTO
                        {
                            VariantID=2,
                            Color="黑",
                            Size="L",
                            SKU="BLACK-L",
                            Stock=16,
                            Price=283
                        },
                        new ProductVariantDTO
                        {
                            VariantID=3,
                            Color="米",
                            Size="L",
                            SKU="WHEAT-L",
                            Stock=3,
                            Price=150
                        },
                        new ProductVariantDTO
                        {
                            VariantID=4,
                            Color="咖啡",
                            Size="M",
                            SKU="BROWN-M",
                            Stock=17,
                            Price=199
                        },
                        new ProductVariantDTO
                        {
                            VariantID=5,
                            Color="咖啡",
                            Size="L",
                            SKU="BROWN-L",
                            Stock=20,
                            Price=211
                        }
                    },
                    CoverImg="http://localhost:9000/coat4.jpg"
                },
                new ProductInfomationDTO
                {
                    Title="超質感綠色皮衣",
                    ProductId=13790367,
                    //Stock=18,
                    //Price=179,
                    //DiscountPrice=159,
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    Variants=new List<ProductVariantDTO>
                    {
                        new ProductVariantDTO
                        {
                            VariantID=1,
                            Color="黑",
                            Size="S",
                            SKU="BLACK-S",
                            Stock=2,
                            Price=99
                        },
                        new ProductVariantDTO
                        {
                            VariantID=2,
                            Color="黑",
                            Size="L",
                            SKU="BLACK-L",
                            Stock=16,
                            Price=283
                        },
                        new ProductVariantDTO
                        {
                            VariantID=3,
                            Color="米",
                            Size="L",
                            SKU="WHEAT-L",
                            Stock=3,
                            Price=150
                        },
                        new ProductVariantDTO
                        {
                            VariantID=4,
                            Color="咖啡",
                            Size="M",
                            SKU="BROWN-M",
                            Stock=17,
                            Price=199
                        },
                        new ProductVariantDTO
                        {
                            VariantID=5,
                            Color="咖啡",
                            Size="L",
                            SKU="BROWN-L",
                            Stock=20,
                            Price=211
                        }
                    },
                    CoverImg="http://localhost:9000/coat3.jpg"
                },
                new ProductInfomationDTO
                {
                    Title="海島風情黑色短袖襯衫",
                    ProductId=33790012,
                    //Stock=60,
                    //Price=100,
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    Variants=new List<ProductVariantDTO>
                    {
                        new ProductVariantDTO
                        {
                            VariantID=1,
                            Color="黑",
                            Size="S",
                            SKU="BLACK-S",
                            Stock=2,
                            Price=99
                        },
                        new ProductVariantDTO
                        {
                            VariantID=2,
                            Color="黑",
                            Size="L",
                            SKU="BLACK-L",
                            Stock=16,
                            Price=283
                        },
                        new ProductVariantDTO
                        {
                            VariantID=3,
                            Color="米",
                            Size="L",
                            SKU="WHEAT-L",
                            Stock=3,
                            Price=150
                        },
                        new ProductVariantDTO
                        {
                            VariantID=4,
                            Color="咖啡",
                            Size="M",
                            SKU="BROWN-M",
                            Stock=17,
                            Price=199
                        },
                        new ProductVariantDTO
                        {
                            VariantID=5,
                            Color="咖啡",
                            Size="L",
                            SKU="BROWN-L",
                            Stock=20,
                            Price=211
                        }
                    },
                    CoverImg="http://localhost:9000/coat2.jpg"
                },
                new ProductInfomationDTO
                {
                    Title="帥氣單寧",
                    ProductId=34690012,
                    //Stock=60,
                    //Price=799,
                    //DiscountPrice=599,
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    Variants=new List<ProductVariantDTO>
                    {
                       new ProductVariantDTO
                        {
                            VariantID=1,
                            Color="黑",
                            Size="S",
                            SKU="BLACK-S",
                            Stock=2,
                            Price=99,
                            DiscountPrice=22
                        },
                        new ProductVariantDTO
                        {
                            VariantID=2,
                            Color="黑",
                            Size="L",
                            SKU="BLACK-L",
                            Stock=16,
                            Price=283
                        },
                        new ProductVariantDTO
                        {
                            VariantID=3,
                            Color="米",
                            Size="L",
                            SKU="WHEAT-L",
                            Stock=3,
                            Price=150
                        },
                        new ProductVariantDTO
                        {
                            VariantID=4,
                            Color="咖啡",
                            Size="M",
                            SKU="BROWN-M",
                            Stock=17,
                            Price=199
                        },
                        new ProductVariantDTO
                        {
                            VariantID=5,
                            Color="咖啡",
                            Size="L",
                            SKU="BROWN-L",
                            Stock=20,
                            Price=211
                        }
                    },
                    CoverImg="http://localhost:9000/coat5.jpg"
                },

            };
    }
}
