
using Application.DTOs;
using Application.Interfaces;
using Common.Interfaces.Infrastructure;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Diagnostics;
using System.Text.Json;
using System.Web;

namespace Application.Services
{
    public class PaymentService : BaseService<PaymentService>,IPaymentService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepostory _orderRepository;
        private readonly IPaymentCompletedProducer _paymentCompletedProducer;
        private readonly IEncryptionService _encryptionService;
        private readonly IInventoryService _inventoryService;
        private readonly IHttpUtils _httpUtils;
        private readonly IRedisService _redisService;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly IConfiguration _configuration;
        public PaymentService(
            IPaymentRepository paymentRepository, 
            IOrderRepostory orderRepository,
            IHttpContextAccessor contextAccessor, 
            IPaymentCompletedProducer paymentCompletedProducer,
            IEncryptionService encryptionService, 
            IInventoryService inventoryService,
            IConfiguration configuration,
            IHttpUtils httpUtils,
            IRedisService redisService,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<PaymentService> logger):base(logger)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _contextAccessor = contextAccessor;
            _paymentCompletedProducer = paymentCompletedProducer;
            _encryptionService = encryptionService;
            _inventoryService = inventoryService;
            _configuration = configuration;
            _httpUtils = httpUtils;
            _redisService = redisService;
            _serviceScopeFactory = serviceScopeFactory;
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

                // 獲取配置（帶緩存）
                var config = await GetTenantConfigWithCache(requestData.RecordNo);

                if (config == null)
                {
                    return Fail<PaymentInfomation>("請求配置失敗");
                }

                // 驗證金額
                var orderAmount = Convert.ToInt32(config.TenantConfig.PaymentAmount).ToString();
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

        /// <summary>
        /// 獲取租戶配置和支付記錄（簡化版）
        /// TenantConfig 從緩存獲取（應用啟動時已載入），Payment 從資料庫查詢
        /// </summary>
        private async Task<PaymentWithTenantConfig?> GetTenantConfigWithCache(string recordCode)
        {
            try
            {
                // 從緩存獲取 TenantConfig（應用啟動時已載入）
                const string cacheKey = "tenant_config:default";
                var cachedJson = await _redisService.GetCacheAsync(cacheKey);
                
                TenantConfigCacheData? tenantConfigData = null;
                if (!string.IsNullOrEmpty(cachedJson))
                {
                    try
                    {
                        // 使用與 Program.cs 相同的命名策略（CamelCase）
                        var jsonOptions = new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        };
                        tenantConfigData = JsonSerializer.Deserialize<TenantConfigCacheData>(cachedJson, jsonOptions);
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogWarning(ex, "緩存數據格式錯誤，將從資料庫查詢租戶配置");
                    }
                }

                // 如果緩存中沒有，從資料庫查詢（不應該發生，因為應用啟動時已載入）
                if (tenantConfigData == null)
                {
                    _logger.LogWarning("緩存中沒有租戶配置，從資料庫查詢");
                    var tenantConfig = await _paymentRepository.GetDefaultTenantConfigAsync();
                    if (tenantConfig == null)
                    {
                        _logger.LogError("資料庫中沒有租戶配置");
                        return null;
                    }
                    
                    tenantConfigData = new TenantConfigCacheData
                    {
                        TenantConfigId = tenantConfig.Id,
                        MerchantId = tenantConfig.MerchantId,
                        SecretKey = tenantConfig.SecretKey,
                        HashIV = tenantConfig.HashIV,
                        PaymentAmount = "" // PaymentAmount 需要從 Payment 獲取
                    };
                    
                    // 重新寫入緩存（使用與 Program.cs 相同的命名策略）
                    var jsonOptions = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = false
                    };
                    var json = JsonSerializer.Serialize(tenantConfigData, jsonOptions);
                    await _redisService.SetCacheAsync(cacheKey, json, TimeSpan.FromDays(1));
                }

                // 從資料庫查詢 Payment（不包含 TenantConfig，因為已從緩存獲取）
                var payment = await _paymentRepository.GetPaymentByRecordCode(recordCode);
                
                if (payment == null)
                {
                    return null;
                }

