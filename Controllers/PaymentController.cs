using EcommerceBackend.Interfaces.Services;
using EcommerceBackend.Models;
using EcommerceBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;

        }

        //綠界支付
        [HttpGet("ECPayPayment")]
        public IActionResult ECPayPayment([FromQuery] PaymentRequestData requestData)
        {
            var result = _paymentService.PayRedirect(requestData);

            if (!result.IsSuccess || result.Data == null)
            {
                return Content("請求支付頁面異常，請聯繫管理員");
            }

            return AutoSubmitFormHtml(result.Data.PaymentData, result.Data.PaymentUrl);

        }

        //回調通知
        [HttpPost("ECPayReturn")]
        public IActionResult ECPayReturn()
        {
            var result = _paymentService.PayReturn();
            if (!result.IsSuccess || result.Data == null)
            {
                return Content("支付回調異常，請聯繫管理員");
            }
            return Ok("1|OK");
        }

        
    }
}
