using EcommerceBackend.Interfaces.Services;
using EcommerceBackend.Models;
using StackExchange.Redis;

namespace EcommerceBackend.Services
{
    public class RedisService: IRedisService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisService(IConnectionMultiplexer redis)
        {
            _redis=redis;
            _db=_redis.GetDatabase();
        }

        public async Task SetUserInfoAsync(string sessonId,string  userInfo)
        {
            string key= $"user:{sessonId}";

            try
            {

                await _db.StringSetAsync(key, userInfo, TimeSpan.FromHours(2));

                Console.WriteLine($"userkey: {key}");
            }
            catch (RedisConnectionException ex)
            {
                Console.WriteLine($"Redis connection error: {ex.Message}");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            
        }

        public async Task<string?> GetUserInfoAsync(string sessonId)
        {
            string key = $"user:{sessonId}";

            try
            {
                var result = await _db.StringGetAsync(key);
                return result;
            }
            catch (RedisConnectionException ex)
            {
                Console.WriteLine($"Redis connection error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }

            
        }

        public async Task DelUserInfoAsync(string sessonId)
        {
            string key = $"user:{sessonId}";

            
            bool wasDeleted = await _db.KeyDeleteAsync(key);

            if (wasDeleted)
            {
                Console.WriteLine($"Key '{key}' was successfully deleted.");
            }
            else
            {
                Console.WriteLine($"Key '{key}' does not exist.");
            }
        }
    }
}
