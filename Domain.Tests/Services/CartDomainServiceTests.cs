using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Services
{
    [TestFixture]
    public class CartDomainServiceTests
    {
        private CartDomainService _cartDomainService;

        [SetUp]
        public void Setup()
        {
            _cartDomainService = new CartDomainService();
        }

        [Test]
        public void MergeCartItems_UpdatesExistingItemQuantity()
        {
            // Arrange
            var cart = Cart.CreateForUser(1);
            cart.AddItem(new ProductVariant { Id = 1 }, 1);

            var frontEndCartItems = new List<CartItem>
            {
                CartItem.Create(1, 5)
            };

            var productVariants = new List<ProductVariant>
            {
                new ProductVariant { Id = 1 }
            };

            // Act - 直接使用 Cart.MergeItems 方法（因為 CartDomainService 的方法已過時）
            cart.MergeItems(frontEndCartItems, productVariants);

            // Assert
            Assert.AreEqual(1, cart.CartItems.Count);
            Assert.AreEqual(5, cart.CartItems.First().Quantity);
        }

        [Test]
        public void MergeCartItems_AddsNewItemToCart()
        {
            // Arrange
            var cart = Cart.CreateForUser(1);

            var frontEndCartItems = new List<CartItem>
            {
                CartItem.Create(2, 3)
            };

            var productVariants = new List<ProductVariant>
            {
                new ProductVariant { Id = 2 }
            };

            // Act - 直接使用 Cart.MergeItems 方法
            cart.MergeItems(frontEndCartItems, productVariants);

            // Assert
            Assert.AreEqual(1, cart.CartItems.Count);
            Assert.AreEqual(3, cart.CartItems.First().Quantity);
            Assert.AreEqual(2, cart.CartItems.First().ProductVariantId);
        }



        [Test]
        public void ClearAndRebuildCart_ClearsExistingItemsAndAddsNewOnes()
        {
            // Arrange
            var cart = Cart.CreateForUser(1);
            cart.AddItem(new ProductVariant { Id = 1 }, 2);

            var frontEndCartItems = new List<CartItem>
            {
                CartItem.Create(2, 4)
            };

            var productVariants = new List<ProductVariant>
            {
                new ProductVariant { Id = 2 }
            };

            // Act - 直接使用 Cart.Rebuild 方法
            cart.Rebuild(frontEndCartItems, productVariants);

            // Assert
            Assert.AreEqual(1, cart.CartItems.Count);
            Assert.AreEqual(2, cart.CartItems.First().ProductVariantId);
            Assert.AreEqual(4, cart.CartItems.First().Quantity);
        }
    }
}
