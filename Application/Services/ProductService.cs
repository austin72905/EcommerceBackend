using Application.DTOs;
using Application.Extensions;
using Application.Interfaces;
using Common.Interfaces.Infrastructure;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using static Application.Extensions.ProductExtensions;

namespace Application.Services
{
    public class ProductService : BaseService<ProductService>, IProductService
    {
        public readonly IProductRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IRedisService _redisService;

        // JSON 序列化選項（提升效能）
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        public ProductService(
            IProductRepository repository, 
            IUserRepository userRepository, 
            IRedisService redisService,
            ILogger<ProductService> logger) : base(logger)
        {
            _repository = repository;
            _userRepository = userRepository;
            _redisService = redisService;
        }





        public async Task<ServiceResult<ProductWithFavoriteStatusDTO>> GetProductById(int productId)
        {
            try
            {
                // 方案1：順序查詢（避免 DbContext 並行問題）
                // 查詢1：基本資訊（有緩存，通常很快）
                var basicInfoResult = await GetProductBasicInfoById(productId);
                if (!basicInfoResult.IsSuccess || basicInfoResult.Data == null)
                {
                    return Fail<ProductWithFavoriteStatusDTO>(basicInfoResult.ErrorMessage ?? "產品不存在");
                }

                // 查詢2：動態資訊（變體、價格、折扣等）
                var dynamicInfoResult = await GetProductDynamicInfoById(productId);
                if (!dynamicInfoResult.IsSuccess || dynamicInfoResult.Data == null || !dynamicInfoResult.Data.Any())
                {
                    return Fail<ProductWithFavoriteStatusDTO>(dynamicInfoResult.ErrorMessage ?? "無法獲取商品變體資訊");
                }

                var basicInfo = basicInfoResult.Data;
                var dynamicInfo = dynamicInfoResult.Data.First(); // 應該只有一個，因為是同一個 productId

                // 組裝完整商品資訊
                var productDto = new ProductInfomationDTO
                {
                    ProductId = basicInfo.ProductId,
                    Title = basicInfo.Title,
                    Material = basicInfo.Material,
                    HowToWash = basicInfo.HowToWash,
                    Features = basicInfo.Features,
                    Images = basicInfo.Images,
                    CoverImg = basicInfo.CoverImg,
                    Variants = dynamicInfo.Variants
                };

                return Success<ProductWithFavoriteStatusDTO>(new ProductWithFavoriteStatusDTO
                {
                    Product = productDto
                });
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
                // 1. 先嘗試從緩存讀取
                var cachedJson = await _redisService.GetProductBasicInfoAsync(productId);
                if (!string.IsNullOrEmpty(cachedJson))
                {
                    var cachedProduct = JsonSerializer.Deserialize<ProductBasicDTO>(cachedJson, _jsonOptions);
                    if (cachedProduct != null)
                    {
                        return Success<ProductBasicDTO>(cachedProduct);
                    }
                }

                // 2. 緩存未命中，從資料庫讀取
                var product = await _repository.GetProductBasicInfoById(productId);

                if (product == null)
                {
                    return Fail<ProductBasicDTO>("產品不存在");
                }

                var productDto = product.ToProductBasicDTO();

                // 3. 同步寫入緩存（避免高並發時的緩存穿透）
                try
                {
                    var json = JsonSerializer.Serialize(productDto, _jsonOptions);
                    await _redisService.SetProductBasicInfoAsync(productId, json);
                }
                catch (Exception ex)
                {
                    // 緩存寫入失敗不影響正常回應
                    Console.WriteLine($"Error caching product {productId}: {ex.Message}");
                }

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
                // 1. 先嘗試從緩存讀取動態資訊
                var cachedJson = await _redisService.GetProductDynamicInfoCacheAsync(productId);
                if (!string.IsNullOrEmpty(cachedJson))
                {
                    try
                    {
                        var cachedDynamicInfo = JsonSerializer.Deserialize<List<ProductDynamicDTO>>(cachedJson, _jsonOptions);
                        if (cachedDynamicInfo != null && cachedDynamicInfo.Count > 0)
                        {
                            return Success<List<ProductDynamicDTO>>(cachedDynamicInfo);
                        }
                    }
                    catch (Exception ex)
                    {
                        // 緩存反序列化失敗，繼續查詢資料庫
                        Console.WriteLine($"Error deserializing cached product dynamic info: {ex.Message}");
                    }
                }

                // 2. 緩存未命中，從資料庫讀取
                var productVariants = await _repository.GetProductVariantsByProductId(productId);

                // 要先分組
                var variantGroup = productVariants.GroupBy(pv => pv.ProductId);

                List<ProductDynamicDTO> productDynamicDtos = new List<ProductDynamicDTO>();
                foreach (var variant in variantGroup)
                {
                    var list = variant.Select(v => v.ToProductVariantDTO());
                    productDynamicDtos.Add(new ProductDynamicDTO { ProductId = variant.Key, Variants = list.ToList(), IsFavorite = false });
                }

                var result = productDynamicDtos.ToList();

                // 3. 同步寫入緩存（避免高並發時的緩存穿透）
                if (result.Count > 0)
                {
                    try
                    {
                        var json = JsonSerializer.Serialize(result, _jsonOptions);
                        await _redisService.SetProductDynamicInfoCacheAsync(productId, json, TimeSpan.FromMinutes(10));
                    }
                    catch (Exception ex)
                    {
                        // 緩存寫入失敗不影響正常回應
                        Console.WriteLine($"Error caching product dynamic info: {ex.Message}");
                    }
                }

                return Success<List<ProductDynamicDTO>>(result);
            }
            catch (Exception ex)
            {
                return Error<List<ProductDynamicDTO>>(ex.Message);
            }
        }

