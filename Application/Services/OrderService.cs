﻿using Application.DTOs;
using Application.DummyData;
using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class OrderService : BaseService<OrderService>, IOrderService
    {
        private readonly IOrderRepostory _orderRepostory;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentRepository _paymentRepository;

        private readonly IOrderDomainService _orderDomainService;

        private readonly IConfiguration _configuration;

        public OrderService(
            IOrderRepostory orderRepostory, 
            IProductRepository productRepository, 
            IPaymentRepository paymentRepository, 
            IOrderDomainService orderDomainService, 
            IConfiguration configuration, 
            ILogger<OrderService> logger) : base(logger)
        {
            _orderRepostory = orderRepostory;
            _productRepository = productRepository;
            _paymentRepository = paymentRepository;
            _orderDomainService = orderDomainService;
            _configuration = configuration;
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
        /// 生成訂單
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<ServiceResult<PaymentRequestDataWithUrl>> GenerateOrder(OrderInfo info)
        {
            try
            {
                var variantIds = info.Items.Select(i => i.VariantId).ToList();
                var productVariants = await _productRepository.GetProductVariants(variantIds);


                var order = new Domain.Entities.Order
                {
                    RecordCode = $"EC{Guid.NewGuid().ToString("N").Substring(0, 10)}",
                    UserId = info.UserId,
                    Status = (int)OrderStatus.Created,
                    Receiver = info.ReceiverName,
                    RecieveStore = info.RecieveStore,
                    PhoneNumber = info.ReceiverPhone,
                    ShippingAddress = info.ShippingAddress,
                    ShippingPrice = (int)info.ShippingFee,
                    RecieveWay = info.RecieveWay,
                    Email = info.Email,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    OrderProducts = new List<OrderProduct>()
                };

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

                    var orderProduct = new OrderProduct
                    {
                        ProductVariantId = productVariant.Id,
                        ProductPrice = productVariant.VariantPrice,
                        Count = item.Quantity,
                        ProductVariant = productVariant,
                    };

                    // order entity 加入 OrderProduct entity
                    order.OrderProducts.Add(orderProduct);
                }

                // 計算總金額
                order.OrderPrice = _orderDomainService.CalculateOrderTotal(order.OrderProducts.ToList(), order.ShippingPrice);

                // 加入訂單步驟
                var orderSteps = new List<OrderStep>()
                {
                    new OrderStep
                    {
                        OrderId=order.Id,
                        StepStatus=(int)OrderStatus.Created,
                        CreatedAt= DateTime.Now,
                        UpdatedAt= DateTime.Now
                    }
                };

                order.OrderSteps = orderSteps;

                // 加入 物流情況
                var shipments = new List<Shipment>()
                {
                    new Shipment
                    {
                        OrderId=order.Id,
                        ShipmentStatus=(int)ShipmentStatus.Pending,
                        CreatedAt= DateTime.Now,
                        UpdatedAt= DateTime.Now
                    }
                };

                order.Shipments = shipments;


                // savechanges 後 會有 astracking 笑我
                await _orderRepostory.GenerateOrder(order);

                // 生成 payment record
                var payment = new Payment
                {
                    OrderId = order.Id, // 此時 order.Id 已經有值 
                    PaymentAmount = (int)order.OrderPrice,
                    PaymentStatus = (int)OrderStatus.WaitingForPayment, // 初始狀態，可以根據需求設置
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    TenantConfigId = 1 // 假設預設值
                };

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
