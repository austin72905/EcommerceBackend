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
        public async Task EnsureUserNotExists_UserExists_ThrowsException()
        {
            // Arrange
            var existingUser = new User { Username = "ExistingUser", Email = "existing@example.com" };
            _userRepositoryMock
                .Setup(repo => repo.CheckUserExists("ExistingUser", "existing@example.com"))
                .ReturnsAsync(existingUser);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _userDomainService.EnsureUserNotExists("ExistingUser", "existing@example.com"));
            Assert.AreEqual("已有相同信箱 existing@example.com", ex.Message);
        }

        [Test]
        public async Task EnsureUserNotExists_UserDoesNotExist_DoesNotThrow()
        {
            // Arrange
            _userRepositoryMock
                .Setup(repo => repo.CheckUserExists("NewUser", "new@example.com"))
                .ReturnsAsync((User)null);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () =>
                await _userDomainService.EnsureUserNotExists("NewUser", "new@example.com"));
        }

        [Test]
        public void EnsurePasswordCanBeChanged_InvalidOldPassword_ThrowsException()
        {
            // Arrange
            var user = new User { PasswordHash = BCryptUtils.HashPassword("correct-password") };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                _userDomainService.EnsurePasswordCanBeChanged(user, "wrong-password", "new-password"));
            Assert.AreEqual("舊密碼輸入錯誤", ex.Message);
        }

        [Test]
        public void EnsurePasswordCanBeChanged_SameOldAndNewPassword_ThrowsException()
        {
            // Arrange
            var user = new User { PasswordHash = BCryptUtils.HashPassword("password") };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                _userDomainService.EnsurePasswordCanBeChanged(user, "password", "password"));
            Assert.AreEqual("新密碼不得與舊密碼相同", ex.Message);
        }

        [Test]
        public void EnsurePasswordCanBeChanged_ValidOldPassword_DoesNotThrow()
        {
            // Arrange
            var user = new User { PasswordHash = BCryptUtils.HashPassword("password") };

            // Act & Assert
            Assert.DoesNotThrow(() =>
                _userDomainService.EnsurePasswordCanBeChanged(user, "password", "new-password"));
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
