using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController: ControllerBase
    {
        [HttpGet("GetCartContent")]
        public IActionResult GetCartContent()
        {
            return Content("ok");
        }

        [HttpPost("AddToCart")] // 登入時，
        public IActionResult AddToCart()
        {
            return Content("ok");
        }

        [HttpPost("ModifyCartContent")]
        public IActionResult ModifyCartContent()
        {
            return Content("ok");
        }

        [HttpDelete("RemoveFromCart")]
        public IActionResult RemoveFromCart()
        {
            return Content("ok");
        }
    }
}
