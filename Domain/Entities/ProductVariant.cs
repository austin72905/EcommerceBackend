namespace Domain.Entities
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Color { get; set; }
        public int SizeId { get; set; }
        public int Stock { get; set; }
        public string SKU { get; set; }
        public int VariantPrice { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // 導航屬性
        public Product Product { get; set; }
        public Size Size { get; set; }
        public ICollection<ProductVariantDiscount> ProductVariantDiscounts { get; set; }
    }
}
