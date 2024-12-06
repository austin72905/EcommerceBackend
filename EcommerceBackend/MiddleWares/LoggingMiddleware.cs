using Serilog.Context;

namespace EcommerceBackend.MiddleWares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            // 為當前請求生成 RequestId
            var requestId = Guid.NewGuid();

            // 使用 LogContext.PushProperty 将 RequestId 添加到日志上下文
            using (LogContext.PushProperty("RequestId", requestId))
            {
                // 可选：将 RequestId 添加到 HttpContext.Items，供其他地方使用
                //context.Items["RequestId"] = requestId;

                // 调用下一个中间件
                await _next(context);

            }
        }

    }
}
