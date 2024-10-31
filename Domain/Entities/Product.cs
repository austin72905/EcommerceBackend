namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string HowToWash { get; set; }
        public string Features { get; set; }
        public string CoverImg { get; set; }
        public string Material { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<ProductKindTag> ProductKindTags { get; set; }

        public ICollection<ProductDiscount> ProductDiscounts { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }
        public ICollection<ProductMaterial> ProductMaterials { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