        /// <summary>
        /// 獲取完整的商品資訊（包含基本資訊和動態資訊）- 未登入版本
        /// </summary>
        public async Task<ServiceResult<ProductCompleteDTO>> GetProductCompleteInfoById(int productId)
        {
            try
            {
                // 獲取基本資訊
                var basicInfoResult = await GetProductBasicInfoById(productId);
                if (!basicInfoResult.IsSuccess || basicInfoResult.Data == null)
                {
                    return Fail<ProductCompleteDTO>(basicInfoResult.ErrorMessage ?? "產品不存在");
                }

                // 獲取動態資訊
                var dynamicInfoResult = await GetProductDynamicInfoById(productId);
                if (!dynamicInfoResult.IsSuccess || dynamicInfoResult.Data == null || !dynamicInfoResult.Data.Any())
                {
                    return Fail<ProductCompleteDTO>(dynamicInfoResult.ErrorMessage ?? "無法獲取商品變體資訊");
                }

                var basicInfo = basicInfoResult.Data;
                var dynamicInfo = dynamicInfoResult.Data.First(); // 應該只有一個，因為是同一個 productId

                // 整合資料
                var completeInfo = new ProductCompleteDTO
                {
                    ProductId = basicInfo.ProductId,
                    Title = basicInfo.Title,
                    Material = basicInfo.Material,
                    HowToWash = basicInfo.HowToWash,
                    Features = basicInfo.Features,
                    Images = basicInfo.Images,
                    CoverImg = basicInfo.CoverImg,
                    Variants = dynamicInfo.Variants,
                    IsFavorite = false // 未登入用戶預設為 false
                };

                return Success<ProductCompleteDTO>(completeInfo);
            }
            catch (Exception ex)
            {
                return Error<ProductCompleteDTO>(ex.Message);
            }
        }

        /// <summary>
        /// 獲取完整的商品資訊（包含基本資訊和動態資訊）- 已登入用戶版本
        /// </summary>
        public async Task<ServiceResult<ProductCompleteDTO>> GetProductCompleteInfoByIdForUser(int userId, int productId)
        {
            try
            {
                // 獲取基本資訊
                var basicInfoResult = await GetProductBasicInfoById(productId);
                if (!basicInfoResult.IsSuccess || basicInfoResult.Data == null)
                {
                    return Fail<ProductCompleteDTO>(basicInfoResult.ErrorMessage ?? "產品不存在");
                }

                // 獲取動態資訊
                var dynamicInfoResult = await GetProductDynamicInfoById(productId);
                if (!dynamicInfoResult.IsSuccess || dynamicInfoResult.Data == null || !dynamicInfoResult.Data.Any())
                {
                    return Fail<ProductCompleteDTO>(dynamicInfoResult.ErrorMessage ?? "無法獲取商品變體資訊");
                }

                // 獲取用戶收藏狀態
                var favoriteProductIds = await _userRepository.GetFavoriteProductIdsByUser(userId);
                bool isFavorite = favoriteProductIds.Contains(productId);

                var basicInfo = basicInfoResult.Data;
                var dynamicInfo = dynamicInfoResult.Data.First(); // 應該只有一個，因為是同一個 productId

                // 整合資料
                var completeInfo = new ProductCompleteDTO
                {
                    ProductId = basicInfo.ProductId,
                    Title = basicInfo.Title,
                    Material = basicInfo.Material,
                    HowToWash = basicInfo.HowToWash,
                    Features = basicInfo.Features,
                    Images = basicInfo.Images,
                    CoverImg = basicInfo.CoverImg,
                    Variants = dynamicInfo.Variants,
                    IsFavorite = isFavorite
                };

                return Success<ProductCompleteDTO>(completeInfo);
            }
            catch (Exception ex)
            {
                return Error<ProductCompleteDTO>(ex.Message);
            }
        }




