namespace Domain.Entities
{
    public class UserShipAddress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RecipientName { get; set; }
        public string PhoneNumber { get; set; }
        // 收件方式 (7-11 OR 全家)
        public string RecieveWay { get; set; }
        // 門市名稱
        public string? RecieveStore { get; set; }

        public string AddressLine { get; set; }
        public bool IsDefault { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }
    }
}
