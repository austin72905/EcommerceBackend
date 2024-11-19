using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;

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
            Assert.AreEqual("no", result.Data);
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
    }
}
