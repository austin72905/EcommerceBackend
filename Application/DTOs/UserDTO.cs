namespace Application.DTOs
{
    // 返回給用戶
    public class UserInfoDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? NickName { get; set; }  // 用戶自訂義名稱
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }  // 性別
        public DateTime? Birthday { get; set; }  // 生日

        public string? Picture { get; set; }

        public string? Type { get; set; }

    }

    public class UserShipAddressDTO
    {
        public int AddressId { get; set; }
        public string? RecipientName { get; set; }  // 收件人姓名
        public string? PhoneNumber { get; set; }  // 收件人電話
        public string? Email { get; set; }  // 收件人電子信箱
        public string? AddressLine { get; set; }  // 主要地址行
        public bool IsDefault { get; set; }  // 是否為默認地址

    }

}
