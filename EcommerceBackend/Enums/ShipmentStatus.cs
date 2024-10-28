namespace EcommerceBackend.Enums
{
    // 只管跟 物流有關的
    public enum ShipmentStatus
    {
        /// <summary>
        /// 尚未出貨
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 包裹已寄出
        /// </summary>
        Shipped = 1,

        /// <summary>
        /// 包裹運送中
        /// </summary>
        InTransit = 2,

        /// <summary>
        /// 包裹已送達指定的配送站 (配送中)
        /// </summary>
        OutForDelivery = 3,

        /// <summary>
        /// 買家已取件或包裹已送達
        /// </summary>
        Delivered = 4,


        /// <summary>
        /// 買家已取件成功
        /// </summary>
        PickedUpByCustomer = 5,

        /// <summary>
        /// 包裹配送失敗（例如買家不在）
        /// </summary>
        DeliveryFailed = 6,

        /// <summary>
        /// 包裹已退回給發貨方
        /// </summary>
        Returned = 7
    }
}
