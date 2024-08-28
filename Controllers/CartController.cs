using EcommerceBackend.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController: ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("GetCartContent")]
        public IActionResult GetCartContent([FromQuery] int userid)
        {
            var cartcontent = _cartService.GetCartContent(userid);
            return Ok(cartcontent);
        }

        [HttpPost("AddToCart")] // 登入時，
        public IActionResult AddToCart([FromQuery] string? userid)
        {
            return Content("ok");
        }

        [HttpPost("ModifyCartContent")]
        public IActionResult ModifyCartContent([FromQuery] string? userid)
        {
            return Content("ok");
        }

        [HttpDelete("RemoveFromCart")]
        public IActionResult RemoveFromCart([FromQuery] string? userid)
        {
            return Content("ok");
        }
    }
}
