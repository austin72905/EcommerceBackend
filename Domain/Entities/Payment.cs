namespace Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal PaymentAmount { get; set; }
        public byte PaymentStatus { get; set; }
        public string TransactionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Order Order { get; set; }
        public TenantConfig TenantConfig { get; set; }
    }
}
