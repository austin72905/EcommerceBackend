using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Common.Interfaces.Infrastructure;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Reflection;

namespace Application.Tests.Services
{
    [TestFixture]
    public class OrderServiceTests
    {
        
        private Mock<IOrderRepostory> _orderRepositoryMock;
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<IPaymentRepository> _paymentRepositoryMock;
        private Mock<IOrderDomainService> _orderDomainServiceMock;
        private Mock<IInventoryService> _inventoryServiceMock;
        private Mock<IOrderTimeoutProducer> _orderTimeoutProducerMock;
        private OrderService _orderService;
        private Mock<IConfiguration> _configurationMock;
        private Mock<ILogger<OrderService>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _orderRepositoryMock = new Mock<IOrderRepostory>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _paymentRepositoryMock = new Mock<IPaymentRepository>();
            _orderDomainServiceMock = new Mock<IOrderDomainService>();
            _inventoryServiceMock = new Mock<IInventoryService>();
            _orderTimeoutProducerMock = new Mock<IOrderTimeoutProducer>();
            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<OrderService>>();

            // 設定 OrderTimeoutProducer Mock 行為
            _orderTimeoutProducerMock
                .Setup(x => x.SendOrderTimeoutMessageAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            _orderService = new OrderService(
                _inventoryServiceMock.Object,
                _orderRepositoryMock.Object,
                _productRepositoryMock.Object,
                _paymentRepositoryMock.Object,
                _orderDomainServiceMock.Object,
                _orderTimeoutProducerMock.Object,
                _configurationMock.Object,
                _loggerMock.Object
            );
        }

