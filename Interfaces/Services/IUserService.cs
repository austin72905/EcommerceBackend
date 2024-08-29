using EcommerceBackend.Models;

namespace EcommerceBackend.Interfaces.Services
{
    public interface IUserService
    {
        public UserInfoDTO GetUserInfo(string? userid);

        public List<UserShipAddressDTO> GetUserShippingAddress(string? userid);

        public string AddUserShippingAddress(string? userid, UserShipAddressDTO address);

        public string ModifyUserShippingAddress(string? userid, UserShipAddressDTO address);

        public string DeleteUserShippingAddress(string? userid, int addressId);
    }
}
