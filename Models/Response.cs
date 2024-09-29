using EcommerceBackend.Models.DTOs;
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

    public class LoginReponse
    {
        public string RedirectUrl { get; set; }
        public UserInfoDTO? UserInfo { get; set; }


    }

    public class OAuth2GoogleResp
    {
        public string? token_type { get; set; }
        public string? access_token { get; set; }
        public int expires_in { get; set; }
        public string? scope { get; set; }

        public string? refresh_token { get; set; }
        public string? id_token { get; set; }

        public string? error { get; set; }

        public string? error_description { get; set; }
    }
}
