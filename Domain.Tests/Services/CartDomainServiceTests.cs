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
            var cart = new Cart
            {
                CartItems = new List<CartItem>
                {
                    new CartItem { ProductVariantId = 1, Quantity = 1 }
                }
            };


            var frontEndCartItems = new List<CartItem>
            {
                new CartItem { ProductVariantId = 1, Quantity = 5 }
            };


            var productVariants = new List<ProductVariant>
            {
                new ProductVariant { Id = 1 }
            };

            // Act
            _cartDomainService.MergeCartItems(cart, frontEndCartItems, productVariants);

            // Assert
            Assert.AreEqual(1, cart.CartItems.Count);
            Assert.AreEqual(5, cart.CartItems.First().Quantity);
        }

        [Test]
        public void MergeCartItems_AddsNewItemToCart()
        {
            // Arrange
            var cart = new Cart
            {
                CartItems = new List<CartItem>()
            };



            var frontEndCartItems = new List<CartItem>
            {
                new CartItem { ProductVariantId = 2, Quantity = 3 }
            };



            var productVariants = new List<ProductVariant>
            {
                new ProductVariant { Id = 2 }
            };

            // Act
            _cartDomainService.MergeCartItems(cart, frontEndCartItems, productVariants);

            // Assert
            Assert.AreEqual(1, cart.CartItems.Count);
            Assert.AreEqual(3, cart.CartItems.First().Quantity);
            Assert.AreEqual(2, cart.CartItems.First().ProductVariantId);
        }



        [Test]
        public void ClearAndRebuildCart_ClearsExistingItemsAndAddsNewOnes()
        {
            // Arrange
            var cart = new Cart
            {
                CartItems = new List<CartItem>
                {
                    new CartItem { ProductVariantId = 1, Quantity = 2 }
                }
            };



            var frontEndCartItems = new List<CartItem>
            {
                new CartItem { ProductVariantId = 2, Quantity = 4 }
            };


            var productVariants = new List<ProductVariant>
            {
                new ProductVariant { Id = 2 }
            };

            // Act
            _cartDomainService.ClearAndRebuildCart(cart, frontEndCartItems, productVariants);

            // Assert
            Assert.AreEqual(1, cart.CartItems.Count);
            Assert.AreEqual(2, cart.CartItems.First().ProductVariantId);
            Assert.AreEqual(4, cart.CartItems.First().Quantity);
        }
    }
}
