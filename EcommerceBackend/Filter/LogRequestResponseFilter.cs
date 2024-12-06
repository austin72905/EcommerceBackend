using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog.Context;

namespace EcommerceBackend.Filter
{
    public class LogRequestResponseFilter : IActionFilter
    {
        private readonly ILogger<LogRequestResponseFilter> _logger;

        public LogRequestResponseFilter(ILogger<LogRequestResponseFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Items["RequestLog"] is not { } requestLog) return;

            var requestInfo = (dynamic)requestLog;

            var responseInfo = new
            {
                StatusCode = context.HttpContext.Response.StatusCode,
                Result = context.Result is ObjectResult objectResult? objectResult.Value: context.Result
            };



            var logData = new
            {
                Request = requestInfo,
                Response = responseInfo
            };

            _logger.LogInformation("Request-Response Log: {@LogData}", logData);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //LogContext.PushProperty("RequestId", Guid.NewGuid());
            var url = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
            var headers = context.HttpContext.Request.Headers.Select(header=> $"{header.Key}:{header.Value}");
            var cookies = context.HttpContext.Request.Cookies.Select(cookie => $"{cookie.Key}:{cookie.Value}");

            var actionArguments = context.ActionArguments;

            // 存储到 HttpContext.Items 中，供 OnActionExecuted 使用
            context.HttpContext.Items["RequestLog"] = new
            {
                Url = url,
                ActionArguments = actionArguments,
                Cookies = cookies,
                Headers = headers
            };

        }
    }
}
