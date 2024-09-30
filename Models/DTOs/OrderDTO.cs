using EcommerceBackend.Enums;

namespace EcommerceBackend.Models.DTOs
{
    public class OrderInfomationDTO
    {
        public int Id { get; set; }
        public string? RecordCode { get; set; }

        public int UserId { get; set; }

        public List<ProductWithCountDTO>? ProductList { get; set; }
        public int OrderPrice { get; set; }

        public OrderAddress? Address { get; set; }

        public OrderStatus Status { get; set; }

        public PaymentMethod PayWay { get; set; }

        public int ShippingPrice { get; set; }

        public List<OrderStepDTO>? OrderStepInfomation { get; set; }

        public List<ShipmentDTO>? ShipInfomation { get; set; }

        // 新增的更新時間
        public DateTime UpdatedAt { get; set; }

    }

    public class OrderStepDTO
    {
        public string? Description { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
