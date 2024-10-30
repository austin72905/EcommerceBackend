namespace Domain.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public string MaterialName { get; set; }

        public ICollection<ProductMaterial> ProductMaterials { get; set; }
    }
}
