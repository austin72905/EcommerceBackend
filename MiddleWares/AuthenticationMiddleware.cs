namespace EcommerceBackend.MiddleWares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate requestDelegate)
        {
            _next= requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 先無條件的是登錄狀態，後續在做redis 驗證
            context.Items["IsAuthenticated"] = true;  // context.Items是 ASP.NET Core 中的一個字典，允許你在請求的生命週期內儲存和共享資料。這個字典中的資料只在目前請求中有效，隨著請求的結束，這些資料會被清除。
            await _next(context);
            return;
        }
    }
}
