

using Application;
using Application.DTOs;
using Application.DummyData;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepostory _orderRepostory;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentRepository _paymentRepository;

        private readonly IOrderDomainService _orderDomainService;
        
        public OrderService(IOrderRepostory orderRepostory, IProductRepository productRepository, IPaymentRepository paymentRepository , IOrderDomainService orderDomainService) 
        {
            _orderRepostory = orderRepostory;
            _productRepository= productRepository;
            _paymentRepository= paymentRepository;
            _orderDomainService = orderDomainService;
        }

        public  async Task<ServiceResult<OrderInfomationDTO>> GetOrderInfo(int userid, string recordCode)
        {
            //var orderInfo =await _orderRepostory.GetOrderInfoByUserId(userid, recordCode);

            var orderDto = new OrderInfomationDTO
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

            return new ServiceResult<OrderInfomationDTO>()
            {
                IsSuccess = true,
                Data = orderDto
            };
        }

        public async Task<ServiceResult<List<OrderInfomationDTO>>>  GetOrders(int userid)
        {
            //var orderList=await _orderRepostory.GetOrdersByUserId(userid);

            var ordersDto = new List<OrderInfomationDTO>()
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

            return new ServiceResult<List<OrderInfomationDTO>>()
            {
                IsSuccess = true,
                Data = ordersDto
            };

        }


        public async Task<ServiceResult<PaymentRequestDataWithUrl>> GenerateOrder(OrderInfo info)
        {

            var variantIds = info.Items.Select(i=>i.VariantId).ToList();
            var productVariants =await _productRepository.GetProductVariants(variantIds);

            
            var order = new Order
            {
                RecordCode=$"EC{Guid.NewGuid().ToString("N").Substring(0, 10)}",
                UserId = info.UserId,
                Status= (int)OrderStatus.Created,
                Receiver = info.ReceiverName,
                PhoneNumber = info.ReceiverPhone,
                ShippingAddress = info.ShippingAddress,
                ShippingPrice = (int)info.ShippingFee,
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
                    ProductVariant= productVariant,
                };

                // order entity 加入 OrderProduct entity
                order.OrderProducts.Add(orderProduct);
            }

            // 計算總金額
            order.OrderPrice = _orderDomainService.CalculateOrderTotal(order.OrderProducts.ToList(), order.ShippingPrice);

            // savechanges 後 會有 astracking 笑我
            await _orderRepostory.GenerateOrder(order);

            // 生成 payment record
            var payment = new Payment
            {
                OrderId = order.Id, // 此時 order.Id 已經有值 
                PaymentAmount = (int)order.OrderPrice ,
                PaymentStatus = (int)OrderStatus.WaitingForPayment, // 初始狀態，可以根據需求設置
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                TenantConfigId = 1 // 假設預設值
            };

            await _paymentRepository.GeneratePaymentRecord(payment);

            return new ServiceResult<PaymentRequestDataWithUrl>
            {
                IsSuccess = true,
                Data = new PaymentRequestDataWithUrl() 
                { 
                    Amount=order.OrderPrice.ToString(), // 訂單金額
                    RecordNo= order.RecordCode, // 後端生成的訂單號
                    PaymentUrl= "http://localhost:5025/Payment/ECPayPayment", 
                    PayType="ECPAY" // 支付類型 (銀行、綠界第三方支付)
                }
            };
        }
    }
}
