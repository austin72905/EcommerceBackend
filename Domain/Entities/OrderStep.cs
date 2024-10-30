namespace Domain.Entities
{
    public class OrderStep
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int StepStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Order Order { get; set; }
    }
}
