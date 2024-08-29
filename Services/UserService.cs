using EcommerceBackend.Interfaces.Repositorys;
using EcommerceBackend.Interfaces.Services;
using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository=userRepository;
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
    }
}
