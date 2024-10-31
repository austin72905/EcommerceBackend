namespace Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }

        // 導航屬性
        public Cart Cart { get; set; }
        public ProductVariant ProductVariant { get; set; }
    }
}
