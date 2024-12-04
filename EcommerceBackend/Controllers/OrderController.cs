
using Application;
using Application.Interfaces;
using EcommerceBackend.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IRedisService _redisService;
        public OrderController(IOrderService orderService, IRedisService redisService)
        {
            _orderService = orderService;
            _redisService = redisService;
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders([FromQuery] GetOrdersReq req)
        {
            ApiResponse resp;

            if (UserInfo == null)
            {

                return UnAuthorized();
            }

            var result = await _orderService.GetOrders(UserInfo.UserId,req.query);

            if (result.IsSuccess)
            {
                return Success(result.Data);
            }
            else
            {
                return Fail();
            }

        }

        [HttpGet("GetOrderInfo")]
        public async Task<IActionResult> GetOrderInfo([FromQuery] string recordCode)
        {
            ApiResponse resp;

            if (UserInfo == null)
            {

                return UnAuthorized();
            }

            var result = await _orderService.GetOrderInfo(UserInfo.UserId, recordCode);
            if (result.IsSuccess)
            {
                return Success(result.Data);
            }
            else
            {
                return Fail();
            }

        }

        [HttpPost("SubmitOrder")]
        public async Task<IActionResult> SubmitOrder([FromBody] SubmitOrderReq req)
        {
            var info = new OrderInfo
            {
                UserId = UserInfo != null ? UserInfo.UserId : 0,
                Items=req.Items,
                ReceiverName = req.ReceiverName,
                ShippingAddress = req.ShippingAddress,
                ReceiverPhone = req.ReceiverPhone,
                RecieveStore=req.RecieveStore,
                RecieveWay = req.RecieveWay,
                ShippingFee = req.ShippingFee,
                Email =req.Email
            };

            var result = await _orderService.GenerateOrder(info);
            if (result.IsSuccess)
            {
                return Success(result.Data);
            }
            else
            {
                return Fail();
            }
        }

        public class SubmitOrderReq
        {
            /// <summary>
            /// 訂單產品項目列表
            /// </summary>
            public List<OrderItem> Items { get; set; } = new List<OrderItem>();
            /// <summary>
            /// 訂單運費
            /// </summary>
            public decimal ShippingFee { get; set; }
            /// <summary>
            /// 訂單地址 (超商地址)
            /// </summary>
            public string ShippingAddress { get; set; }

            /// <summary>
            /// 收件門市
            /// </summary>
            public string RecieveStore { get; set; }


            public string RecieveWay { get; set; }


            /// <summary>
            /// 收件人姓名
            /// </summary>
            public string ReceiverName { get; set; }

            /// <summary>
            /// 收件人電話
            /// </summary>
            public string ReceiverPhone { get; set; }

            public string Email { get; set; }
        }

        public class GetOrdersReq
        {
            public string? query { get; set; }

        }



    }
}
