using EcommerceBackend.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class ProductInfomation
    {
        public string? Title { get; set; }

        public int ProductId { get; set; }

        public int Price { get; set; }

        public int Stock { get; set; }

        public List<string>? Size { get; set; }

        public List<string>? Color { get; set; }

        public List<string>? Material { get; set; }

        public List<ProductVariantDTO>? Varients { get; set; }

        public string? HowToWash { get; set; }

        public string? Features { get; set; }

        public List<string>? Images { get; set; }

        public string? CoverImg { get; set; }
    }

    public class ProductWithFavoriteStatus
    {
        public  ProductInfomation? product {  get; set; } 

        public bool? IsFavorite { get; set; }
    }

    public class ProductWithCount
    {
        public ProductInfomationDTO? product { get; set; }

        public int Count { get; set; }

        public ProductVariant? SelectedVariant { get; set; }
    }

    public class ProductSelection
    {
        public ProductInfomation? Product { get; set; }
        public string? SelectColor { get; set; }
        public string? SelectSize { get; set; }
        public int Count { get; set; }
    }

    // 顏色與尺寸
    public class Size
    {
        public int SizeId { get; set; }   
        public string? SizeValue { get; set; }
    }

    // 儲存 顏色與尺寸 與產品的對應關係
    public class ProductVariant
    {
        public int VariantID { get; set; }
        public int ProductId { get; set; }
        public string? Color { get; set; }

        public int SizeID { get; set; }

        public int Stock { get; set; }

        public string? SKU { get; set; }

        public int Price { get; set; }
    }


    

}
