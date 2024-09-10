namespace EcommerceBackend.Models
{
    public class OrderInfomation
    {
        public string? RecordCode { get; set; }

        public List<ProductWithCount>? ProductList { get; set; }
        public int OrderPrice { get; set; }

        public OrderAddress? Address { get; set; }

        public string? Status { get; set; }

        public string? PayWay { get; set; }

        public int ShippingPrice { get; set; }

        public List<OrderStep>? OrderStepInfomation { get; set; }

        public List<ShipmentInfo>? ShipInfomation { get; set; }

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
        public string? Date { get; set; }
    }



}
