using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Services;
using Common.Interfaces.Infrastructure;
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
        private Mock<IEncryptionService> _encryptionServiceMock;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _encryptionServiceMock = new Mock<IEncryptionService>();
            
            // 設定 Mock 行為
            _encryptionServiceMock.Setup(x => x.HashPassword(It.IsAny<string>()))
                .Returns((string password) => $"hashed_{password}");
            _encryptionServiceMock.Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string password, string hash) => hash == $"hashed_{password}");
            
            _userDomainService = new UserDomainService(_userRepositoryMock.Object, _encryptionServiceMock.Object);
        }

        [Test]
        public void UpdateUser_UpdatesUserProperties()
        {
            // Arrange
            var user = User.CreateWithPassword("old@example.com", "OldName", "hashed_password");
            var updateInfo = User.CreateWithPassword("new@example.com", "NewName", "hashed_password");
            updateInfo.UpdateProfile(nickName: "NewNick", phoneNumber: "123456789", gender: "男", birthday: null);

            // Act - 直接使用 User.UpdateProfile 方法（因為 UserDomainService.UpdateUser 已過時）
            user.UpdateProfile(
                nickName: updateInfo.NickName,
                phoneNumber: updateInfo.PhoneNumber,
                gender: updateInfo.Gender,
                birthday: updateInfo.Birthday
            );

            // Assert
            Assert.AreEqual(DateTime.Now.Date, user.UpdatedAt.Date); // Verify UpdatedAt is set
        }

        [Test]
        public async Task EnsureUserNotExists_UserWithSameEmail_ReturnIsSuccessFail()
        {
            // Arrange
            var existingUser = User.CreateWithPassword("existing@example.com", "ExistingUser", "hashed_password"); // 模擬repo執行後返回的節骨
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
            var existingUser = User.CreateWithPassword("existing@example.com", "ExistingUser", "hashed_password");
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
            var user = User.CreateWithGoogle("test@example.com", "google123", "TestUser");

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
            var user = User.CreateWithPassword("test@example.com", "testuser", "hashed_correct-password");

            var result =_userDomainService.EnsurePasswordCanBeChanged(user, "wrong-password", "new-password");
            // Act & Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("舊密碼輸入錯誤", result.ErrorMessage);
            
        }

        [Test]
        public void EnsurePasswordCanBeChanged_SameOldAndNewPassword_ReturnIsSuccessFail()
        {
            // Arrange
            var user = User.CreateWithPassword("test@example.com", "testuser", "hashed_password");

            var result = _userDomainService.EnsurePasswordCanBeChanged(user, "password", "password");

            // Act & Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("新密碼不得與舊密碼相同", result.ErrorMessage);


        }

        [Test]
        public void EnsurePasswordCanBeChanged_ValidOldPasswordAndNewPassword_ReturnIsSuccessTrue()
        {
            // Arrange
            var user = User.CreateWithPassword("test@example.com", "testuser", "hashed_password");

            var result = _userDomainService.EnsurePasswordCanBeChanged(user, "password", "new-password");

            // Act & Assert
            Assert.IsTrue(result.IsSuccess);
        }

      




        [Test]
        public void ChangePassword_UpdatesPasswordHash()
        {
            // Arrange
            var user = User.CreateWithPassword("test@example.com", "testuser", "old_hash");
            var newPassword = "new-password";

            // Act
            _userDomainService.ChangePassword(user, newPassword);

            // Assert
            Assert.AreEqual($"hashed_{newPassword}", user.PasswordHash);
        }
    }
}
