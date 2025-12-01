using Application.DTOs;
using Application.DummyData;
using Application.Extensions;
using Application.Interfaces;
using Common.Interfaces.Application.Services;
using Common.Interfaces.Infrastructure;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DomainOrder = Domain.Entities.Order; // 使用別名避免命名空間衝突

namespace Application.Services
{
    public class OrderService : BaseService<OrderService>, IOrderService, IOrderStatusSyncService
    {
        private readonly IOrderRepostory _orderRepostory;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IOrderTimeoutProducer _orderTimeoutProducer;

        private readonly IOrderDomainService _orderDomainService;

        private readonly IConfiguration _configuration;

        public OrderService(
            IInventoryService inventoryService,
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
            _inventoryService = inventoryService;
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

                // 按更新時間降序排序（最新的在上面）
                orderList = orderList.OrderByDescending(o => o.UpdatedAt);

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

                // 用來給庫存服務檢查庫存的
                var variantInventoryPair = new Dictionary<int, int>();

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

                    variantInventoryPair.Add(productVariant.Id, item.Quantity);

                    // 使用領域方法添加商品
                    order.AddOrderProduct(productVariant, item.Quantity);
                }

                // 檢查並預扣庫存（使用庫存服務）
                var inventoryResult = await _inventoryService.CheckAndHoldInventoryAsync(
                    order.RecordCode, 
                    variantInventoryPair);

                if (!inventoryResult.IsSuccess)
                {
                    return Error<PaymentRequestDataWithUrl>(inventoryResult.ErrorMessage ?? "庫存檢查失敗");
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

                _logger.LogInformation("Processing timeout for order {RecordCode}, rolling back inventory", recordcode);
                
                // 回滾庫存（使用庫存服務）
                var rollbackResult = await _inventoryService.RollbackInventoryAsync(recordcode);
                
                if (!rollbackResult.IsSuccess)
                {
                    _logger.LogWarning("回滾庫存失敗，訂單編號: {RecordCode}, 錯誤: {Error}", 
                        recordcode, rollbackResult.ErrorMessage);
                    // 即使回滾失敗，也繼續處理訂單取消
                }
                
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

        /// <summary>
        /// 從訂單狀態服務同步訂單狀態
        /// </summary>
        public async Task SyncOrderStatusFromStateServiceAsync(string recordCode, string fromStatus, string toStatus)
        {
            try
            {
                _logger.LogInformation("[訂單狀態同步] 開始同步訂單狀態: RecordCode={RecordCode}, FromStatus={FromStatus}, ToStatus={ToStatus}",
                    recordCode, fromStatus, toStatus);

                // 根據 RecordCode 獲取訂單（不使用 userId，因為這是從外部服務同步）
                var order = await _orderRepostory.GetOrderInfoByRecordCode(recordCode);

                if (order == null)
                {
                    _logger.LogWarning("[訂單狀態同步] 訂單不存在: RecordCode={RecordCode}", recordCode);
                    return;
                }

                // 將 Go 服務的狀態字串映射到 .NET 的 OrderStatus 枚舉
                OrderStatus? targetStatus = MapGoStatusToDotNetStatus(toStatus);
                if (targetStatus == null)
                {
                    _logger.LogWarning("[訂單狀態同步] 無法映射狀態: ToStatus={ToStatus}, RecordCode={RecordCode}", toStatus, recordCode);
                    return;
                }

                // 檢查當前狀態是否與目標狀態一致
                var currentStatus = (OrderStatus)order.Status;
                if (currentStatus == targetStatus.Value)
                {
                    _logger.LogInformation("[訂單狀態同步] 訂單狀態已是最新: RecordCode={RecordCode}, Status={Status}", 
                        recordCode, targetStatus.Value);
                    return;
                }

                // 使用領域方法更新狀態（這會自動添加 OrderStep）
                order.UpdateStatus(targetStatus.Value);

                // 保存變更
                await _orderRepostory.SaveChangesAsync();

                _logger.LogInformation("[訂單狀態同步] 訂單狀態同步成功: RecordCode={RecordCode}, FromStatus={FromStatus}, ToStatus={ToStatus}",
                    recordCode, fromStatus, toStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[訂單狀態同步] 同步訂單狀態失敗: RecordCode={RecordCode}, FromStatus={FromStatus}, ToStatus={ToStatus}",
                    recordCode, fromStatus, toStatus);
                throw;
            }
        }

        /// <summary>
        /// 將 Go 服務的狀態字串映射到 .NET 的 OrderStatus 枚舉
        /// </summary>
        private OrderStatus? MapGoStatusToDotNetStatus(string goStatus)
        {
            return goStatus switch
            {
                "Created" => OrderStatus.Created,
                "WaitingForPayment" => OrderStatus.WaitingForPayment,
                "WaitingForShipment" => OrderStatus.WaitingForShipment,
                "InTransit" => OrderStatus.InTransit,
                "WaitPickup" => OrderStatus.WaitPickup,
                "Completed" => OrderStatus.Completed,
                "Canceled" => OrderStatus.Canceled,
                "Refund" => OrderStatus.Refund,
                _ => null
            };
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
