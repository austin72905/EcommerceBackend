using Application.DTOs;
using Domain.Enums;
using EcommerceBackend.Models;

using Microsoft.AspNetCore.Mvc;
using System.Text;
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



        protected  UserInfoDTO? UserInfo
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

        /// <summary>
        /// 返回自動提交的表單給用戶
        /// </summary>
        /// <param name="url"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        protected ContentResult AutoSubmitFormHtml(IEnumerable<KeyValuePair<string, string>> keyValues, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<form action='" + url + $"' method='POST'>");
            foreach (KeyValuePair<string, string> kv in keyValues)
            {
                sb.Append("<input type='hidden' name='" + kv.Key + "' value='" + kv.Value + "'/>");
            }
            sb.Append("</form>");
            sb.Append("<script>document.forms[0].submit();</script>");
            string html = sb.ToString();

            return Content(html, "text/html", Encoding.UTF8);
        }
    }
}
