using Application.DTOs;
using Application.Oauth;
using Domain.Entities;
using Common.Interfaces.Infrastructure;

namespace Application.Extensions
{
    public static class UserExtensions
    {
        /// <summary>
        /// Google 用戶資料轉換為用戶實體 - 使用富領域模型工廠方法
        /// </summary>
        public static User ToUserEntity(this GoogleUserInfo jwtUserInfo)
        {
            return User.CreateWithGoogle(
                email: jwtUserInfo.Email,
                googleId: jwtUserInfo.Sub,
                nickName: jwtUserInfo.Name,
                picture: jwtUserInfo.Picture
            );
        }

        public static UserInfoDTO ToUserInfoDTO(this User user)
        {


            return new UserInfoDTO
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                NickName = user.NickName,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                Birthday = user.Birthday.HasValue ? user.Birthday.Value.ToString("yyyy/M/d") : string.Empty,
                Picture = user.Picture, // 假設 Picture 的來源需要單獨處理
                Type = user.Role // 假設 Type 與 Role 對應
            };
        }

        /// <summary>
        /// DTO 轉換為用戶實體 - 已棄用，此方法不應用於創建新用戶
        /// 請使用 User.CreateWithPassword() 或 User.CreateWithGoogle()
        /// </summary>
        [Obsolete("此方法已棄用，請使用 User 的工廠方法創建用戶，或使用 User.UpdateProfile() 更新現有用戶")]
        public static User ToUserEntity(this UserInfoDTO dto)
        {
            // 注意：這個方法實際上不應該被使用，因為無法透過 DTO 創建完整的 User
            // 保留此方法僅為向後兼容，但標記為過時
            throw new NotSupportedException(
                "無法從 UserInfoDTO 創建 User 實體。" +
                "請使用 User.CreateWithPassword() 或 User.CreateWithGoogle() 創建新用戶，" +
                "或使用 User.UpdateProfile() 更新現有用戶。");
        }

        /// <summary>
        /// 註冊 DTO 轉換為用戶實體 - 使用富領域模型工廠方法
        /// </summary>
        public static User ToUserEntity(this SignUpDTO signUpDto, IEncryptionService encryptionService)
        {
            var user = User.CreateWithPassword(
                email: signUpDto.Email,
                username: signUpDto.Username,
                passwordHash: encryptionService.HashPassword(signUpDto.Password)
            );

            // 設置額外資訊
            if (!string.IsNullOrWhiteSpace(signUpDto.NickName))
            {
                user.UpdateProfile(nickName: signUpDto.NickName);
            }

            return user;
        }
    }
}
