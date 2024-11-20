
using Application.DTOs;
using Domain.Enums;
using EcommerceBackend.Models;
using Infrastructure.Interfaces;
using System.Linq;
using System.Text.Json;

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

            // 去 redis 獲取對應用戶資料
            if (sessionId !=null)
            {
                userInfo = await _redisService.GetUserInfoAsync(sessionId);
                //Console.WriteLine($"Setting UserInfo in Middleware: {userInfo}");
            }


            // 比對需要登陸才能使用的路徑
            var path = context.Request.Path.ToString().ToLower();

            if (HaveAuthPath(path))
            {
                
                if (sessionId==null || userInfo == null)
                {
                    //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    var resp = new ApiResponse { Code = (int)RespCode.UN_AUTHORIZED, Message = "未授權，請重新登入" };
                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(resp));
                    return;
                }



                // csrf token 檢查
                // 只對 POST、PUT、DELETE 請求進行 CSRF 檢查
                if (context.Request.Method == HttpMethods.Post ||
                    context.Request.Method == HttpMethods.Put ||
                    context.Request.Method == HttpMethods.Delete)
                {
                    // 從請求標頭中獲取 CSRF Token
                    var csrfToken = context.Request.Headers["X-CSRF-Token"].FirstOrDefault();

                    // 從服務端會話或 Redis 中獲取存儲的 CSRF Token
                    var sessionCsrfToken = context.Request.Cookies["X-CSRF-Token"]; // 假設會話中存儲 Token

                    if (string.IsNullOrEmpty(csrfToken) || csrfToken != sessionCsrfToken)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("Invalid CSRF Token");
                        return;
                    }
                }

            }




            context.Items["UserInfo"] = userInfo; // context.Items是 ASP.NET Core 中的一個字典，允許你在請求的生命週期內儲存和共享資料。這個字典中的資料只在目前請求中有效，隨著請求的結束，這些資料會被清除。
                                                  //Console.WriteLine($"Setting UserInfo in Middleware: {context.Items["UserInfo"]}");


            

            await _next(context);
            return;
        }

       
        /// <summary>
        /// 需要驗證的路由
        /// </summary>

        private static Dictionary<string, HashSet<string>> AuthPathRules = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase)
        {
            { "/user", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "/user/UserRegister", "/user/UserLogin", "/user/AuthLogin" } },
            { "/order", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { } } // 無排除路径
        };

        internal bool HaveAuthPath(string path)
        {
            foreach (var rule in AuthPathRules)
            {
                
                if (path.StartsWith(rule.Key, StringComparison.OrdinalIgnoreCase))
                {
                    
                    if (rule.Value.Contains(path))
                    {
                        return false; 
                    }
                    return true; 
                }
            }

            return false; 
        }


    }
}
