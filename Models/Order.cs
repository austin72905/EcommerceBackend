using EcommerceBackend.Enums;

namespace EcommerceBackend.Models
{
    public class OrderInfomation
    {
        public string? RecordCode { get; set; }

        public List<ProductWithCount>? ProductList { get; set; }
        public int OrderPrice { get; set; }

        public OrderAddress? Address { get; set; }

        public OrderStatus Status { get; set; }

        public PaymentMethod PayWay { get; set; }

        public int ShippingPrice { get; set; }

        public List<OrderStep>? OrderStepInfomation { get; set; }

        public List<ShipmentInfo>? ShipInfomation { get; set; }

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
        public string? UnachieveDescription { get; set; }
        public string? AchieveDescription { get; set; }
        public DateTime Date { get; set; }
        // 新增的更新時間
        public DateTime UpdatedAt { get; set; }
    }



}
