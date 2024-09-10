using EcommerceBackend.Enums;
using EcommerceBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EcommerceBackend.Controllers
{
    public abstract class BaseController : ControllerBase
    {

        /// <summary>
        /// 是否已驗證 (登陸)
        /// </summary>
        protected bool IsAuthenticated => HttpContext.Items.ContainsKey("IsAuthenticated") && Convert.ToBoolean(HttpContext.Items["IsAuthenticated"]);

        [FromHeader(Name = "ALP-User-Id")]
        protected string? UserId { get; set; }



        protected UserInfoDTO? UserInfo
        {
            get
            {

                string? userInfo = HttpContext.Items.ContainsKey("UserInfo") ? HttpContext.Items["UserInfo"] as string : null;

                if (userInfo == null)
                {
                    return null;

                }

                return JsonSerializer.Deserialize<UserInfoDTO>(userInfo);


            }

        }


        protected string? SessionId
        {
            get
            {
                string? sessionId = Request.Cookies["session-id"];
                return sessionId;
            }

        }


        protected OkObjectResult Success(object? data = null)
        {
            var response = new ApiResponse()
            {
                Code = (int)RespCode.SUCCESS,
                Message = "請求成功",
                Data = data
            };

            return Ok(response);
        }

        protected OkObjectResult UnAuthorized()
        {
            var response = new ApiResponse()
            {
                Code = (int)RespCode.UN_AUTHORIZED,
                Message = "未驗證，請先登入",
            };

            return Ok(response);
        }

        protected OkObjectResult Fail(object? data = null, string msg = "")
        {
            var response = new ApiResponse()
            {
                Code = (int)RespCode.FAIL,
                Message = "請求失敗，請聯繫管理員",
                Data = data
            };

            if (!string.IsNullOrEmpty(msg))
            {
                response.Message = msg;
            }


            return Ok(response);
        }
    }
}
