using EcommerceBackend.Enums;

namespace EcommerceBackend.Constants
{
    public static class OrderstepDescription
    {
        public static Dictionary<OrderStep, string> OrderstepMap = new Dictionary<OrderStep, string>() 
        {
            { OrderStep.Created,"訂單已成立" },
            { OrderStep.WaitingForPayment,"等待付款" },
            { OrderStep.PaymentReceived,"已收款" },
            { OrderStep.WaitingForShipment,"尚未出貨" },
            { OrderStep.ShipmentCompleted,"已出貨" },
            { OrderStep.OrderCompleted,"已完成訂單" },
        };
    }
}