                // 組合返回結果
                return new PaymentWithTenantConfig
                {
                    Payment = payment,
                    TenantConfig = new TenantConfigCacheData
                    {
                        TenantConfigId = tenantConfigData.TenantConfigId,
                        MerchantId = tenantConfigData.MerchantId,
                        SecretKey = tenantConfigData.SecretKey,
                        HashIV = tenantConfigData.HashIV,
                        PaymentAmount = payment.PaymentAmount.ToString()
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "獲取租戶配置（帶緩存）時發生錯誤，訂單號: {RecordCode}", recordCode);
                return null;
            }
        }

        /// <summary>
        /// 租戶配置緩存數據結構
        /// </summary>
        private class TenantConfigCacheData
        {
            public int TenantConfigId { get; set; }
            public string MerchantId { get; set; } = string.Empty;
            public string SecretKey { get; set; } = string.Empty;
            public string HashIV { get; set; } = string.Empty;
            public string PaymentAmount { get; set; } = string.Empty;
        }

        /// <summary>
        /// 支付記錄與租戶配置組合
        /// </summary>
        private class PaymentWithTenantConfig
        {
            public Payment Payment { get; set; } = null!;
            public TenantConfigCacheData TenantConfig { get; set; } = null!;
        }

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
            var sw = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation("收到支付回調，訂單號: {RecordCode}", RecordCode);

                //檢查是否交易成功
                if (!IsSuccessfulTransaction())
                {
                    _logger.LogWarning("支付回調交易失敗，訂單號: {RecordCode}", RecordCode);
                    return await TransactionFail();
                }

                //請求配置（帶緩存）
                var configResult = await GetTenantConfigWithCache(RecordCode);

                if (configResult == null)
                {
                    _logger.LogError("獲取租戶配置失敗，訂單號: {RecordCode}", RecordCode);
                    return Fail<object>("請求配置失敗");
                    
                }

                //驗證回調簽名
                if (!VerifySign(configResult.TenantConfig.SecretKey, configResult.TenantConfig.HashIV))
                {
                    // _logger.LogWarning("支付回調簽名驗證失敗，訂單號: {RecordCode}", RecordCode);
                    return SignVerificationFailed();
                }

                // _logger.LogInformation("支付回調簽名驗證成功，開始處理交易成功邏輯，訂單號: {RecordCode}", RecordCode);

                return await TransactionSuccess();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "處理支付回調時發生錯誤，訂單號: {RecordCode}", RecordCode);
                return Error<object>(ex.Message);
                
            }
            finally
            {
                sw.Stop();
                if (sw.Elapsed > TimeSpan.FromSeconds(10))
                {
                    _logger.LogWarning("支付回調耗時過長：{Elapsed}，訂單號: {RecordCode}", sw.Elapsed, RecordCode);
                }
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
            
            // 添加調試日誌（已註解）
            // _logger.LogInformation("簽名驗證 - 原始字串: {RawString}", rawString);
            // _logger.LogInformation("簽名驗證 - URL編碼後: {Encoded}", encoded);
            // _logger.LogInformation("簽名驗證 - 計算的簽名: {CalculatedSign}", sign);
            // _logger.LogInformation("簽名驗證 - 接收的簽名: {ReceivedSign}", returnSign);
            
            bool isValid = string.Equals(sign, returnSign, StringComparison.OrdinalIgnoreCase);
            // _logger.LogInformation("簽名驗證結果: {IsValid}", isValid);
            
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
            // 使用帶追蹤的方法獲取支付記錄，以便 EF Core 可以追蹤變更
            var payment = await _paymentRepository.GetPaymentRecordForUpdate(recordCode: RecordCode);

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
            var swTotal = Stopwatch.StartNew();
            _logger.LogInformation("開始處理交易成功邏輯，訂單號: {RecordCode}", RecordCode);

            // 非阻塞分散式鎖（單次嘗試，短 TTL）
            string lockKey = $"payment_callback:{RecordCode}";
            string lockValue = Guid.NewGuid().ToString();
            bool lockAcquired = false;

            try
            {
                lockAcquired = await _redisService.TryAcquireLockAsync(lockKey, lockValue, TimeSpan.FromSeconds(5));
                if (!lockAcquired)
                {
                    _logger.LogWarning("同筆訂單鎖被占用，回應重試，訂單號: {RecordCode}", RecordCode);
                    return Fail<object>("RETRY_LATER");
                }

                var swDb = Stopwatch.StartNew();
                // 以單一 SQL 幂等更新付款/訂單狀態，避免應用層鎖競爭
                var updateResult = await _paymentRepository.TryMarkPaymentAsPaidAsync(RecordCode);
                swDb.Stop();
                _logger.LogInformation("TryMarkPaymentAsPaidAsync 耗時：{Elapsed}，訂單號: {RecordCode}", swDb.Elapsed, RecordCode);

                if (!updateResult.Updated)
                {
                    _logger.LogInformation("訂單已經處理過或不存在，直接返回，訂單號: {RecordCode}", RecordCode);
                    return Success<object>("OK");
                }

                _logger.LogInformation("已標記為已付款，訂單號: {RecordCode}, 支付ID: {PaymentId}, 訂單ID: {OrderId}", 
                    RecordCode, updateResult.PaymentId, updateResult.OrderId);

                // 發送支付完成事件，交由背景消費者處理後續動作
                var evt = new PaymentCompletedEvent(
                    RecordCode,
                    updateResult.OrderId,
                    updateResult.PaymentId,
                    DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));

                await _paymentCompletedProducer.SendMessage(evt);
                return Success<object>("OK");
            }
            finally
            {
                if (lockAcquired)
                {
                    try
                    {
                        await _redisService.ReleaseLockAsync(lockKey, lockValue);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "釋放分散式鎖失敗，訂單號: {RecordCode}", RecordCode);
                    }
                }

                swTotal.Stop();
                if (swTotal.Elapsed > TimeSpan.FromSeconds(10))
                {
                    _logger.LogWarning("TransactionSuccess 耗時過長：{Elapsed}，訂單號: {RecordCode}", swTotal.Elapsed, RecordCode);
                }
            }
        }

        #endregion
    }
}
