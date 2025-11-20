namespace Domain.Entities
{
    /// <summary>
    /// 物流實體
    /// </summary>
    public class Shipment
    {
        // 私有構造函數
        private Shipment() { }

        // 內部工廠方法（供 Order 使用）
        internal static Shipment Create(int shipmentStatus)
        {
            return new Shipment
            {
                ShipmentStatus = shipmentStatus,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        // 公開工廠方法（供應用層使用，用於創建物流記錄）
        public static Shipment CreateForOrder(int orderId, int shipmentStatus)
        {
            return new Shipment
            {
                OrderId = orderId,
                ShipmentStatus = shipmentStatus,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        // 只讀屬性或私有 setter
        public int Id { get; private set; }
        public int OrderId { get; private set; }
        public int ShipmentStatus { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // 導航屬性
        public Order Order { get; private set; }

        // ============ 業務邏輯方法 ============

        /// <summary>
        /// 更新物流狀態
        /// </summary>
        public void UpdateStatus(int newStatus)
        {
            ShipmentStatus = newStatus;
            UpdatedAt = DateTime.Now;
        }
    }
}
