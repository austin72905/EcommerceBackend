using Application.Oauth;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Extensions;
using Application.DTOs;
using Common.Interfaces.Infrastructure;
using Moq;

namespace Application.Tests.Extensions
{
    [TestFixture]
    public class UserExtensionsTests
    {
        [Test]
        public void ToUserEntity_GoogleUserInfo_MapsCorrectly()
        {
            // Arrange
            var googleUserInfo = new GoogleUserInfo
            {
                Email = "test@example.com",
                Sub = "google-id",
                Name = "Google User"
            };

            // Act
            var user = googleUserInfo.ToUserEntity();

            // Assert
            Assert.AreEqual(googleUserInfo.Email, user.Email);
            Assert.AreEqual(googleUserInfo.Sub, user.GoogleId);
            Assert.AreEqual(googleUserInfo.Name, user.NickName);
        }

        [Test]
        public void ToUserInfoDTO_User_MapsCorrectly()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Username = "testuser",
                Email = "test@example.com",
                NickName = "Test User",
                PhoneNumber = "1234567890",
                Gender = "Male",
                Birthday = new DateTime(2000, 1, 1)
            };

            // Act
            var dto = user.ToUserInfoDTO();

            // Assert
            Assert.AreEqual(user.Id, dto.UserId);
            Assert.AreEqual(user.Username, dto.Username);
            Assert.AreEqual(user.Email, dto.Email);
            Assert.AreEqual(user.NickName, dto.NickName);
            Assert.AreEqual(user.PhoneNumber, dto.PhoneNumber);
            Assert.AreEqual(user.Gender, dto.Gender);
            Assert.AreEqual("2000/1/1", dto.Birthday);
        }


        [Test]
        public void ToUserEntity_FromUserInfoDTO_MapsCorrectly()
        {
            // Arrange
            var dto = new UserInfoDTO
            {
                UserId = 1,
                Username = "testuser",
                Email = "test@example.com",
                NickName = "Test Nickname",
                PhoneNumber = "1234567890",
                Gender = "Male",
                Birthday = "2000/1/1",
                Picture = "profile.jpg",
                Type = "admin"
            };

            // Act
            var user = dto.ToUserEntity();

            // Assert
            Assert.AreEqual(dto.UserId, user.Id);
            Assert.AreEqual(dto.Username, user.Username);
            Assert.AreEqual(dto.Email, user.Email);
            Assert.AreEqual(dto.NickName, user.NickName);
            Assert.AreEqual(dto.PhoneNumber, user.PhoneNumber);
            Assert.AreEqual(dto.Gender, user.Gender);
            Assert.AreEqual(new DateTime(2000, 1, 1), user.Birthday);
            Assert.AreEqual(dto.Picture, user.Picture);
            Assert.AreEqual(dto.Type, user.Role);
        }

        [Test]
        public void ToUserEntity_FromUserInfoDTO_WithNullValues_UsesDefaults()
        {
            // Arrange
            var dto = new UserInfoDTO
            {
                UserId = 2,
                Username = "testuser",
                NickName = "Test Nickname",
                Birthday = null, // 沒有生日
                Type = null      // 沒有角色
            };

            // Act
            var user = dto.ToUserEntity();

            // Assert
            Assert.AreEqual(dto.UserId, user.Id);
            Assert.AreEqual(dto.Username, user.Username);
            Assert.AreEqual(string.Empty, user.Email); // 預設值
            Assert.AreEqual(dto.NickName, user.NickName);
            Assert.AreEqual(DateTime.MinValue, user.Birthday); // 預設值   DateTime.MinValue    (DateTime? 沒有值)  試圖直接訪問未賦值的 DateTime? 時，可能隱式轉換為 DateTime 類型，這時候 DateTime 的默認值是 0001-01-01 00:00:00（即 DateTime.MinValue）。
            Assert.AreEqual("user", user.Role); // 預設值
        }

        [Test]
        public void ToUserEntity_FromSignUpDTO_MapsCorrectly()
        {
            // Arrange
            var signUpDto = new SignUpDTO
            {
                Username = "newuser",
                Email = "newuser@example.com",
                NickName = "New Nickname",
                Password = "securepassword"
            };

            var mockEncryptionService = new Mock<IEncryptionService>();
            mockEncryptionService.Setup(x => x.HashPassword(It.IsAny<string>()))
                .Returns((string password) => $"hashed_{password}");

            // Act
            var user = signUpDto.ToUserEntity(mockEncryptionService.Object);

            // Assert
            Assert.AreEqual(signUpDto.Username, user.Username);
            Assert.AreEqual(signUpDto.Email, user.Email);
            Assert.AreEqual(signUpDto.NickName, user.NickName);
            Assert.AreEqual($"hashed_{signUpDto.Password}", user.PasswordHash); // 密碼哈希驗證
            Assert.AreEqual("user", user.Role); // 預設值
            Assert.IsNotNull(user.CreatedAt);
            Assert.IsNotNull(user.UpdatedAt);
            Assert.IsNotNull(user.LastLogin);
        }
    }
}
