using Application.DTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using static Application.Extensions.ProductExtensions;

namespace Application.Services
{
    public class ProductService : BaseService<ProductService>, IProductService
    {
        public readonly IProductRepository _repository;
        private readonly IUserRepository _userRepository;

        public ProductService(IProductRepository repository, IUserRepository userRepository, ILogger<ProductService> logger) : base(logger)
        {
            _repository = repository;
            _userRepository = userRepository;

        }





        public async Task<ServiceResult<ProductWithFavoriteStatusDTO>> GetProductById(int productId)
        {
            try
            {
                var product = await _repository.GetProductById(productId);

                if (product == null)
                {
                    return Fail<ProductWithFavoriteStatusDTO>("產品不存在");

                }

                var productDto = product.ToProductInformationDTO();

                //productDto = fakeProductList.FirstOrDefault(p => p.ProductId == productId);
                ;

                return Success<ProductWithFavoriteStatusDTO>(new ProductWithFavoriteStatusDTO
                {
                    Product = productDto
                });
                //return new ServiceResult<ProductWithFavoriteStatusDTO>()
                //{

                //    IsSuccess = true,
                //    Data = new ProductWithFavoriteStatusDTO
                //    {
                //        Product = productDto
                //    }

                //};
            }
            catch (Exception ex)
            {
                return Error<ProductWithFavoriteStatusDTO>(ex.Message);

            }



        }

        public async Task<ServiceResult<ProductBasicDTO>> GetProductBasicInfoById(int productId)
        {
            try
            {
                var product = await _repository.GetProductBasicInfoById(productId);

                if (product == null)
                {
                    return Fail<ProductBasicDTO>("產品不存在");
                }

                var productDto = product.ToProductBasicDTO();

                //productDto = fakeProductList.FirstOrDefault(p => p.ProductId == productId);

                return Success<ProductBasicDTO>(productDto);
            }
            catch (Exception ex)
            {
                return Error<ProductBasicDTO>(ex.Message);
            }
        }

        public async Task<ServiceResult<List<ProductDynamicDTO>>> GetProductDynamicInfoById(int productId)
        {
            try
            {

                var productVariants = await _repository.GetProductVariantsByProductId(productId);

                // 要先分組
                var variantGroup = productVariants.GroupBy(pv => pv.ProductId);

                List<ProductDynamicDTO> productDynamicDtos = new List<ProductDynamicDTO>();
                foreach (var variant in variantGroup)
                {
                    var list = variant.Select(v => v.ToProductVariantDTO());
                    productDynamicDtos.Add(new ProductDynamicDTO { ProductId = variant.Key, Variants = list.ToList(), IsFavorite = false });
                }

                return Success<List<ProductDynamicDTO>>(productDynamicDtos.ToList());


            }
            catch (Exception ex)
            {
                return Error<List<ProductDynamicDTO>>(ex.Message);
            }
        }




        public async Task<ServiceResult<ProductWithFavoriteStatusDTO>> GetProductByIdForUser(int userid, int productId)
        {
            try
            {
                var product = await _repository.GetProductById(productId);

                if (product == null)
                {
                    return Fail<ProductWithFavoriteStatusDTO>("產品不存在");

                }

                var productDto = product.ToProductInformationDTO();

                //productDto = fakeProductList.FirstOrDefault(p => p.ProductId == productId);

                var favoriteProductIds = await _userRepository.GetFavoriteProductIdsByUser(userid);


                return Success<ProductWithFavoriteStatusDTO>(new ProductWithFavoriteStatusDTO
                {
                    Product = productDto,
                    IsFavorite = favoriteProductIds.Contains(productDto.ProductId)
                });

                
            }
            catch (Exception ex)
            {
                return Error<ProductWithFavoriteStatusDTO>(ex.Message);
            }

        }


        public async Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetProducts(string? kind, string? tag, string? query=null)
        {
            try
            {
                IEnumerable<Domain.Entities.Product> products = Enumerable.Empty<Product>(); // 如果沒有指定 tag 或 kind，返回空集合

                if (!string.IsNullOrEmpty(tag))
                {
                    products = await _repository.GetProductsByTag(tag);

                }
                else if (!string.IsNullOrEmpty(kind))
                {
                    products = await _repository.GetProductsByKind(kind);

                }
                else if (!string.IsNullOrEmpty(query))
                {

                    products = await _repository.GetProductsByQuery(tag);

                }

                // 過濾出符合query 條件的結果
                if (!string.IsNullOrEmpty(query))
                {
                    products = products.Where(p => p.Title.Contains(query));
                }

                var productsDtos = products.ToProductInformationDTOs();

                var productWithFavorite = productsDtos.Select(p => new ProductWithFavoriteStatusDTO { Product = p });

                return Success<List<ProductWithFavoriteStatusDTO>>(productWithFavorite.ToList());

            }
            catch (Exception ex)
            {
                return Error<List<ProductWithFavoriteStatusDTO>>(ex.Message);              
            }

        }



