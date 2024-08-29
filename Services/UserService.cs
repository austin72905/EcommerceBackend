using EcommerceBackend.Interfaces.Repositorys;
using EcommerceBackend.Interfaces.Services;
using EcommerceBackend.Models;
using System.Net;

namespace EcommerceBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository=userRepository;
        }

        public List<UserShipAddressDTO> GetUserShippingAddress(string? userid)
        {
            if (userid == null)
            {
                return new List<UserShipAddressDTO>();
            }

            return _userRepository.GetUserShippingAddress(userid).ToList();
        }

        public UserInfoDTO GetUserInfo(string? userid)
        {
            if (userid == null) 
            { 
                return new UserInfoDTO();
            }

            var user =_userRepository.GetUserInfo(userid);

            return user;
        }

        public string AddUserShippingAddress(string? userid, UserShipAddressDTO address)
        {
            if (userid == null)
            {
                return "no";
            }

            var msg =_userRepository.AddUserShippingAddress(userid, address);

            return msg;
        }

        public string ModifyUserShippingAddress(string? userid, UserShipAddressDTO address)
        {
            if (userid == null)
            {
                return "no";
            }

            var msg = _userRepository.ModifyUserShippingAddress(userid, address);

            return msg;
        }

        public string DeleteUserShippingAddress(string? userid, int addressId)
        {
            if (userid == null)
            {
                return "no";
            }

            var msg = _userRepository.DeleteUserShippingAddress(userid, addressId);
            return msg;
        }
    }
}
