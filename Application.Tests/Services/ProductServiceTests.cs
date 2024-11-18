using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _productService = new ProductService(_productRepositoryMock.Object, _userRepositoryMock.Object);
        }


        /*
            當產品存在時
                * 反回的 ServiceResult 包含成功標誌 (IsSuccess = true)
                * 包含正確的產品數據。
                * 確保 GetProductById 方法被調用一次。
             
        */
        [Test]
        public async Task GetProductById_ProductExists_ReturnsProductWithSuccess()
        {
            // Arrange
            int productId = 1;
            var product = new Product
            {
                Id = productId,
                Title = "Test Product",
                Material = "Cotton",
                HowToWash = "Hand wash only",
                Features = "Lightweight",
                CoverImg = "cover.jpg"
            };

            _productRepositoryMock
                .Setup(repo => repo.GetProductById(productId))
                .ReturnsAsync(product);

            // ToProductInformationDTO已在其他測試中驗證其功能正確性，這樣在 ProductService 的單元測試中無需重複測試。


            // Act
            var result = await _productService.GetProductById(productId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);

            Assert.AreEqual(productId, result.Data.Product.ProductId);
            Assert.AreEqual(product.Title, result.Data.Product.Title);

            //  確保 ProductService 中的 GetProductById 方法實際調用了 _repository.GetProductById，並且只調用了一次。
            _productRepositoryMock.Verify(repo => repo.GetProductById(productId), Times.Once);  
        }


        /*
            當產品不存在時
                * 返回的 ServiceResult 包含失敗標誌 (IsSuccess = false)
                * 錯誤信息正確為 "產品不存在"。
                * 確保 GetProductById 方法被調用一次。
         
        */

        [Test]
        public async Task GetProductById_ProductDoesNotExist_ReturnsError()
        {
            // Arrange
            int productId = 999;

            _productRepositoryMock
                .Setup(repo => repo.GetProductById(productId))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _productService.GetProductById(productId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("產品不存在", result.ErrorMessage);
            Assert.IsNull(result.Data);

            _productRepositoryMock.Verify(repo => repo.GetProductById(productId), Times.Once);
        }



        [Test]
        public async Task GetProductByIdForUser_ProductExistsAndIsFavorite_ReturnsFavoriteTrue()
        {
            // Arrange
            int userId = 1;
            int productId = 1;
            var product = new Product
            {
                Id = productId,
                Title = "Test Product",
                Material = "Cotton",
                HowToWash = "Hand wash only",
                Features = "Lightweight",
                CoverImg = "cover.jpg"
            };
            var favoriteProductIds = new List<int> { productId };

            _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).ReturnsAsync(product);
            _userRepositoryMock.Setup(repo => repo.GetFavoriteProductIdsByUser(userId)).ReturnsAsync(favoriteProductIds);

            // Act
            var result = await _productService.GetProductByIdForUser(userId, productId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);

            Assert.AreEqual(productId, result.Data.Product.ProductId);
            Assert.IsTrue(result.Data.IsFavorite);

            _productRepositoryMock.Verify(repo => repo.GetProductById(productId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetFavoriteProductIdsByUser(userId), Times.Once);
        }


        /*
            沒有包含在喜愛名單內 
        */
        [Test]
        public async Task GetProductByIdForUser_ProductExistsAndIsNotFavorite_ReturnsFavoriteFalse()
        {
            // Arrange
            int userId = 1;
            int productId = 1;
            var product = new Product
            {
                Id = productId,
                Title = "Test Product",
                Material = "Cotton",
                HowToWash = "Hand wash only",
                Features = "Lightweight",
                CoverImg = "cover.jpg"
            };
            var favoriteProductIds = new List<int> { 2 }; // 不包含此產品

            _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).ReturnsAsync(product);
            _userRepositoryMock.Setup(repo => repo.GetFavoriteProductIdsByUser(userId)).ReturnsAsync(favoriteProductIds);

            // Act
            var result = await _productService.GetProductByIdForUser(userId, productId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(productId, result.Data.Product.ProductId);
            Assert.IsFalse(result.Data.IsFavorite);

            _productRepositoryMock.Verify(repo => repo.GetProductById(productId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetFavoriteProductIdsByUser(userId), Times.Once);
        }



        [Test]
        public async Task GetProductByIdForUser_ProductDoesNotExist_ReturnsError()
        {
            // Arrange
            int userId = 1;
            int productId = 999;

            _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await _productService.GetProductByIdForUser(userId, productId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("產品不存在", result.ErrorMessage);
            Assert.IsNull(result.Data);

            _productRepositoryMock.Verify(repo => repo.GetProductById(productId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetFavoriteProductIdsByUser(userId), Times.Never);
        }



        /*
            * 測試當僅提供 tag 時，應返回相關的產品列表。
            * 驗證存儲庫 GetProductsByTag 方法是否被正確調用一次。
            * 確保未調用 GetProductsByKind。 
         
         
        */
        [Test]
        public async Task GetProducts_WithTag_ReturnsProductsByTag()
        {
            // Arrange
            string tag = "Sports";
            string kind = null;

            var products = new List<Product>
            {
                new Product 
                { 
                    Id = 1,
                    Title = "Product 1",
                    Material = "Cotton, Polyester",
                    HowToWash = "Hand wash only",
                    Features = "Lightweight, Durable",
                    CoverImg = "cover.jpg",
                    ProductVariants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            Id = 1,
                            Color = "紅",
                            Stock = 20,
                            SKU = "紅-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },
                            
                        },
                        new ProductVariant
                        {
                            Id = 2,
                            Color = "綠",
                            Stock = 20,
                            SKU = "綠-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },
                            
                        }
                    },
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "img1.jpg" },
                        new ProductImage { ImageUrl = "img2.jpg" }
                    }
                },
                new Product 
                { 
                    Id = 2, 
                    Title = "Product 2",
                    Material = "Cotton, Polyester",
                    HowToWash = "Hand wash only",
                    Features = "Lightweight, Durable",
                    CoverImg = "cover.jpg",
                    ProductVariants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            Id = 1,
                            Color = "紅",
                            Stock = 20,
                            SKU = "紅-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },                         
                        },
                        new ProductVariant
                        {
                            Id = 2,
                            Color = "綠",
                            Stock = 20,
                            SKU = "綠-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },
                           
                        }
                    },
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "img1.jpg" },
                        new ProductImage { ImageUrl = "img2.jpg" }
                    }
                }
            };

            _productRepositoryMock
                .Setup(repo => repo.GetProductsByTag(tag))
                .ReturnsAsync(products);

            // Act
            var result = await _productService.GetProducts(kind, tag);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count);

            Assert.AreEqual("Product 1", result.Data[0].Product.Title);
            Assert.AreEqual("Product 2", result.Data[1].Product.Title);

            _productRepositoryMock.Verify(repo => repo.GetProductsByTag(tag), Times.Once);
            _productRepositoryMock.Verify(repo => repo.GetProductsByKind(It.IsAny<string>()), Times.Never);
        }

        /*
         
            * 測試當僅提供 kind 時，應返回相關的產品列表。
            * 驗證存儲庫 GetProductsByKind 方法是否被正確調用一次。
            * 確保未調用 GetProductsByTag。 
         
        */
        [Test]
        public async Task GetProducts_WithKind_ReturnsProductsByKind()
        {
            // Arrange
            string kind = "Electronics";
            string tag = null;

            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Title = "Product 1",
                    Material = "Cotton, Polyester",
                    HowToWash = "Hand wash only",
                    Features = "Lightweight, Durable",
                    CoverImg = "cover.jpg",
                    ProductVariants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            Id = 1,
                            Color = "紅",
                            Stock = 20,
                            SKU = "紅-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },

                        },
                        new ProductVariant
                        {
                            Id = 2,
                            Color = "綠",
                            Stock = 20,
                            SKU = "綠-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },

                        }
                    },
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "img1.jpg" },
                        new ProductImage { ImageUrl = "img2.jpg" }
                    }
                },
                new Product
                {
                    Id = 2,
                    Title = "Product 2",
                    Material = "Cotton, Polyester",
                    HowToWash = "Hand wash only",
                    Features = "Lightweight, Durable",
                    CoverImg = "cover.jpg",
                    ProductVariants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            Id = 1,
                            Color = "紅",
                            Stock = 20,
                            SKU = "紅-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },
                        },
                        new ProductVariant
                        {
                            Id = 2,
                            Color = "綠",
                            Stock = 20,
                            SKU = "綠-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },

                        }
                    },
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "img1.jpg" },
                        new ProductImage { ImageUrl = "img2.jpg" }
                    }
                }
            };

            _productRepositoryMock
                .Setup(repo => repo.GetProductsByKind(kind))
                .ReturnsAsync(products);

            // Act
            var result = await _productService.GetProducts(kind, tag);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count);

            Assert.AreEqual("Product 1", result.Data[0].Product.Title);
            Assert.AreEqual("Product 2", result.Data[1].Product.Title);

            _productRepositoryMock.Verify(repo => repo.GetProductsByKind(kind), Times.Once);
            _productRepositoryMock.Verify(repo => repo.GetProductsByTag(It.IsAny<string>()), Times.Never);
        }

        /*
            * 測試當 tag 和 kind 均為空時，應返回空集合。
            * 確保存儲庫的任何方法均未被調用。 
         
        */
        [Test]
        public async Task GetProducts_WithNoKindOrTag_ReturnsEmptyList()
        {
            // Arrange
            string kind = null;
            string tag = null;

            // Act
            var result = await _productService.GetProducts(kind, tag);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(result.Data);

            _productRepositoryMock.Verify(repo => repo.GetProductsByKind(It.IsAny<string>()), Times.Never);
            _productRepositoryMock.Verify(repo => repo.GetProductsByTag(It.IsAny<string>()), Times.Never);
        }



        /*
            測試當有推薦產品且部分產品被收藏時，返回的產品列表中 IsFavorite 的值是否正確。 
        */
        [Test]
        public async Task GetRecommendationProduct_ReturnsProductsWithFavoriteStatus()
        {
            // Arrange
            int userId = 1;
            int productId = 101;

            // 模擬推薦的產品
            var recommendedProducts = new List<Product>
            {
                new Product
                {
                    Id = 201,
                    Title = "Product 1",
                    Material = "Cotton, Polyester",
                    HowToWash = "Hand wash only",
                    Features = "Lightweight, Durable",
                    CoverImg = "cover.jpg",
                    ProductVariants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            Id = 1,
                            Color = "紅",
                            Stock = 20,
                            SKU = "紅-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },

                        },
                        new ProductVariant
                        {
                            Id = 2,
                            Color = "綠",
                            Stock = 20,
                            SKU = "綠-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },

                        }
                    },
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "img1.jpg" },
                        new ProductImage { ImageUrl = "img2.jpg" }
                    }
                },
                new Product
                {
                    Id = 202,
                    Title = "Product 2",
                    Material = "Cotton, Polyester",
                    HowToWash = "Hand wash only",
                    Features = "Lightweight, Durable",
                    CoverImg = "cover.jpg",
                    ProductVariants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            Id = 1,
                            Color = "紅",
                            Stock = 20,
                            SKU = "紅-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },
                        },
                        new ProductVariant
                        {
                            Id = 2,
                            Color = "綠",
                            Stock = 20,
                            SKU = "綠-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },

                        }
                    },
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "img1.jpg" },
                        new ProductImage { ImageUrl = "img2.jpg" }
                    }
                }
            };

            // 模擬用戶收藏的產品
            var favoriteProductIds = new List<int> { 201 };

            _productRepositoryMock
                .Setup(repo => repo.GetRecommendationProduct(userId, productId))
                .ReturnsAsync(recommendedProducts);

            _userRepositoryMock
                .Setup(repo => repo.GetFavoriteProductIdsByUser(userId))
                .ReturnsAsync(favoriteProductIds);

            // Act
            var result = await _productService.GetRecommendationProduct(userId, productId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count);

            // 檢查推薦產品的收藏狀態
            Assert.AreEqual(201, result.Data[0].Product.ProductId);
            Assert.IsTrue(result.Data[0].IsFavorite);

            Assert.AreEqual(202, result.Data[1].Product.ProductId);
            Assert.IsFalse(result.Data[1].IsFavorite);

            _productRepositoryMock.Verify(repo => repo.GetRecommendationProduct(userId, productId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetFavoriteProductIdsByUser(userId), Times.Once);
        }




        /*
            測試當用戶有收藏產品時，返回的產品列表是否正確。 
            確保每個產品的 IsFavorite 屬性都為 true

            測試當用戶沒有收藏產品時，返回的列表是否為空。
         
        */
        [Test]
        public async Task GetfavoriteList_UserHasFavorites_ReturnsFavoriteProducts()
        {
            // Arrange
            int userId = 1;

            var favoriteProducts= new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Title = "Product 1",
                    Material = "Cotton, Polyester",
                    HowToWash = "Hand wash only",
                    Features = "Lightweight, Durable",
                    CoverImg = "cover.jpg",
                    ProductVariants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            Id = 1,
                            Color = "紅",
                            Stock = 20,
                            SKU = "紅-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },

                        },
                        new ProductVariant
                        {
                            Id = 2,
                            Color = "綠",
                            Stock = 20,
                            SKU = "綠-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },

                        }
                    },
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "img1.jpg" },
                        new ProductImage { ImageUrl = "img2.jpg" }
                    }
                },
                new Product
                {
                    Id = 2,
                    Title = "Product 2",
                    Material = "Cotton, Polyester",
                    HowToWash = "Hand wash only",
                    Features = "Lightweight, Durable",
                    CoverImg = "cover.jpg",
                    ProductVariants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            Id = 1,
                            Color = "紅",
                            Stock = 20,
                            SKU = "紅-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },
                        },
                        new ProductVariant
                        {
                            Id = 2,
                            Color = "綠",
                            Stock = 20,
                            SKU = "綠-x",
                            VariantPrice = 20,
                            Size = new Size { SizeValue = "X" },

                        }
                    },
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "img1.jpg" },
                        new ProductImage { ImageUrl = "img2.jpg" }
                    }
                }
            };

            _productRepositoryMock
                .Setup(repo => repo.GetfavoriteProducts(userId))
                .ReturnsAsync(favoriteProducts);

            // Act
            var result = await _productService.GetfavoriteList(userId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count);

            // 檢查每個產品是否標記為收藏
            Assert.AreEqual(1, result.Data[0].Product.ProductId);
            Assert.IsTrue(result.Data[0].IsFavorite);

            Assert.AreEqual(2, result.Data[1].Product.ProductId);
            Assert.IsTrue(result.Data[1].IsFavorite);

            _productRepositoryMock.Verify(repo => repo.GetfavoriteProducts(userId), Times.Once);
        }

        [Test]
        public async Task GetfavoriteList_UserHasNoFavorites_ReturnsEmptyList()
        {
            // Arrange
            int userId = 1;

            _productRepositoryMock
                .Setup(repo => repo.GetfavoriteProducts(userId))
                .ReturnsAsync(new List<Product>()); // 無收藏產品

            // Act
            var result = await _productService.GetfavoriteList(userId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(result.Data);

            _productRepositoryMock.Verify(repo => repo.GetfavoriteProducts(userId), Times.Once);
        }



    }
}
