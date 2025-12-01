namespace Domain.Entities
{
    /// <summary>
    /// 訂單步驟實體
    /// </summary>
    public class OrderStep
    {
        // 私有構造函數
        private OrderStep() { }

        // 內部工廠方法（供 Order 使用）
        internal static OrderStep Create(int stepStatus)
        {
            return new OrderStep
            {
                StepStatus = stepStatus,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        // 公開工廠方法（供應用層使用）
        public static OrderStep CreateForOrder(int orderId, int stepStatus)
        {
            return new OrderStep
            {
                OrderId = orderId,
                StepStatus = stepStatus,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        // 只讀屬性或私有 setter
        public int Id { get; private set; }
        public int OrderId { get; private set; }
        public int StepStatus { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // 導航屬性
        public Order Order { get; private set; }
    }
}
