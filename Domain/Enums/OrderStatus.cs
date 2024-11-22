namespace Domain.Enums
{
    public enum OrderStatus
    {
        /// <summary>
        /// 訂單已創建
        /// </summary>
        Created = 0,
        /// <summary>
        /// 等待付款
        /// </summary>
        WaitingForPayment = 1,

        /// <summary>
        /// 等待出貨
        /// </summary>
        WaitingForShipment = 2,

        /// <summary>
        /// 等待取貨
        /// </summary>
        WaitPickup = 3,

        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 4,

        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = 5,

        /// <summary>
        /// 退貨/款
        /// </summary>
        Refund = 6,

        /// <summary>
        /// 運送中
        /// </summary>
        InTransit = 7

    }
}
