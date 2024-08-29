using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    public abstract class BaseController: ControllerBase
    {
        
        /// <summary>
        /// 是否已驗證 (登陸)
        /// </summary>
        protected bool IsAuthenticated => HttpContext.Items.ContainsKey("IsAuthenticated") && Convert.ToBoolean(HttpContext.Items["IsAuthenticated"]);

        [FromHeader(Name = "ALP-User-Id")]
        protected string? UserId { get; set; }
    }
}
