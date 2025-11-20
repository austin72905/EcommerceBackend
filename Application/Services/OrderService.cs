using Application.DTOs;
using Application.DummyData;
using Application.Extensions;
using Application.Interfaces;
using Common.Interfaces.Infrastructure;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;
using DomainOrder = Domain.Entities.Order; // 使用別名避免命名空間衝突

namespace Application.Services
{
    public class OrderService : BaseService<OrderService>, IOrderService
    {
        private readonly IOrderRepostory _orderRepostory;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IRedisService _redisService;
        private readonly IOrderTimeoutProducer _orderTimeoutProducer;

        private readonly IOrderDomainService _orderDomainService;

        private readonly IConfiguration _configuration;

        public OrderService(
            IRedisService redisService,
            IOrderRepostory orderRepostory, 
            IProductRepository productRepository, 
            IPaymentRepository paymentRepository, 
            IOrderDomainService orderDomainService, 
            IOrderTimeoutProducer orderTimeoutProducer,
            IConfiguration configuration, 
            ILogger<OrderService> logger) : base(logger)
        {
            _orderRepostory = orderRepostory;
            _productRepository = productRepository;
            _paymentRepository = paymentRepository;
            _orderDomainService = orderDomainService;
            _configuration = configuration;
            _redisService = redisService;
            _orderTimeoutProducer = orderTimeoutProducer;
        }

        public async Task<ServiceResult<OrderInfomationDTO>> GetOrderInfo(int userid, string recordCode)
        {
            try
            {
                var order = await _orderRepostory.GetOrderInfoByUserId(userid, recordCode);
                if (order == null)
                {
                    return Success<OrderInfomationDTO>();

                }

                var orderDto = order.ToOrderDTO();

                return Success<OrderInfomationDTO>(orderDto);

            }
            catch (Exception ex)
            {

                return Error<OrderInfomationDTO>(ex.Message);

            }

        }

        public async Task<ServiceResult<List<OrderInfomationDTO>>> GetOrders(int userid, string query)
        {

            try
            {


                var orderList = await _orderRepostory.GetOrdersByUserId(userid);

                if (!string.IsNullOrEmpty(query))
                {
                    orderList = orderList.Where(o =>
                                            o.OrderProducts.Any(op => op.ProductVariant.Product.Title.Contains(query)) ||
                                            o.RecordCode == query
                    );
                }

                var ordersDto = orderList.ToOrderDTOList();
                
                return Success<List<OrderInfomationDTO>>(ordersDto);                
            }
            catch (Exception ex)
            {

                return Error<List<OrderInfomationDTO>>(ex.Message);
            
            }


        }

        /// <summary>
        /// 生成訂單 - 使用富領域模型方法
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<ServiceResult<PaymentRequestDataWithUrl>> GenerateOrder(OrderInfo info)
        {
            try
            {
                // 獲取商品變體資料
                var variantIds = info.Items.Select(i => i.VariantId).ToList();
                var productVariants = await _productRepository.GetProductVariants(variantIds);

                // 使用富領域模型的工廠方法創建訂單
                var order = DomainOrder.Create(
                    userId: info.UserId,
                    receiver: info.ReceiverName,
                    phoneNumber: info.ReceiverPhone,
                    shippingAddress: info.ShippingAddress,
                    recieveWay: info.RecieveWay,
                    email: info.Email,
                    shippingPrice: (int)info.ShippingFee,
                    recieveStore: info.RecieveStore
                );

                // 用來給 redis 檢查庫存的
                var variantStockPair = new Dictionary<int, int>();

                // 添加訂單商品（使用領域方法）
                foreach (var item in info.Items)
                {
                    var productVariant = productVariants.FirstOrDefault(pv => pv.Id == item.VariantId);

                    if (productVariant == null)
                    {
                        return new ServiceResult<PaymentRequestDataWithUrl>()
                        {
                            IsSuccess = false,
                            ErrorMessage = "品項不存在"
                        };
                    }

                    variantStockPair.Add(productVariant.Id, item.Quantity);

                    // 使用領域方法添加商品
                    order.AddOrderProduct(productVariant, item.Quantity);
                }

                // 檢查庫存
                var checkStockStatus = await _redisService.CheckAndHoldStockAsync(order.RecordCode, variantStockPair);

                if (checkStockStatus == null)
                {
                    return Error<PaymentRequestDataWithUrl>("redis 沒有找到key");
                }

                StockCheckDTO result;
                try
                {
                    result = JsonSerializer.Deserialize<StockCheckDTO>(
                        checkStockStatus.ToString(),
                        new JsonSerializerOptions 
                        { 
                            PropertyNameCaseInsensitive = true  // 忽略大小寫
                        });
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON 解析失敗: {ex.Message}");
                    Console.WriteLine($"原始 JSON: {checkStockStatus}");
                    return Error<PaymentRequestDataWithUrl>("庫存檢查結果解析失敗");
                }

                if (result == null)
                {
                    return Error<PaymentRequestDataWithUrl>("庫存檢查結果為空");
                }

                if (result.Status == "error")
                {
                    var failedItems = result.Failed != null && result.Failed.Any() 
                        ? string.Join(",", result.Failed) 
                        : "未知商品";
                    return Fail<PaymentRequestDataWithUrl>($"庫存不足: {failedItems}");
                }

                // 發送延遲超時訊息 (2分鐘後執行)
                await _orderTimeoutProducer.SendOrderTimeoutMessageAsync(info.UserId, order.RecordCode, 2);

                // 使用領域方法計算總金額（業務邏輯在 Domain 層）
                order.CalculateTotalPrice(_orderDomainService);

                // 保存訂單（領域模型已經自動添加了 OrderStep 和 Shipment）
                await _orderRepostory.GenerateOrder(order);

                // 使用領域模型的工廠方法創建付款記錄
                var payment = Payment.Create(
                    orderId: order.Id, // 此時 order.Id 已經有值 
                    paymentAmount: (int)order.OrderPrice,
                    tenantConfigId: 1 // 假設預設值
                );

                await _paymentRepository.GeneratePaymentRecord(payment);

                return Success<PaymentRequestDataWithUrl>
                    (
                        new PaymentRequestDataWithUrl()
                        {
                            Amount = order.OrderPrice.ToString(), // 訂單金額
                            RecordNo = order.RecordCode, // 後端生成的訂單號
                            PaymentUrl = _configuration["AppSettings:PaymentRedirectUrl"],
                            PayType = "ECPAY" // 支付類型 (銀行、綠界第三方支付)
                        }
                    );
            }
            catch (Exception ex)
            {
                return Error<PaymentRequestDataWithUrl>(ex.Message);              
            }
        }

