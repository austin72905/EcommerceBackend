using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utils.EncryptMethod
{
    public class BCryptUtils
    {

        // 註冊階段 - 加密並存儲密碼
        public static string HashPassword(string plainPassword)
        {
            // 生成帶有隨機鹽值的哈希密碼
            return BCrypt.Net.BCrypt.HashPassword(plainPassword);
        }

        // 登錄階段 - 比對密碼
        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            // 比對用戶輸入的密碼與儲存的哈希
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
        }
    }
}
