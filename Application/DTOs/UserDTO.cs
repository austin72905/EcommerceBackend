﻿namespace Application.DTOs
{
    // 返回給用戶
    public class UserInfoDTO
    {
        public int UserId { get; set; } 
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? NickName { get; set; }  // 用戶自訂義名稱
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }  // 性別
        public string? Birthday { get; set; }  // 生日

        public string? Picture { get; set; }

        public string? Type { get; set; }

    }

    public class UserShipAddressDTO
    {
        public int AddressId { get; set; }
        public string RecipientName { get; set; }  // 收件人姓名
        public string PhoneNumber { get; set; }  // 收件人電話
        public string RecieveWay { get; set; } // 收件方式
        public string RecieveStore { get; set; } // 收件門市
        public string AddressLine { get; set; }  // 主要地址行
        public bool IsDefault { get; set; }  // 是否為默認地址

    }


    public class SignUpDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }  // 用戶自訂義名稱

        public string Password { get; set; }
    }

    public class LoginDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

    }

}
