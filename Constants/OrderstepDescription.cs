using EcommerceBackend.Enums;

namespace EcommerceBackend.Constants
{
    public static class OrderstepDescription
    {
        public static Dictionary<OrderStepStatus, string> OrderstepMap = new Dictionary<OrderStepStatus, string>() 
        {
            { OrderStepStatus.Created,"訂單已成立" },
            { OrderStepStatus.WaitingForPayment,"等待付款" },
            { OrderStepStatus.PaymentReceived,"已收款" },
            { OrderStepStatus.WaitingForShipment,"尚未出貨" },
            { OrderStepStatus.ShipmentCompleted,"已出貨" },
            { OrderStepStatus.OrderCompleted,"已完成訂單" },
        };
    }
}
