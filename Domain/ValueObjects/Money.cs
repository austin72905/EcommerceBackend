namespace Domain.ValueObjects
{
    /// <summary>
    /// 金額值對象
    /// 封裝金額計算和貨幣單位
    /// </summary>
    public class Money : ValueObject
    {
        /// <summary>
        /// 金額值（以最小單位計算，例如：分、元）
        /// </summary>
        public int Amount { get; }

        /// <summary>
        /// 貨幣單位（預設為 TWD - 新台幣）
        /// </summary>
        public string Currency { get; }

        // 私有構造函數
        private Money(int amount, string currency = "TWD")
        {
            if (amount < 0)
                throw new ArgumentException("金額不能為負數", nameof(amount));

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("貨幣單位不能為空", nameof(currency));

            Amount = amount;
            Currency = currency;
        }

        /// <summary>
        /// 創建金額值對象
        /// </summary>
        /// <param name="amount">金額</param>
        /// <param name="currency">貨幣單位（預設為 TWD）</param>
        /// <returns>金額值對象</returns>
        public static Money Create(int amount, string currency = "TWD")
        {
            return new Money(amount, currency);
        }

        /// <summary>
        /// 創建零金額
        /// </summary>
        public static Money Zero(string currency = "TWD")
        {
            return new Money(0, currency);
        }

        /// <summary>
        /// 金額相加
        /// </summary>
        public Money Add(Money other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (Currency != other.Currency)
                throw new InvalidOperationException($"無法相加不同貨幣：{Currency} 和 {other.Currency}");

            return new Money(Amount + other.Amount, Currency);
        }

        /// <summary>
        /// 金額相減
        /// </summary>
        public Money Subtract(Money other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (Currency != other.Currency)
                throw new InvalidOperationException($"無法相減不同貨幣：{Currency} 和 {other.Currency}");

            var result = Amount - other.Amount;
            if (result < 0)
                throw new InvalidOperationException("金額相減後不能為負數");

            return new Money(result, Currency);
        }

        /// <summary>
        /// 金額相乘（用於折扣等場景）
        /// </summary>
        public Money Multiply(decimal multiplier)
        {
            if (multiplier < 0)
                throw new ArgumentException("乘數不能為負數", nameof(multiplier));

            var result = (int)(Amount * multiplier);
            return new Money(result, Currency);
        }

        /// <summary>
        /// 比較金額大小
        /// </summary>
        public bool IsGreaterThan(Money other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (Currency != other.Currency)
                throw new InvalidOperationException($"無法比較不同貨幣：{Currency} 和 {other.Currency}");

            return Amount > other.Amount;
        }

        /// <summary>
        /// 比較金額大小
        /// </summary>
        public bool IsLessThan(Money other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (Currency != other.Currency)
                throw new InvalidOperationException($"無法比較不同貨幣：{Currency} 和 {other.Currency}");

            return Amount < other.Amount;
        }

        /// <summary>
        /// 格式化為顯示字串
        /// </summary>
        public string ToDisplayString()
        {
            // 將金額轉換為元（假設 Amount 以分為單位）
            var dollars = Amount / 100.0m;
            return $"{Currency} {dollars:N2}";
        }

        /// <summary>
        /// 隱式轉換為整數（方便使用）
        /// </summary>
        public static implicit operator int(Money money)
        {
            return money?.Amount ?? 0;
        }

        /// <summary>
        /// 顯示轉換為 Money
        /// </summary>
        public static explicit operator Money(int amount)
        {
            return Create(amount);
        }

        /// <summary>
        /// 轉換為字串
        /// </summary>
        public override string ToString() => $"{Amount} {Currency}";

        /// <summary>
        /// 獲取用於比較的屬性
        /// </summary>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        // 運算符重載
        public static Money operator +(Money left, Money right)
        {
            return left?.Add(right) ?? throw new ArgumentNullException(nameof(left));
        }

        public static Money operator -(Money left, Money right)
        {
            return left?.Subtract(right) ?? throw new ArgumentNullException(nameof(left));
        }

        public static Money operator *(Money money, decimal multiplier)
        {
            return money?.Multiply(multiplier) ?? throw new ArgumentNullException(nameof(money));
        }
    }
}

