using Application.Extensions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Extensions
{
    [TestFixture]
    public class CartItemExtensionsTests
    {
        [Test]
        public void ToCartItemDTO_ShouldMapPropertiesCorrectly()
        {
            // Arrange
            var cartItem = new CartItem
            {
                ProductVariantId = 101,
                Quantity = 2
            };

            // Act
            var result = cartItem.ToCartItemDTO();

            // Assert
            Assert.AreEqual(cartItem.ProductVariantId, result.ProductVariantId);
            Assert.AreEqual(cartItem.Quantity, result.Quantity);
        }

        [Test]
        public void ToCartItemDTOList_ShouldMapListCorrectly()
        {
            // Arrange
            var cartItems = new List<CartItem>
        {
            new CartItem { ProductVariantId = 101, Quantity = 2 },
            new CartItem { ProductVariantId = 102, Quantity = 3 }
        };

            // Act
            var result = cartItems.ToCartItemDTOList();

            // Assert
            Assert.AreEqual(cartItems.Count, result.Count());
            Assert.AreEqual(cartItems[0].ProductVariantId, result.ElementAt(0).ProductVariantId);
            Assert.AreEqual(cartItems[1].Quantity, result.ElementAt(1).Quantity);
        }

        [Test]
        public void ToProductWithCountDTO_ShouldMapPropertiesCorrectly()
        {
            // Arrange
            var cartItem = new CartItem
            {
                ProductVariantId = 101,
                Quantity = 2,
                ProductVariant = new ProductVariant
                {
                    Id = 101,
                    Color = "Red",
                    Size = new Size { SizeValue = "M" },
                    VariantPrice = 200,
                    Stock = 10,
                    SKU = "RED-M",
                    Product = new Product
                    {
                        Id = 1,
                        Title = "Test Product",
                        CoverImg = "cover.jpg"
                    },
                    ProductVariantDiscounts = new List<ProductVariantDiscount>
                    {
                        new ProductVariantDiscount
                        {
                            Discount = new Discount
                            {
                                StartDate = DateTime.Now.AddDays(-1),
                                EndDate = DateTime.Now.AddDays(1),
                                DiscountAmount = 50
                            }
                        }
                    }
                }
            };

            // Act
            var result = cartItem.ToProductWithCountDTO();

            // Assert
            Assert.AreEqual(cartItem.Quantity, result.Count);
            Assert.AreEqual(cartItem.ProductVariant.Product.Id, result.Product.ProductId);
            Assert.AreEqual(cartItem.ProductVariant.Color, result.SelectedVariant.Color);
            Assert.AreEqual(cartItem.ProductVariant.Size.SizeValue, result.SelectedVariant.Size);
            Assert.AreEqual(cartItem.ProductVariant.VariantPrice, result.SelectedVariant.Price);
            Assert.AreEqual(50, result.SelectedVariant.DiscountPrice);
        }

        [Test]
        public void ToProductWithCountDTOList_ShouldMapListCorrectly()
        {
            // Arrange
            var cartItems = new List<CartItem>
            {
                new CartItem
                {
                    ProductVariantId = 101,
                    Quantity = 2,
                    ProductVariant = new ProductVariant
                    {
                        Id = 101,
                        Color = "Red",
                        Size = new Size { SizeValue = "M" },
                        VariantPrice = 200,
                        Stock = 10,
                        SKU = "RED-M",
                        Product = new Product
                        {
                            Id = 1,
                            Title = "Test Product 1",
                            CoverImg = "cover1.jpg"
                        }
                    }
                },
                new CartItem
                {
                    ProductVariantId = 102,
                    Quantity = 3,
                    ProductVariant = new ProductVariant
                    {
                        Id = 102,
                        Color = "Blue",
                        Size = new Size { SizeValue = "L" },
                        VariantPrice = 300,
                        Stock = 5,
                        SKU = "BLUE-L",
                        Product = new Product
                        {
                            Id = 2,
                            Title = "Test Product 2",
                            CoverImg = "cover2.jpg"
                        }
                    }
                }
            };

            // Act
            var result = cartItems.ToProductWithCountDTOList();

            // Assert
            Assert.AreEqual(cartItems.Count, result.Count);
            Assert.AreEqual(cartItems[0].ProductVariant.Product.Id, result[0].Product.ProductId);
            Assert.AreEqual(cartItems[1].ProductVariant.Color, result[1].SelectedVariant.Color);
        }


        [Test]
        public void CalculateDiscountPrice_ShouldReturnNull_WhenNoDiscountsAvailable()
        {
            // Arrange
            var cartItem = new CartItem
            {
                ProductVariantId = 101,
                Quantity = 2,
                ProductVariant = new ProductVariant
                {
                    Id = 101,
                    Color = "Red",
                    Size = new Size { SizeValue = "M" },
                    VariantPrice = 200,
                    Stock = 10,
                    SKU = "RED-M",
                    Product = new Product
                    {
                        Id = 1,
                        Title = "Test Product",
                        CoverImg = "cover.jpg"
                    },
                    ProductVariantDiscounts = null
                }
            };

            // Act
            var result = cartItem.ToProductWithCountDTO();

            // Assert
            Assert.IsNull(result.SelectedVariant.DiscountPrice);
        }

    }
}
