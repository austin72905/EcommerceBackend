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

        public async Task SetUserInfoAsync(string sessonId,string  userIndo)
        {
            string key= $"user:{sessonId}";

            try
            {

                await _db.StringSetAsync(key, userIndo, TimeSpan.FromHours(2));

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
    }
}
