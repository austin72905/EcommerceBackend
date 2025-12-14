using Common.Interfaces.Infrastructure;
using Infrastructure.Utils.EncryptMethod;
using System.Text;

namespace Infrastructure.Services
{
    public class EncryptionService : IEncryptionService
    {
        public string HashPassword(string password)
        {
            return BCryptUtils.HashPassword(password);
        }

        /// <summary>
        /// 異步執行 BCrypt 雜湊，避免阻塞請求處理線程
        /// 在高併發場景下，將 CPU 密集型操作移到背景線程執行
        /// </summary>
        public async Task<string> HashPasswordAsync(string password)
        {
            // 將 CPU 密集型操作移到背景線程執行，避免阻塞 ASP.NET Core 請求處理線程
            return await Task.Run(() => BCryptUtils.HashPassword(password));
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCryptUtils.VerifyPassword(password, hash);
        }

        public string Sha256Hash(string input, Encoding? encoding = null)
        {
            return SHA.Hash256(input, encoding);
        }
    }
}