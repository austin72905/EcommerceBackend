
using Application;
using Application.DTOs;
using Application.Interfaces;
using Application.Oauth;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRedisService _redisService;

        public UserController(IUserService userService, IRedisService redisService)
        {
            _userService = userService;
            _redisService = redisService;
        }

        [HttpGet("GetMemberList")]
        public IActionResult GetMemberList()
        {
            return Content("ok");
        }

        [HttpPost("UserRegister")]
        public IActionResult UserRegister()
        {
            return Content("ok");
        }

        [HttpPost("AuthLogin")]
        public async Task<IActionResult> AuthLogin([FromBody] AuthLogin authLogin)
        {

            var result = await _userService.UserAuthLogin(authLogin);

            if (result.IsSuccess)
            {
                if (result.UserInfo != null)
                {
                    string sessionId=await SaveUserInfoToRedis(result.UserInfo);

                    // SameSite 只要設為none，Secure 就必須為true
                    // https 會造成 無法寫給前端http cookie
                    var cookieOption = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTime.Now.AddHours(2),
                        //SameSite = SameSiteMode.None,
                        Secure = false,
                        //Domain = "localhost"
                    };

                    var cookieOption2 = new CookieOptions
                    {
                        HttpOnly = false,
                        Expires = DateTime.Now.AddHours(2),
                        //SameSite = SameSiteMode.None,
                        Secure = false,
                        //Domain= "localhost"
                    };

                    Response.Cookies.Append("session-id", sessionId, cookieOption);
                    Response.Cookies.Append("has-session-id", "true", cookieOption2);
                }
               
                var loginResponse = new LoginReponse { UserInfo = result.UserInfo, RedirectUrl = authLogin.redirect_url };

                return Success(loginResponse);

            }
            else
            {
                
                var loginResponse = new LoginReponse { UserInfo = null, RedirectUrl = authLogin.redirect_url };
                return Fail(loginResponse);

            }


        }


        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin()
        {
            //var resp = await _userService.GetIDTokenFromGoogle();


            return Ok("ok");
        }


        [HttpGet("UserLogout")]
        public async Task<IActionResult> UserLogout()
        {
            var sessionId = Request.Cookies["session-id"];

            if (sessionId != null)
            {
                await _redisService.DelUserInfoAsync(sessionId);
            }
            

           
            Response.Cookies.Delete("session-id");
            Response.Cookies.Delete("has-session-id");

            var resp = Success();
            return Ok(resp);
        }

        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            UserInfoDTO? user=null;
     
            
            
            // 去redis緩存找
            if (SessionId != null)
            {
                var userInfo = await _redisService.GetUserInfoAsync(SessionId);

                if (userInfo == null)
                {
                    //user = _userService.GetUserInfo(UserId);
                    return Fail("請重新登入");
               
                }
                else
                {
                    user = JsonSerializer.Deserialize<UserInfoDTO>(userInfo);
                }
            }
            else
            {
                return Fail("請重新登入");

            }

            return Success(user);

            
        }

        [HttpPost("ModifyUserInfo")]
        public async Task<IActionResult> ModifyUserInfo([FromBody] UserInfoDTO user)
        {
                     
            

            int userid = UserInfo != null ? UserInfo.UserId : 0;
            if (UserInfo == null)
            {
                return Fail("請重新登入");

            }

            // 去資料庫修改，修改完，要再修改redis
            user.UserId = userid;
            var result = await _userService.ModifyUserInfo(user,SessionId);

            return Success(result.Data);


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

            int userid = UserInfo != null ? UserInfo.UserId : 0;
            if (UserInfo == null)
            {
                return Fail("請重新登入");
              
            }

            var result = _userService.GetUserShippingAddress(userid);
            return Success(result.Data);
         
        }

        [HttpPost("SetDefaultShippingAddress")]
        public IActionResult SetDefaultShippingAddress([FromBody] UserShipAddressDTO address)
        {

            int userid = UserInfo != null ? UserInfo.UserId : 0;
            if (UserInfo == null)
            {
                return Fail("請重新登入");

            }
            var result = _userService.SetDefaultShippingAddress(UserInfo.UserId, address.AddressId);
            return Success(result.Data);

        }

        [HttpPost("ModifyShippingAddress")]
        public IActionResult ModifyShippingAddress([FromBody] UserShipAddressDTO address)
        {

            int userid = UserInfo != null ? UserInfo.UserId : 0;
            if (UserInfo == null)
            {
                return Fail("請重新登入");
             
            }
            var result = _userService.ModifyUserShippingAddress(UserInfo.UserId, address);
            return Success(result.Data);
            
        }

        [HttpPost("AddShippingAddress")]
        public IActionResult AddShippingAddress([FromBody] UserShipAddressDTO address)
        {

            int userid = UserInfo != null ? UserInfo.UserId : 0;
            if (UserInfo == null)
            {
                return Fail("請重新登入");
           
            }
            var result = _userService.AddUserShippingAddress(UserInfo.UserId, address);
            return Success(result.Data);
  
        }

        [HttpDelete("DeleteShippingAddress")]
        public IActionResult DeleteShippingAddress([FromBody] UserShipAddressDTO address)
        {

            int userid = UserInfo != null ? UserInfo.UserId : 0;
            if (UserInfo == null)
            {
                return Fail("請重新登入");

            }
            var result = _userService.DeleteUserShippingAddress(UserInfo.UserId, address.AddressId);
            return Success(result.Data);

        }

        [HttpPost("AddTofavoriteList")]
        public async Task<IActionResult> AddTofavoriteList([FromBody] favoriteRequest req)
        {
            int userid = UserInfo != null ? UserInfo.UserId : 0;

            var result = await _userService.AddTofavoriteList(userid, req.ProductId);

            return Success(result.Data);
        }


        [HttpPost("RemoveFromfavoriteList")]
        public async Task<IActionResult> RemoveFromfavoriteList([FromBody] favoriteRequest req)
        {
            int userid = UserInfo != null ? UserInfo.UserId : 0;

            var result = await _userService.RemoveFromfavoriteList(userid, req.ProductId);

            return Success(result.Data);
        }



        private async Task<string> SaveUserInfoToRedis(UserInfoDTO userInfo)
        {
            string guid = Guid.NewGuid().ToString();

            string result = JsonSerializer.Serialize(userInfo);

            await _redisService.SetUserInfoAsync(guid, result);

            return guid;
        }

        public class favoriteRequest
        {
            public int ProductId { get; set; }
        }


    }
}
