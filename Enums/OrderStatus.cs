namespace EcommerceBackend.Enums
{
    public enum OrderStatus
    {
        /// <summary>
        /// 訂單已創建
        /// </summary>
        CREATED =0,
        /// <summary>
        /// 等待付款
        /// </summary>
        WAIT_PAY=1,

        /// <summary>
        /// 等待出貨
        /// </summary>
        WAIT_SHIPPMENT=2,

        /// <summary>
        /// 等待取貨
        /// </summary>
        WAIT_PICKUP=3,

        /// <summary>
        /// 已完成
        /// </summary>
        COMPLETE=4,

        /// <summary>
        /// 已取消
        /// </summary>
        CANCELED =5,

        /// <summary>
        /// 退貨/款
        /// </summary>
        REFUND =6

    }
}
