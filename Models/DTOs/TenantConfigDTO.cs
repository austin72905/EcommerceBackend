namespace EcommerceBackend.Models.DTOs
{
    public class TenantConfigDTO
    {
        public required string RecordNo { get; set; }
        public required string MerchantId { get; set; }
        public required string SecretKey { get; set; }

        public required string HashIV { get; set; }

        public required string Amount { get; set; }
    }
}
