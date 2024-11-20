using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Utils.EncryptMethod;

namespace Domain.Services
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _userRepository;

        public UserDomainService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void UpdateUser(User user, User updateInfo)
        {
            user.Username = updateInfo.Username;
            user.Email = updateInfo.Email ?? string.Empty;// 確保不為 null
            user.NickName = updateInfo.NickName;
            user.PhoneNumber = updateInfo.PhoneNumber;
            user.Gender = updateInfo.Gender;
            user.Picture = updateInfo.Picture;
            user.Birthday = updateInfo.Birthday;
            user.UpdatedAt = DateTime.Now;   // 新增或自訂
        }



        public async Task EnsureUserNotExists(string userName,string email)
        {
            var user = await _userRepository.CheckUserExists(userName, email);

            if (user != null)
            {
                if (user.Email == email)
                {
                    throw new Exception($"已有相同信箱 {user.Email}");
                }
                else
                {
                    throw new Exception($"已有相同帳號 {user.Username}");
                }
            }
        }



        public void EnsurePasswordCanBeChanged(User user, string oldPassword, string newPassword)
        {
            // Oauh 登陸不需要密碼
            if (user.PasswordHash == null)
            {
                throw new Exception("此類型用戶無法修改密碼");
            }

            if (!BCryptUtils.VerifyPassword(oldPassword, user.PasswordHash))
            {
                throw new Exception("舊密碼輸入錯誤");
            }

            if (oldPassword == newPassword)
            {
                throw new Exception("新密碼不得與舊密碼相同");
            }
        }

        public void ChangePassword(User user, string newPassword)
        {
            user.PasswordHash = BCryptUtils.HashPassword(newPassword);
        }



    }
}
