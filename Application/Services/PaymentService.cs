
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
        private readonly IEncryptionService _encryptionService;

        private readonly IConfiguration _configuration;
        public PaymentService(IPaymentRepository paymentRepository, IHttpContextAccessor contextAccessor, IShipmentProducer shipmentProducer, IEncryptionService encryptionService, IConfiguration configuration,ILogger<PaymentService> logger):base(logger)
        {
            _paymentRepository = paymentRepository;
            _contextAccessor = contextAccessor;
            _shipmentProducer = shipmentProducer;
            _encryptionService = encryptionService;
            _configuration = configuration;
        }

        /// <summary>
        /// 支付網關
        /// </summary>
        public string ECPayCreditPaymentUrl { get; set; } = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";




        public async Task<ServiceResult<PaymentInfomation>> PayRedirect(PaymentRequestData requestData)
        {
            try
            {
                //較驗訂單號
                if (!ValidRecordNo(requestData.RecordNo))
                {
                    return Fail<PaymentInfomation>("訂單不合法");
                    
                }



                var config = await _paymentRepository.GetTenantConfig(requestData.RecordNo);

                if (config == null)
                {
                    return Fail<PaymentInfomation>("請求配置失敗");
                   
                }

                var tenantConfig = new TenantConfigDTO()
                {
                    RecordNo = requestData.RecordNo,
                    Amount = Convert.ToInt32(config.PaymentAmount).ToString(),
                    MerchantId = config.TenantConfig.MerchantId,
                    SecretKey = config.TenantConfig.SecretKey,
                    HashIV = config.TenantConfig.HashIV
                };

                //var tenantConfig = new TenantConfigDTO()
                //{
                //    RecordNo = requestData.RecordNo,
                //    Amount = "100",
                //    MerchantId = "3002607",
                //    SecretKey = "pwFHCqoQZGmho4w6",
                //    HashIV = "EkRm7iFT261dpevs"
                //};

                if (tenantConfig == null)
                {
                    return Fail<PaymentInfomation>("請求配置失敗");
                    
                }


                if (!ValidOrderAmount(tenantConfig.Amount, requestData.Amount))
                {
                    return Fail<PaymentInfomation>("金額匹配錯誤");
                    
                }

                //設置簽名參數
                Dictionary<string, string> signDataKeyPairs = PrepareSignData(tenantConfig, requestData);

                string sign = GenerateSign(signDataKeyPairs, tenantConfig.SecretKey, tenantConfig.HashIV);

                //加上非簽名參數
                AddNoneSignData(signDataKeyPairs, sign);

                return Success<PaymentInfomation>
                    (
                        new PaymentInfomation
                        {
                            PaymentData = signDataKeyPairs,
                            PaymentUrl = ECPayCreditPaymentUrl
                        }
                    );
                
            }
            catch (Exception ex)
            {
                return Error<PaymentInfomation>(ex.Message);
                
            }

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

                //檢查是否交易成功
                if (!IsSuccessfulTransaction())
                {

                    return await TransactionFail();
                }

                //請求配置
                var tenantConfig = await _paymentRepository.GetTenantConfig(recordCode: RecordCode);

                if (tenantConfig == null)
                {
                    return Fail<object>("請求配置失敗");
                    
                }

                //驗證回調簽名
                if (!VerifySign(tenantConfig.TenantConfig.SecretKey, tenantConfig.TenantConfig.HashIV))
                {
                    return SignVerificationFailed();
                }




                return await TransactionSuccess();
            }
            catch (Exception ex)
            {
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


            string sign = _encryptionService.Sha256Hash(HttpUtility.UrlEncode(rawString).ToLower()).ToUpper();
            return string.Equals(sign, returnSign, StringComparison.OrdinalIgnoreCase); ;
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
            }

            return Fail<object>("交易失敗");
            
        }


        private ServiceResult<object> SignVerificationFailed()
        {
            return Fail<object>("驗簽失敗");
            
        }

        private async Task<ServiceResult<object>> TransactionSuccess()
        {

            // 修改資料庫狀態
            var payment = await _paymentRepository.GetPaymentRecord(recordCode: RecordCode);

            if (payment != null)
            {
                // 檢查是否已經有更新過數據，避免重複回調
                if(payment.PaymentStatus != (byte)OrderStepStatus.PaymentReceived)
                {
                    // 使用 Payment 的業務方法標記為已付款
                    payment.MarkAsPaid();
                    
                    // 使用 Order 的業務方法標記訂單已付款
                    // 使用 Order 中已存在的 PayWay 作為付款方式
                    payment.Order.MarkAsPaid(payment.Order.PayWay);

                    // 通知物流系統開始處理
                    var message = new
                    {
                        Status = (int)ShipmentStatus.Pending,
                        OrderId = payment.OrderId,
                        RecordCode = payment.Order.RecordCode
                    };
                    await _shipmentProducer.SendMessage(message);
                }

                await _paymentRepository.SaveChangesAsync();

            }

            return Success<object>("OK");
            
        }

        #endregion
    }
}
