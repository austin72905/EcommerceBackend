using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Common.Interfaces.Infrastructure;

namespace Domain.Services
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;

        public UserDomainService(IUserRepository userRepository, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
        }

        /// <summary>
        /// 更新用戶資料 - 已棄用，請直接使用 User.UpdateProfile()
        /// </summary>
        [Obsolete("此方法已棄用，請直接使用 User.UpdateProfile() 方法")]
        public void UpdateUser(User user, User updateInfo)
        {
            // 使用富領域模型方法
            user.UpdateProfile(
                nickName: updateInfo.NickName,
                phoneNumber: updateInfo.PhoneNumber,
                gender: updateInfo.Gender,
                birthday: updateInfo.Birthday
            );

            if (!string.IsNullOrWhiteSpace(updateInfo.Picture))
            {
                user.UpdatePicture(updateInfo.Picture);
            }
        }



        public async Task<DomainServiceResult<object>> EnsureUserNotExists(string userName,string email)
        {
            

            try
            {
                var user = await _userRepository.CheckUserExists(userName, email);

                if (user != null)
                {
                    if (user.Email == email)
                    {
                        return new DomainServiceResult<object>
                        {
                            IsSuccess=false,
                            ErrorMessage = $"已有相同信箱 {user.Email}",
                        };
                    }
                    else
                    {
                        
                        return new DomainServiceResult<object>
                        {
                            IsSuccess = false,
                            ErrorMessage = $"已有相同帳號 {user.Username}",
                        };
                    }
                }

                return new DomainServiceResult<object>
                {
                    IsSuccess = true,
                    ErrorMessage = "OK",
                };
            }
            catch (Exception ex) 
            {
                return new DomainServiceResult<object>
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                };


            }

            
        }



        public DomainServiceResult<object> EnsurePasswordCanBeChanged(User user, string oldPassword, string newPassword)
        {
            try
            {
                // Oauh 登陸不需要密碼
                if (user.PasswordHash == null)
                {
                    
                    return new DomainServiceResult<object>
                    {
                        ErrorMessage = "此類型用戶無法修改密碼",
                    };
                }

                if (!_encryptionService.VerifyPassword(oldPassword, user.PasswordHash))
                {
                   
                    return new DomainServiceResult<object>
                    {
                        ErrorMessage = "舊密碼輸入錯誤",
                    };
                }

                if (oldPassword == newPassword)
                {
                    return new DomainServiceResult<object>
                    {
                        ErrorMessage = "新密碼不得與舊密碼相同",
                    };
                }

                return new DomainServiceResult<object>
                {
                    IsSuccess=true,
                    ErrorMessage = "OK",
                };
            }
            catch (Exception ex) 
            {
                return new DomainServiceResult<object>
                {
                    ErrorMessage = ex.Message,
                };

            }


        }

        /// <summary>
        /// 修改密碼 - 使用富領域模型方法
        /// </summary>
        public void ChangePassword(User user, string newPassword)
        {
            var hashedPassword = _encryptionService.HashPassword(newPassword);
            user.UpdatePassword(hashedPassword);
        }



    }
}
