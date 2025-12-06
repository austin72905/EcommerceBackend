using System;

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

    /// <summary>
    /// 新增商品請求 DTO
    /// </summary>
    public class AddProductRequestDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty; // 逗號分隔的字串，例如 "聚酯纖維,聚氨酯纖維"
        public string HowToWash { get; set; } = string.Empty;
        public string Features { get; set; } = string.Empty;
        public string CoverImg { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new List<string>();
        
        // 商品分類和標籤（使用名稱，後端會查找或創建）
        public List<string> KindNames { get; set; } = new List<string>();
        public List<string> TagNames { get; set; } = new List<string>();
        
        // 商品變體
        public List<AddProductVariantDTO> Variants { get; set; } = new List<AddProductVariantDTO>();
    }

    /// <summary>
    /// 新增商品變體 DTO
    /// </summary>
    public class AddProductVariantDTO
    {
        public string Color { get; set; } = string.Empty;
        public string SizeValue { get; set; } = string.Empty; // 例如 "S", "M", "L"
        public int Stock { get; set; }
        public string SKU { get; set; } = string.Empty;
        public int Price { get; set; }
    }

    /// <summary>
    /// 新增商品回應 DTO
    /// </summary>
    public class AddProductResponseDTO
    {
        public int ProductId { get; set; }
    }

    /// <summary>
    /// 分頁請求 DTO
    /// </summary>
    public class PagedRequestDTO
    {
        public int Page { get; set; } = 1; // 預設第 1 頁
        public int PageSize { get; set; } = 20; // 預設每頁 20 筆

        /// <summary>
        /// 驗證並修正分頁參數
        /// </summary>
        public void Validate()
        {
            if (Page < 1) Page = 1;
            if (PageSize < 1) PageSize = 20;
            if (PageSize > 100) PageSize = 100; // 限制最大每頁 100 筆
        }
    }

    /// <summary>
    /// 分頁回應 DTO
    /// </summary>
    public class PagedResponseDTO<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < TotalPages;
    }
}
