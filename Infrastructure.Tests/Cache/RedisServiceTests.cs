using Infrastructure.Cache;
using Moq;
using StackExchange.Redis;

namespace Infrastructure.Tests.Cache
{
    [TestFixture]
    public class RedisServiceTests
    {
        private Mock<IConnectionMultiplexer> _connectionMultiplexerMock;
        private Mock<IDatabase> _databaseMock;
        private RedisService _redisService;

        [SetUp]
        public void Setup()
        {
            _connectionMultiplexerMock = new Mock<IConnectionMultiplexer>();
            _databaseMock = new Mock<IDatabase>();

            // 模擬 GetDatabase 返回 _databaseMock
            _connectionMultiplexerMock
                .Setup(redis => redis.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(_databaseMock.Object);

            _redisService = new RedisService(_connectionMultiplexerMock.Object);
        }

        [Test]
        public async Task SetUserInfoAsync_ShouldCallStringSetAsync_WithCorrectParameters()
        {
            // Arrange
            string sessionId = "session123";
            string userInfo = "{\"UserId\":1,\"Username\":\"TestUser\"}";
            RedisKey key = new RedisKey($"user:{sessionId}"); // 顯式使用 RedisKey
            TimeSpan expiration = TimeSpan.FromHours(2);

            // 模擬 StringSetAsync 行為
            _databaseMock
                .Setup(db => db.StringSetAsync(
                    It.Is<RedisKey>(k => k == key), // 匹配 RedisKey
                    It.Is<RedisValue>(v => v == userInfo), // RedisValue 也需要匹配
                    It.Is<TimeSpan>(e => e == expiration),
                    It.Is<When>(w => w == When.Always),
                    It.Is<CommandFlags>(c => c == CommandFlags.None)
                ))
                .ReturnsAsync(true);

            // Act
            await _redisService.SetUserInfoAsync(sessionId, userInfo);

            // Assert
            _databaseMock.Verify(
                db => db.StringSetAsync(
                    It.Is<RedisKey>(k => k == key), // 匹配 RedisKey
                    It.Is<RedisValue>(v => v == userInfo), // 匹配 RedisValue
                    It.Is<TimeSpan>(e => e == expiration),
                    It.Is<When>(w => w == When.Always),
                    It.Is<CommandFlags>(c => c == CommandFlags.None)
                ),
                Times.Once
            );
        }

        [Test]
        public async Task GetUserInfoAsync_ShouldReturnCorrectValue()
        {
            // Arrange
            string sessionId = "session123";
            string expectedValue = "{\"UserId\":1,\"Username\":\"TestUser\"}";
            string key = $"user:{sessionId}";

            _databaseMock
                .Setup(db => db.StringGetAsync(key, It.IsAny<CommandFlags>()))
                .ReturnsAsync(expectedValue);

            // Act
            var result = await _redisService.GetUserInfoAsync(sessionId);

            // Assert
            Assert.AreEqual(expectedValue, result);
            _databaseMock.Verify(
                db => db.StringGetAsync(key, It.IsAny<CommandFlags>()),
                Times.Once
            );
        }

        [Test]
        public async Task DelUserInfoAsync_ShouldCallKeyDeleteAsync()
        {
            // Arrange
            string sessionId = "session123";
            string key = $"user:{sessionId}";

            _databaseMock
                .Setup(db => db.KeyDeleteAsync(key, It.IsAny<CommandFlags>()))
                .ReturnsAsync(true);

            // Act
            await _redisService.DelUserInfoAsync(sessionId);

            // Assert
            _databaseMock.Verify(
                db => db.KeyDeleteAsync(key, It.IsAny<CommandFlags>()),
                Times.Once
            );
        }

        [Test]
        public async Task GetUserInfoAsync_ShouldHandleRedisConnectionException()
        {
            // Arrange
            string sessionId = "session123";
            string key = $"user:{sessionId}";

            _databaseMock
                .Setup(db => db.StringGetAsync(key, It.IsAny<CommandFlags>()))
                .ThrowsAsync(new RedisConnectionException(ConnectionFailureType.SocketFailure, "Connection failed"));

            // Act
            var result = await _redisService.GetUserInfoAsync(sessionId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task SetUserInfoAsync_ShouldHandleException()
        {
            // Arrange
            string sessionId = "session123";
            string userInfo = "{\"UserId\":1,\"Username\":\"TestUser\"}";
            string key = $"user:{sessionId}";

            _databaseMock
                .Setup(db => db.StringSetAsync(key, userInfo, It.IsAny<TimeSpan>(), It.IsAny<When>(), It.IsAny<CommandFlags>()))
                .ThrowsAsync(new Exception("An unexpected error"));

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await _redisService.SetUserInfoAsync(sessionId, userInfo));
        }
    }
}
