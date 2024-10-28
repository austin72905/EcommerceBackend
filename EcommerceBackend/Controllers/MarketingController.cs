using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class MarketingController: ControllerBase
    {
        [HttpGet("GetVouchers")]
        public IActionResult GetVouchers()
        {
            return Content("ok");
        }

        [HttpGet("GetActivities")]
        public IActionResult GetActivities()
        {
            return Content("ok");
        }
    }
}
