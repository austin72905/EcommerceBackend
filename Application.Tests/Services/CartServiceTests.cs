using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private Mock<ILogger<CartService>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _cartRepositoryMock = new Mock<ICartRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _cartDomainServiceMock = new Mock<ICartDomainService>();
            _loggerMock = new Mock<ILogger<CartService>>();

            _cartService = new CartService(
                _cartDomainServiceMock.Object,
                _productRepositoryMock.Object,
                Mock.Of<IUserRepository>(), // 不需要用戶邏輯，可以用空模擬
                _cartRepositoryMock.Object,
                _loggerMock.Object
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

            var newCart = Cart.CreateForUser(userId);

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
                    // 模擬富領域模型的 MergeItems 行為
                    var domainCartItems = frontEndCartItems.Select(dto => 
                        CartItem.Create(dto.ProductVariantId, dto.Quantity)
                    ).ToList();
                    cart.MergeItems(domainCartItems, productVariants);
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await _cartService.MergeCartContent(userId, frontEndCartItems);

            // Assert
            Assert.IsTrue(result.IsSuccess, "Expected the result to be successful.");
            Assert.AreEqual(1, result.Data!.Count, "Expected one item in the cart.");
            Assert.AreEqual(101, result.Data![0].SelectedVariant.VariantID, "Expected ProductVariantId to match.");

            // 驗證新增購物車行為
            _cartRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Cart>()), Times.Once);

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

            var existingCart = Cart.CreateForUser(userId);
            var cartItem = CartItem.Create(102, 1);
            typeof(Cart).GetProperty("CartItems")!.SetValue(existingCart, new List<CartItem> { cartItem });

            // 模擬返回現有的購物車
            _cartRepositoryMock.Setup(repo => repo.GetCartByUserId(userId)).ReturnsAsync(existingCart);

            // 模擬返回產品變體
            _productRepositoryMock.Setup(repo => repo.GetProductVariants(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(productVariants);

            // Act
            var result = await _cartService.MergeCartContentCover(userId, frontEndCartItems);

            // Assert
            Assert.IsTrue(result.IsSuccess, "Expected the result to be successful.");
            Assert.AreEqual(1, result.Data!.Count, "Expected one item in the cart.");
            Assert.AreEqual(101, result.Data![0].SelectedVariant.VariantID, "Expected ProductVariantId to match.");
            Assert.AreEqual(2, result.Data![0].Count, "Expected Quantity to match the front-end item.");

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
            // BaseService.Error 方法的簽名是 Error<E>(string exceptionMsg, string? message = null)
            // 如果 message 為 null，則返回 "系統錯誤，請聯繫管理員"
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
            // BaseService.Error 方法的簽名是 Error<E>(string exceptionMsg, string? message = null)
            // 如果 message 為 null，則返回 "系統錯誤，請聯繫管理員"
            Assert.AreEqual("系統錯誤，請聯繫管理員", result.ErrorMessage);
        }
    }
}
