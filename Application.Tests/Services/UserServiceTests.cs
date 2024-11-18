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

    }
}
