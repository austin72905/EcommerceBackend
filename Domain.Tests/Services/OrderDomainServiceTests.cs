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
    public class OrderDomainServiceTests
    {
        private OrderDomainService _orderDomainService;

        [SetUp]
        public void Setup()
        {
            _orderDomainService = new OrderDomainService();
        }

        [Test]
        public void CalculateOrderTotal_WithValidProductsAndShipping_ReturnsCorrectTotal()
        {
            // Arrange
            var orderProducts = new List<OrderProduct>
            {
                new OrderProduct
                {
                    Count = 2,
                    ProductVariant = new ProductVariant
                    {
                        VariantPrice = 100,
                        ProductVariantDiscounts = new List<ProductVariantDiscount>
                        {
                            new ProductVariantDiscount
                            {
                                Discount = new Discount
                                {
                                    StartDate = DateTime.Now.AddDays(-1),
                                    EndDate = DateTime.Now.AddDays(1),
                                    DiscountAmount = 20
                                }
                            }
                        }
                    }
                },
                new OrderProduct
                {
                    Count = 1,
                    ProductVariant = new ProductVariant
                    {
                        VariantPrice = 200,
                        ProductVariantDiscounts = new List<ProductVariantDiscount>()
                    }
                }
            };

            int shippingPrice = 50;

            // Act
            var total = _orderDomainService.CalculateOrderTotal(orderProducts, shippingPrice);

            // Assert
            Assert.AreEqual(290, total); //   40 +200 +50
        }

        [Test]
        public void CalculateOrderTotal_WithNoDiscounts_ReturnsCorrectTotal()
        {
            // Arrange
            var orderProducts = new List<OrderProduct>
            {
                new OrderProduct
                {
                    Count = 3,
                    ProductVariant = new ProductVariant
                    {
                        VariantPrice = 100,
                        ProductVariantDiscounts = new List<ProductVariantDiscount>()
                    }
                }
            };

            int shippingPrice = 20;

            // Act
            var total = _orderDomainService.CalculateOrderTotal(orderProducts, shippingPrice);

            // Assert
            Assert.AreEqual(320, total); // (100*3) + (20) = 320
        }

        [Test]
        public void GetDiscountAmountForOrderProduct_WithValidDiscount_ReturnsDiscountAmount()
        {
            // Arrange
            var orderProduct = new OrderProduct
            {
                ProductVariant = new ProductVariant
                {
                    ProductVariantDiscounts = new List<ProductVariantDiscount>
                    {
                        new ProductVariantDiscount
                        {
                            Discount = new Discount
                            {
                                StartDate = DateTime.Now.AddDays(-1),
                                EndDate = DateTime.Now.AddDays(1),
                                DiscountAmount = 30
                            }
                        }
                    }
                }
            };

            // Act
            var discount = _orderDomainService.GetType()
                .GetMethod("GetDiscountAmountForOrderProduct", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(_orderDomainService, new object[] { orderProduct });

            // Assert
            Assert.AreEqual(30, discount);
        }

        [Test]
        public void GetDiscountAmountForOrderProduct_WithNoValidDiscount_ReturnsZero()
        {
            // Arrange
            var orderProduct = new OrderProduct
            {
                ProductVariant = new ProductVariant
                {
                    ProductVariantDiscounts = new List<ProductVariantDiscount>()
                }
            };

            // Act
            var discount = _orderDomainService.GetType()
                .GetMethod("GetDiscountAmountForOrderProduct", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(_orderDomainService, new object[] { orderProduct });

            // Assert
            Assert.AreEqual(0, discount);
        }

        [Test]
        public void GetDiscountAmountForOrderProduct_WithNullProductVariant_ReturnsZero()
        {
            // Arrange
            var orderProduct = new OrderProduct { ProductVariant = null };

            // Act
            var discount = _orderDomainService.GetType()
                .GetMethod("GetDiscountAmountForOrderProduct", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(_orderDomainService, new object[] { orderProduct });

            // Assert
            Assert.AreEqual(0, discount);
        }
    }

}
