
using Application.DTOs;
using Application.Interfaces;
using Common.Interfaces.Infrastructure;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Web;

namespace Application.Services
{
    public class PaymentService : BaseService<PaymentService>,IPaymentService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IShipmentProducer _shipmentProducer;
        private readonly IOrderStateProducer _orderStateProducer;
        private readonly IEncryptionService _encryptionService;
        private readonly IInventoryService _inventoryService;
        private readonly IHttpUtils _httpUtils;

        private readonly IConfiguration _configuration;
        public PaymentService(
            IPaymentRepository paymentRepository, 
            IHttpContextAccessor contextAccessor, 
            IShipmentProducer shipmentProducer,
            IOrderStateProducer orderStateProducer,
            IEncryptionService encryptionService, 
            IInventoryService inventoryService,
            IConfiguration configuration,
            IHttpUtils httpUtils,
            ILogger<PaymentService> logger):base(logger)
        {
            _paymentRepository = paymentRepository;
            _contextAccessor = contextAccessor;
            _shipmentProducer = shipmentProducer;
            _orderStateProducer = orderStateProducer;
            _encryptionService = encryptionService;
            _inventoryService = inventoryService;
            _configuration = configuration;
            _httpUtils = httpUtils;
        }

        /// <summary>
        /// 支付網關
        /// </summary>
        public string ECPayCreditPaymentUrl { get; set; } = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";




