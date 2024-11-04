﻿
using Application.DTOs;
using Application.Oauth;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public ServiceResult<UserInfoDTO> GetUserInfo(int userid);

        public ServiceResult<List<UserShipAddressDTO>> GetUserShippingAddress(int userid);

        public string AddUserShippingAddress(int userid, UserShipAddressDTO address);

        public string ModifyUserShippingAddress(int userid, UserShipAddressDTO address);

        public string DeleteUserShippingAddress(int userid, int addressId);

        public Task<GoogleOAuth> UserAuthLogin(AuthLogin authLogin);
    }
}