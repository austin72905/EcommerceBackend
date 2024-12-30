using Infrastructure.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Cache
{
    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisService(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _db = _redis.GetDatabase();
        }

        public async Task SetUserInfoAsync(string sessonId, string userInfo)
        {
            string key = $"user:{sessonId}";

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

        /// <summary>
        /// 設置輸入密碼錯誤次數
        /// </summary>
        /// <param name="username"></param>
        /// <param name="keepTtl"></param>
        /// <returns></returns>
        public async Task SetWrongPasswordTimeAsync(string username,bool keepTtl=false)
        {
            string key = $"login:fail:{username}";

            try
            {
                if (keepTtl)
                {
                    await _db.StringIncrementAsync(key,1); // INCR 和 DECR 命令執行時，不會影響鍵的 TTL
                }
                else
                {
                    await _db.StringSetAsync(key, 1, TimeSpan.FromMinutes(10));
                }
                

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

        /// <summary>
        /// 鎖定用戶
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task LockUserAsync(string username)
        {
            string key = $"login:fail:{username}";

            try
            {
                await _db.StringSetAsync(key, 3, TimeSpan.FromMinutes(15));

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


        /// <summary>
        /// 檢查是否有密碼輸入錯誤的紀錄
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<int?> GetWrongPasswordTimeAsync(string username)
        {
            string key = $"login:fail:{username}";

            try
            {
                var result = await _db.StringGetAsync(key);
                return (int)result;
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
