using Common.Interfaces.Infrastructure;
using Domain.Enums;
using EcommerceBackend.MiddleWares;
using EcommerceBackend.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Text.Json;

namespace EcommerceBackend.Tests.MiddleWares
{
    [TestFixture]
    public class AuthenticationMiddlewareTests
    {
        private Mock<IRedisService> _redisServiceMock;
        private Mock<RequestDelegate> _nextDelegateMock;
        private AuthenticationMiddleware _middleware;

        [SetUp]
        public void Setup()
        {
            _redisServiceMock = new Mock<IRedisService>();
            _nextDelegateMock = new Mock<RequestDelegate>();
            _middleware = new AuthenticationMiddleware(_nextDelegateMock.Object, _redisServiceMock.Object);
        }

        private DefaultHttpContext CreateHttpContext(string path, string method = "GET", Dictionary<string, string>? cookies = null, string? csrfToken = null)
        {
            var context = new DefaultHttpContext();
            context.Request.Path = path;
            context.Request.Method = method;
            context.Response.Body = new MemoryStream();

            if (cookies != null)
            {
                var cookieHeader = string.Join("; ", cookies.Select(kvp => $"{kvp.Key}={kvp.Value}"));
                context.Request.Headers["Cookie"] = cookieHeader;
            }

            if (!string.IsNullOrEmpty(csrfToken))
            {
                context.Request.Headers["X-CSRF-Token"] = csrfToken;
            }

            return context;
        }

        [Test]
        public async Task InvokeAsync_WithValidSessionIdAndAuthPath_AllowsRequest()
        {
            // Arrange
            var sessionId = "valid-session-id";
            var path = "/user/profile";
            var userInfo = "{\"UserId\":1,\"Username\":\"testuser\"}";

            var cookies = new Dictionary<string, string>
            {
                { "session-id", sessionId }
            };

            var context = CreateHttpContext(path, "GET", cookies);

            _redisServiceMock.Setup(redis => redis.GetUserInfoAsync(sessionId))
                .ReturnsAsync(userInfo);

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            Assert.AreEqual(userInfo, context.Items["UserInfo"]);
            Assert.AreEqual(StatusCodes.Status200OK, context.Response.StatusCode);
            _nextDelegateMock.Verify(next => next(context), Times.Once);
        }

        [Test]
        public async Task InvokeAsync_WithoutSessionId_ReturnsUnauthorized()
        {
            // Arrange
            var path = "/user/profile";
            var context = CreateHttpContext(path);

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            Assert.AreEqual(StatusCodes.Status200OK, context.Response.StatusCode);
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            using var reader = new StreamReader(context.Response.Body);
            var responseText = await reader.ReadToEndAsync();

            // 如果中間件返回的是 JSON 格式，需解析 JSON
            Assert.IsNotEmpty(responseText, "Response body should not be empty.");

            var responseJson = JsonSerializer.Deserialize<ApiResponse>(responseText);
            Assert.IsNotNull(responseJson);
            Assert.AreEqual((int)RespCode.UN_AUTHORIZED, responseJson.Code);
            Assert.AreEqual("未授權，請重新登入", responseJson.Message);
        }

        /*
            測試 headers 跟 cookies 的csrf token 不一樣時的返回 
        */
        [Test]
        public async Task InvokeAsync_WithInvalidCsrfToken_ReturnsForbidden()
        {
            // Arrange
            var sessionId = "valid-session-id";
            var path = "/user/update";
            var userInfo = "{\"UserId\":1,\"Username\":\"testuser\"}";

            var cookies = new Dictionary<string, string>
            {
                { "session-id", sessionId },
                { "X-CSRF-Token", "valid-token" } // 模擬存儲在服務端的有效 CSRF Token
            };

            var context = CreateHttpContext(path, "POST", cookies, csrfToken: "invalid-token");

            _redisServiceMock.Setup(redis => redis.GetUserInfoAsync(sessionId))
                .ReturnsAsync(userInfo);

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            Assert.AreEqual(StatusCodes.Status403Forbidden, context.Response.StatusCode);
            var responseBody = context.Response.Body;
            responseBody.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(responseBody);
            var responseText = await reader.ReadToEndAsync();
            Assert.IsTrue(responseText.Contains("Invalid CSRF Token"));
        }

        [Test]
        public async Task InvokeAsync_WithExemptPath_AllowsRequest()
        {
            // Arrange
            var path = "/user/UserLogin";
            var context = CreateHttpContext(path);

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            Assert.AreEqual(StatusCodes.Status200OK, context.Response.StatusCode);
            _nextDelegateMock.Verify(next => next(context), Times.Once);
        }
    }
}
