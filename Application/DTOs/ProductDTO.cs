namespace Application.DTOs
{
    public class ProductInfomationDTO
    {
        public string? Title { get; set; }

        public int ProductId { get; set; }

        //public int Price { get; set; }

        //public int DiscountPrice { get; set; }

        //public int Stock { get; set; }


        public List<string>? Material { get; set; }

        public List<ProductVariantDTO>? Variants { get; set; }

        public string? HowToWash { get; set; }

        public string? Features { get; set; }

        public List<string>? Images { get; set; }

        public string? CoverImg { get; set; }
    }

    public class ProductWithCountDTO
    {
        public ProductInfomationDTO? Product { get; set; }

        public int Count { get; set; }

        public ProductVariantDTO? SelectedVariant { get; set; }
    }

    public class ProductWithFavoriteStatusDTO
    {
        public ProductInfomationDTO? Product { get; set; }

        public bool? IsFavorite { get; set; }
    }


    public class ProductVariantDTO
    {
        public int VariantID { get; set; }
        public string? Color { get; set; }

        public string? Size { get; set; }

        public int Stock { get; set; }

        public string? SKU { get; set; }

        public int Price { get; set; }

        public int? DiscountPrice { get; set; }
    }
}
