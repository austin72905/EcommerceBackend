
using Application;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentController> _logger;
        
        public PaymentController(IPaymentService paymentService, IConfiguration configuration, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _configuration = configuration;
            _logger = logger;
        }

        //支付請求（調用 ec-payment-service）
        [HttpGet("ECPayPayment")]
        public async Task<IActionResult> ECPayPayment([FromQuery] PaymentRequestData requestData)
        {
            var result = await _paymentService.PayRedirect(requestData);

            if (!result.IsSuccess || result.Data == null)
            {
                return Fail(msg: result.ErrorMessage ?? "支付請求失敗");
            }

            // 返回 JSON 響應，包含支付資訊
            var recordNo = result.Data.PaymentData.ContainsKey("RecordNo") ? result.Data.PaymentData["RecordNo"] : requestData.RecordNo;
            var paymentId = result.Data.PaymentData.ContainsKey("PaymentID") ? result.Data.PaymentData["PaymentID"] : "";
            var status = result.Data.PaymentData.ContainsKey("Status") ? result.Data.PaymentData["Status"] : "Pending";
            var message = result.Data.PaymentData.ContainsKey("Message") ? result.Data.PaymentData["Message"] : "支付請求已接收";

            return Success(new
            {
                recordNo = recordNo,
                paymentId = paymentId,
                status = status,
                message = message
            });
        }

        //回調通知
        [HttpPost("ECPayReturn")]
        public async Task<IActionResult> ECPayReturn()
        {
            // 記錄接收到的表單資料（用於調試）
            var formData = Request.Form;
            var formDataDict = formData.Keys.ToDictionary(k => k, k => formData[k].ToString());
            
            _logger.LogInformation("收到支付回調請求，表單資料: {FormData}", 
                string.Join(", ", formDataDict.Select(kv => $"{kv.Key}={kv.Value}")));
            
            var result = await _paymentService.PayReturn();
            if (!result.IsSuccess || result.Data == null)
            {
                _logger.LogWarning("支付回調處理失敗: {ErrorMessage}", result.ErrorMessage);
                if (result.ErrorMessage == "RETRY_LATER")
                {
                    Response.Headers["Retry-After"] = "2";
                    return StatusCode(StatusCodes.Status429TooManyRequests, "請稍後重試");
                }
                return Fail(msg: result.ErrorMessage ?? "支付回調處理失敗");
            }
            
            _logger.LogInformation("支付回調處理成功");
            // 返回成功 JSON 響應
            return Success("OK");
        }

        
    }
}
