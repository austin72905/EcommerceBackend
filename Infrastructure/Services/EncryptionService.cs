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