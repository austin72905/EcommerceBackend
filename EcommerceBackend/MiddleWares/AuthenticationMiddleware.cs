
using Domain.Enums;
using EcommerceBackend.Models;
using Infrastructure.Interfaces;

namespace EcommerceBackend.MiddleWares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRedisService _redisService;

        public AuthenticationMiddleware(RequestDelegate requestDelegate, IRedisService redisService)
        {
            _next= requestDelegate;
            _redisService= redisService;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            // /user/*  除了 /user/login 只要沒經過驗證都返回

            //bool isAuthenticated = true;
            string? sessionId=context.Request.Cookies["session-id"];
            //Console.WriteLine($"sessionId: {sessionId}");
            //Console.WriteLine($" context.Request.Path: {context.Request.Path}");
           

            string? userInfo= null;

            if (sessionId !=null)
            {
                userInfo = await _redisService.GetUserInfoAsync(sessionId);
                //Console.WriteLine($"Setting UserInfo in Middleware: {userInfo}");
            }


            var path = context.Request.Path.ToString().ToLower();

            if (path.StartsWith("/user/")  && !path.Contains("userlogin", StringComparison.OrdinalIgnoreCase) && !path.Contains("AuthLogin",StringComparison.OrdinalIgnoreCase) || path.StartsWith("/order/"))
            {
                if (sessionId == null)
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    var resp = new ApiResponse { Code = (int)RespCode.UN_AUTHORIZED, Message = "未授權，請重新登入" };
                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(resp));
                    return;
                }

                //userInfo = await _redisService.GetUserInfoAsync(sessionId);

                if (userInfo == null)
                {
                    //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    var resp = new ApiResponse { Code = (int)RespCode.UN_AUTHORIZED, Message = "Invalid session" };
                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(resp));
                    return;
                }
            }



            // 先無條件的是登錄狀態，後續在做redis 驗證
            context.Items["UserInfo"] = userInfo; // context.Items是 ASP.NET Core 中的一個字典，允許你在請求的生命週期內儲存和共享資料。這個字典中的資料只在目前請求中有效，隨著請求的結束，這些資料會被清除。
            //Console.WriteLine($"Setting UserInfo in Middleware: {context.Items["UserInfo"]}");
            await _next(context);
            return;
        }
    }
}
