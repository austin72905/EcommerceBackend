using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Email 值對象
    /// 封裝 Email 驗證邏輯和格式化
    /// </summary>
    public class Email : ValueObject
    {
        private static readonly Regex EmailRegex = new(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Email 地址值
        /// </summary>
        public string Value { get; }

        // 私有構造函數，強制使用工廠方法
        private Email(string value)
        {
            Value = value;
        }

        /// <summary>
        /// 創建 Email 值對象
        /// </summary>
        /// <param name="email">Email 地址字串</param>
        /// <returns>Email 值對象</returns>
        /// <exception cref="ArgumentException">當 Email 格式無效時拋出</exception>
        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email 不能為空", nameof(email));

            // 轉換為小寫並去除前後空白
            var normalizedEmail = email.Trim().ToLowerInvariant();

            if (!IsValid(normalizedEmail))
                throw new ArgumentException($"無效的 Email 格式: {email}", nameof(email));

            return new Email(normalizedEmail);
        }

        /// <summary>
        /// 驗證 Email 格式
        /// </summary>
        public static bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return EmailRegex.IsMatch(email.Trim());
        }

        /// <summary>
        /// 隱式轉換為字串（方便使用）
        /// </summary>
        public static implicit operator string(Email email)
        {
            return email?.Value ?? string.Empty;
        }

        /// <summary>
        /// 顯示轉換為 Email（需要驗證）
        /// </summary>
        public static explicit operator Email(string email)
        {
            return Create(email);
        }

        /// <summary>
        /// 轉換為字串
        /// </summary>
        public override string ToString() => Value;

        /// <summary>
        /// 獲取用於比較的屬性
        /// </summary>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}

