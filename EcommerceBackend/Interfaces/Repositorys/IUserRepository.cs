using EcommerceBackend.Models;
using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Interfaces.Repositorys
{
    public interface IUserRepository
    {
        public IEnumerable<int> GetFavoriteProductIdsByUser(string userId);

        public UserInfoDTO GetUserInfo(string userid);

        public IEnumerable<UserShipAddressDTO> GetUserShippingAddress(string userid);

        public string AddUserShippingAddress(string userid, UserShipAddressDTO address);

        public string ModifyUserShippingAddress(string userid, UserShipAddressDTO address);

        public string DeleteUserShippingAddress(string userid, int addressId);
    }
}
