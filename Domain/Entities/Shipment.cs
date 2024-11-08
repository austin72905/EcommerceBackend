namespace Domain.Entities
{
    public class Shipment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ShipmentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Order Order { get; set; }
    }
}
