using EcommerceBackend.Enums;

namespace EcommerceBackend.Models
{
    public class OrderInfomation
    {
        public int Id { get; set; }
        public string? RecordCode { get; set; }
         
        public int UserId { get; set; }

        public int AddressId { get; set; }

        public string? Receiver { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ShippingAddress { get; set; }


        public int OrderPrice { get; set; }


        public OrderStatus Status { get; set; }

        public PaymentMethod PayWay { get; set; }

        public int ShippingPrice { get; set; }


        // 新增的更新時間
        public DateTime UpdatedAt { get; set; }

    }

    


    public class OrderAddress
    {
        public string? Receiver { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ShippingAddress { get; set; }
    }

    public class OrderStep
    {
        public int StepId { get; set; }

        public int OrderId { get; set; }

        public int StepStatus { get; set; }

        public DateTime CreatedAt { get; set; }
        // 新增的更新時間
        public DateTime UpdatedAt { get; set; }
    }

    


}
