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
    }
}
