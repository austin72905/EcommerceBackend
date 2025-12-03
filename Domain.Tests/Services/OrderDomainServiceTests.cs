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
            var productVariant1 = new ProductVariant
            {
                Id = 1,
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
            };

            var productVariant2 = new ProductVariant
            {
                Id = 2,
                VariantPrice = 200,
                ProductVariantDiscounts = new List<ProductVariantDiscount>()
            };

            var orderProducts = new List<OrderProduct>
            {
                OrderProduct.Create(1, 100, 2), // ProductPrice = 100, Count = 2
                OrderProduct.Create(2, 200, 1)  // ProductPrice = 200, Count = 1
            };

            var productVariants = new Dictionary<int, ProductVariant>
            {
                { 1, productVariant1 },
                { 2, productVariant2 }
            };

            int shippingPrice = 50;

            // Act
            var total = _orderDomainService.CalculateOrderTotal(orderProducts, shippingPrice, productVariants);

            // Assert
            // 第一個商品：折扣價 20 * 2 = 40
            // 第二個商品：原價 200 * 1 = 200
            // 運費：50
            // 總計：40 + 200 + 50 = 290
            Assert.AreEqual(290, total);
        }

        [Test]
        public void CalculateOrderTotal_WithNoDiscounts_ReturnsCorrectTotal()
        {
            // Arrange
            var productVariant = new ProductVariant
            {
                Id = 1,
                VariantPrice = 100,
                ProductVariantDiscounts = new List<ProductVariantDiscount>()
            };

            var orderProducts = new List<OrderProduct>
            {
                OrderProduct.Create(1, 100, 3) // ProductPrice = 100, Count = 3
            };

            var productVariants = new Dictionary<int, ProductVariant>
            {
                { 1, productVariant }
            };

            int shippingPrice = 20;

            // Act
            var total = _orderDomainService.CalculateOrderTotal(orderProducts, shippingPrice, productVariants);

            // Assert
            Assert.AreEqual(320, total); // (100*3) + (20) = 320
        }

        [Test]
        public void GetDiscountAmountForOrderProduct_WithValidDiscount_ReturnsDiscountAmount()
        {
            // Arrange
            var orderProduct = OrderProduct.Create(1, 100, 1);
            var productVariant = new ProductVariant
            {
                Id = 1,
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
            };

            var productVariants = new Dictionary<int, ProductVariant>
            {
                { 1, productVariant }
            };

            // Act
            var discount = _orderDomainService.GetType()
                .GetMethod("GetDiscountAmountForOrderProduct", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(_orderDomainService, new object[] { orderProduct, productVariants });

            // Assert
            Assert.AreEqual(30, discount);
        }

        [Test]
        public void GetDiscountAmountForOrderProduct_WithNoValidDiscount_ReturnsZero()
        {
            // Arrange
            var orderProduct = OrderProduct.Create(1, 100, 1);
            var productVariant = new ProductVariant
            {
                Id = 1,
                ProductVariantDiscounts = new List<ProductVariantDiscount>()
            };

            var productVariants = new Dictionary<int, ProductVariant>
            {
                { 1, productVariant }
            };

            // Act
            var discount = _orderDomainService.GetType()
                .GetMethod("GetDiscountAmountForOrderProduct", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(_orderDomainService, new object[] { orderProduct, productVariants });

            // Assert
            Assert.AreEqual(0, discount);
        }

        [Test]
        public void GetDiscountAmountForOrderProduct_WithNullProductVariant_ReturnsZero()
        {
            // Arrange
            var orderProduct = OrderProduct.Create(1, 100, 1);
            // 不提供 productVariants 字典，或提供 null

            // Act
            var discount = _orderDomainService.GetType()
                .GetMethod("GetDiscountAmountForOrderProduct", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(_orderDomainService, new object[] { orderProduct, null });

            // Assert
            Assert.AreEqual(0, discount);
        }
    }

}
