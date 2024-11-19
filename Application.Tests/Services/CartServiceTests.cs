using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    [TestFixture]
    public class CartServiceTests
    {
        private Mock<ICartRepository> _cartRepositoryMock;
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<ICartDomainService> _cartDomainServiceMock;
        private CartService _cartService;

        [SetUp]
        public void Setup()
        {
            _cartRepositoryMock = new Mock<ICartRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _cartDomainServiceMock = new Mock<ICartDomainService>();

            _cartService = new CartService(
                _cartDomainServiceMock.Object,
                _productRepositoryMock.Object,
                Mock.Of<IUserRepository>(), // 不需要用戶邏輯，可以用空模擬
                _cartRepositoryMock.Object
            );
        }

        /*
            當購物車不存在時，測試是否正確創建新購物車並合併項目。 
            測試是否調用 Domain Service 合併購物車項目。
        */
        [Test]
        public async Task MergeCartContent_WhenCartDoesNotExist_CreatesNewCartAndMergesItems()
        {
            // Arrange
            int userId = 1;

            var frontEndCartItems = new List<CartItemDTO>
            {
                new CartItemDTO { ProductVariantId = 101, Quantity = 2 }
            };

            var productVariants = new List<ProductVariant>
            {
                new ProductVariant 
                { 
                    Id = 101, 
                    VariantPrice = 100 ,
                    Color="洪",
                    Size=new Size
                    {
                        SizeValue="x"
                    },
                    Stock=5,
                    SKU="紅-X",
                    Product=new Product
                    {
                        Id=1,
                        Title="",
                        CoverImg=""
                    }
                }
            };

            var newCart = new Cart
            {
                UserId = userId,
                CartItems = new List<CartItem>()
            };

            // 模擬購物車不存在
            _cartRepositoryMock.Setup(repo => repo.GetCartByUserId(userId))
                .ReturnsAsync((Cart)null);

            // 模擬 ProductVariant 返回
            _productRepositoryMock.Setup(repo => repo.GetProductVariants(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(productVariants);

            // 模擬 AddAsync 用於新增購物車
            _cartRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Cart>()))
                .Callback<Cart>(cart =>
                {
                    newCart = cart; // 模擬保存新的購物車
                })
                .Returns(Task.CompletedTask);

            // 模擬 MergeCartItems
            _cartDomainServiceMock
                .Setup(service => service.MergeCartItems(It.IsAny<Cart>(), It.IsAny<List<CartItem>>(), It.IsAny<IEnumerable<ProductVariant>>()))
                .Callback<Cart, List<CartItem>, IEnumerable<ProductVariant>>((cart, items, variants) =>
                {
                    cart.CartItems = items.Select(item => new CartItem
                    {
                        ProductVariantId = item.ProductVariantId,
                        Quantity = item.Quantity,
                        ProductVariant = variants.FirstOrDefault(ci => ci.Id == item.ProductVariantId),
                    }).ToList();
                });

            // Act
            var result = await _cartService.MergeCartContent(userId, frontEndCartItems);

            // Assert
            Assert.IsTrue(result.IsSuccess, "Expected the result to be successful.");
            Assert.AreEqual(1, result.Data.Count, "Expected one item in the cart.");
            Assert.AreEqual(101, result.Data[0].SelectedVariant.VariantID, "Expected ProductVariantId to match.");

            // 驗證新增購物車行為
            _cartRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Cart>()), Times.Once);

            // 驗證合併購物車行為
            _cartDomainServiceMock.Verify(
                service => service.MergeCartItems(It.IsAny<Cart>(), It.IsAny<List<CartItem>>(), It.IsAny<IEnumerable<ProductVariant>>()),
                Times.Once);

            // 驗證保存更改
            _cartRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }


        /*
            測試購物車存在時，是否清空並使用前端數據重新構建購物車。
            測試是否調用 Domain Service 清空並重建購物車。
        */
        [Test]
        public async Task MergeCartContentCover_WhenCartExists_ReplacesWithFrontEndItems()
        {
            // Arrange
            int userId = 1;
            var frontEndCartItems = new List<CartItemDTO>
            {
                new CartItemDTO { ProductVariantId = 101, Quantity = 2 }
            };

            var productVariants = new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = 101,
                    VariantPrice = 100,
                    Color = "紅",
                    Size = new Size { SizeValue = "X" },
                    Stock = 5,
                    SKU = "紅-X",
                    Product = new Product
                    {
                        Id = 1,
                        Title = "Sample Product",
                        CoverImg = "cover.jpg"
                    }
                }
            };

            var existingCart = new Cart
            {
                UserId = userId,
                CartItems = new List<CartItem>
                {
                    new CartItem { ProductVariantId = 102, Quantity = 1 }
                }
            };

            // 模擬返回現有的購物車
            _cartRepositoryMock.Setup(repo => repo.GetCartByUserId(userId)).ReturnsAsync(existingCart);

            // 模擬返回產品變體
            _productRepositoryMock.Setup(repo => repo.GetProductVariants(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(productVariants);

            // 模擬 ClearAndRebuildCart 行為
            _cartDomainServiceMock
                .Setup(service => service.ClearAndRebuildCart(It.IsAny<Cart>(), It.IsAny<List<CartItem>>(), It.IsAny<IEnumerable<ProductVariant>>()))
                .Callback<Cart, List<CartItem>, IEnumerable<ProductVariant>>((cart, items, variants) =>
                {
                    cart.CartItems = items.Select(item => new CartItem
                    {
                        ProductVariantId = item.ProductVariantId,
                        Quantity = item.Quantity,
                        ProductVariant = variants.FirstOrDefault(v => v.Id == item.ProductVariantId)
                    }).ToList();
                });

            // Act
            var result = await _cartService.MergeCartContentCover(userId, frontEndCartItems);

            // Assert
            Assert.IsTrue(result.IsSuccess, "Expected the result to be successful.");
            Assert.AreEqual(1, result.Data.Count, "Expected one item in the cart.");
            Assert.AreEqual(101, result.Data[0].SelectedVariant.VariantID, "Expected ProductVariantId to match.");
            Assert.AreEqual(2, result.Data[0].Count, "Expected Quantity to match the front-end item.");

            // 驗證 ClearAndRebuildCart 被調用
            _cartDomainServiceMock.Verify(
                service => service.ClearAndRebuildCart(existingCart, It.IsAny<List<CartItem>>(), productVariants),
                Times.Once);

            // 驗證保存行為
            _cartRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        /*
            模擬存儲庫層異常，驗證是否正確返回錯誤結果。
         
        */
        [Test]
        public async Task MergeCartContent_WhenExceptionOccurs_ReturnsErrorResult()
        {
            // Arrange
            int userId = 1;
            var frontEndCartItems = new List<CartItemDTO>
            {
                new CartItemDTO { ProductVariantId = 101, Quantity = 2 }
            };

            _cartRepositoryMock.Setup(repo => repo.GetCartByUserId(userId)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _cartService.MergeCartContent(userId, frontEndCartItems);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("系統錯誤，請聯繫管理員", result.ErrorMessage);
        }

        [Test]
        public async Task MergeCartContentCover_WhenExceptionOccurs_ReturnsErrorResult()
        {
            // Arrange
            int userId = 1;
            var frontEndCartItems = new List<CartItemDTO>
            {
                new CartItemDTO { ProductVariantId = 101, Quantity = 2 }
            };

            _cartRepositoryMock.Setup(repo => repo.GetCartByUserId(userId)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _cartService.MergeCartContentCover(userId, frontEndCartItems);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("系統錯誤，請聯繫管理員", result.ErrorMessage);
        }
    }
}
