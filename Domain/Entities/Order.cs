namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RecordCode { get; set; }
        public int? AddressId { get; set; }
        public string Receiver { get; set; }
        public string PhoneNumber { get; set; }
        public string ShippingAddress { get; set; }
        public int OrderPrice { get; set; }
        // 訂單狀態
        public int Status { get; set; }
        // 支付方式
        public int PayWay { get; set; }
        public int ShippingPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public User User { get; set; }
        public UserShipAddress Address { get; set; }
        public Payment Payment { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
        public ICollection<OrderStep> OrderSteps { get; set; }

        public ICollection<Shipment> Shipments { get; set; }
    }
}
