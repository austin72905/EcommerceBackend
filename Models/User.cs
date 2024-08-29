namespace EcommerceBackend.Models
{
    // 資料庫 映射
    public class User
    {
        public int Id { get; set; }
        public string? UserId { get; set; }  // GUID
        public string? Username { get; set; }  // 用戶名 (帳號)
        public string? Email { get; set; }  // 電子郵件，唯一
        public string? PasswordHash { get; set; }  // 密碼

        public string? GoogleId { get; set; }  // 使用google帳號登陸時
        public string? NickName { get; set; }  // 用戶自訂義名稱
        public string? PhoneNumber { get; set; }  // 電話號碼

        public string? Gender { get; set; }  // 性別
        public DateTime? Birthday { get; set; }  // 生日
        public string Role { get; set; } = "user";  // 用户角色，如admin、user等
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // 帳戶創建時間
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;  // 帳戶更新時間
        public DateTime? LastLogin { get; set; }  // 上次登陸時間
    }

    // 返回給用戶
    public class UserInfoDTO
    {
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? NickName { get; set; }  // 用戶自訂義名稱
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }  // 性別
        public DateTime? Birthday { get; set; }  // 生日


    }

    public class UserShipAddress
    {
        public int AddressId { get; set; }  // 地址ID，主键
        public string? UserId { get; set; }  // 外键，關聯到User
        public string? RecipientName { get; set; }  // 收件人姓名
        public string? PhoneNumber { get; set; }  // 收件人電話
        public string? Email { get; set; }  // 收件人電子信箱
        public string? AddressLine { get; set; }  // 主要地址行
        public bool IsDefault { get; set; }  // 是否為默認地址

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
