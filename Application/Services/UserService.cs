﻿using Application.DTOs;
using Application.Extensions;
using Application.Interfaces;
using Application.Oauth;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Interfaces;
using Infrastructure.Utils.EncryptMethod;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Application.Services
{
    public class UserService : BaseService<UserService>,IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IRedisService _redisService;
        private readonly IUserDomainService _userDomainService;
        private readonly IHttpUtils _httpUtils;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IRedisService redisService, IUserDomainService userDomainService, IHttpUtils httpUtils,ILogger<UserService> logger):base(logger)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _redisService = redisService;
            _userDomainService = userDomainService;
            _httpUtils = httpUtils;
        }

        


        public async Task<ServiceResult<UserInfoDTO>> GetUserInfo(int userid)
        {
            try
            {

                var user = await _userRepository.GetUserInfo(userid);

                if (user != null)
                {
                    var userInfoDto = user.ToUserInfoDTO();

                    return Success<UserInfoDTO>(userInfoDto);
                    
                }

                return Fail<UserInfoDTO>("沒有此用戶");
                

            }
            catch (Exception ex) 
            {
                return Error<UserInfoDTO>(ex.Message);
                
            }


        }


        /// <summary>
        /// 修改用戶資料
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ServiceResult<string>> ModifyUserInfo(UserInfoDTO userDto, string sessionId)
        {
            try
            {
                //var user =userDto.ToUserEntity();
                //await _userRepository.ModifyUserInfo(user);

                var user = await _userRepository.GetUserInfo(userDto.UserId);

                if (user != null)
                {
                    // 將user 資料改變
                    var updateInfo = userDto.ToUserEntity();

                    // 將user 資料改變
                    _userDomainService.UpdateUser(user, updateInfo);

                    await _userRepository.SaveChangesAsync();


                    // 修改 redis，可以做成transaction or mq
                    await UpdateRedisUserInfoAsync(sessionId, user);

                    return Success<string>("操作成功");
                    

                }

                return Fail<string>("用戶不存在");
               
            }
            catch (Exception ex) 
            {
                return Error<string>(ex.Message);
                
            }
            
        }

        /// <summary>
        /// 用戶使用帳密登陸
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> UserLogin(LoginDTO loginDto)
        {
            try
            {

                var user = await _userRepository.CheckUserExists(loginDto.Username, loginDto.Username);

                // 用戶不存在
                if (user == null)
                {
                    return Fail<string>("用戶不存在");                    
                }

                // 檢查密碼

                var passwordHash = user.PasswordHash;

                if (!BCryptUtils.VerifyPassword(loginDto.Password, passwordHash))
                {
                    return Fail<string>("密碼錯誤");                    
                }




                var userDto = user.ToUserInfoDTO();

                string redisKey = await SaveUserInfoToRedis(userDto);

                return Success<string>(redisKey,message: "登入成功");
               
            }
            catch (Exception ex) 
            {
                return Error<string>(ex.Message);               
            }


           
        }


        /// <summary>
        /// 用戶註冊
        /// </summary>
        /// <param name="signUpDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ServiceResult<string>> UserRegister(SignUpDTO signUpDto)
        {
            // 後端應該也要對input做一些驗證?
            try
            {
                // 檢查用戶是否存在 (已存在就throw exception)
                var isUserExisted= await _userDomainService.EnsureUserNotExists(signUpDto.Username, signUpDto.Email);

                if (!isUserExisted.IsSuccess)
                {
                    return Fail<string>(isUserExisted.ErrorMessage);
                }

                // 新增用戶
                var userEntity = signUpDto.ToUserEntity();
                await _userRepository.AddUser(userEntity);

                // 新增完了
                var user = await _userRepository.CheckUserExists(signUpDto.Username, signUpDto.Email);
                // 將用戶資料存在redis，並將key返回給api 層
                var userDto = user.ToUserInfoDTO();

                string redisKey = await SaveUserInfoToRedis(userDto);

                return Success<string>(redisKey,message: "註冊成功");
              
            }
            catch (Exception ex)
            {
                return Error<string>(ex.Message);
                
            }

        }


        /// <summary>
        /// 修改密碼
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="modifyPasswordDto"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> ModifyPassword(int userid, ModifyPasswordDTO modifyPasswordDto)
        {

            try
            {
                if (userid == 0)
                {
                    return Fail<string>("用戶不存在");
                    
                }


                var user = await _userRepository.GetUserInfo(userid);

                if (user == null)
                {
                    return Fail<string>("用戶不存在");
                    
                }



                try
                {
                    // 調用 Domain Service 執行業務邏輯
                    var result=_userDomainService.EnsurePasswordCanBeChanged(user, modifyPasswordDto.OldPassword, modifyPasswordDto.Password);

                    if (!result.IsSuccess)
                    {
                        return Fail<string>(result.ErrorMessage);
                    }

                    // 修改密碼
                    _userDomainService.ChangePassword(user, modifyPasswordDto.Password);

                    // 保存到數據庫
                    await _userRepository.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Error<string>(ex.Message);
                    
                }

                return Success<string>(message: "修改密碼成功");
                
            }
            catch(Exception ex)
            {
                return Error<string>(ex.Message,message:ex.Message);
                
            }

        }



        /// <summary>
        /// 獲取用戶常用地址
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ServiceResult<List<UserShipAddressDTO>> GetUserShippingAddress(int userid)
        {
            try
            {
                if (userid == 0)
                {
                    return Fail<List<UserShipAddressDTO>>("用戶不存在");
                    
                }

                var addressList = _userRepository.GetUserShippingAddress(userid);

                var addressListDto = addressList.Select(address => address.ToUserShipAddressDTO()).ToList();


                return Success<List<UserShipAddressDTO>>(addressListDto);
                


            }
            catch (Exception ex)
            {
                return Error<List<UserShipAddressDTO>>(ex.Message);
               
            }



        }


        /// <summary>
        /// 新增常用地址
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public ServiceResult<string> AddUserShippingAddress(int userid, UserShipAddressDTO address)
        {
            try
            {
                if (userid == 0)
                {
                    return Fail<string>("不存在的用戶");                    
                }

                var addressEntity = new UserShipAddress
                {
                    UserId = userid,
                    RecipientName = address.RecipientName,
                    PhoneNumber = address.PhoneNumber,
                    RecieveStore = address.RecieveStore,
                    RecieveWay = address.RecieveWay,
                    AddressLine = address.AddressLine,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };


                _userRepository.AddUserShippingAddress(userid, addressEntity);

                return Success<string>(message:"操作成功");
                
            }
            catch (Exception ex) 
            {
                return Error<string>(ex.Message);
                
            }
            

        }

        /// <summary>
        /// 修改常用地址
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public ServiceResult<string> ModifyUserShippingAddress(int userid, UserShipAddressDTO address)
        {
            try
            {
                if (userid == 0)
                {
                    return Fail<string>("不存在的用戶");
                    
                }

                var addressEntity = new UserShipAddress
                {
                    Id = address.AddressId,
                    UserId = userid,
                    RecipientName = address.RecipientName,
                    PhoneNumber = address.PhoneNumber,
                    RecieveStore = address.RecieveStore,
                    RecieveWay = address.RecieveWay,
                    AddressLine = address.AddressLine,
                    //CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                _userRepository.ModifyUserShippingAddress(userid, addressEntity);

                return Success<string>(message: "操作成功");
            }
            catch(Exception ex)
            {
                return Error<string>(ex.Message);
            }
            
        }

        /// <summary>
        /// 刪除常用地址
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public ServiceResult<string> DeleteUserShippingAddress(int userid, int addressId)
        {
            try
            {
                if (userid == 0)
                {
                    return Fail<string>("不存在的用戶");
                }

                _userRepository.DeleteUserShippingAddress(userid, addressId);
                return Success<string>(message: "操作成功");
            }
            catch(Exception ex)
            {
                return Error<string>(ex.Message);
            }
            
        }

        /// <summary>
        /// 設定預設地址
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public ServiceResult<string> SetDefaultShippingAddress(int userid, int addressId)
        {
            try
            {

                if (userid == 0)
                {
                    return Fail<string>("不存在的用戶");
                }

                _userRepository.SetDefaultShippingAddress(userid, addressId);
                return Success<string>(message: "操作成功");

            }
            catch(Exception ex)
            {
                return Error<string>(ex.Message);
            }

            
        }




        // 對喜愛清單的操作
        public async Task<ServiceResult<string>> RemoveFromfavoriteList(int userid, int productId)
        {
            try
            {

                if (userid == 0)
                {
                    return Fail<string>("不存在的用戶");
                }


                await _userRepository.RemoveFromFavoriteList(userid, productId);

                return Success<string>(message: "操作成功");

            }
            catch(Exception ex)
            {
                return Error<string>(ex.Message);
            }

            
        }


        public async Task<ServiceResult<string>> AddTofavoriteList(int userid, int productId)
        {

            try
            {
                if (userid == 0)
                {
                    return Fail<string>("不存在的用戶");
                }



                await _userRepository.AddToFavoriteList(userid, productId);

                return Success<string>(message: "操作成功");


            }
            catch(Exception ex)
            {
                return Error<string>(ex.Message);
            }

           
        }


        /// <summary>
        /// 解碼jwt token
        /// </summary>
        /// <param name="idToken"></param>
        /// <returns></returns>
        internal GoogleUserInfo? DecodeIDToken(string idToken)
        {
            string[] jwtContent = idToken.Split('.');
            string jwtPayloadString = Encoding.UTF8.GetString(Base64UrlTextEncoder.Decode(jwtContent[1]));
            Console.WriteLine(jwtPayloadString);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var userInfo = JsonSerializer.Deserialize<GoogleUserInfo>(jwtPayloadString, options);
            return userInfo;
        }

        /// <summary>
        /// 使用Oauth  登陸
        /// </summary>
        /// <param name="authLogin"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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
                    {"redirect_uri",_configuration["AppSettings:RedirectAuthUri"] },
                    //{"code_verifier","" },
                };

                //var client = new HttpClient();

                //var resp = await client.PostAsync("https://oauth2.googleapis.com/token", new FormUrlEncodedContent(body));

                //var jsonResp = await resp.Content.ReadAsStringAsync();

                //var result = JsonSerializer.Deserialize<OAuth2GoogleResp>(jsonResp);



                var result = await _httpUtils.PostFormAsync<OAuth2GoogleResp>("https://oauth2.googleapis.com/token", body);


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


                //檢查DB 是否有該google id (sub)的用戶，沒有就註冊
                var user = await _userRepository.GetUserIfExistsByGoogleID(jwtUserInfo.Sub);

                if (user == null)
                {
                    var userRegistInfo = jwtUserInfo.ToUserEntity();
                    //var userRegistInfo = new User()
                    //{
                    //    Email = jwtUserInfo.Email,
                    //    GoogleId = jwtUserInfo.Sub,
                    //    Username = jwtUserInfo.Name,
                    //    CreatedAt = DateTime.Now
                    //};
                    //await _userRepository.AddUser(userRegistInfo);
                    await _userRepository.AddAsync(userRegistInfo);
                    await _userRepository.SaveChangesAsync();

                    // 從新獲取
                    user = await _userRepository.GetUserIfExistsByGoogleID(jwtUserInfo.Sub);
                }

                //將用戶訊息存到redis

                var userInfo = new UserInfoDTO
                {
                    //UserId = jwtUserInfo.Sub,
                    UserId = user.Id, // 改成 從 db 拿 user.Id
                    Email = user.Email,
                    NickName = user.NickName,
                    Picture = user.Picture,
                    Type = authLogin.state,
                    Birthday = user.Birthday.HasValue ? user.Birthday.Value.ToString("yyyy/M/d") : string.Empty,
                    Gender = user.Gender,
                    PhoneNumber = user.PhoneNumber,
                  
                };

                Console.WriteLine(userInfo);


                return new GoogleOAuth { UserInfo = userInfo, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new GoogleOAuth { ErrorMessage = "some error occured" };
            }

        }



        /// <summary>
        /// 將用戶session寫進redis
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private async Task<string> SaveUserInfoToRedis(UserInfoDTO userInfo)
        {
            string guid = Guid.NewGuid().ToString();

            string result = JsonSerializer.Serialize(userInfo);

            await _redisService.SetUserInfoAsync(guid, result);

            return guid;
        }



        /// <summary>
        /// 更新 用戶資料到redis
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task UpdateRedisUserInfoAsync(string sessionId, User user)
        {
            var redisUserInfo = await _redisService.GetUserInfoAsync(sessionId);
            if (redisUserInfo != null)
            {
                var userEntity = user.ToUserInfoDTO();
                await _redisService.SetUserInfoAsync(sessionId, JsonSerializer.Serialize(userEntity));
            }
        }

    }
}
