
using Application;
using Application.Interfaces;
using Common.Interfaces.Infrastructure;
using EcommerceBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IRedisService _redisService;
        private readonly IOrderTimeoutProducer _orderTimeoutProducer;
        
        public OrderController(IOrderService orderService, IRedisService redisService, IOrderTimeoutProducer orderTimeoutProducer)
        {
            _orderService = orderService;
            _redisService = redisService;
            _orderTimeoutProducer = orderTimeoutProducer;
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
                return Fail(result.ErrorMessage);
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
     

        ///// <summary>
        ///// 快速測試延遲訊息（30秒延遲）
        ///// </summary>
        //[HttpPost("QuickTestDelayedMessage")]
        //public async Task<IActionResult> QuickTestDelayedMessage()
        //{
        //    try
        //    {
        //        var testOrderCode = $"QUICK_TEST_{DateTime.Now:yyyyMMddHHmmss}";
        //        var currentTime = DateTime.Now;
        //        var delaySeconds = 30;
        //        var expectedTime = currentTime.AddSeconds(delaySeconds);

        //        // 發送 30 秒延遲的測試訊息
        //        await _orderTimeoutProducer.SendOrderTimeoutMessageWithSecondsAsync(999, testOrderCode, delaySeconds);

        //        return Success(new { 
        //            Message = "快速測試延遲訊息已發送！",
        //            OrderCode = testOrderCode,
        //            UserId = 999,
        //            DelaySeconds = delaySeconds,
        //            SentAt = currentTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
        //            ExpectedProcessAt = expectedTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
        //            Note = $"請在 {delaySeconds} 秒內觀察日誌輸出，應該會看到接收和處理訊息的日誌"
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Fail($"發送快速測試延遲訊息失敗: {ex.Message}");
        //    }
        //}

        ///// <summary>
        ///// 診斷 RabbitMQ 連線和隊列狀態
        ///// </summary>
        //[HttpGet("DiagnoseRabbitMQ")]
        //public async Task<IActionResult> DiagnoseRabbitMQ()
        //{
        //    try
        //    {
        //        var connectionFactory = new RabbitMQ.Client.ConnectionFactory
        //        {
        //            HostName = "localhost" // 或從配置讀取
        //        };

        //        using var connection = await connectionFactory.CreateConnectionAsync();
        //        using var channel = await connection.CreateChannelAsync();

        //        var diagnostics = new
        //        {
        //            ConnectionStatus = "已連線",
        //            ServerVersion = connection.ServerProperties?.ContainsKey("version") == true 
        //                ? Encoding.UTF8.GetString((byte[])connection.ServerProperties["version"])
        //                : "未知",
        //            ExchangeExists = false,
        //            QueueExists = false,
        //            QueueMessageCount = 0,
        //            CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
        //        };

        //        try
        //        {
        //            // 檢查 Exchange 是否存在（被動聲明）
        //            await channel.ExchangeDeclarePassiveAsync("order.timeout.delayed");
        //            diagnostics = diagnostics with { ExchangeExists = true };
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Exchange check failed: {ex.Message}");
        //        }

        //        try
        //        {
        //            // 檢查 Queue 是否存在並獲取訊息數量
        //            var queueInfo = await channel.QueueDeclarePassiveAsync("order_timeout_queue");
        //            diagnostics = diagnostics with { 
        //                QueueExists = true, 
        //                QueueMessageCount = (int)queueInfo.MessageCount 
        //            };
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Queue check failed: {ex.Message}");
        //        }

        //        return Success(diagnostics);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Fail(new { 
        //            Error = ex.Message,
        //            ConnectionStatus = "連線失敗",
        //            Note = "請檢查 RabbitMQ 服務是否正在運行"
        //        });
        //    }
        //}

        public class TestDelayedMessageReq
        {
            public int UserId { get; set; }
            public string RecordCode { get; set; }
            public int? DelayMinutes { get; set; }
        }
    }
}
