using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController: BaseController
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
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


        [HttpPost("MergeCartContent")]
        public async Task<IActionResult> MergeCartContent([FromBody] CartDTO cartDto)
        {
            int userid = UserInfo != null ? UserInfo.UserId : 0;
            if (UserInfo == null)
            {
                return Fail("請重新登入");

            }
            var result =await _cartService.MergeCartContent(userid, cartDto.Items,cartDto.IsCover);

            if (!result.IsSuccess)
            {
                return Fail(msg:result.ErrorMessage);
            }

            return Success(result.Data);
        }
    }
}
