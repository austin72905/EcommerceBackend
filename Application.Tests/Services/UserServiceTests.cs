using Application.DTOs;
using Application.Extensions;
using Application.Oauth;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Interfaces;
using Infrastructure.Utils.EncryptMethod;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Net;

namespace Application.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private UserService _userService;
        private Mock<IConfiguration> _configurationMock;
        private Mock<IRedisService> _redisServiceMock;
        private Mock<IUserDomainService> _userDomainServiceMock;
        private Mock<IHttpUtils> _httpUtilsMock;

        [SetUp]
        public void Setup()
        {
            // 初始化所有依賴的模擬對象
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _redisServiceMock = new Mock<IRedisService>();
            _userDomainServiceMock = new Mock<IUserDomainService>();
            _httpUtilsMock = new Mock<IHttpUtils>();
           

        // 注入模擬對象到 UserService
        _userService = new UserService(
                _userRepositoryMock.Object,
                _configurationMock.Object,
                _redisServiceMock.Object,
                _userDomainServiceMock.Object,
                _httpUtilsMock.Object
            );
        }


        /*
             UserInfo 類
         
        */
        [Test]
        public async Task GetUserInfo_UserExists_ReturnsSuccessWithUserInfo()
        {
            // Arrange
            int userId = 1;
            var user = new User
            {
                Id = userId,
                NickName = "Test User",
                Email = "test@example.com",
                Username = "aaaaa",
                PhoneNumber = "11111111",
                Gender = "男",
                Picture = "",
                Birthday = null,
                Role = "user"

            };

            // 模擬從 repo 拿到的資料是 user
            _userRepositoryMock
                .Setup(repo => repo.GetUserInfo(userId))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserInfo(userId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);

            Assert.AreEqual(user.Id, result.Data.UserId);
            Assert.AreEqual(user.NickName, result.Data.NickName);
            Assert.AreEqual(user.Email, result.Data.Email);
        }

        [Test]
        public async Task GetUserInfo_UserDoesNotExist_ReturnsError()
        {
            // Arrange
            int userId = 999;
            _userRepositoryMock
                .Setup(repo => repo.GetUserInfo(userId))
                .ReturnsAsync((User)null);

            // Act
            var result = await _userService.GetUserInfo(userId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNull(result.Data);

            Assert.AreEqual("沒有此用戶", result.ErrorMessage);
        }


        /*
            1. 當用戶存在時，驗證：
                 * 正確更新用戶資料。
                 * 調用了 UpdateUser 和 SaveChangesAsync 方法。
                 * 用戶資料被更新到 Redis 中。
            
            2. 當用戶不存在時，返回錯誤信息。
            3. 驗證對 Redis 操作的行為是否正確。 
         
         
        */


        [Test]
        public async Task ModifyUserInfo_WhenUserExists_UpdatesUserAndRedis()
        {
            // Arrange
            var userDto = new UserInfoDTO
            {
                UserId = 1,
                Username = "UpdatedUser",
                Email = "updated@example.com"
            };
            var sessionId = "session123";

            var existingUser = new User
            {
                Id = 1,
                Username = "OldUser",
                Email = "old@example.com"
            };

            _userRepositoryMock
                .Setup(repo => repo.GetUserInfo(userDto.UserId))
                .ReturnsAsync(existingUser);

            _redisServiceMock
                .Setup(redis => redis.GetUserInfoAsync(sessionId))
                .ReturnsAsync("userinfo json");

            _redisServiceMock
                .Setup(redis => redis.SetUserInfoAsync(sessionId, It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            _userRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _userService.ModifyUserInfo(userDto, sessionId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("操作成功", result.Data);

            _userDomainServiceMock.Verify(service => service.UpdateUser(existingUser, It.IsAny<User>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
            _redisServiceMock.Verify(redis => redis.SetUserInfoAsync(sessionId, It.IsAny<string>()), Times.Once);
        }


        [Test]
        public async Task ModifyUserInfo_UserDoesNotExist_ReturnsError()
        {
            // Arrange
            var userId = 999;
            var sessionId = "session123";
            var userDto = new UserInfoDTO { UserId = userId };

            _userRepositoryMock.Setup(repo => repo.GetUserInfo(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.ModifyUserInfo(userDto, sessionId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(null, result.Data);
            Assert.AreEqual("用戶不存在", result.ErrorMessage);

            _userDomainServiceMock.Verify(service => service.UpdateUser(It.IsAny<User>(), It.IsAny<User>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Never);
            _redisServiceMock.Verify(redis => redis.SetUserInfoAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }


        [Test]
        public async Task ModifyUserInfo_WhenExceptionThrown_ReturnsError()
        {
            // Arrange
            var userDto = new UserInfoDTO
            {
                UserId = 1,
                Username = "ErrorUser"
            };
            var sessionId = "session123";

            _userRepositoryMock
                .Setup(repo => repo.GetUserInfo(userDto.UserId))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _userService.ModifyUserInfo(userDto, sessionId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("操作異常，請聯繫管理員", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Never);
            _redisServiceMock.Verify(redis => redis.SetUserInfoAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }



        /*
            ModifyPassword 
         
        */

        [Test]
        public async Task ModifyPassword_WhenUserIdIsZero_ReturnsError()
        {
            // Arrange
            int userId = 0;
            var modifyPasswordDto = new ModifyPasswordDTO
            {
                OldPassword = "oldPass123",
                Password = "newPass123"
            };

            // Act
            var result = await _userService.ModifyPassword(userId, modifyPasswordDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("用戶不存在", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.GetUserInfo(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public async Task ModifyPassword_WhenUserDoesNotExist_ReturnsError()
        {
            // Arrange
            int userId = 1;
            var modifyPasswordDto = new ModifyPasswordDTO
            {
                OldPassword = "oldPass123",
                Password = "newPass123"
            };

            _userRepositoryMock.Setup(repo => repo.GetUserInfo(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.ModifyPassword(userId, modifyPasswordDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("用戶不存在", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.GetUserInfo(userId), Times.Once);
            _userDomainServiceMock.Verify(service => service.EnsurePasswordCanBeChanged(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task ModifyPassword_WhenDomainServiceThrowsException_ReturnsError()
        {
            // Arrange
            int userId = 1;
            var modifyPasswordDto = new ModifyPasswordDTO
            {
                OldPassword = "wrongOldPassword",
                Password = "newPass123"
            };

            var user = new User { Id = userId, PasswordHash = "hashedOldPassword" };

            _userRepositoryMock.Setup(repo => repo.GetUserInfo(userId)).ReturnsAsync(user);
            _userDomainServiceMock
                .Setup(service => service.EnsurePasswordCanBeChanged(user, modifyPasswordDto.OldPassword, modifyPasswordDto.Password))
                .Throws(new Exception("舊密碼不正確"));

            // Act
            var result = await _userService.ModifyPassword(userId, modifyPasswordDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("舊密碼不正確", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.GetUserInfo(userId), Times.Once);
            _userDomainServiceMock.Verify(service => service.EnsurePasswordCanBeChanged(user, modifyPasswordDto.OldPassword, modifyPasswordDto.Password), Times.Once);
            _userDomainServiceMock.Verify(service => service.ChangePassword(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task ModifyPassword_WhenPasswordChangedSuccessfully_ReturnsSuccess()
        {
            // Arrange
            int userId = 1;
            var modifyPasswordDto = new ModifyPasswordDTO
            {
                OldPassword = "oldPass123",
                Password = "newPass123"
            };

            var user = new User { Id = userId, PasswordHash = "hashedOldPassword" };

            _userRepositoryMock.Setup(repo => repo.GetUserInfo(userId)).ReturnsAsync(user);

            _userDomainServiceMock
                .Setup(service => service.EnsurePasswordCanBeChanged(user, modifyPasswordDto.OldPassword, modifyPasswordDto.Password));

            _userDomainServiceMock
                .Setup(service => service.ChangePassword(user, modifyPasswordDto.Password));

            _userRepositoryMock.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _userService.ModifyPassword(userId, modifyPasswordDto);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("修改密碼成功", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.GetUserInfo(userId), Times.Once);
            _userDomainServiceMock.Verify(service => service.EnsurePasswordCanBeChanged(user, modifyPasswordDto.OldPassword, modifyPasswordDto.Password), Times.Once);
            _userDomainServiceMock.Verify(service => service.ChangePassword(user, modifyPasswordDto.Password), Times.Once);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }



        /*
            UserRegister 
         
        */

        [Test]
        public async Task UserRegister_WhenUserAlreadyExists_ReturnsError()
        {
            // Arrange
            var signUpDto = new SignUpDTO
            {
                Username = "existingUser",
                Email = "existing@example.com",
                Password = "password123",
                NickName = "Test User"
            };

            _userDomainServiceMock
                .Setup(service => service.EnsureUserNotExists(signUpDto.Username, signUpDto.Email))
                .Throws(new Exception("User already exists"));

            // Act
            var result = await _userService.UserRegister(signUpDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("操作異常，請聯繫管理員", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Never);
            _redisServiceMock.Verify(redis => redis.SetUserInfoAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task UserRegister_WhenRegistrationSuccessful_ReturnsRedisKey()
        {
            // Arrange
            var signUpDto = new SignUpDTO
            {
                Username = "newUser",
                Email = "newuser@example.com",
                Password = "password123",
                NickName = "New User"
            };

            var userEntity = signUpDto.ToUserEntity();
            var createdUser = new User
            {
                Id = 1,
                Username = signUpDto.Username,
                Email = signUpDto.Email,
                NickName = signUpDto.NickName,
                Role = "user",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var userInfoDto = createdUser.ToUserInfoDTO();

            //檢查email、用戶銘是否存在
            _userDomainServiceMock
                .Setup(service => service.EnsureUserNotExists(signUpDto.Username, signUpDto.Email));

            _userRepositoryMock
                .Setup(repo => repo.AddUser(It.IsAny<User>()))
                .Callback<User>(user => user.Id = createdUser.Id);

            _userRepositoryMock
                .Setup(repo => repo.CheckUserExists(signUpDto.Username, signUpDto.Email))
                .ReturnsAsync(createdUser);

            _redisServiceMock
                .Setup(redis => redis.SetUserInfoAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _userService.UserRegister(signUpDto);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("註冊成功", result.ErrorMessage);

            Assert.IsNotNull(result.Data);

            _userDomainServiceMock.Verify(service => service.EnsureUserNotExists(signUpDto.Username, signUpDto.Email), Times.Once);
            _userRepositoryMock.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.CheckUserExists(signUpDto.Username, signUpDto.Email), Times.Once);
            _redisServiceMock.Verify(redis => redis.SetUserInfoAsync(It.IsAny<string>(), It.Is<string>(data => data.Contains(userInfoDto.Username))), Times.Once);
        }

        [Test]
        public async Task UserRegister_WhenExceptionThrown_ReturnsError()
        {
            // Arrange
            var signUpDto = new SignUpDTO
            {
                Username = "newUser",
                Email = "newuser@example.com",
                Password = "password123",
                NickName = "New User"
            };

            _userDomainServiceMock
                .Setup(service => service.EnsureUserNotExists(signUpDto.Username, signUpDto.Email))
                .Throws(new Exception("Unexpected exception"));

            // Act
            var result = await _userService.UserRegister(signUpDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("操作異常，請聯繫管理員", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Never);
            _redisServiceMock.Verify(redis => redis.SetUserInfoAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }


        /*
            UserLogin 
         
        */

        [Test]
        public async Task UserLogin_WhenUserDoesNotExist_ReturnsError()
        {
            // Arrange
            var loginDto = new LoginDTO
            {
                Username = "nonexistentUser",
                Password = "password123"
            };

            _userRepositoryMock
                .Setup(repo => repo.CheckUserExists(loginDto.Username, loginDto.Username))
                .ReturnsAsync((User)null);

            // Act
            var result = await _userService.UserLogin(loginDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("用戶不存在", result.ErrorMessage);

            _redisServiceMock.Verify(redis => redis.SetUserInfoAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task UserLogin_WhenPasswordIsIncorrect_ReturnsError()
        {
            // Arrange
            var loginDto = new LoginDTO
            {
                Username = "existingUser",
                Password = "wrongPassword"
            };

            var user = new User
            {
                Id = 1,
                Username = loginDto.Username,
                PasswordHash = BCryptUtils.HashPassword("correctPassword")
            };

            _userRepositoryMock
                .Setup(repo => repo.CheckUserExists(loginDto.Username, loginDto.Username))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.UserLogin(loginDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("密碼錯誤", result.ErrorMessage);

            _redisServiceMock.Verify(redis => redis.SetUserInfoAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task UserLogin_WhenLoginIsSuccessful_ReturnsRedisKey()
        {
            // Arrange
            var loginDto = new LoginDTO
            {
                Username = "existingUser",
                Password = "correctPassword"
            };

            var user = new User
            {
                Id = 1,
                Username = loginDto.Username,
                PasswordHash = BCryptUtils.HashPassword(loginDto.Password),
                Email = "test@example.com",
                Role = "user"
            };

            var userInfoDto = user.ToUserInfoDTO();

            _userRepositoryMock
                .Setup(repo => repo.CheckUserExists(loginDto.Username, loginDto.Username))
                .ReturnsAsync(user);

            _redisServiceMock
                .Setup(redis => redis.SetUserInfoAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _userService.UserLogin(loginDto);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("登入成功", result.ErrorMessage);
            Assert.IsNotNull(result.Data);

            _redisServiceMock.Verify(redis => redis.SetUserInfoAsync(It.IsAny<string>(), It.Is<string>(data => data.Contains(userInfoDto.Username))), Times.Once);
        }

        [Test]
        public async Task UserLogin_WhenExceptionThrown_ReturnsError()
        {
            // Arrange
            var loginDto = new LoginDTO
            {
                Username = "existingUser",
                Password = "password123"
            };

            _userRepositoryMock
                .Setup(repo => repo.CheckUserExists(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception("Unexpected exception"));

            // Act
            var result = await _userService.UserLogin(loginDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("操作異常，請聯繫管理員", result.ErrorMessage);

            _redisServiceMock.Verify(redis => redis.SetUserInfoAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }



        /*
            UserAuthLogin  
         
        */
        [Test]
        public async Task UserAuthLogin_Success_ReturnsUserInfo()
        {
            // Arrange
            var authLogin = new AuthLogin { code = "valid-code", state = "user" };

            var googleResponse = new OAuth2GoogleResp
            {
                id_token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwiZW1haWwiOiJ0ZXN0QGV4YW1wbGUuY29tIiwibmFtZSI6IlRlc3QgVXNlciIsImlhdCI6MTcwMDAwMDAwMCwiZXhwIjoxNzAwMDAzNjAwfQ.2wX5oJmOMF-UQW4YdYmJHXPZC7TSXHSN_yD1ivSYgWQ",
                access_token = "valid-access-token",
                expires_in = 3600,
                token_type = "Bearer"
            };

            var jwtUserInfo = new GoogleUserInfo
            {
                Sub = "1234567890",
                Email = "test@example.com",
                Name = "TestNick",
                Picture = "https://example.com/picture.jpg",
                EmailVerified = true,
                GivenName = "Test",
                FamilyName = "User",
                Iat = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Exp = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()
            };

            var user = new User
            {
                Id = 1,
                Username = "Test User",
                Email = "test@example.com",
                GoogleId = "1234567890",
                NickName = "TestNick",
                Picture = jwtUserInfo.Picture,
                Role = "user",
                CreatedAt = DateTime.Now.AddDays(-10),
                UpdatedAt = DateTime.Now,
                LastLogin = DateTime.Now
            };

            _configurationMock.Setup(c => c["GoogleAuthClientSecret"]).Returns("test-secret");

            _httpUtilsMock
                .Setup(utils => utils.PostFormAsync<OAuth2GoogleResp>(
                    "https://oauth2.googleapis.com/token",
                    It.IsAny<Dictionary<string, string>>(),
                    null
                ))
                .ReturnsAsync(googleResponse);

            _userRepositoryMock
                .Setup(repo => repo.GetUserIfExistsByGoogleID(jwtUserInfo.Sub))
                .ReturnsAsync(user);

            _userRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            _userRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _userService.UserAuthLogin(authLogin);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(user.Email, result.UserInfo.Email);
            Assert.AreEqual(jwtUserInfo.Picture, result.UserInfo.Picture);
            Assert.AreEqual(jwtUserInfo.Name, result.UserInfo.NickName);

            _httpUtilsMock.Verify(utils => utils.PostFormAsync<OAuth2GoogleResp>(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), null), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetUserIfExistsByGoogleID(jwtUserInfo.Sub), Times.Once);
        }

        [Test]
        public async Task UserAuthLogin_UserDoesNotExist_CreatesAndReturnsUserInfo()
        {
            // Arrange
            // Arrange
            var authLogin = new AuthLogin { code = "valid-code", state = "user" };

            var googleResponse = new OAuth2GoogleResp
            {
                id_token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwiZW1haWwiOiJ0ZXN0QGV4YW1wbGUuY29tIiwibmFtZSI6IlRlc3QgVXNlciIsImlhdCI6MTcwMDAwMDAwMCwiZXhwIjoxNzAwMDAzNjAwfQ.2wX5oJmOMF-UQW4YdYmJHXPZC7TSXHSN_yD1ivSYgWQ",
                access_token = "valid-access-token",
                expires_in = 3600,
                token_type = "Bearer"
            };

            var jwtUserInfo = new GoogleUserInfo
            {
                Sub = "1234567890",
                Email = "test@example.com",
                Name = "TestNick",
                Picture = "https://example.com/picture.jpg",
                EmailVerified = true,
                GivenName = "Test",
                FamilyName = "User",
                Iat = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Exp = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()
            };

            var user = new User
            {
                Id = 1,
                Username = "Test User",
                Email = "test@example.com",
                GoogleId = "1234567890",
                NickName = "TestNick",
                Picture = jwtUserInfo.Picture,
                Role = "user",
                CreatedAt = DateTime.Now.AddDays(-10),
                UpdatedAt = DateTime.Now,
                LastLogin = DateTime.Now
            };


            _configurationMock.Setup(c => c["GoogleAuthClientSecret"]).Returns("test-secret");

            _httpUtilsMock
                .Setup(utils => utils.PostFormAsync<OAuth2GoogleResp>(
                    "https://oauth2.googleapis.com/token",
                    It.IsAny<Dictionary<string, string>>(),
                    null
                ))
                .ReturnsAsync(googleResponse);

            _userRepositoryMock
                .SetupSequence(repo => repo.GetUserIfExistsByGoogleID(jwtUserInfo.Sub))
                .ReturnsAsync((User)null) // First call: user does not exist
                .ReturnsAsync(user);  // Second call: user created and retrieved

            _userRepositoryMock
                .Setup(repo => repo.AddAsync(It.Is<User>(u =>
                    u.GoogleId == jwtUserInfo.Sub &&
                    u.Email == jwtUserInfo.Email &&
                    u.NickName == jwtUserInfo.Name
                )))
                .Returns(Task.CompletedTask);

            _userRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _userService.UserAuthLogin(authLogin);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(user.Email, result.UserInfo.Email);
            Assert.AreEqual(user.Picture, result.UserInfo.Picture);
            Assert.AreEqual(user.NickName, result.UserInfo.NickName);

            _httpUtilsMock.Verify(utils => utils.PostFormAsync<OAuth2GoogleResp>(
                "https://oauth2.googleapis.com/token",
                It.IsAny<Dictionary<string, string>>(),
                null
            ), Times.Once);

            _userRepositoryMock.Verify(repo => repo.GetUserIfExistsByGoogleID(jwtUserInfo.Sub), Times.Exactly(2)); // Before and after creation
            _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void DecodeIDToken_ValidToken_ReturnsGoogleUserInfo()
        {
            // Arrange
            var idToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwiZW1haWwiOiJ0ZXN0QGV4YW1wbGUuY29tIiwibmFtZSI6IlRlc3QgVXNlciIsImlhdCI6MTcwMDAwMDAwMCwiZXhwIjoxNzAwMDAzNjAwfQ.2wX5oJmOMF-UQW4YdYmJHXPZC7TSXHSN_yD1ivSYgWQ";

            // Act
            var decodedResult = typeof(UserService)
                .GetMethod("DecodeIDToken", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.Invoke(_userService, new object[] { idToken }) as GoogleUserInfo;

            // Assert
            Assert.NotNull(decodedResult);
            Assert.AreEqual("1234567890", decodedResult.Sub);
            Assert.AreEqual("test@example.com", decodedResult.Email);
            Assert.AreEqual("Test User", decodedResult.Name);
        }
        [Test]
        public async Task UserAuthLogin_InvalidCode_ReturnsError()
        {
            // Arrange
            var authLogin = new AuthLogin { code = "invalid-code", state = "user" };
            _configurationMock.Setup(c => c["GoogleAuthClientSecret"]).Returns("test-secret");

            _httpUtilsMock
                .Setup(utils => utils.PostFormAsync<OAuth2GoogleResp>("https://oauth2.googleapis.com/token", It.IsAny<Dictionary<string, string>>(), null))
                .ReturnsAsync((OAuth2GoogleResp)null);

            // Act
            var result = await _userService.UserAuthLogin(authLogin);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("some error occured when request token", result.ErrorMessage);
        }

        

        [Test]
        public async Task UserAuthLogin_ConfigurationMissing_ThrowsError()
        {
            // Arrange
            var authLogin = new AuthLogin { code = "valid-code", state = "user" };
            _configurationMock.Setup(c => c["GoogleAuthClientSecret"]).Returns((string)null);

            // Act
            var result = await _userService.UserAuthLogin(authLogin);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("some error occured", result.ErrorMessage);
        }

        /*
            UserShippingAddress 類
         
         
        */
        [Test]
        public void GetUserShippingAddress_WhenUserIdIsZero_ReturnsError()
        {
            // Arrange
            int userId = 0;

            // Act
            var result = _userService.GetUserShippingAddress(userId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("用戶不存在", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.GetUserShippingAddress(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void GetUserShippingAddress_WhenUserExists_ReturnsShippingAddresses()
        {
            // Arrange
            int userId = 1;

            var addressList = new List<UserShipAddress>
            {
                new UserShipAddress { Id = 1, UserId = userId, RecipientName = "John Doe", PhoneNumber = "1234567890", RecieveWay = "7-11", RecieveStore = "Central Store", AddressLine = "123 Main St", IsDefault = true },
                new UserShipAddress { Id = 2, UserId = userId, RecipientName = "Jane Smith", PhoneNumber = "0987654321", RecieveWay = "FamilyMart", RecieveStore = "West Store", AddressLine = "456 Elm St", IsDefault = false }
            };

            _userRepositoryMock
                .Setup(repo => repo.GetUserShippingAddress(userId))
                .Returns(addressList);

            // Act
            var result = _userService.GetUserShippingAddress(userId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count);

            var firstAddress = result.Data[0];
            Assert.AreEqual("John Doe", firstAddress.RecipientName);
            Assert.AreEqual("1234567890", firstAddress.PhoneNumber);
            Assert.AreEqual("7-11", firstAddress.RecieveWay);
            Assert.AreEqual("Central Store", firstAddress.RecieveStore);
            Assert.AreEqual("123 Main St", firstAddress.AddressLine);
            Assert.IsTrue(firstAddress.IsDefault);

            _userRepositoryMock.Verify(repo => repo.GetUserShippingAddress(userId), Times.Once);
        }

        [Test]
        public void GetUserShippingAddress_WhenExceptionThrown_ReturnsError()
        {
            // Arrange
            int userId = 1;

            _userRepositoryMock
                .Setup(repo => repo.GetUserShippingAddress(userId))
                .Throws(new Exception("Database error"));

            // Act
            var result = _userService.GetUserShippingAddress(userId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("操作異常，請聯繫管理員", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.GetUserShippingAddress(userId), Times.Once);
        }


        [Test]
        public void AddUserShippingAddress_WhenUserIdIsZero_ReturnsError()
        {
            // Arrange
            int userId = 0;
            var addressDto = new UserShipAddressDTO
            {
                RecipientName = "John Doe",
                PhoneNumber = "1234567890",
                RecieveWay = "7-11",
                RecieveStore = "Central Store",
                AddressLine = "123 Main St",
                IsDefault = false
            };

            // Act
            var result = _userService.AddUserShippingAddress(userId, addressDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("不存在的用戶", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.AddUserShippingAddress(It.IsAny<int>(), It.IsAny<UserShipAddress>()), Times.Never);
        }

        [Test]
        public void AddUserShippingAddress_WhenValidAddress_AddsAddress()
        {
            // Arrange
            int userId = 1;
            var addressDto = new UserShipAddressDTO
            {
                RecipientName = "John Doe",
                PhoneNumber = "1234567890",
                RecieveWay = "7-11",
                RecieveStore = "Central Store",
                AddressLine = "123 Main St",
                IsDefault = false
            };

            // Act
            var result = _userService.AddUserShippingAddress(userId, addressDto);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("ok", result.Data);

            _userRepositoryMock.Verify(repo => repo.AddUserShippingAddress(userId, It.Is<UserShipAddress>(address =>
                address.UserId == userId &&
                address.RecipientName == addressDto.RecipientName &&
                address.PhoneNumber == addressDto.PhoneNumber &&
                address.RecieveWay == addressDto.RecieveWay &&
                address.RecieveStore == addressDto.RecieveStore &&
                address.AddressLine == addressDto.AddressLine &&
                address.CreatedAt != default &&
                address.UpdatedAt != default
            )), Times.Once);
        }

        [Test]
        public void AddUserShippingAddress_WhenExceptionThrown_ReturnsError()
        {
            // Arrange
            int userId = 1;
            var addressDto = new UserShipAddressDTO
            {
                RecipientName = "John Doe",
                PhoneNumber = "1234567890",
                RecieveWay = "7-11",
                RecieveStore = "Central Store",
                AddressLine = "123 Main St",
                IsDefault = false
            };

            _userRepositoryMock
                .Setup(repo => repo.AddUserShippingAddress(It.IsAny<int>(), It.IsAny<UserShipAddress>()))
                .Throws(new Exception("Database error"));

            // Act
            var result = _userService.AddUserShippingAddress(userId, addressDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("系統異常，請聯繫管理員", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.AddUserShippingAddress(userId, It.IsAny<UserShipAddress>()), Times.Once);
        }


        [Test]
        public void ModifyUserShippingAddress_WhenUserIdIsZero_ReturnsError()
        {
            // Arrange
            int userId = 0;
            var addressDto = new UserShipAddressDTO
            {
                AddressId = 1,
                RecipientName = "John Doe",
                PhoneNumber = "1234567890",
                RecieveWay = "7-11",
                RecieveStore = "Central Store",
                AddressLine = "123 Main St"
            };

            // Act
            var result = _userService.ModifyUserShippingAddress(userId, addressDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("不存在的用戶", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.ModifyUserShippingAddress(It.IsAny<int>(), It.IsAny<UserShipAddress>()), Times.Never);
        }

        [Test]
        public void ModifyUserShippingAddress_WhenValidAddress_ModifiesAddress()
        {
            // Arrange
            int userId = 1;
            var addressDto = new UserShipAddressDTO
            {
                AddressId = 1,
                RecipientName = "John Doe",
                PhoneNumber = "1234567890",
                RecieveWay = "7-11",
                RecieveStore = "Central Store",
                AddressLine = "123 Main St"
            };

            // Act
            var result = _userService.ModifyUserShippingAddress(userId, addressDto);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("修改成功", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.ModifyUserShippingAddress(userId, It.Is<UserShipAddress>(address =>
                address.Id == addressDto.AddressId &&
                address.UserId == userId &&
                address.RecipientName == addressDto.RecipientName &&
                address.PhoneNumber == addressDto.PhoneNumber &&
                address.RecieveWay == addressDto.RecieveWay &&
                address.RecieveStore == addressDto.RecieveStore &&
                address.AddressLine == addressDto.AddressLine &&
                address.UpdatedAt != default
            )), Times.Once);
        }

        [Test]
        public void ModifyUserShippingAddress_WhenExceptionThrown_ReturnsError()
        {
            // Arrange
            int userId = 1;
            var addressDto = new UserShipAddressDTO
            {
                AddressId = 1,
                RecipientName = "John Doe",
                PhoneNumber = "1234567890",
                RecieveWay = "7-11",
                RecieveStore = "Central Store",
                AddressLine = "123 Main St"
            };

            _userRepositoryMock
                .Setup(repo => repo.ModifyUserShippingAddress(It.IsAny<int>(), It.IsAny<UserShipAddress>()))
                .Throws(new Exception("Database error"));

            // Act
            var result = _userService.ModifyUserShippingAddress(userId, addressDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("系統異常，請聯繫管理員", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.ModifyUserShippingAddress(userId, It.IsAny<UserShipAddress>()), Times.Once);
        }


        [Test]
        public void DeleteUserShippingAddress_WhenUserIdIsZero_ReturnsError()
        {
            // Arrange
            int userId = 0;
            int addressId = 1;

            // Act
            var result = _userService.DeleteUserShippingAddress(userId, addressId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("不存在的用戶", result.Data);

            _userRepositoryMock.Verify(repo => repo.DeleteUserShippingAddress(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void DeleteUserShippingAddress_WhenValidUserAndAddress_DeletesAddress()
        {
            // Arrange
            int userId = 1;
            int addressId = 1;

            // Act
            var result = _userService.DeleteUserShippingAddress(userId, addressId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("操作成功", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.DeleteUserShippingAddress(userId, addressId), Times.Once);
        }

        [Test]
        public void DeleteUserShippingAddress_WhenExceptionThrown_ReturnsError()
        {
            // Arrange
            int userId = 1;
            int addressId = 1;

            _userRepositoryMock
                .Setup(repo => repo.DeleteUserShippingAddress(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception("Database error"));

            // Act
            var result = _userService.DeleteUserShippingAddress(userId, addressId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("系統異常，請聯繫管理員", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.DeleteUserShippingAddress(userId, addressId), Times.Once);
        }


        [Test]
        public void SetDefaultShippingAddress_WhenUserIdIsZero_ReturnsError()
        {
            // Arrange
            int userId = 0;
            int addressId = 1;

            // Act
            var result = _userService.SetDefaultShippingAddress(userId, addressId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("不存在的用戶", result.Data);

            _userRepositoryMock.Verify(repo => repo.SetDefaultShippingAddress(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void SetDefaultShippingAddress_WhenValidUserAndAddress_SetsDefaultAddress()
        {
            // Arrange
            int userId = 1;
            int addressId = 1;

            // Act
            var result = _userService.SetDefaultShippingAddress(userId, addressId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("操作成功", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.SetDefaultShippingAddress(userId, addressId), Times.Once);
        }

        [Test]
        public void SetDefaultShippingAddress_WhenExceptionThrown_ReturnsError()
        {
            // Arrange
            int userId = 1;
            int addressId = 1;

            _userRepositoryMock
                .Setup(repo => repo.SetDefaultShippingAddress(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception("Database error"));

            // Act
            var result = _userService.SetDefaultShippingAddress(userId, addressId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("系統異常，請聯繫管理員", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.SetDefaultShippingAddress(userId, addressId), Times.Once);
        }


        /*
            FavoriteList 類 
         
        */

        [Test]
        public async Task RemoveFromFavoriteList_WhenUserIdIsZero_ReturnsError()
        {
            // Arrange
            int userId = 0;
            int productId = 101;

            // Act
            var result = await _userService.RemoveFromfavoriteList(userId, productId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("不存在的用戶", result.Data);

            _userRepositoryMock.Verify(repo => repo.RemoveFromFavoriteList(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public async Task RemoveFromFavoriteList_WhenValidUserAndProduct_RemovesProduct()
        {
            // Arrange
            int userId = 1;
            int productId = 101;

            // 模擬 Repository 方法成功執行
            _userRepositoryMock.Setup(repo => repo.RemoveFromFavoriteList(userId, productId)).Returns(Task.CompletedTask);

            // Act
            var result = await _userService.RemoveFromfavoriteList(userId, productId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("操作成功", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.RemoveFromFavoriteList(userId, productId), Times.Once);
        }

        [Test]
        public async Task RemoveFromFavoriteList_WhenExceptionThrown_ReturnsError()
        {
            // Arrange
            int userId = 1;
            int productId = 101;

            // 模擬 Repository 方法拋出異常
            _userRepositoryMock
                .Setup(repo => repo.RemoveFromFavoriteList(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _userService.RemoveFromfavoriteList(userId, productId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("系統異常，請聯繫管理員", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.RemoveFromFavoriteList(userId, productId), Times.Once);
        }


        [Test]
        public async Task AddToFavoriteList_WhenUserIdIsZero_ReturnsError()
        {
            // Arrange
            int userId = 0;
            int productId = 101;

            // Act
            var result = await _userService.AddTofavoriteList(userId, productId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("不存在的用戶", result.Data);

            _userRepositoryMock.Verify(repo => repo.AddToFavoriteList(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public async Task AddToFavoriteList_WhenValidUserAndProduct_AddsProductToFavorites()
        {
            // Arrange
            int userId = 1;
            int productId = 101;

            // 模擬 Repository 方法成功執行
            _userRepositoryMock.Setup(repo => repo.AddToFavoriteList(userId, productId)).Returns(Task.CompletedTask);

            // Act
            var result = await _userService.AddTofavoriteList(userId, productId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("操作成功", result.Data);

            _userRepositoryMock.Verify(repo => repo.AddToFavoriteList(userId, productId), Times.Once);
        }

        [Test]
        public async Task AddToFavoriteList_WhenExceptionThrown_ReturnsError()
        {
            // Arrange
            int userId = 1;
            int productId = 101;

            // 模擬 Repository 方法拋出異常
            _userRepositoryMock
                .Setup(repo => repo.AddToFavoriteList(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _userService.AddTofavoriteList(userId, productId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("系統異常，請聯繫管理員", result.ErrorMessage);

            _userRepositoryMock.Verify(repo => repo.AddToFavoriteList(userId, productId), Times.Once);
        }


    }
}
