using EcommerceBackend.Services;

namespace EcommerceBackend.Models
{
    public class ProductResponse
    {
        public IEnumerable<ProductWithFavoriteStatus>? Products { get; set; }
        //public bool IsUserLoggedIn { get; set; }
    }
}
