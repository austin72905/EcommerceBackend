using EcommerceBackend.Models;

namespace EcommerceBackend.Interfaces.Services
{
    public interface IUserService
    {
        public UserInfoDTO GetUserInfo(string? userid);
    }
}
