namespace Domain.Entities
{
    public class ProductMaterial
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int MaterialId { get; set; }

        public Product Product { get; set; }
        public Material Material { get; set; }
    }
}
