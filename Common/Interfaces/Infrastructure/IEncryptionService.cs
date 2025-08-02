using System.Text;

namespace Common.Interfaces.Infrastructure
{
    public interface IEncryptionService
    {
        /// <summary>
        /// 對密碼進行 BCrypt 雜湊
        /// </summary>
        /// <param name="password">原始密碼</param>
        /// <returns>雜湊後的密碼</returns>
        string HashPassword(string password);

        /// <summary>
        /// 驗證密碼
        /// </summary>
        /// <param name="password">原始密碼</param>
        /// <param name="hash">雜湊值</param>
        /// <returns>是否匹配</returns>
        bool VerifyPassword(string password, string hash);

        /// <summary>
        /// SHA256 雜湊
        /// </summary>
        /// <param name="input">輸入字串</param>
        /// <param name="encoding">編碼方式，預設為UTF8</param>
        /// <returns>雜湊結果</returns>
        string Sha256Hash(string input, Encoding? encoding = null);
    }
}