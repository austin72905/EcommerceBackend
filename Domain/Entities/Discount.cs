namespace Domain.Entities
{
    public class Discount
    {
        public int Id { get; set; }
        public int DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<ProductDiscount> ProductDiscounts { get; set; }
        public ICollection<ProductVariantDiscount> ProductVariantDiscounts { get; set; }
    }
}
