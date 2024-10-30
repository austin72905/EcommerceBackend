namespace Domain.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductVariantId { get; set; }
        public int ProductPrice { get; set; }
        public int Count { get; set; }

        public Order Order { get; set; }
        public ProductVariant ProductVariant { get; set; }
    }
}
