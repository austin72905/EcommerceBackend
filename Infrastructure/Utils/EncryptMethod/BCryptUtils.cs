using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utils.EncryptMethod
{
    public class BCryptUtils
    {
        // PBKDF2 配置（用於高併發場景，效能比 BCrypt 好）
        private const int PBKDF2_ITERATIONS = 100000; // 迭代次數（平衡安全性和效能）
        private const int SALT_SIZE = 16; // 鹽值大小（128 bits）
        private const int HASH_SIZE = 32; // 雜湊大小（256 bits）

        // 註冊階段 - 加密並存儲密碼
        // 使用 PBKDF2 替代 BCrypt，在高併發下效能更好
        // PBKDF2 優點：
        // 1. .NET 內建，無需額外套件
        // 2. 在高併發下效能比 BCrypt 好（約 50-100ms vs 200-400ms）
        // 3. 安全性與 BCrypt 相當
        // 4. 微軟官方推薦的密碼雜湊方法
        public static string HashPassword(string plainPassword)
        {
            // 使用 PBKDF2 進行密碼雜湊（效能更好，適合高併發）
            return HashPasswordPBKDF2(plainPassword);
        }

        // 登錄階段 - 比對密碼
        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            // 檢查是否為 PBKDF2 格式（包含 $pbkdf2$ 前綴）
            if (storedHash.StartsWith("$pbkdf2$"))
            {
                return VerifyPasswordPBKDF2(inputPassword, storedHash);
            }
            
            // 向後兼容：如果是舊的 BCrypt 格式，使用 BCrypt 驗證
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
        }

        /// <summary>
        /// 使用 PBKDF2 進行密碼雜湊
        /// 格式：$pbkdf2$iterations$salt$hash (Base64 編碼)
        /// </summary>
        private static string HashPasswordPBKDF2(string password)
        {
            // 生成隨機鹽值
            byte[] salt = new byte[SALT_SIZE];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // 使用 PBKDF2 進行雜湊
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                PBKDF2_ITERATIONS,
                HashAlgorithmName.SHA256,
                HASH_SIZE
            );

            // 組合格式：$pbkdf2$iterations$salt$hash
            return $"$pbkdf2${PBKDF2_ITERATIONS}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// 驗證 PBKDF2 密碼
        /// </summary>
        private static bool VerifyPasswordPBKDF2(string password, string storedHash)
        {
            try
            {
                // 解析儲存的雜湊：$pbkdf2$iterations$salt$hash
                string[] parts = storedHash.Split('$');
                if (parts.Length != 5 || parts[1] != "pbkdf2")
                {
                    return false;
                }

                int iterations = int.Parse(parts[2]);
                byte[] salt = Convert.FromBase64String(parts[3]);
                byte[] storedHashBytes = Convert.FromBase64String(parts[4]);

                // 使用相同的參數重新計算雜湊
                byte[] computedHash = Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(password),
                    salt,
                    iterations,
                    HashAlgorithmName.SHA256,
                    storedHashBytes.Length
                );

                // 使用固定時間比較，避免時間攻擊
                return CryptographicOperations.FixedTimeEquals(computedHash, storedHashBytes);
            }
            catch
            {
                return false;
            }
        }
    }
}
