using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController: ControllerBase
    {
        [HttpGet("GetOrders")]
        public IActionResult GetOrders()
        {
            return Content("ok");
        }
    }
}
