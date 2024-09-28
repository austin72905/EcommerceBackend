namespace EcommerceBackend.Enums
{
    public enum OrderStep
    {
        /// <summary>
        /// 訂單已成立
        /// </summary>
        Created = 0,

        /// <summary>
        /// 等待付款
        /// </summary>
        WaitingForPayment = 1,

        /// <summary>
        /// 已收款
        /// </summary>
        PaymentReceived = 2,

        /// <summary>
        /// 等待出貨
        /// </summary>
        WaitingForShipment = 3,

        /// <summary>
        /// 已出貨
        /// </summary>
        ShipmentCompleted = 4,

        /// <summary>
        /// 已完成訂單
        /// </summary>
        OrderCompleted = 5
    }
}
