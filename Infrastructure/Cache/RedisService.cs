using Common.Interfaces.Infrastructure;
using StackExchange.Redis;
using System.Linq;

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

        /// <summary>
        /// 設置單個商品變體的庫存
        /// </summary>
        /// <param name="productId">商品ID</param>
        /// <param name="variantId">變體ID</param>
        /// <param name="stock">庫存數量</param>
        /// <returns></returns>
        public async Task SetProductStockAsync(int variantId, int stock)
        {
            string key = "product:stock";
            string field = variantId.ToString();

            try
            {
                await _db.HashSetAsync(key, field, stock);
                Console.WriteLine($"Set stock for variant {variantId}: {stock}");
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
        /// 獲取單個商品變體的庫存
        /// </summary>
        /// <param name="productId">商品ID</param>
        /// <param name="variantId">變體ID</param>
        /// <returns></returns>
        public async Task<int?> GetProductStockAsync(int variantId)
        {
            string key = "product:stock";
            string field = variantId.ToString();

            try
            {
                var result = await _db.HashGetAsync(key, field);
                if (result.HasValue)
                {
                    return (int)result;
                }
                return null;
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

        /// <summary>
        /// 批量設置商品庫存
        /// </summary>
        /// <param name="stockData">庫存資料字典，key格式為 "productId:variantId"</param>
        /// <returns></returns>
        public async Task SetProductStocksAsync(Dictionary<string, int> stockData)
        {
            try
            {
                string key = "product:stock";
                var hashEntries = new HashEntry[stockData.Count];
                int index = 0;

                foreach (var item in stockData)
                {
                   
                     hashEntries[index] = new HashEntry(item.Key, item.Value);
                     index++;
                    
                }

                // 批量設置所有庫存
                await _db.HashSetAsync(key, hashEntries);
                Console.WriteLine($"Successfully set {stockData.Count} product stock records to Redis");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while setting product stocks: {ex.Message}");
            }
        }

        /// <summary>
        /// 獲取所有商品庫存資料
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, int>> GetAllProductStocksAsync()
        {
            var result = new Dictionary<string, int>();

            try
            {
                string key = "product:stock";
                var hashEntries = await _db.HashGetAllAsync(key);
                
                foreach (var entry in hashEntries)
                {
                    var variantId = entry.Name.ToString();
                    result[variantId] = (int)entry.Value;
                }

                Console.WriteLine($"Retrieved {result.Count} product stock records from Redis");
                return result;
            }
            catch (RedisConnectionException ex)
            {
                Console.WriteLine($"Redis connection error: {ex.Message}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return result;
            }
        }

        public async Task<Dictionary<int, int>> GetProductStocksAsync(int[] variantIds)
        {

            var result = new Dictionary<int, int>();

            try
            {
                string key = "product:stock";

                // 將 variantIds 轉換為 RedisValue 陣列
                var redisFields = variantIds.Select(id => (RedisValue)id.ToString()).ToArray();

                // 一次性查詢所有 variant 的庫存
                var stocks = await _db.HashGetAsync(key, redisFields);

                // 將結果轉換為 Dictionary
                for (int i = 0; i < variantIds.Length; i++)
                {
                    if (stocks[i].HasValue)
                    {
                        result[variantIds[i]] = (int)stocks[i];
                    }
                }

                Console.WriteLine($"Retrieved {result.Count} product stocks from Redis for {variantIds.Length} variants");
                return result;
            }
            catch (RedisConnectionException ex)
            {
                Console.WriteLine($"Redis connection error: {ex.Message}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return result;
            }
        }

        public async Task<object> CheckAndHoldStockAsync(string orderId, Dictionary<int, int> variantStock)
        {
            var stockKey = "product:stock";
            // 預扣，如果未支付or 失敗，就補回去
            var holdKey = $"stock:hold:{orderId}";

            var expireSeconds = 150;

            string luaScript = @"
            local stockKey = KEYS[1]
            local holdKey = KEYS[2]
            local expireSeconds = tonumber(ARGV[#ARGV])

            -- 最後一個參數，過期秒數，例如 600 秒
            local totalArgs = #ARGV - 1

            local insufficient = {}
            
            -- 檢查所有庫存是否足夠
            for i = 1, totalArgs, 2 do
                local variantId = ARGV[i]
                local needQty = tonumber(ARGV[i+1])
                local currentQty = tonumber(redis.call('HGET', stockKey, variantId) or '-1')
                if currentQty < needQty then
                    table.insert(insufficient, variantId)                -- 不足的加入列表
                end
            end
            
            -- 有任何 variant 不足，則直接返回錯誤（但用 JSON 包裝）
            if #insufficient > 0 then
                return cjson.encode({ status = 'error', failed = insufficient })
            end
            
            -- 通過檢查 → 開始扣庫存 + 寫入 holdKey
            for i = 1, totalArgs, 2 do
                local variantId = ARGV[i]
                local needQty = tonumber(ARGV[i+1])
                redis.call('HINCRBY', stockKey, variantId, -needQty)   -- 扣庫存
                redis.call('HSET', holdKey, variantId, needQty)        -- 建立 hold 資料
            end
            
            -- 設定 holdKey 自動過期時間（保留 10 分鐘）
            redis.call('EXPIRE', holdKey, expireSeconds)
            
            -- 回傳成功訊息
            return cjson.encode({ status = 'success' })
            ";

            var args = new List<RedisValue>();

            foreach (var kvp in variantStock) 
            {
                args.Add(kvp.Key);
                args.Add(kvp.Value);
            }

            args.Add(expireSeconds);


            var result = await _db.ScriptEvaluateAsync(
                luaScript,
                new RedisKey[] { stockKey ,holdKey},
                args.ToArray()
                );
        
            return  result;

        }

        public async Task<object?> RollbackStockAsync(string orderId)
        {
            var stockKey = "product:stock";
            // 預扣，如果未支付or 失敗，就補回去
            var holdKey = $"stock:hold:{orderId}";

            string luaScript = @"
            local stockKey = KEYS[1]
            local holdKey = KEYS[2]
            local heldItems = redis.call('HGETALL', holdKey)

            for i = 1, #heldItems, 2 do
                local variantId = heldItems[i]
                local qty = tonumber(heldItems[i+1])
                redis.call('HINCRBY', stockKey, variantId, qty)
            end

            redis.call('DEL', holdKey)
            return cjson.encode({ status = 'success' })
            ";

            var result = await _db.ScriptEvaluateAsync(
                luaScript,
                new RedisKey[] { stockKey, holdKey },
                Array.Empty<RedisValue>()
                );

            return result ?? null ;
        }



        public async Task<bool> IsRateLimitExceededAsync(string userId, string apiKey)
        {
            string key = $"ratelimit:{apiKey}:{userId}";
            long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var (maxTokens, refillRate) = GetTokenBucketRule(apiKey);

            string luaScript = @"
            -- KEYS[1] = Redis key（每個使用者 + API 一組 key）
            -- ARGV[1] = maxTokens（桶子的最大容量）
            -- ARGV[2] = refillRate（每秒補充多少 token，可以是小數）
            -- ARGV[3] = now（目前時間戳（秒））

            -- 把參數取出
            local key = KEYS[1]
            local maxTokens = tonumber(ARGV[1])
            local refillRate = tonumber(ARGV[2])
            local now = tonumber(ARGV[3])

            -- 從 Redis Hash 裡一次讀出目前可用 token 數量 和 上次補充時間戳
            local bucket = redis.call('HMGET', key, 'tokens', 'lastRefillTs')

            -- 沒有的話就給預設值（第一次初始化）
            local tokens = tonumber(bucket[1]) or maxTokens
            local lastRefillTs = tonumber(bucket[2]) or now

            -- 計算從上次補充到現在經過幾秒
            local delta = math.max(0, now - lastRefillTs)

            -- 根據 refillRate 計算出應該補幾個 token  ， 假設 refillRate = 2 tokens/sec，且距離上次補充已過 3 秒，就會補 6 個 token
            local refill = delta * refillRate

            -- 目前 token 數量 = 原本剩下的 + 新補的，但不能超過桶子最大容量
            tokens = math.min(maxTokens, tokens + refill)
            
            -- 如果剩下不到 1 個 token，代表不能通過限流，直接 return 0（阻擋請求）
            if tokens < 1 then
                return 0
            end
            
            -- 可以通過，扣掉 1 個令牌
            tokens = tokens - 1

            -- 更新 Redis 中目前 token 數量與上次補充時間
            redis.call('HMSET', key, 'tokens', tokens, 'lastRefillTs', now)

            -- 設定過期時間（如果 60 秒內沒再使用這個 key，它會被 Redis 自動清除。）
            redis.call('EXPIRE', key, 60)

            -- 成功通過限流，return 1
            return 1
            ";

            var result = (int)await _db.ScriptEvaluateAsync(
                luaScript,
                new RedisKey[] { key },
                new RedisValue[] { maxTokens, refillRate, now }
            );

            return result == 1;
        }


        private (int maxTokens, double refillRate) GetTokenBucketRule(string apiKey)
        {

            /*
                元祖 () 代表意義
                最大突發請求數  補充速率
                ex: (3,0.2) 代表 最大突發請求數  3 次 , 補充速率 每秒 0.2 次
             
             
             */

            return apiKey switch
            {
                "user/login" => (3, 0.2),   // 每 5 秒一次 (因為每秒0.2次)
                "order/submitorder" => (5, 1.0),   // 每秒最多 1 次， 最大突發請求數  5 次
                "product/list" => (5, 2.0),// 非常寬鬆 每秒 10 次
                _ => (5, 1), //  如果是 0.1 就會變成 1/ 0.1 =10秒補一個token，前面手賤1秒打完5個，後面就是慢慢等，
            };
        }




    }






}
