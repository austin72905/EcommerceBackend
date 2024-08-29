using EcommerceBackend.Interfaces.Services;
using EcommerceBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("GetMemberList")]
        public IActionResult GetMemberList()
        {
            return Content("ok");
        }


        [HttpPost("UserLogin")]
        public IActionResult UserLogin() 
        {
            return Content("ok");
        }


        [HttpPost("UserLogout")]
        public IActionResult UserLogout()
        {
            return Content("ok");
        }

        [HttpGet("GetUserInfo")]
        public IActionResult GetUserInfo()
        {
            var user = _userService.GetUserInfo(UserId);

            return Ok(user);
        }

        // password
        [HttpPost("ModifyPassword")]
        public IActionResult ModifyPassword()
        {
            return Content("ok");
        }



        // address
        [HttpGet("GetUserShippingAddress")]
        public IActionResult GetUserShippingAddress()
        {
            var address = _userService.GetUserAddress(UserId);
            return Ok(address);
        }

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
