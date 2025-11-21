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


    public class ProductBasicDTO
    {
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public List<string>? Material { get; set; }
        public string? HowToWash { get; set; }
        public string? Features { get; set; }
        public List<string>? Images { get; set; }
        public string? CoverImg { get; set; }
    }

    public class ProductDynamicDTO
    {
        public int ProductId { get; set; }
        public List<ProductVariantDTO> Variants { get; set; }

        public bool? IsFavorite { get; set; }

    }

    /// <summary>
    /// 整合的商品資訊 DTO，包含基本資訊和動態資訊（變體、庫存、價格等）
    /// </summary>
    public class ProductCompleteDTO
    {
        // 基本資訊
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public List<string>? Material { get; set; }
        public string? HowToWash { get; set; }
        public string? Features { get; set; }
        public List<string>? Images { get; set; }
        public string? CoverImg { get; set; }

        // 動態資訊
        public List<ProductVariantDTO> Variants { get; set; } = new List<ProductVariantDTO>();
        public bool? IsFavorite { get; set; }
    }
}