        public async Task<ServiceResult<PaymentInfomation>> PayRedirect(PaymentRequestData requestData)
        {
            try
            {
                // 驗證訂單號
                if (!ValidRecordNo(requestData.RecordNo))
                {
                    return Fail<PaymentInfomation>("訂單不合法");
                }

                // 獲取配置
                var config = await _paymentRepository.GetTenantConfig(requestData.RecordNo);

                if (config == null)
                {
                    return Fail<PaymentInfomation>("請求配置失敗");
                }

                // 驗證金額
                var orderAmount = Convert.ToInt32(config.PaymentAmount).ToString();
                if (!ValidOrderAmount(orderAmount, requestData.Amount))
                {
                    return Fail<PaymentInfomation>("金額匹配錯誤");
                }

                // 構建回調 URL（優先使用配置的 ReturnURL，否則從當前請求構建）
                var callbackUrl = _configuration["AppSettings:ReturnURL"];
                if (string.IsNullOrEmpty(callbackUrl))
                {
                    var baseUrl = $"{_contextAccessor.HttpContext?.Request.Scheme}://{_contextAccessor.HttpContext?.Request.Host}";
                    callbackUrl = $"{baseUrl}/Payment/ECPayReturn";
                }

                // 獲取 ClientBackURL（支付完成後跳轉的前端頁面）
                var clientBackUrl = _configuration["AppSettings:ClientBackURL"];
                if (string.IsNullOrEmpty(clientBackUrl))
                {
                    clientBackUrl = "http://localhost:3000/"; // 預設值
                }

                // 調用 ec-payment-service API
                var paymentServiceUrl = _configuration["AppSettings:PaymentServiceUrl"] ?? "http://localhost:8081";
                var apiUrl = $"{paymentServiceUrl}/api/payment/process";

                var paymentRequest = new
                {
                    recordNo = requestData.RecordNo,
                    amount = requestData.Amount,
                    payType = requestData.PayType,
                    callbackUrl = callbackUrl,
                    clientBackUrl = clientBackUrl
                };

                try
                {
                    _logger.LogInformation("準備發送支付請求到 ec-payment-service，訂單號: {RecordNo}, 金額: {Amount}, 支付類型: {PayType}, 回調URL: {CallbackUrl}", 
                        requestData.RecordNo, requestData.Amount, requestData.PayType, callbackUrl);
                    
                    var response = await _httpUtils.PostJsonAsync<PaymentServiceResponse>(apiUrl, paymentRequest);
                    
                    _logger.LogInformation("支付請求已發送到 ec-payment-service，訂單號: {RecordNo}, 支付ID: {PaymentID}, 返回的訂單號: {ResponseRecordNo}", 
                        requestData.RecordNo, response?.PaymentID, response?.RecordNo);

                    // 回傳支付請求已接收的確認
                    return Success<PaymentInfomation>
                    (
                        new PaymentInfomation
                        {
                            PaymentData = new Dictionary<string, string>
                            {
                                { "PaymentID", response?.PaymentID ?? "" },
                                { "RecordNo", response?.RecordNo ?? requestData.RecordNo },
                                { "Status", response?.Status ?? "Pending" },
                                { "Message", response?.Message ?? "支付請求已接收" }
                            },
                            PaymentUrl = "" // 不再需要支付表單 URL
                        }
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "調用 ec-payment-service 失敗，訂單號: {RecordNo}", requestData.RecordNo);
                    return Error<PaymentInfomation>($"調用支付服務失敗: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "處理支付請求時發生錯誤，訂單號: {RecordNo}", requestData?.RecordNo);
                return Error<PaymentInfomation>(ex.Message);
            }
        }

        // 支付服務回應模型
        private class PaymentServiceResponse
        {
            public string PaymentID { get; set; } = "";
            public string RecordNo { get; set; } = "";
            public string Status { get; set; } = "";
            public string Message { get; set; } = "";
        }


        /// <summary>
        /// 驗證訂單是否合法
        /// </summary>
        /// <param name="recordCode"></param>
        /// <returns></returns>
        private bool ValidRecordNo(string recordCode)
        {
            return true;
        }

        /// <summary>
        /// 校驗訂單金額
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private bool ValidOrderAmount(string orderAmount, string requestAmount)
        {
            return orderAmount == requestAmount;
        }


        /// <summary>
        /// 準備加密需要的資料
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> PrepareSignData(TenantConfigDTO tenantConfig, PaymentRequestData requestData)
        {
            var data = new Dictionary<string, string>()
            {
                // 特店編號
                { "MerchantID",tenantConfig.MerchantId },
                // 特店訂單編號均為唯一值，不可重複使用
                { "MerchantTradeNo",requestData.RecordNo },
                // yyyy/MM/dd HH:mm:ss
                { "MerchantTradeDate",DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },
                // 請固定填入 aio
                { "PaymentType","aio" },
                // 交易金額
                { "TotalAmount",tenantConfig.Amount },
                // 交易描述
                { "TradeDesc","credit card pay" },
                // 商品名稱
                { "ItemName","item" },
                // 付款完成通知回傳網址
                { "ReturnURL",_configuration["AppSettings:ReturnURL"] },
                // 選擇預設付款方式
                { "ChoosePayment","Credit" },
                // CheckMacValue加密類型 (請固定填入1，使用SHA256加密。)
                { "EncryptType","1" },
                // 消費者點選此按鈕後，會將頁面導回到此設定的網址
                { "ClientBackURL",_configuration["AppSettings:ClientBackURL"]},

            };

            return data;
        }

        /// <summary>
        /// 生成簽名值
        /// </summary>
        /// <param name="keyValues"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        private string GenerateSign(Dictionary<string, string> keyValues, string key, string iv)
        {
            string rawString = $"HashKey={key}&" + string.Join("&", keyValues.OrderBy(o => o.Key, StringComparer.Ordinal).Select(o => $"{o.Key}={o.Value}")) + $"&HashIV={iv}";

            string sign = _encryptionService.Sha256Hash(HttpUtility.UrlEncode(rawString).ToLower()).ToUpper();

            return sign;
        }

        /// <summary>
        /// 加入未簽名的資料
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <param name="sign"></param>
        private void AddNoneSignData(Dictionary<string, string> keyValuePairs, string sign)
        {
            keyValuePairs.Add("CheckMacValue", sign);
        }



        #region 支付回調

        private decimal Amount => Convert.ToDecimal(ReqData["TradeAmt"]);

        private string RecordCode => ReqData["MerchantTradeNo"].ToString();

        private string TradeSuccessStatus = "1"; // 若回傳值為1時，為付款成功，  其餘代碼皆為交易異常，請至廠商管理後台確認後再出貨。

        private string ReturnStatus => ReqData["RtnCode"].ToString();//支付狀態 成功


        // 是否為模擬出款  1：代表此交易為模擬付款，RtnCode也為1。並非是由消費者實際真的付款，所以綠界也不會撥款給廠商，請勿對該筆交易做出貨等動作，以避免損失。
        private string SimulatePaid => ReqData["SimulatePaid"].ToString();


        private Dictionary<string, string>? _reqData;

        public Dictionary<string, string> ReqData
        {
            get
            {
                if (_reqData == null)
                {
                    _reqData = ParseFormData<Dictionary<string, string>>();
                }


                return _reqData ?? new Dictionary<string, string>();
            }
        }

        public async Task<ServiceResult<object>> PayReturn()
        {
            try
            {
                _logger.LogInformation("收到支付回調，訂單號: {RecordCode}", RecordCode);

                //檢查是否交易成功
                if (!IsSuccessfulTransaction())
                {
                    _logger.LogWarning("支付回調交易失敗，訂單號: {RecordCode}", RecordCode);
                    return await TransactionFail();
                }

                //請求配置
                var tenantConfig = await _paymentRepository.GetTenantConfig(recordCode: RecordCode);

                if (tenantConfig == null)
                {
                    _logger.LogError("獲取租戶配置失敗，訂單號: {RecordCode}", RecordCode);
                    return Fail<object>("請求配置失敗");
                    
                }

                //驗證回調簽名
                if (!VerifySign(tenantConfig.TenantConfig.SecretKey, tenantConfig.TenantConfig.HashIV))
                {
                    _logger.LogWarning("支付回調簽名驗證失敗，訂單號: {RecordCode}", RecordCode);
                    return SignVerificationFailed();
                }

                _logger.LogInformation("支付回調簽名驗證成功，開始處理交易成功邏輯，訂單號: {RecordCode}", RecordCode);

                return await TransactionSuccess();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "處理支付回調時發生錯誤，訂單號: {RecordCode}", RecordCode);
                return Error<object>(ex.Message);
                
            }

        }


        /// <summary>
        /// 檢查回調是否成功
        /// </summary>
        /// <returns></returns>
        private bool IsSuccessfulTransaction()
        {
            bool transOK = string.Equals(ReturnStatus, TradeSuccessStatus, StringComparison.OrdinalIgnoreCase);

            return transOK;
        }


        /// <summary>
        /// 驗證回調簽名
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool VerifySign(string key, string iv)
        {
            string rawString = $"HashKey={key}&" + string.Join("&", ReqData
                .Where(o => !o.Key.Contains("CheckMacValue"))
                .OrderBy(o => o.Key, StringComparer.Ordinal)
                .Select(o => $"{o.Key}={o.Value}")) + $"&HashIV={iv}";

            string returnSign = ReqData["CheckMacValue"].ToString();

            string encoded = HttpUtility.UrlEncode(rawString).ToLower();
            string sign = _encryptionService.Sha256Hash(encoded).ToUpper();
            
            // 添加調試日誌
            _logger.LogInformation("簽名驗證 - 原始字串: {RawString}", rawString);
            _logger.LogInformation("簽名驗證 - URL編碼後: {Encoded}", encoded);
            _logger.LogInformation("簽名驗證 - 計算的簽名: {CalculatedSign}", sign);
            _logger.LogInformation("簽名驗證 - 接收的簽名: {ReceivedSign}", returnSign);
            
            bool isValid = string.Equals(sign, returnSign, StringComparison.OrdinalIgnoreCase);
            _logger.LogInformation("簽名驗證結果: {IsValid}", isValid);
            
            return isValid;
        }

        /// <summary>
        /// 轉換form data 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T? ParseFormData<T>() where T : class, new()
        {
            var Request = _contextAccessor.HttpContext?.Request;

            if (Request?.Form?.Count > 0)
            {
                T data = new T();

                Type t = data.GetType();
                bool isDict = t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Dictionary<,>);

                if (isDict)
                {

                    Type valueType = t.GetGenericArguments()[1];

                    IDictionary? dic = data as IDictionary;

                    foreach (string fkey in Request.Form.Keys)
                    {
                        if (string.IsNullOrEmpty(fkey)) continue;
                        string? fval = Request.Form[fkey];
                        if (fval == null) continue;
                        dic?.Add(fkey, Convert.ChangeType(fval, valueType));
                    }

                    return data;
                }

                foreach (System.Reflection.PropertyInfo prop in typeof(T).GetProperties())
                {
                    string? val = Request.Form[prop.Name];

                    if (val != null)
                    {
                        prop.SetValue(data, Convert.ChangeType(val, prop.PropertyType));
                    }
                }

                return data;
            }

            return null;
        }

