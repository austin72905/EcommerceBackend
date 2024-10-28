using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController: ControllerBase
    {
        [HttpGet("GetOrders")]
        public IActionResult GetOrders()
        {
            return Content("ok");
        }
    }
}