        public async Task<ServiceResult<List<ProductBasicDTO>>> GetProductsBasicInfo(string? kind, string? tag, string? query)
        {
            try
            {
                IEnumerable<Domain.Entities.Product> products = Enumerable.Empty<Product>(); // 如果沒有指定 tag 或 kind，返回空集
                if (!string.IsNullOrEmpty(tag))
                {
                    products = await _repository.GetProductsBasicInfByTag(tag);

                }
                else if (!string.IsNullOrEmpty(kind))
                {
                    products = await _repository.GetProductsBasicInfoByKind(kind);

                }
                else if (!string.IsNullOrEmpty(query))
                {
                    products = await _repository.GetProductsBasicInfoByQuery(query);
                }

                // 過濾出符合query 條件的結果
                if (!string.IsNullOrEmpty(query))
                {
                    products = products.Where(p => p.Title.Contains(query));
                }

                var productsDtos = products.ToProductInBasicDTOs();

                return Success<List<ProductBasicDTO>>(productsDtos.ToList());

            }
            catch (Exception ex)
            {
                return Error<List<ProductBasicDTO>>(ex.Message);            
            }
        }


        public async Task<ServiceResult<List<ProductDynamicDTO>>> GetProductsDynamicInfo(List<int> productIdList)
        {
            try
            {

                var productVariants = await _repository.GetProductVariantsByProductIdList(productIdList);


                // 要先分組
                var variantGroup = productVariants.GroupBy(pv => pv.ProductId);

                List<ProductDynamicDTO> productDynamicDtos = new List<ProductDynamicDTO>();
                foreach (var variant in variantGroup)
                {
                    var list = variant.Select(v => v.ToProductVariantDTO());
                    productDynamicDtos.Add(new ProductDynamicDTO { ProductId = variant.Key, Variants = list.ToList(), IsFavorite = false });
                }


                return Success<List<ProductDynamicDTO>>(productDynamicDtos.ToList());              

            }
            catch (Exception ex)
            {
                return Error<List<ProductDynamicDTO>>(ex.Message);              
            }
        }


        public async Task<ServiceResult<List<ProductDynamicDTO>>> GetProductsDynamicInfoForUser(List<int> productIdList, int userid)
        {
            try
            {

                var productVariants = await _repository.GetProductVariantsByProductIdList(productIdList);

                var favoriteProductIds = await _userRepository.GetFavoriteProductIdsByUser(userid);

                // 要先分組
                var variantGroup = productVariants.GroupBy(pv => pv.ProductId);

                List<ProductDynamicDTO> productDynamicDtos = new List<ProductDynamicDTO>();
                foreach (var variant in variantGroup)
                {
                    var list = variant.Select(v => v.ToProductVariantDTO());
                    productDynamicDtos.Add(new ProductDynamicDTO { ProductId = variant.Key, Variants = list.ToList(), IsFavorite = favoriteProductIds.Contains(variant.Key) });
                }


                return Success<List<ProductDynamicDTO>>(productDynamicDtos.ToList());               

            }
            catch (Exception ex)
            {
                return Error<List<ProductDynamicDTO>>(ex.Message);                
            }
        }


        public async Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetProductsForUser(int userid, string? kind, string? tag)
        {
            try
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
                var favoriteProductIds = await _userRepository.GetFavoriteProductIdsByUser(userid);
                var productWithFavorite = productsDtos.Select(p => new ProductWithFavoriteStatusDTO
                {
                    Product = p,
                    IsFavorite = favoriteProductIds.Contains(p.ProductId)
                });


                return Success<List<ProductWithFavoriteStatusDTO>>(productWithFavorite.ToList());
                
            }
            catch (Exception ex)
            {
                return Error<List<ProductWithFavoriteStatusDTO>>(ex.Message);               
            }

        }




        public async Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetRecommendationProduct(int userid, int productId)
        {
            try
            {
                var products = await _repository.GetRecommendationProduct(userid, productId);
                var productsDto = products.ToProductInformationDTOs();

                var favoriteProductIds = await _userRepository.GetFavoriteProductIdsByUser(userid);
                var productWithFavorite = productsDto.Select(p => new ProductWithFavoriteStatusDTO
                {
                    Product = p,
                    IsFavorite = favoriteProductIds.Contains(p.ProductId)
                });


                return Success<List<ProductWithFavoriteStatusDTO>>(productWithFavorite.ToList());
              
            }
            catch (Exception ex)
            {
                return Error<List<ProductWithFavoriteStatusDTO>>(ex.Message);              
            }



        }

        public async Task<ServiceResult<List<ProductBasicDTO>>> GetRecommendationProductBasicInfo(int userid, int productId)
        {
            try
            {
                var products = await _repository.GetRecommendationProductBasicInfo(userid, productId);
                //var productsDto = products.ToProductInformationDTOs();

                var productsDtos = products.ToProductInBasicDTOs();

                //var favoriteProductIds = await _userRepository.GetFavoriteProductIdsByUser(userid);
                //var productWithFavorite = productsDto.Select(p => new ProductWithFavoriteStatusDTO
                //{
                //    Product = p,
                //    IsFavorite = favoriteProductIds.Contains(p.ProductId)
                //});


                return Success<List<ProductBasicDTO>>(productsDtos.ToList());
              
            }
            catch (Exception ex)
            {
                return Error<List<ProductBasicDTO>>(ex.Message);             
            }



        }

        public async Task<ServiceResult<List<ProductWithFavoriteStatusDTO>>> GetfavoriteList(int userid)
        {
            try
            {
                var products = await _repository.GetfavoriteProducts(userid);

                var productsDto = products.ToProductInformationDTOs();

                var productWithFavorite = productsDto.Select(p => new ProductWithFavoriteStatusDTO
                {
                    Product = p,
                    IsFavorite = true
                }).ToList();


                return Success<List<ProductWithFavoriteStatusDTO>>(productWithFavorite);
               
            }
            catch (Exception ex)
            {
                return Error<List<ProductWithFavoriteStatusDTO>>(ex.Message);               
            }


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
