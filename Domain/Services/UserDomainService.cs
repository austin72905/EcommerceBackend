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

        public void ChangePassword(User user, string newPassword)
        {
            user.PasswordHash = _encryptionService.HashPassword(newPassword);
        }



    }
}
