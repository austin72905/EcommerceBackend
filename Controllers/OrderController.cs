using EcommerceBackend.Interfaces.Services;
using EcommerceBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController: BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IRedisService _redisService;
        public OrderController(IOrderService orderService,IRedisService redisService) 
        {
            _orderService=orderService;
            _redisService=redisService;
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            ApiResponse resp;
            if (SessionId == null)
            {
                resp = UnAuthorized();
                return Ok(resp);
            }

            string? userId = await _redisService.GetUserInfoAsync(SessionId);

            if(userId == null)
            {
                resp = UnAuthorized();
                return Ok(resp);
            }

            var orderList=_orderService.GetOrders(userId);
            resp=Success(orderList);
            return Ok(resp);
        }
    }
}