        public async Task<ServiceResult<ProductWithFavoriteStatusDTO>> GetProductByIdForUser(int userid, int productId)
        {
            try
            {
                // 方案1：順序查詢（避免 DbContext 並行問題）
                // 查詢1：基本資訊（有緩存，通常很快）
                var basicInfoResult = await GetProductBasicInfoById(productId);
                if (!basicInfoResult.IsSuccess || basicInfoResult.Data == null)
                {
                    return Fail<ProductWithFavoriteStatusDTO>(basicInfoResult.ErrorMessage ?? "產品不存在");
                }

                // 查詢2：動態資訊（變體、價格、折扣等）
                var dynamicInfoResult = await GetProductDynamicInfoById(productId);
                if (!dynamicInfoResult.IsSuccess || dynamicInfoResult.Data == null || !dynamicInfoResult.Data.Any())
                {
                    return Fail<ProductWithFavoriteStatusDTO>(dynamicInfoResult.ErrorMessage ?? "無法獲取商品變體資訊");
                }

                // 查詢3：用戶收藏狀態
                var favoriteProductIds = await _userRepository.GetFavoriteProductIdsByUser(userid);
                bool isFavorite = favoriteProductIds.Contains(productId);

                var basicInfo = basicInfoResult.Data;
                var dynamicInfo = dynamicInfoResult.Data.First(); // 應該只有一個，因為是同一個 productId

                // 組裝完整商品資訊
                var productDto = new ProductInfomationDTO
                {
                    ProductId = basicInfo.ProductId,
                    Title = basicInfo.Title,
                    Material = basicInfo.Material,
                    HowToWash = basicInfo.HowToWash,
                    Features = basicInfo.Features,
                    Images = basicInfo.Images,
                    CoverImg = basicInfo.CoverImg,
                    Variants = dynamicInfo.Variants
                };

                return Success<ProductWithFavoriteStatusDTO>(new ProductWithFavoriteStatusDTO
                {
                    Product = productDto,
                    IsFavorite = isFavorite
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
                // 將過濾條件傳遞給 Repository，在資料庫層執行，避免載入所有資料到記憶體
                IEnumerable<Domain.Entities.Product> products = Enumerable.Empty<Product>();

                if (!string.IsNullOrEmpty(tag))
                {
                    products = await _repository.GetProductsByTag(tag, query);
                }
                else if (!string.IsNullOrEmpty(kind))
                {
                    products = await _repository.GetProductsByKind(kind, query);
                }
                else if (!string.IsNullOrEmpty(query))
                {
                    products = await _repository.GetProductsByQuery(query);
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
                // 1. 先嘗試從緩存讀取列表
                var cachedJson = await _redisService.GetProductListCacheAsync(kind, tag, query);
                if (!string.IsNullOrEmpty(cachedJson))
                {
                    var cachedProducts = JsonSerializer.Deserialize<List<ProductBasicDTO>>(cachedJson, _jsonOptions);
                    if (cachedProducts != null && cachedProducts.Count > 0)
                    {
                        return Success<List<ProductBasicDTO>>(cachedProducts);
                    }
                }

                // 2. 緩存未命中，從資料庫讀取
                IEnumerable<Domain.Entities.Product> products = Enumerable.Empty<Product>();
                
                if (!string.IsNullOrEmpty(tag))
                {
                    products = await _repository.GetProductsBasicInfByTag(tag, query);
                }
                else if (!string.IsNullOrEmpty(kind))
                {
                    products = await _repository.GetProductsBasicInfoByKind(kind, query);
                }
                else if (!string.IsNullOrEmpty(query))
                {
                    products = await _repository.GetProductsBasicInfoByQuery(query);
                }

                var productsDtos = products.ToProductInBasicDTOs();
                var productList = productsDtos.ToList();

                // 3. 同步寫入緩存（避免高並發時的緩存穿透）
                if (productList.Count > 0)
                {
                    try
                    {
                        var json = JsonSerializer.Serialize(productList, _jsonOptions);
                        await _redisService.SetProductListCacheAsync(kind, tag, query, json);
                    }
                    catch (Exception ex)
                    {
                        // 緩存寫入失敗不影響正常回應
                        Console.WriteLine($"Error caching product list: {ex.Message}");
                    }
                }

                return Success<List<ProductBasicDTO>>(productList);

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
                // 使用特殊的緩存 key：recommendation:{productId}
                string cacheKind = $"recommendation:{productId}";
                
                // 1. 先嘗試從緩存讀取
                var cachedJson = await _redisService.GetProductListCacheAsync(cacheKind, null, null);
                if (!string.IsNullOrEmpty(cachedJson))
                {
                    var cachedProducts = JsonSerializer.Deserialize<List<ProductBasicDTO>>(cachedJson, _jsonOptions);
                    if (cachedProducts != null && cachedProducts.Count > 0)
                    {
                        return Success<List<ProductBasicDTO>>(cachedProducts);
                    }
                }

                // 2. 緩存未命中，從資料庫讀取
                var products = await _repository.GetRecommendationProductBasicInfo(userid, productId);
                var productsDtos = products.ToProductInBasicDTOs();
                var productList = productsDtos.ToList();

                // 3. 同步寫入緩存（避免高並發時的緩存穿透）
                if (productList.Count > 0)
                {
                    try
                    {
                        var json = JsonSerializer.Serialize(productList, _jsonOptions);
                        await _redisService.SetProductListCacheAsync(cacheKind, null, null, json, TimeSpan.FromMinutes(30));
                    }
                    catch (Exception ex)
                    {
                        // 緩存寫入失敗不影響正常回應
                        Console.WriteLine($"Error caching recommendation products: {ex.Message}");
                    }
                }

                return Success<List<ProductBasicDTO>>(productList);
              
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

        /// <summary>
        /// 新增商品
        /// </summary>
        public async Task<ServiceResult<AddProductResponseDTO>> AddProduct(AddProductRequestDTO request)
        {
            try
            {
                // 1. 創建 Product 實體
                var product = new Product
                {
                    Title = request.Title,
                    Material = request.Material,
                    HowToWash = request.HowToWash,
                    Features = request.Features,
                    CoverImg = request.CoverImg,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    ProductTags = new List<ProductTag>(),
                    ProductKinds = new List<ProductKind>(),
                    ProductVariants = new List<ProductVariant>(),
                    ProductImages = new List<ProductImage>()
                };

                // 2. 處理 Tags（查找或創建）
                foreach (var tagName in request.TagNames)
                {
                    var tag = await _repository.GetOrCreateTagAsync(tagName);
                    product.ProductTags.Add(new ProductTag { Tag = tag });
                }

                // 3. 處理 Kinds（查找或創建）
                foreach (var kindName in request.KindNames)
                {
                    var kind = await _repository.GetOrCreateKindAsync(kindName);
                    product.ProductKinds.Add(new ProductKind { Kind = kind });
                }

                // 4. 處理 ProductImages
                foreach (var imageUrl in request.Images)
                {
                    product.ProductImages.Add(new ProductImage { ImageUrl = imageUrl });
                }

                // 5. 處理 ProductVariants
                foreach (var variantDto in request.Variants)
                {
                    // 查找或創建 Size
                    var size = await _repository.GetOrCreateSizeAsync(variantDto.SizeValue);

                    var variant = new ProductVariant
                    {
                        Color = variantDto.Color,
                        SizeId = size.Id,
                        Stock = variantDto.Stock,
                        SKU = variantDto.SKU,
                        VariantPrice = variantDto.Price,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    product.ProductVariants.Add(variant);
                }

                // 6. 保存商品
                var savedProduct = await _repository.AddProductAsync(product);

                // 7. 清除相關緩存
                await _redisService.InvalidateAllProductListCacheAsync();
                await _redisService.InvalidateProductCacheAsync(savedProduct.Id);

                return Success<AddProductResponseDTO>(new AddProductResponseDTO { ProductId = savedProduct.Id });
            }
            catch (Exception ex)
            {
                return Error<AddProductResponseDTO>(ex.Message);
            }
        }

        /// <summary>
        /// 分頁查詢商品列表（完整資訊）
        /// </summary>
        public async Task<ServiceResult<PagedResponseDTO<ProductWithFavoriteStatusDTO>>> GetProductsPaged(string? kind, string? tag, string? query, int page, int pageSize)
        {
            try
            {
                // 1. 先嘗試從緩存讀取完整列表
                var cachedJson = await _redisService.GetProductListFullPagedCacheAsync(kind, tag, query, page, pageSize);
                if (!string.IsNullOrEmpty(cachedJson))
                {
                    try
                    {
                        var cachedResponse = JsonSerializer.Deserialize<PagedResponseDTO<ProductWithFavoriteStatusDTO>>(cachedJson, _jsonOptions);
                        if (cachedResponse != null && cachedResponse.Items != null && cachedResponse.Items.Count > 0)
                        {
                            return Success<PagedResponseDTO<ProductWithFavoriteStatusDTO>>(cachedResponse);
                        }
                    }
                    catch (Exception ex)
                    {
                        // 緩存反序列化失敗，繼續查詢資料庫
                        Console.WriteLine($"Error deserializing cached full paged product list: {ex.Message}");
                    }
                }

                // 2. 緩存未命中，從資料庫讀取
                IEnumerable<Product> products;
                int totalCount;

                if (!string.IsNullOrEmpty(tag))
                {
                    var result = await _repository.GetProductsByTagPagedAsync(tag, query, page, pageSize);
                    products = result.Products;
                    totalCount = result.TotalCount;
                }
                else if (!string.IsNullOrEmpty(kind))
                {
                    var result = await _repository.GetProductsByKindPagedAsync(kind, query, page, pageSize);
                    products = result.Products;
                    totalCount = result.TotalCount;
                }
                else if (!string.IsNullOrEmpty(query))
                {
                    var result = await _repository.GetProductsByQueryPagedAsync(query, page, pageSize);
                    products = result.Products;
                    totalCount = result.TotalCount;
                }
                else
                {
                    return Fail<PagedResponseDTO<ProductWithFavoriteStatusDTO>>("請求類型不得為空");
                }

                var productsDtos = products.ToProductInformationDTOs();
                var productWithFavorite = productsDtos.Select(p => new ProductWithFavoriteStatusDTO { Product = p });

                var pagedResponse = new PagedResponseDTO<ProductWithFavoriteStatusDTO>
                {
                    Items = productWithFavorite.ToList(),
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize
                };

                // 3. 同步寫入緩存（避免高並發時的緩存穿透）
                if (pagedResponse.Items.Count > 0)
                {
                    try
                    {
                        var json = JsonSerializer.Serialize(pagedResponse, _jsonOptions);
                        await _redisService.SetProductListFullPagedCacheAsync(kind, tag, query, page, pageSize, json, TimeSpan.FromMinutes(10));
                    }
                    catch (Exception ex)
                    {
                        // 緩存寫入失敗不影響正常回應
                        Console.WriteLine($"Error caching full paged product list: {ex.Message}");
                    }
                }

                return Success<PagedResponseDTO<ProductWithFavoriteStatusDTO>>(pagedResponse);
            }
            catch (Exception ex)
            {
                return Error<PagedResponseDTO<ProductWithFavoriteStatusDTO>>(ex.Message);
            }
        }

        /// <summary>
        /// 分頁查詢商品列表（完整資訊）- 已登入用戶
        /// </summary>
        public async Task<ServiceResult<PagedResponseDTO<ProductWithFavoriteStatusDTO>>> GetProductsPagedForUser(int userid, string? kind, string? tag, int page, int pageSize)
        {
            try
            {
                IEnumerable<Product> products;
                int totalCount;

                if (!string.IsNullOrEmpty(tag))
                {
                    var result = await _repository.GetProductsByTagPagedAsync(tag, null, page, pageSize);
                    products = result.Products;
                    totalCount = result.TotalCount;
                }
                else if (!string.IsNullOrEmpty(kind))
                {
                    var result = await _repository.GetProductsByKindPagedAsync(kind, null, page, pageSize);
                    products = result.Products;
                    totalCount = result.TotalCount;
                }
                else
                {
                    return Fail<PagedResponseDTO<ProductWithFavoriteStatusDTO>>("請求類型不得為空");
                }

                var productsDtos = products.ToProductInformationDTOs();
                var favoriteProductIds = await _userRepository.GetFavoriteProductIdsByUser(userid);
                var productWithFavorite = productsDtos.Select(p => new ProductWithFavoriteStatusDTO
                {
                    Product = p,
                    IsFavorite = favoriteProductIds.Contains(p.ProductId)
                });

                var pagedResponse = new PagedResponseDTO<ProductWithFavoriteStatusDTO>
                {
                    Items = productWithFavorite.ToList(),
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize
                };

                return Success<PagedResponseDTO<ProductWithFavoriteStatusDTO>>(pagedResponse);
            }
            catch (Exception ex)
            {
                return Error<PagedResponseDTO<ProductWithFavoriteStatusDTO>>(ex.Message);
            }
        }

        /// <summary>
        /// 分頁查詢商品基本資訊列表
        /// </summary>
        public async Task<ServiceResult<PagedResponseDTO<ProductBasicDTO>>> GetProductsBasicInfoPaged(string? kind, string? tag, string? query, int page, int pageSize)
        {
            try
            {
                // 1. 先嘗試從緩存讀取分頁列表
                var cachedJson = await _redisService.GetProductListPagedCacheAsync(kind, tag, query, page, pageSize);
                if (!string.IsNullOrEmpty(cachedJson))
                {
                    try
                    {
                        var cachedResponse = JsonSerializer.Deserialize<PagedResponseDTO<ProductBasicDTO>>(cachedJson, _jsonOptions);
                        if (cachedResponse != null && cachedResponse.Items != null && cachedResponse.Items.Count > 0)
                        {
                            return Success<PagedResponseDTO<ProductBasicDTO>>(cachedResponse);
                        }
                    }
                    catch (Exception ex)
                    {
                        // 緩存反序列化失敗，繼續查詢資料庫
                        Console.WriteLine($"Error deserializing cached paged product list: {ex.Message}");
                    }
                }

                // 2. 緩存未命中，從資料庫讀取
                IEnumerable<Product> products;
                int totalCount;

                if (!string.IsNullOrEmpty(tag))
                {
                    var result = await _repository.GetProductsBasicInfoByTagPagedAsync(tag, query, page, pageSize);
                    products = result.Products;
                    totalCount = result.TotalCount;
                }
                else if (!string.IsNullOrEmpty(kind))
                {
                    var result = await _repository.GetProductsBasicInfoByKindPagedAsync(kind, query, page, pageSize);
                    products = result.Products;
                    totalCount = result.TotalCount;
                }
                else if (!string.IsNullOrEmpty(query))
                {
                    var result = await _repository.GetProductsBasicInfoByQueryPagedAsync(query, page, pageSize);
                    products = result.Products;
                    totalCount = result.TotalCount;
                }
                else
                {
                    return Fail<PagedResponseDTO<ProductBasicDTO>>("請求類型不得為空");
                }

                var productsDtos = products.ToProductInBasicDTOs();

                var pagedResponse = new PagedResponseDTO<ProductBasicDTO>
                {
                    Items = productsDtos.ToList(),
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize
                };

                // 3. 同步寫入緩存（避免高並發時的緩存穿透）
                if (pagedResponse.Items.Count > 0)
                {
                    try
                    {
                        var json = JsonSerializer.Serialize(pagedResponse, _jsonOptions);
                        await _redisService.SetProductListPagedCacheAsync(kind, tag, query, page, pageSize, json, TimeSpan.FromMinutes(10));
                    }
                    catch (Exception ex)
                    {
                        // 緩存寫入失敗不影響正常回應
                        Console.WriteLine($"Error caching paged product list: {ex.Message}");
                    }
                }

                return Success<PagedResponseDTO<ProductBasicDTO>>(pagedResponse);
            }
            catch (Exception ex)
            {
                return Error<PagedResponseDTO<ProductBasicDTO>>(ex.Message);
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