        /// <summary>
        /// 處理訂單超時 - 使用富領域模型方法
        /// </summary>
        public async Task HandleOrderTimeoutAsync(int userId, string recordcode)
        {
            try
            {
                _logger.LogInformation("Handling order timeout for user {UserId}, order {RecordCode}", userId, recordcode);
                
                var order = await _orderRepostory.GetOrderInfoByUserId(userId, recordcode);

                // 如果訂單不存在或狀態不是 Created（未付款），則不需要處理
                if (order == null || order.Status != (int)OrderStatus.Created)
                {
                    _logger.LogInformation("Order {RecordCode} not found or not in Created status. Current status: {Status}", 
                        recordcode, order?.Status);
                    return;
                }

                _logger.LogInformation("Processing timeout for order {RecordCode}, rolling back stock", recordcode);
                
                // 回滾庫存
                await _redisService.RollbackStockAsync(recordcode);
                
                // 使用領域方法取消訂單（這會自動添加訂單步驟）
                // 注意：由於富領域模型的 setter 是 private，我們需要保持原有的 repository 更新方式
                // 或者需要修改 repository 來支持直接保存領域模型的變更
                await _orderRepostory.UpdateOrderStatusAsync(recordcode, (int)OrderStatus.Canceled);
                
                _logger.LogInformation("Order {RecordCode} timeout processed successfully", recordcode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling order timeout for order {RecordCode}", recordcode);
                throw;
            }
        }

        private static OrderInfomationDTO fakeOderInfo = new OrderInfomationDTO
        {
            Id = 56723,
            ProductList = ProductList.ProductListInOrder,

            RecordCode = "TX20230122063253",
            OrderPrice = 139,
            Address = new OrderAddressDTO
            {
                Receiver = "王大明",
                PhoneNumber = "(+886)964816276",
                ShippingAddress = "7-11 雅典門市 台中市南區三民西路377號西川一路1號 店號950963"
            },
            Status = OrderStatus.Completed,
            PayWay = PaymentMethod.BankTransfer,
            ShippingPrice = 139,
            OrderStepInfomation = new List<OrderStepDTO>
                {
                    new OrderStepDTO
                    {
                        Status=OrderStepStatus.Created,
                        UpdatedAt=DateTime.Now,
                    },
                    new OrderStepDTO
                    {
                        Status=OrderStepStatus.WaitingForPayment,
                        UpdatedAt=DateTime.Now,
                    },
                    new OrderStepDTO
                    {
                        Status=OrderStepStatus.OrderCanceled,
                        UpdatedAt=DateTime.Now,
                    },


                },
            ShipInfomation = new List<ShipmentDTO>
                {
                    new ShipmentDTO
                    {
                        Status=ShipmentStatus.Pending,
                        UpdatedAt=DateTime.Now
                    },
                    new ShipmentDTO
                    {
                        Status=ShipmentStatus.Shipped,
                        UpdatedAt=DateTime.Now
                    },
                    new ShipmentDTO
                    {
                        Status=ShipmentStatus.InTransit,
                        UpdatedAt=DateTime.Now
                    },
                    new ShipmentDTO
                    {
                        Status=ShipmentStatus.OutForDelivery,
                        UpdatedAt=DateTime.Now
                    },
                    new ShipmentDTO
                    {
                        Status=ShipmentStatus.DeliveryFailed,
                        UpdatedAt=DateTime.Now
                    },
                    new ShipmentDTO
                    {
                        Status=ShipmentStatus.Returned,
                        UpdatedAt=DateTime.Now
                    },


                },
            UpdatedAt = DateTime.Now

        };

        private static List<OrderInfomationDTO> fakeOderInfoList = new List<OrderInfomationDTO>()
            {
                new OrderInfomationDTO
                {
                    Id=56723,
                    ProductList=ProductList.ProductListInOrder,

                    RecordCode="TX20230122063253",
                    OrderPrice=139,
                    Address=new OrderAddressDTO
                    {
                        Receiver="王大明",
                        PhoneNumber="(+886)964816276",
                        ShippingAddress="7-11 雅典門市 台中市南區三民西路377號西川一路1號 店號950963"
                    },
                    Status=OrderStatus.WaitingForPayment,
                    PayWay=PaymentMethod.BankTransfer,
                    ShippingPrice=139,
                    OrderStepInfomation=new List<OrderStepDTO>
                    {
                        new OrderStepDTO
                        {
                            Status=OrderStepStatus.Created,
                            UpdatedAt=DateTime.Now,
                        }
                    },
                    ShipInfomation=new List<ShipmentDTO>
                    {
                        new ShipmentDTO
                        {
                            Status=ShipmentStatus.Pending,
                            UpdatedAt=DateTime.Now
                        }
                    },
                    UpdatedAt = DateTime.Now

                },
                new OrderInfomationDTO
                {
                    Id=56724,
                    ProductList=ProductList.ProductListInOrder,

                    RecordCode="TX20230122063256",
                    OrderPrice=139,
                    Address=new OrderAddressDTO
                    {
                        Receiver="王大明",
                        PhoneNumber="(+886)964816276",
                        ShippingAddress="7-11 雅典門市 台中市南區三民西路377號西川一路1號 店號950963"
                    },
                    Status=OrderStatus.Completed,
                    PayWay=PaymentMethod.BankTransfer,
                    ShippingPrice=139,
                    OrderStepInfomation=new List<OrderStepDTO>
                    {
                        new OrderStepDTO
                        {
                            Status=OrderStepStatus.Created,
                            UpdatedAt=DateTime.Now,
                        }
                    },
                    ShipInfomation=new List<ShipmentDTO>
                    {
                        new ShipmentDTO
                        {
                            Status=ShipmentStatus.Pending,
                            UpdatedAt=DateTime.Now
                        }
                    },
                    UpdatedAt = DateTime.Now

                },
                new OrderInfomationDTO
                {
                    Id=56725,
                    ProductList=ProductList.ProductListInOrder,

                    RecordCode="TX20230122063254",
                    OrderPrice=139,
                    Address=new OrderAddressDTO
                    {
                        Receiver="王大明",
                        PhoneNumber="(+886)964816276",
                        ShippingAddress="7-11 雅典門市 台中市南區三民西路377號西川一路1號 店號950963"
                    },
                    Status=OrderStatus.Created,
                    PayWay=PaymentMethod.BankTransfer,
                    ShippingPrice=139,
                    OrderStepInfomation=new List<OrderStepDTO>
                    {
                        new OrderStepDTO
                        {
                            Status=OrderStepStatus.Created,
                            UpdatedAt=DateTime.Now,
                        }
                    },
                    ShipInfomation=new List<ShipmentDTO>
                    {
                        new ShipmentDTO
                        {
                            Status=ShipmentStatus.Pending,
                            UpdatedAt=DateTime.Now
                        },
                        new ShipmentDTO
                        {
                            Status=ShipmentStatus.Shipped,
                            UpdatedAt=DateTime.Now
                        },
                        new ShipmentDTO
                        {
                            Status=ShipmentStatus.InTransit,
                            UpdatedAt=DateTime.Now
                        },
                        new ShipmentDTO
                        {
                            Status=ShipmentStatus.OutForDelivery,
                            UpdatedAt=DateTime.Now
                        },
                        new ShipmentDTO
                        {
                            Status=ShipmentStatus.Delivered,
                            UpdatedAt=DateTime.Now
                        },
                        new ShipmentDTO
                        {
                            Status=ShipmentStatus.PickedUpByCustomer,
                            UpdatedAt=DateTime.Now
                        },


                    },
                    UpdatedAt = DateTime.Now

                }
            };
    }
}
