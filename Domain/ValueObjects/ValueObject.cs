namespace Domain.ValueObjects
{
    /// <summary>
    /// 值對象基類
    /// 值對象的特點：
    /// 1. 不可變（Immutable）
    /// 2. 通過值相等性比較（而非引用相等性）
    /// 3. 沒有唯一標識（Identity）
    /// 4. 封裝驗證邏輯
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// 獲取用於比較的值對象屬性
        /// </summary>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// 值對象相等性比較
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// 獲取值對象的雜湊碼
        /// </summary>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// 相等運算符重載
        /// </summary>
        public static bool operator ==(ValueObject? left, ValueObject? right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        /// <summary>
        /// 不等運算符重載
        /// </summary>
        public static bool operator !=(ValueObject? left, ValueObject? right)
        {
            return !(left == right);
        }
    }
}

