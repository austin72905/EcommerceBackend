using System.Security.Cryptography;
using System.Text;

namespace EcommerceBackend.Utils.EncryptMethod
{
    public sealed class SHA
    {

        public static string Hash256(string text, Encoding? encoding= null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            byte[] data = encoding.GetBytes(text);
            return Hash256ToHex(data);
        }

        /// <summary>
        /// 生成sha256雜湊值，並轉成hex
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Hash256ToHex(byte[] data)
        {
            byte[] hashBytes = hash256(data);
            return Text.BytesToHex(hashBytes);
        }

        /// <summary>
        /// 生成雜湊值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] hash256(byte[] data)
        {
            using (var hasher = SHA256.Create())
            {
                return hasher.ComputeHash(data);
            }
        }
    }
}
