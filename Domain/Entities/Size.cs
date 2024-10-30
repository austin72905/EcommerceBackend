namespace Domain.Entities
{
    public class Size
    {
        public int Id { get; set; }
        public string SizeValue { get; set; }

        public ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
