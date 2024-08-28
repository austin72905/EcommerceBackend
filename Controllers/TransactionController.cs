using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController: ControllerBase
    {
        [HttpPost("SubmitOrder")]
        public IActionResult SubmitOrder()
        {
            return Content("ok");
        }

        [HttpPost("ModifyOrder")]
        public IActionResult ModifyOrder()
        {
            return Content("ok");
        }
    }
}
