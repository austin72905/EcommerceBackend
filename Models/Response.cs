using EcommerceBackend.Services;

namespace EcommerceBackend.Models
{
    public class ProductListResponse
    {
        public IEnumerable<ProductWithFavoriteStatus>? Products { get; set; }
        //public bool IsUserLoggedIn { get; set; }
    }

    public class ProductResponse
    {
        public ProductWithFavoriteStatus? Product { get; set; }
    }
}
