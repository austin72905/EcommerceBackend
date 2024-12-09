using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Services;
using Infrastructure.Utils.EncryptMethod;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Services
{
    [TestFixture]
    public class UserDomainServiceTests
    {
        private UserDomainService _userDomainService;
        private Mock<IUserRepository> _userRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userDomainService = new UserDomainService(_userRepositoryMock.Object);
        }

        [Test]
        public void UpdateUser_UpdatesUserProperties()
        {
            // Arrange
            var user = new User { Username = "OldName", Email = "old@example.com" };
            var updateInfo = new User { Username = "NewName", Email = "new@example.com" };

            // Act
            _userDomainService.UpdateUser(user, updateInfo);

            // Assert
            Assert.AreEqual("NewName", user.Username);
            Assert.AreEqual("new@example.com", user.Email);
            Assert.AreEqual(DateTime.Now.Date, user.UpdatedAt.Date); // Verify UpdatedAt is set
        }

        [Test]
        public async Task EnsureUserNotExists_UserWithSameEmail_ReturnIsSuccessFail()
        {
            // Arrange
            var existingUser = new User { Username = "ExistingUser", Email = "existing@example.com" }; // 模擬repo執行後返回的節骨
            _userRepositoryMock
                .Setup(repo => repo.CheckUserExists("NewUser", "existing@example.com"))  //模擬當 CheckUserExists 方法被調用，並傳入 "NewUser" 和 "existing@example.com" 這兩個參數時，應返回什麼結果。
                .ReturnsAsync(existingUser);
            /*
             
                repo.CheckUserExists("NewUser", "existing@example.com") 
                _userDomainService.EnsureUserNotExists("NewUser", "existing@example.com")

                傳遞的參數要寫一樣的
            */
            var result =await _userDomainService.EnsureUserNotExists("NewUser", "existing@example.com");
            // Act & Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("已有相同信箱 existing@example.com", result.ErrorMessage);          
        }

        [Test]
        public async Task EnsureUserNotExists_UserWithSameUsername_ReturnIsSuccessFail()
        {
            // Arrange
            var existingUser = new User { Username = "ExistingUser", Email = "existing@example.com" };
            _userRepositoryMock
                .Setup(repo => repo.CheckUserExists("ExistingUser", "test@example.com"))
                .ReturnsAsync(existingUser);

            var result = await _userDomainService.EnsureUserNotExists("ExistingUser", "test@example.com");
            // Act & Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("已有相同帳號 ExistingUser", result.ErrorMessage);
        }

        [Test]
        public async Task EnsureUserNotExists_UserDoesNotExist_ReturnIsSuccessTrue()
        {
            // Arrange
            _userRepositoryMock
                .Setup(repo => repo.CheckUserExists("NewUser", "new@example.com"))
                .ReturnsAsync((User)null);

            var result = await _userDomainService.EnsureUserNotExists("NewUser", "new@example.com");
            // Act & Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task EnsureUserNotExists_WhenExceptionOccurs_ReturnException()
        {
            // Arrange
            _userRepositoryMock
                .Setup(repo => repo.CheckUserExists(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new System.Exception("Database connection failed"));

            // Act
            var result = await _userDomainService.EnsureUserNotExists("TestUser", "test@example.com");

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Database connection failed", result.ErrorMessage);
        }


        // 以google 登陸 不可以修改密碼
        [Test]
        public void EnsurePasswordCanBeChanged_PasswordHashIsNull_ReturnIsSuccessFail()
        {
            // Arrange
            var user = new User { PasswordHash = null };

            // Act
            var result = _userDomainService.EnsurePasswordCanBeChanged(user, "oldPassword", "newPassword");

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("此類型用戶無法修改密碼", result.ErrorMessage);
        }

        // 確認修改密碼內容無誤
        [Test]
        public void EnsurePasswordCanBeChanged_InvalidOldPassword_ReturnIsSuccessFail()
        {
            // Arrange
            var user = new User { PasswordHash = BCryptUtils.HashPassword("correct-password") };

            var result =_userDomainService.EnsurePasswordCanBeChanged(user, "wrong-password", "new-password");
            // Act & Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("舊密碼輸入錯誤", result.ErrorMessage);
            
        }

        [Test]
        public void EnsurePasswordCanBeChanged_SameOldAndNewPassword_ReturnIsSuccessFail()
        {
            // Arrange
            var user = new User { PasswordHash = BCryptUtils.HashPassword("password") };

            var result = _userDomainService.EnsurePasswordCanBeChanged(user, "password", "password");

            // Act & Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("新密碼不得與舊密碼相同", result.ErrorMessage);


        }

        [Test]
        public void EnsurePasswordCanBeChanged_ValidOldPasswordAndNewPassword_ReturnIsSuccessTrue()
        {
            // Arrange
            var user = new User { PasswordHash = BCryptUtils.HashPassword("password") };

            var result = _userDomainService.EnsurePasswordCanBeChanged(user, "password", "new-password");

            // Act & Assert
            Assert.IsTrue(result.IsSuccess);
        }

      




        [Test]
        public void ChangePassword_UpdatesPasswordHash()
        {
            // Arrange
            var user = new User();
            var newPassword = "new-password";

            // Act
            _userDomainService.ChangePassword(user, newPassword);

            // Assert
            Assert.IsTrue(BCryptUtils.VerifyPassword(newPassword, user.PasswordHash));
        }
    }
}
