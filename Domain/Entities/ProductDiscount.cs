namespace Domain.Entities
{
    public class ProductDiscount
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int DiscountId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Product Product { get; set; }
        public Discount Discount { get; set; }
    }
}
