namespace Domain.ValueObjects
{
    /// <summary>
    /// 收貨地址值對象
    /// 封裝地址驗證和格式化邏輯
    /// </summary>
    public class ShippingAddress : ValueObject
    {
        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string RecipientName { get; }

        /// <summary>
        /// 電話號碼
        /// </summary>
        public string PhoneNumber { get; }

        /// <summary>
        /// 地址詳細資訊
        /// </summary>
        public string AddressLine { get; }

        /// <summary>
        /// 收貨方式
        /// </summary>
        public string ReceiveWay { get; }

        /// <summary>
        /// 收貨門市（如果是超商取貨）
        /// </summary>
        public string? ReceiveStore { get; }

        // 私有構造函數
        private ShippingAddress(
            string recipientName,
            string phoneNumber,
            string addressLine,
            string receiveWay,
            string? receiveStore = null)
        {
            RecipientName = recipientName;
            PhoneNumber = phoneNumber;
            AddressLine = addressLine;
            ReceiveWay = receiveWay;
            ReceiveStore = receiveStore;
        }

        /// <summary>
        /// 創建收貨地址值對象
        /// </summary>
        /// <param name="recipientName">收件人姓名</param>
        /// <param name="phoneNumber">電話號碼</param>
        /// <param name="addressLine">地址詳細資訊</param>
        /// <param name="receiveWay">收貨方式</param>
        /// <param name="receiveStore">收貨門市（可選）</param>
        /// <returns>收貨地址值對象</returns>
        /// <exception cref="ArgumentException">當參數無效時拋出</exception>
        public static ShippingAddress Create(
            string recipientName,
            string phoneNumber,
            string addressLine,
            string receiveWay,
            string? receiveStore = null)
        {
            if (string.IsNullOrWhiteSpace(recipientName))
                throw new ArgumentException("收件人姓名不能為空", nameof(recipientName));

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("電話號碼不能為空", nameof(phoneNumber));

            if (string.IsNullOrWhiteSpace(addressLine))
                throw new ArgumentException("地址不能為空", nameof(addressLine));

            if (string.IsNullOrWhiteSpace(receiveWay))
                throw new ArgumentException("收貨方式不能為空", nameof(receiveWay));

            return new ShippingAddress(
                recipientName.Trim(),
                phoneNumber.Trim(),
                addressLine.Trim(),
                receiveWay.Trim(),
                receiveStore?.Trim());
        }

        /// <summary>
        /// 格式化為完整地址字串
        /// </summary>
        public string ToFullAddressString()
        {
            var parts = new List<string>
            {
                $"收件人：{RecipientName}",
                $"電話：{PhoneNumber}",
                $"地址：{AddressLine}",
                $"收貨方式：{ReceiveWay}"
            };

            if (!string.IsNullOrWhiteSpace(ReceiveStore))
            {
                parts.Add($"收貨門市：{ReceiveStore}");
            }

            return string.Join("，", parts);
        }

        /// <summary>
        /// 轉換為字串
        /// </summary>
        public override string ToString() => ToFullAddressString();

        /// <summary>
        /// 獲取用於比較的屬性
        /// </summary>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return RecipientName;
            yield return PhoneNumber;
            yield return AddressLine;
            yield return ReceiveWay;
            yield return ReceiveStore ?? string.Empty;
        }
    }
}

