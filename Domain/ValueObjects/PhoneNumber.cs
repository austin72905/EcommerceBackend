using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    /// <summary>
    /// 電話號碼值對象
    /// 封裝電話號碼驗證和格式化邏輯
    /// </summary>
    public class PhoneNumber : ValueObject
    {
        // 台灣手機號碼格式：09XX-XXX-XXX 或 09XXXXXXXX
        private static readonly Regex TaiwanMobileRegex = new(
            @"^09\d{8}$",
            RegexOptions.Compiled);

        // 台灣市話格式：0X-XXXX-XXXX 或 0XXXXXXXXX
        private static readonly Regex TaiwanLandlineRegex = new(
            @"^0\d{1,2}-?\d{6,8}$",
            RegexOptions.Compiled);

        /// <summary>
        /// 電話號碼值
        /// </summary>
        public string Value { get; }

        // 私有構造函數
        private PhoneNumber(string value)
        {
            Value = value;
        }

        /// <summary>
        /// 創建電話號碼值對象
        /// </summary>
        /// <param name="phoneNumber">電話號碼字串</param>
        /// <returns>電話號碼值對象</returns>
        /// <exception cref="ArgumentException">當電話號碼格式無效時拋出</exception>
        public static PhoneNumber Create(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("電話號碼不能為空", nameof(phoneNumber));

            // 移除所有非數字字元
            var normalized = Regex.Replace(phoneNumber, @"[^\d]", "");

            if (!IsValid(normalized))
                throw new ArgumentException($"無效的電話號碼格式: {phoneNumber}", nameof(phoneNumber));

            return new PhoneNumber(normalized);
        }

        /// <summary>
        /// 驗證電話號碼格式（支援台灣手機和市話）
        /// </summary>
        public static bool IsValid(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            var normalized = Regex.Replace(phoneNumber, @"[^\d]", "");
            
            // 檢查是否為台灣手機號碼或市話
            return TaiwanMobileRegex.IsMatch(normalized) || 
                   TaiwanLandlineRegex.IsMatch(normalized);
        }

        /// <summary>
        /// 格式化為顯示字串（手機：09XX-XXX-XXX，市話：0X-XXXX-XXXX）
        /// </summary>
        public string ToFormattedString()
        {
            if (Value.Length == 10 && Value.StartsWith("09"))
            {
                // 手機號碼：09XX-XXX-XXX
                return $"{Value.Substring(0, 4)}-{Value.Substring(4, 3)}-{Value.Substring(7, 3)}";
            }
            else if (Value.Length >= 9 && Value.Length <= 10)
            {
                // 市話：0X-XXXX-XXXX
                if (Value.Length == 9)
                    return $"{Value.Substring(0, 2)}-{Value.Substring(2, 4)}-{Value.Substring(6, 3)}";
                else
                    return $"{Value.Substring(0, 2)}-{Value.Substring(2, 4)}-{Value.Substring(6, 4)}";
            }

            return Value;
        }

        /// <summary>
        /// 隱式轉換為字串（方便使用）
        /// </summary>
        public static implicit operator string(PhoneNumber phoneNumber)
        {
            return phoneNumber?.Value ?? string.Empty;
        }

        /// <summary>
        /// 顯示轉換為 PhoneNumber（需要驗證）
        /// </summary>
        public static explicit operator PhoneNumber(string phoneNumber)
        {
            return Create(phoneNumber);
        }

        /// <summary>
        /// 轉換為字串
        /// </summary>
        public override string ToString() => ToFormattedString();

        /// <summary>
        /// 獲取用於比較的屬性
        /// </summary>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}

