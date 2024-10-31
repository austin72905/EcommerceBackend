namespace Domain.Entities
{
    public class UserShipAddress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RecipientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AddressLine { get; set; }
        public bool IsDefault { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }
    }
}
