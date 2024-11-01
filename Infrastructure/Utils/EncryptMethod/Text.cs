using System.Text;

namespace Infrastructure.Utils.EncryptMethod
{
    public class Text
    {

        /// <summary>
        /// 將 Byte Array 轉換為 Hex 字串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>小寫的 Hex 字串</returns>
        public static string BytesToHex(byte[] bytes)
        {
            var builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
