using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class ProductInfomation
    {
        public string? Title { get; set; }

        public string? ProductId { get; set; }

        public int Price { get; set; }

        public int Stock { get; set; }

        public List<string>? Size { get; set; }

        public List<string>? Color { get; set; }

        public List<string>? Material { get; set; }

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
        public ProductInfomation? product { get; set; }

        public int Count { get; set; }
    }

    public class ProductSelection
    {
        public ProductInfomation? Product { get; set; }
        public string? SelectColor { get; set; }
        public string? SelectSize { get; set; }
        public int Count { get; set; }
    }
}