        private async Task<ServiceResult<object>> TransactionFail()
        {
            // 修改資料庫狀態
            var payment = await _paymentRepository.GetPaymentRecord(recordCode: RecordCode);

            if (payment != null)
            {
                // 使用 Payment 的業務方法標記為已取消
                payment.MarkAsCanceled();
                // 使用 Order 的業務方法取消訂單
                payment.Order.Cancel();

                await _paymentRepository.SaveChangesAsync();

                // 付款失敗後，回滾庫存
                var rollbackResult = await _inventoryService.RollbackInventoryAsync(RecordCode);
                if (!rollbackResult.IsSuccess)
                {
                    _logger.LogWarning("付款失敗後回滾庫存失敗，訂單編號: {RecordCode}, 錯誤: {Error}", 
                        RecordCode, rollbackResult.ErrorMessage);
                    // 即使回滾失敗，也繼續處理
                }
            }

            return Fail<object>("交易失敗");
            
        }


        private ServiceResult<object> SignVerificationFailed()
        {
            return Fail<object>("驗簽失敗");
            
        }

        private async Task<ServiceResult<object>> TransactionSuccess()
        {
            _logger.LogInformation("開始處理交易成功邏輯，訂單號: {RecordCode}", RecordCode);

            // 修改資料庫狀態
            var payment = await _paymentRepository.GetPaymentRecord(recordCode: RecordCode);

            if (payment == null)
            {
                _logger.LogError("找不到支付記錄，訂單號: {RecordCode}", RecordCode);
                return Fail<object>("找不到支付記錄");
            }

            _logger.LogInformation("找到支付記錄，訂單號: {RecordCode}, 支付ID: {PaymentId}, 當前支付狀態: {PaymentStatus}, 訂單狀態: {OrderStatus}", 
                RecordCode, payment.Id, payment.PaymentStatus, payment.Order.Status);

            // 檢查是否已經有更新過數據，避免重複回調
            if(payment.PaymentStatus != (byte)OrderStepStatus.PaymentReceived)
            {
                _logger.LogInformation("開始標記為已付款，訂單號: {RecordCode}", RecordCode);
                
                // 使用 Payment 的業務方法標記為已付款
                payment.MarkAsPaid();
                
                // 使用 Order 的業務方法標記訂單已付款
                // 使用 Order 中已存在的 PayWay 作為付款方式
                payment.Order.MarkAsPaid(payment.Order.PayWay);
                
                _logger.LogInformation("已標記為已付款，訂單號: {RecordCode}, 訂單狀態: {OrderStatus}", RecordCode, payment.Order.Status);

                    // 付款成功後，確認庫存（正式扣除）
                    var confirmResult = await _inventoryService.ConfirmInventoryAsync(RecordCode);
                    if (!confirmResult.IsSuccess)
                    {
                        _logger.LogWarning("付款成功後確認庫存失敗，訂單編號: {RecordCode}, 錯誤: {Error}", 
                            RecordCode, confirmResult.ErrorMessage);
                        // 即使確認失敗，也繼續處理（庫存已經在創建訂單時預扣）
                    }

                    // 通知物流系統開始處理
                    var shipmentMessage = new
                    {
                        Status = (int)ShipmentStatus.Pending,
                        OrderId = payment.OrderId,
                        RecordCode = payment.Order.RecordCode
                    };
                    await _shipmentProducer.SendMessage(shipmentMessage);

                    // 通知訂單狀態服務支付已完成
                    var orderStateMessage = new
                    {
                        eventType = "PaymentCompleted",
                        orderId = payment.Order.RecordCode, // 使用 RecordCode 作為 orderId
                        timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                    };
                    await _orderStateProducer.SendMessage(orderStateMessage);
                    
                    _logger.LogInformation("支付成功，已發送訂單狀態更新消息，訂單號: {RecordCode}", RecordCode);
                }
                else
                {
                    _logger.LogInformation("訂單已經標記為已付款，跳過處理，訂單號: {RecordCode}", RecordCode);
                }

            await _paymentRepository.SaveChangesAsync();
            _logger.LogInformation("支付記錄已保存，訂單號: {RecordCode}", RecordCode);

            return Success<object>("OK");
            
        }

        #endregion
    }
}
