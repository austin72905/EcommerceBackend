using EcommerceBackend.Models;
using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Interfaces.Services
{
    public interface IUserService
    {
        public ServiceResult<UserInfoDTO> GetUserInfo(string userid);

        public ServiceResult<List<UserShipAddressDTO>> GetUserShippingAddress(string userid);

        public string AddUserShippingAddress(string userid, UserShipAddressDTO address);

        public string ModifyUserShippingAddress(string userid, UserShipAddressDTO address);

        public string DeleteUserShippingAddress(string userid, int addressId);

        public Task<GoogleOAuth> UserAuthLogin(AuthLogin authLogin);
    }
}
