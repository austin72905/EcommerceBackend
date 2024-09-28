using EcommerceBackend.Enums;

namespace EcommerceBackend.Constants
{
    public class ShipmentDescription
    {
        public static Dictionary<ShipmentStatus, string> OrderstepMap = new Dictionary<ShipmentStatus, string>()
        {
            { ShipmentStatus.Pending,"尚未出貨" },

            { ShipmentStatus.Shipped,"包裹已寄出" },

            { ShipmentStatus.InTransit,"包裹運送中" },

            { ShipmentStatus.OutForDelivery,"包裹已送達指定的配送站" },

            { ShipmentStatus.Delivered,"包裹已送達" },

            { ShipmentStatus.PickedUpByCustomer,"買家已取件成功" },

            { ShipmentStatus.DeliveryFailed,"包裹配送失敗" },

            { ShipmentStatus.Returned,"包裹已退回給發貨方" },
        };
    }
}
