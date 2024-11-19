using Application.Services;
using Domain.Entities;
using Domain.Enums;
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
    public class OrderServiceTests
    {
        private Mock<IOrderRepostory> _orderRepositoryMock;
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<IPaymentRepository> _paymentRepositoryMock;
        private Mock<IOrderDomainService> _orderDomainServiceMock;
        private OrderService _orderService;

        [SetUp]
        public void SetUp()
        {
            _orderRepositoryMock = new Mock<IOrderRepostory>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _paymentRepositoryMock = new Mock<IPaymentRepository>();
            _orderDomainServiceMock = new Mock<IOrderDomainService>();

            _orderService = new OrderService(
                _orderRepositoryMock.Object,
                _productRepositoryMock.Object,
                _paymentRepositoryMock.Object,
                _orderDomainServiceMock.Object
            );
        }

        [Test]
        public async Task GetOrderInfo_WhenOrderExists_ReturnsOrderDTO()
        {
            // Arrange
            int userId = 1;
            string recordCode = "TX20230122063253";
            var order = new Order
            {
                Id = 1,
                RecordCode = recordCode,
                OrderPrice = 139,
                Status = (int)OrderStatus.Completed,
                PayWay = (int)PaymentMethod.BankTransfer,
                ShippingPrice = 20,
                UpdatedAt = DateTime.Now,
                OrderProducts = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        Count = 2,
                        ProductVariant = new ProductVariant
                        {
                            Id = 101,
                            ProductId = 1,
                            Product = new Product { Title = "Sample Product", CoverImg = "sample.jpg" },
                            Color = "Red",
                            Size = new Size { SizeValue = "M" },
                            SKU = "RED-M",
                            Stock = 10,
                            VariantPrice = 50
                        }
                    }
                },
                Receiver = "John Doe",
                PhoneNumber = "123456789",
                ShippingAddress = "Sample Address",
                OrderSteps = new List<OrderStep>
                {
                    new OrderStep { StepStatus = (int)OrderStepStatus.Created, UpdatedAt = DateTime.Now }
                },
                Shipments = new List<Shipment>
                {
                    new Shipment { ShipmentStatus = (int)ShipmentStatus.Pending, UpdatedAt = DateTime.Now }
                }
            };

            _orderRepositoryMock
                .Setup(repo => repo.GetOrderInfoByUserId(userId, recordCode))
                .ReturnsAsync(order);

            // Act
            var result = await _orderService.GetOrderInfo(userId, recordCode);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);

            Assert.AreEqual(order.Id, result.Data.Id);
            Assert.AreEqual(order.RecordCode, result.Data.RecordCode);

            _orderRepositoryMock.Verify(repo => repo.GetOrderInfoByUserId(userId, recordCode), Times.Once);
        }

        [Test]
        public async Task GetOrderInfo_WhenOrderDoesNotExist_ReturnsNull()
        {
            // Arrange
            int userId = 1;
            string recordCode = "TX20230122063253";

            _orderRepositoryMock
                .Setup(repo => repo.GetOrderInfoByUserId(userId, recordCode))
                .ReturnsAsync((Order)null);

            // Act
            var result = await _orderService.GetOrderInfo(userId, recordCode);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Data);

            _orderRepositoryMock.Verify(repo => repo.GetOrderInfoByUserId(userId, recordCode), Times.Once);
        }

        [Test]
        public async Task GetOrders_WhenOrdersExist_ReturnsOrderDTOList()
        {
            // Arrange
            int userId = 1;
            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    RecordCode = "TX20230122063253",
                    OrderPrice = 139,
                    Status = (int)OrderStatus.Completed,
                    PayWay = (int)PaymentMethod.BankTransfer,
                    ShippingPrice = 20,
                    UpdatedAt = DateTime.Now,
                    Receiver = "John Doe",
                    PhoneNumber = "123456789",
                    ShippingAddress = "123 Sample Street",
                    OrderProducts = new List<OrderProduct>
                    {
                        new OrderProduct
                        {
                            Count = 2,
                            ProductVariant = new ProductVariant
                            {
                                Id = 101,
                                ProductId = 1,
                                Product = new Product
                                {
                                    Id = 1,
                                    Title = "Sample Product",
                                    CoverImg = "sample.jpg"
                                },
                                Color = "Red",
                                Size = new Size
                                {
                                    Id = 1,
                                    SizeValue = "M"
                                },
                                SKU = "RED-M",
                                Stock = 10,
                                VariantPrice = 50
                            }
                        }
                    },
                    OrderSteps = new List<OrderStep>
                    {
                        new OrderStep
                        {
                            StepStatus = (int)OrderStepStatus.Created,
                            UpdatedAt = DateTime.Now.AddDays(-1)
                        },
                        new OrderStep
                        {
                            StepStatus = (int)OrderStepStatus.OrderCompleted,
                            UpdatedAt = DateTime.Now
                        }
                    },
                    Shipments = new List<Shipment>
                    {
                        new Shipment
                        {
                            ShipmentStatus = (int)ShipmentStatus.Shipped,
                            UpdatedAt = DateTime.Now.AddDays(-2)
                        },
                        new Shipment
                        {
                            ShipmentStatus = (int)ShipmentStatus.Delivered,
                            UpdatedAt = DateTime.Now
                        }
                    }
                }
            };

            _orderRepositoryMock
                .Setup(repo => repo.GetOrdersByUserId(userId))
                .ReturnsAsync(orders);

            // Act
            var result = await _orderService.GetOrders(userId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.AreEqual(orders[0].Id, result.Data[0].Id);

            _orderRepositoryMock.Verify(repo => repo.GetOrdersByUserId(userId), Times.Once);
        }

        [Test]
        public async Task GetOrders_WhenNoOrdersExist_ReturnsEmptyList()
        {
            // Arrange
            int userId = 1;

            _orderRepositoryMock
                .Setup(repo => repo.GetOrdersByUserId(userId))
                .ReturnsAsync(new List<Order>());

            // Act
            var result = await _orderService.GetOrders(userId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(0, result.Data.Count);

            _orderRepositoryMock.Verify(repo => repo.GetOrdersByUserId(userId), Times.Once);
        }



        [Test]
        public async Task GenerateOrder_WhenProductVariantsExist_CreatesOrderAndReturnsPaymentData()
        {
            // Arrange
            var orderInfo = new OrderInfo
            {
                UserId = 1,
                ReceiverName = "John Doe",
                ReceiverPhone = "123456789",
                ShippingAddress = "123 Sample Street",
                ShippingFee = 20,
                RecieveWay = "Store Pickup",
                Email = "test@example.com",
                Items = new List<OrderItem>
                {
                    new OrderItem { VariantId = 101, Quantity = 2 }
                }
            };

            var productVariants = new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = 101,
                    VariantPrice = 50
                }
            };

            _productRepositoryMock
                .Setup(repo => repo.GetProductVariants(It.IsAny<List<int>>()))
                .ReturnsAsync(productVariants);

            _orderDomainServiceMock
                .Setup(service => service.CalculateOrderTotal(It.IsAny<List<OrderProduct>>(), It.IsAny<int>()))
                .Returns(120); // 2 items * 50 + 20 shipping fee

            _orderRepositoryMock
                .Setup(repo => repo.GenerateOrder(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            _paymentRepositoryMock
                .Setup(repo => repo.GeneratePaymentRecord(It.IsAny<Payment>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _orderService.GenerateOrder(orderInfo);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("120", result.Data.Amount);
            Assert.IsNotNull(result.Data.RecordNo);

            _productRepositoryMock.Verify(repo => repo.GetProductVariants(It.IsAny<List<int>>()), Times.Once);
            _orderDomainServiceMock.Verify(service => service.CalculateOrderTotal(It.IsAny<List<OrderProduct>>(), 20), Times.Once);
            _orderRepositoryMock.Verify(repo => repo.GenerateOrder(It.IsAny<Order>()), Times.Once);
            _paymentRepositoryMock.Verify(repo => repo.GeneratePaymentRecord(It.IsAny<Payment>()), Times.Once);
        }

        [Test]
        public async Task GenerateOrder_WhenProductVariantDoesNotExist_ReturnsError()
        {
            // Arrange
            var orderInfo = new OrderInfo
            {
                UserId = 1,
                ReceiverName = "John Doe",
                ReceiverPhone = "123456789",
                ShippingAddress = "123 Sample Street",
                ShippingFee = 20,
                RecieveWay = "Store Pickup",
                Email = "test@example.com",
                Items = new List<OrderItem>
                {
                    new OrderItem { VariantId = 999, Quantity = 2 }
                }
            };

            var productVariants = new List<ProductVariant>(); // No matching variants

            _productRepositoryMock
                .Setup(repo => repo.GetProductVariants(It.IsAny<List<int>>()))
                .ReturnsAsync(productVariants);

            // Act
            var result = await _orderService.GenerateOrder(orderInfo);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("品項不存在", result.ErrorMessage);

            _productRepositoryMock.Verify(repo => repo.GetProductVariants(It.IsAny<List<int>>()), Times.Once);
            _orderRepositoryMock.Verify(repo => repo.GenerateOrder(It.IsAny<Order>()), Times.Never);
            _paymentRepositoryMock.Verify(repo => repo.GeneratePaymentRecord(It.IsAny<Payment>()), Times.Never);
        }
    }
}
