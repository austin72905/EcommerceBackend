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
            
            if (UserInfo == null)
            {

                return UnAuthorized();
            }

            var result= _orderService.GetOrders(UserInfo.UserId);
            if (result.IsSuccess)
            {
                return Success(result.Data);
            }
            else
            {
                return Fail();
            }
            
        }
    }
}
