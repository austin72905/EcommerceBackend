

using Application;
using Application.DTOs;
using Application.DummyData;
using Application.Interfaces;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepostory _orderRepostory;
        public OrderService(IOrderRepostory orderRepostory) 
        {
            _orderRepostory = orderRepostory;
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


        public async Task<ServiceResult<PaymentRequestDataWithUrl>> GenerateOrder()
        {
            //await _orderRepostory.GenerateOrder();

            return new ServiceResult<PaymentRequestDataWithUrl>
            {
                IsSuccess = true,
                Data = new PaymentRequestDataWithUrl() 
                { 
                    Amount="100", // 訂單金額
                    RecordNo="RK202411050642", // 後端生成的訂單號
                    PaymentUrl= "http://localhost:5025/Payment/ECPayPayment", 
                    PayType="ECPAY" // 支付類型 (銀行、綠界第三方支付)
                }
            };
        }
    }
}