        [Test]
        public async Task GetOrderInfo_WhenOrderExists_ReturnsOrderDTO()
        {
            // Arrange
            int userId = 1;
            string recordCode = "TX20230122063253";
            var order = Order.Create(1, "John Doe", "0912345678", "Sample Address", "Store Pickup", "test@example.com", 20);
            typeof(Order).GetProperty("Id")!.SetValue(order, 1);
            typeof(Order).GetProperty("RecordCode")!.SetValue(order, recordCode);
            typeof(Order).GetProperty("OrderPrice")!.SetValue(order, 139);
            typeof(Order).GetProperty("Status")!.SetValue(order, (int)OrderStatus.Completed);
            typeof(Order).GetProperty("PayWay")!.SetValue(order, (int)PaymentMethod.BankTransfer);
            typeof(Order).GetProperty("UpdatedAt")!.SetValue(order, DateTime.Now);
            
            // 使用反射調用 internal 方法創建 OrderProduct
            var orderProductType = typeof(OrderProduct);
            var createMethod = orderProductType.GetMethod("Create", BindingFlags.NonPublic | BindingFlags.Static);
            var orderProduct = (OrderProduct)createMethod!.Invoke(null, new object[] { 101, 50, 2, (ProductVariant?)null })!;
            typeof(OrderProduct).GetProperty("ProductVariant")!.SetValue(orderProduct, new ProductVariant
                        {
                            Id = 101,
                            ProductId = 1,
                            Product = new Product { Title = "Sample Product", CoverImg = "sample.jpg" },
                            Color = "Red",
                            Size = new Size { SizeValue = "M" },
                            SKU = "RED-M",
                            Stock = 10,
                            VariantPrice = 50
                        });
            typeof(Order).GetProperty("OrderProducts")!.SetValue(order, new List<OrderProduct> { orderProduct });
            
            var orderStep = OrderStep.CreateForOrder(1, (int)OrderStepStatus.Created);
            typeof(OrderStep).GetProperty("UpdatedAt")!.SetValue(orderStep, DateTime.Now);
            typeof(Order).GetProperty("OrderSteps")!.SetValue(order, new List<OrderStep> { orderStep });
            
            var shipment = Shipment.CreateForOrder(1, (int)ShipmentStatus.Pending);
            typeof(Shipment).GetProperty("UpdatedAt")!.SetValue(shipment, DateTime.Now);
            typeof(Order).GetProperty("Shipments")!.SetValue(order, new List<Shipment> { shipment });

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
                .ReturnsAsync((Order?)null);

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
            var order = Order.Create(1, "John Doe", "0912345678", "123 Sample Street", "Store Pickup", "test@example.com", 20);
            typeof(Order).GetProperty("Id")!.SetValue(order, 1);
            typeof(Order).GetProperty("RecordCode")!.SetValue(order, "TX20230122063253");
            typeof(Order).GetProperty("OrderPrice")!.SetValue(order, 139);
            typeof(Order).GetProperty("Status")!.SetValue(order, (int)OrderStatus.Completed);
            typeof(Order).GetProperty("PayWay")!.SetValue(order, (int)PaymentMethod.BankTransfer);
            typeof(Order).GetProperty("UpdatedAt")!.SetValue(order, DateTime.Now);
            
            var orderProductType = typeof(OrderProduct);
            var createMethod = orderProductType.GetMethod("Create", BindingFlags.NonPublic | BindingFlags.Static);
            var orderProduct = (OrderProduct)createMethod!.Invoke(null, new object[] { 101, 50, 2, (ProductVariant?)null })!;
            var productVariant = new ProductVariant
            {
                Id = 101,
                ProductId = 1,
                Product = new Product { Id = 1, Title = "Sample Product", CoverImg = "sample.jpg" },
                Color = "Red",
                Size = new Size { Id = 1, SizeValue = "M" },
                SKU = "RED-M",
                Stock = 10,
                VariantPrice = 50
            };
            typeof(OrderProduct).GetProperty("ProductVariant")!.SetValue(orderProduct, productVariant);
            typeof(Order).GetProperty("OrderProducts")!.SetValue(order, new List<OrderProduct> { orderProduct });
            
            var orderStep1 = OrderStep.CreateForOrder(1, (int)OrderStepStatus.Created);
            typeof(OrderStep).GetProperty("UpdatedAt")!.SetValue(orderStep1, DateTime.Now.AddDays(-1));
            var orderStep2 = OrderStep.CreateForOrder(1, (int)OrderStepStatus.OrderCompleted);
            typeof(OrderStep).GetProperty("UpdatedAt")!.SetValue(orderStep2, DateTime.Now);
            typeof(Order).GetProperty("OrderSteps")!.SetValue(order, new List<OrderStep> { orderStep1, orderStep2 });
            
            var shipment1 = Shipment.CreateForOrder(1, (int)ShipmentStatus.Shipped);
            typeof(Shipment).GetProperty("UpdatedAt")!.SetValue(shipment1, DateTime.Now.AddDays(-2));
            var shipment2 = Shipment.CreateForOrder(1, (int)ShipmentStatus.Delivered);
            typeof(Shipment).GetProperty("UpdatedAt")!.SetValue(shipment2, DateTime.Now);
            typeof(Order).GetProperty("Shipments")!.SetValue(order, new List<Shipment> { shipment1, shipment2 });
            
            var orders = new List<Order> { order };

            _orderRepositoryMock
                .Setup(repo => repo.GetOrdersByUserId(userId, It.IsAny<string>()))
                .ReturnsAsync(orders);

            // Act
            var result = await _orderService.GetOrders(userId, "");

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.AreEqual(orders[0].Id, result.Data[0].Id);

            _orderRepositoryMock.Verify(repo => repo.GetOrdersByUserId(userId, It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetOrders_WhenNoOrdersExist_ReturnsEmptyList()
        {
            // Arrange
            int userId = 1;

            _orderRepositoryMock
                .Setup(repo => repo.GetOrdersByUserId(userId, It.IsAny<string>()))
                .ReturnsAsync(new List<Order>());

            // Act
            var result = await _orderService.GetOrders(userId, "");

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(0, result.Data.Count);

            _orderRepositoryMock.Verify(repo => repo.GetOrdersByUserId(userId, It.IsAny<string>()), Times.Once);
        }



        [Test]
        public async Task GenerateOrder_WhenProductVariantsExist_CreatesOrderAndReturnsPaymentData()
        {
            // Arrange
            var orderInfo = new OrderInfo
            {
                UserId = 1,
                ReceiverName = "John Doe",
                ReceiverPhone = "0912345678", // 使用有效的台灣電話號碼格式
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

            _inventoryServiceMock
                .Setup(r => r.CheckAndHoldInventoryAsync(It.IsAny<string>(), It.IsAny<Dictionary<int, int>>()))
                .ReturnsAsync(new ServiceResult<InventoryCheckResult> 
                { 
                    IsSuccess = true,
                    Data = new InventoryCheckResult { IsSuccess = true }
                });

            _productRepositoryMock
                .Setup(repo => repo.GetProductVariants(It.IsAny<List<int>>()))
                .ReturnsAsync(productVariants);

            // 設置 CalculateOrderTotal 方法（用於計算訂單總價）
            _orderDomainServiceMock
                .Setup(service => service.CalculateOrderTotal(It.IsAny<List<OrderProduct>>(), It.IsAny<int>(), It.IsAny<Dictionary<int, ProductVariant>>()))
                .Returns((List<OrderProduct> products, int shippingPrice, Dictionary<int, ProductVariant>? variants) => 
                {
                    int total = products.Sum(p => p.ProductPrice * p.Count) + shippingPrice;
                    return total;
                });

            _orderRepositoryMock
                .Setup(repo => repo.GenerateOrder(It.IsAny<Order>()))
                .Callback<Order>(order => typeof(Order).GetProperty("Id")!.SetValue(order, 1))
                .Returns(Task.CompletedTask);

            _paymentRepositoryMock
                .Setup(repo => repo.GeneratePaymentRecord(It.IsAny<Payment>()))
                .Returns(Task.CompletedTask);

            _configurationMock
                .Setup(c => c["AppSettings:PaymentRedirectUrl"])
                .Returns("https://payment.example.com");

            // Act
            var result = await _orderService.GenerateOrder(orderInfo);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(result.Data.Amount);
            Assert.IsNotNull(result.Data.RecordNo);

            _productRepositoryMock.Verify(repo => repo.GetProductVariants(It.IsAny<List<int>>()), Times.Once);
            _inventoryServiceMock.Verify(service => service.CheckAndHoldInventoryAsync(It.IsAny<string>(), It.IsAny<Dictionary<int, int>>()), Times.Once);
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
                ReceiverPhone = "0912345678", // 使用有效的台灣電話號碼格式
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
