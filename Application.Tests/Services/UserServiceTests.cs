using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        [SetUp]
        public void Setup()
        {
            // 初始化所有依賴的模擬對象
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _redisServiceMock = new Mock<IRedisService>();
            _userDomainServiceMock = new Mock<IUserDomainService>();

            // 注入模擬對象到 UserService
            _userService = new UserService(
                _userRepositoryMock.Object,
                _configurationMock.Object,
                _redisServiceMock.Object,
                _userDomainServiceMock.Object
            );
        }

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
                Username= "aaaaa",
                PhoneNumber="11111111",
                Gender="男",
                Picture="",
                Birthday=null,
                Role="user"

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
            Assert.AreEqual("no", result.Data);
            Assert.AreEqual("用戶不存在", result.ErrorMessage);

            _userDomainServiceMock.Verify(service => service.UpdateUser(It.IsAny<User>(), It.IsAny<User>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Never);
            _redisServiceMock.Verify(redis => redis.SetUserInfoAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

    }
}
