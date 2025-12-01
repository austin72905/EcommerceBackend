using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// 付款實體 - 富領域模型
    /// </summary>
    public class Payment
    {
        // 私有構造函數，防止外部直接創建
        private Payment()
        {
        }

        /// <summary>
        /// 工廠方法：創建付款記錄
        /// </summary>
        public static Payment Create(int orderId, int paymentAmount, int tenantConfigId)
        {
            if (orderId <= 0)
                throw new ArgumentException("訂單 ID 必須大於 0", nameof(orderId));

            if (paymentAmount <= 0)
                throw new ArgumentException("付款金額必須大於 0", nameof(paymentAmount));

            if (tenantConfigId <= 0)
                throw new ArgumentException("租戶配置 ID 必須大於 0", nameof(tenantConfigId));

            return new Payment
            {
                OrderId = orderId,
                PaymentAmount = paymentAmount,
                TenantConfigId = tenantConfigId,
                PaymentStatus = (byte)OrderStepStatus.WaitingForPayment,
                TransactionId = string.Empty,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        // 只讀屬性或私有 setter
        public int Id { get; private set; }
        public int OrderId { get; private set; }
        public int TenantConfigId { get; private set; }
        public decimal PaymentAmount { get; private set; }
        public byte PaymentStatus { get; private set; }
        // ECPAY 平台的訂單號
        public string TransactionId { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // 導航屬性
        public Order Order { get; private set; }
        public TenantConfig TenantConfig { get; private set; }

        // ============ 業務邏輯方法 ============

        /// <summary>
        /// 標記為已付款
        /// </summary>
        public void MarkAsPaid()
        {
            if (PaymentStatus == (byte)OrderStepStatus.PaymentReceived)
                throw new InvalidOperationException("付款記錄已經是已付款狀態");

            PaymentStatus = (byte)OrderStepStatus.PaymentReceived;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// 標記為已取消
        /// </summary>
        public void MarkAsCanceled()
        {
            if (PaymentStatus == (byte)OrderStepStatus.OrderCanceled)
                throw new InvalidOperationException("付款記錄已經是已取消狀態");

            PaymentStatus = (byte)OrderStepStatus.OrderCanceled;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// 設置交易 ID（ECPAY 平台的訂單號）
        /// </summary>
        public void SetTransactionId(string transactionId)
        {
            if (string.IsNullOrWhiteSpace(transactionId))
                throw new ArgumentException("交易 ID 不能為空", nameof(transactionId));

            TransactionId = transactionId;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
