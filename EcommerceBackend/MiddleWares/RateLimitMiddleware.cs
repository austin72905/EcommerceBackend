using Common.Interfaces.Infrastructure;
using Infrastructure.Cache;

namespace EcommerceBackend.MiddleWares
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRedisService _redisService;

        public RateLimitMiddleware(RequestDelegate next, IRedisService redisService)
        {
            _next = next;
            _redisService = redisService;
        }


        public async Task InvokeAsync(HttpContext context)
        {

            var userInfo = context.Items["UserInfo"];
            Console.WriteLine($"UserInfo: {userInfo}");
            // 取得 userId 或 fallback 用戶 IP（防止未登入共用一個限流 key）
            var userId = context.User?.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                userId = context.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                      ?? context.Connection.RemoteIpAddress?.ToString()
                      ?? "unknown";
            }

            // 取得 API 路徑 
            var apiKey = ExtractApiKey(context.Request.Path);

            bool isAllowed = await _redisService.IsRateLimitExceededAsync(userId, apiKey);

            if (!isAllowed)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded");
                return;
            }

            await _next(context);
        }


        /// <summary>
        /// 擷取api 路徑
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string ExtractApiKey(PathString path)
        {
            var parts = path.Value?.TrimStart('/').Split('/');

            // 預期格式：/{Controller}/{Action}
            if (parts != null && parts.Length >= 2)
            {
                return $"{parts[0].ToLowerInvariant()}/{parts[1].ToLowerInvariant()}"; // e.g. product/getrecommendationproduct
            }

            return "default"; // fallback
        }



    }
}
