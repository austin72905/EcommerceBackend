namespace EcommerceBackend.Enums
{
    public enum OrderStatus
    {
        /// <summary>
        /// 等待付款
        /// </summary>
        WAIT_PAY=0,

        /// <summary>
        /// 等待出貨
        /// </summary>
        WAIT_SHIPPMENT=1,

        /// <summary>
        /// 等待取貨
        /// </summary>
        WAIT_PICKUP=2,

        /// <summary>
        /// 已完成
        /// </summary>
        COMPLETE=3,

        /// <summary>
        /// 已取消
        /// </summary>
        CANCELED =4,

        /// <summary>
        /// 退貨/款
        /// </summary>
        REFUND =5

    }
}
