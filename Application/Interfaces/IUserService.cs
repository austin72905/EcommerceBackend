
using Application.DTOs;
using Application.Oauth;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public ServiceResult<UserInfoDTO> GetUserInfo(int userid);

        public ServiceResult<List<UserShipAddressDTO>> GetUserShippingAddress(int userid);

        public ServiceResult<string> AddUserShippingAddress(int userid, UserShipAddressDTO address);

        public ServiceResult<string> ModifyUserShippingAddress(int userid, UserShipAddressDTO address);

        public ServiceResult<string> DeleteUserShippingAddress(int userid, int addressId);

        public ServiceResult<string> SetDefaultShippingAddress(int userid, int addressId);
        
        public Task<ServiceResult<string>> RemoveFromfavoriteList(int userid, int productId);

        public Task<ServiceResult<string>> AddTofavoriteList(int userid, int productId);

        public Task<GoogleOAuth> UserAuthLogin(AuthLogin authLogin);
    }
}
