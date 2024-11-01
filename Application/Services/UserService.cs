using Application.DTOs;
using Application.Interfaces;
using Application.Oauth;
using Domain.Interfaces.Repositories;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IRedisService _redisService;
        public UserService(IUserRepository userRepository, IConfiguration configuration, IRedisService redisService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _redisService = redisService;
        }

        public ServiceResult<List<UserShipAddressDTO>> GetUserShippingAddress(int userid)
        {
            var addressList = _userRepository.GetUserShippingAddress(userid).ToList();

            var addressListDto = new List<UserShipAddressDTO>();

            return new ServiceResult<List<UserShipAddressDTO>>()
            {
                IsSuccess = true,
                Data = addressListDto
            };

        }

        public ServiceResult<UserInfoDTO> GetUserInfo(int userid)
        {
            //if (userid == null)
            //{
            //    return new ServiceResult<UserInfoDTO>()
            //    {
            //        IsSuccess = false,
            //        ErrorMessage = "userid不得為空"

            //    };
            //}

            var user = _userRepository.GetUserInfo(userid);

            var userInfoDto = new UserInfoDTO();

            return new ServiceResult<UserInfoDTO>()
            {
                IsSuccess = true,
                Data = userInfoDto,
            };
        }

        public string AddUserShippingAddress(int userid, UserShipAddressDTO address)
        {
            if (userid == null)
            {
                return "no";
            }

            _userRepository.AddUserShippingAddress(userid, "");

            return "ok";
        }

        public string ModifyUserShippingAddress(int userid, UserShipAddressDTO address)
        {
            if (userid == null)
            {
                return "no";
            }

             _userRepository.ModifyUserShippingAddress(userid, "");

            return "ok";
        }

        public string DeleteUserShippingAddress(int userid, int addressId)
        {
            if (userid == null)
            {
                return "no";
            }

            _userRepository.DeleteUserShippingAddress(userid, addressId);
            return "ok";
        }




        private GoogleUserInfo? DecodeIDToken(string idToken)
        {
            string[] jwtContent = idToken.Split('.');
            string jwtPayloadString = Encoding.UTF8.GetString(Base64UrlTextEncoder.Decode(jwtContent[1]));
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var userInfo = JsonSerializer.Deserialize<GoogleUserInfo>(jwtPayloadString, options);
            return userInfo;
        }

        public async Task<GoogleOAuth> UserAuthLogin(AuthLogin authLogin)
        {
            try
            {
                string? secret = _configuration["GoogleAuthClientSecret"];

                if (string.IsNullOrEmpty(secret))
                {
                    throw new InvalidOperationException("The environment variable 'GoogleAuthClientSecret' is not set.");
                }

                var body = new Dictionary<string, string>()
                {
                    {"client_id","88199731036-4ve6gh6a0vdj63j41r4gnhd7cf8s8kpr.apps.googleusercontent.com" },
                    {"client_secret",secret },
                    {"code",authLogin.code},
                    {"grant_type","authorization_code" },
                    {"redirect_uri","http://localhost:3000/auth" },
                    //{"code_verifier","" },
                };

                var client = new HttpClient();

                var resp = await client.PostAsync("https://oauth2.googleapis.com/token", new FormUrlEncodedContent(body));

                var jsonResp = await resp.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<OAuth2GoogleResp>(jsonResp);


                //請求錯誤
                if (result == null || result.id_token == null)
                {
                    return new GoogleOAuth { ErrorMessage = "some error occured when request token" };
                }

                var jwtUserInfo = DecodeIDToken(result.id_token);

                if (jwtUserInfo == null)
                {
                    return new GoogleOAuth { ErrorMessage = "some error occured when parse token" };
                }

                //將用戶訊息存到redis

                var userInfo = new UserInfoDTO
                {
                    //UserId = jwtUserInfo.Sub,
                    Email = jwtUserInfo.Email,
                    Username = jwtUserInfo.Name,
                    Picture = jwtUserInfo.Picture,
                    Type = authLogin.state
                };



                return new GoogleOAuth { UserInfo = userInfo, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new GoogleOAuth { ErrorMessage = "some error occured" };
            }

        }





    }
}
