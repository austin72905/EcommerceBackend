using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        
        [HttpGet("GetMemberList")]
        public IActionResult GetMemberList()
        {
            return Content("ok");
        }

        [HttpGet("GetUserInfo")]
        public IActionResult GetUserInfo()
        {
            return Content("ok");
        }

        // password
        [HttpPost("ModifyPassword")]
        public IActionResult ModifyPassword()
        {
            return Content("ok");
        }

        // address
        [HttpPost("ModifyDefaultShippingAddress")]
        public IActionResult ModifyDefaultShippingAddress()
        {
            return Content("ok");
        }

        [HttpPost("AddShippingAddress")]
        public IActionResult AddShippingAddress()
        {
            return Content("ok");
        }

    }
}
