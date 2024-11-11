using Application.DTOs;
using Application.Oauth;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class UserExtensions
    {
        public static User ToUserEntity(this GoogleUserInfo jwtUserInfo)
        {
            return new User
            {
                Email = jwtUserInfo.Email,
                GoogleId = jwtUserInfo.Sub,
                NickName = jwtUserInfo.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                LastLogin = DateTime.Now,
            };
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
                Birthday = user.Birthday.HasValue ? user.Birthday.Value.ToString("yyyy/M/d") : string.Empty ,
                Picture = user.Picture, // 假設 Picture 的來源需要單獨處理
                Type = user.Role // 假設 Type 與 Role 對應
            };
        }

        public static User ToUserEntity(this UserInfoDTO dto)
        {
            DateTime? birthday = null;
            DateTime.TryParse(dto.Birthday, out DateTime parsedBirthday);
            birthday = parsedBirthday;
            return new User
            {
                Id = dto.UserId,
                Username = dto.Username,
                Email = dto.Email ?? string.Empty, // 確保不為 null
                NickName = dto.NickName,
                PhoneNumber = dto.PhoneNumber,
                Gender = dto.Gender,
                Picture = dto.Picture,
                Birthday = birthday,
                Role = dto.Type ?? "user", // 預設為 "user" 角色
                CreatedAt = DateTime.Now,   // 新增或自訂
                UpdatedAt = DateTime.Now,   // 新增或自訂
                LastLogin = DateTime.Now    // 新增或自訂
            };
        }
    }
}
