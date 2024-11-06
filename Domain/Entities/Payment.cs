namespace Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int TenantConfigId { get; set; }
        public decimal PaymentAmount { get; set; }
        public byte PaymentStatus { get; set; }
        // ECPAY 平台的訂單號
        public string TransactionId { get; set; }=string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Order Order { get; set; }
        public TenantConfig TenantConfig { get; set; }
    }
}
