using EcommerceBackend.Interfaces.Services;
using EcommerceBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            var address = _userService.GetUserShippingAddress(UserId);
            return Ok(address);
        }

        [HttpPost("ModifyDefaultShippingAddress")]
        public IActionResult ModifyDefaultShippingAddress([FromBody] UserShipAddressDTO address)
        {
            var msg = _userService.ModifyUserShippingAddress(UserId, address);
            return Ok(msg);
        }

        [HttpPost("AddShippingAddress")]
        public IActionResult AddShippingAddress([FromBody] UserShipAddressDTO address)
        {
            var msg = _userService.AddUserShippingAddress(UserId, address);
            return Ok(msg);
        }

        [HttpDelete("DeleteShippingAddress")]
        public IActionResult DeleteShippingAddress([FromBody] UserShipAddressDTO address)
        {
            var msg = _userService.DeleteUserShippingAddress(UserId, address.AddressId);
            return Ok(msg);
        }

    }
}
