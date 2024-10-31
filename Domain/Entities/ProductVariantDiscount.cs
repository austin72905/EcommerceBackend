namespace Domain.Entities
{
    public class ProductVariantDiscount
    {
        public int Id { get; set; }
        public int VariantId { get; set; }
        public int DiscountId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ProductVariant ProductVariant { get; set; }
        public Discount Discount { get; set; }
    }
}
