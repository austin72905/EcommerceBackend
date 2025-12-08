using Domain.Enums;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// 訂單聚合根 - 富領域模型
    /// 
    /// 聚合邊界：
    /// - Order（聚合根）
    /// - OrderProduct（聚合內實體）
    /// - OrderStep（聚合內實體）
    /// - Shipment（聚合內實體）
    /// 
    /// 不變性（Invariants）：
    /// 1. 訂單總價 = 所有訂單商品總價 + 運費
    /// 2. 訂單狀態轉換必須符合業務規則（透過 CanTransitionTo 驗證）
    /// 3. 只能在 Created 狀態時添加商品
    /// 4. 只能取消 Created 或 WaitingForPayment 狀態的訂單
    /// 5. 訂單必須有至少一個 OrderStep 記錄狀態變更
    /// 6. 訂單必須有至少一個 Shipment 記錄物流狀態
    /// 7. RecordCode 必須唯一且不可變
    /// </summary>
    public class Order : IAggregateRoot
    {
        // 私有構造函數，防止外部直接創建
        private Order() 
        { 
            OrderProducts = new List<OrderProduct>();
            OrderSteps = new List<OrderStep>();
            Shipments = new List<Shipment>();
        }

        // 工廠方法：創建新訂單（使用值對象進行驗證）
        public static Order Create(
            int userId, 
            string receiver, 
            string phoneNumber, 
            string shippingAddress, 
            string recieveWay, 
            string email,
            int shippingPrice,
            string recieveStore = "")
        {
            // 使用值對象進行驗證
            var emailValue = ValueObjects.Email.Create(email);
            var phoneValue = ValueObjects.PhoneNumber.Create(phoneNumber);
            var addressValue = ValueObjects.ShippingAddress.Create(receiver, phoneNumber, shippingAddress, recieveWay, recieveStore);
            var shippingMoney = ValueObjects.Money.Create(shippingPrice);

            var order = new Order
            {
                RecordCode = GenerateRecordCode(),
                UserId = userId,
                Status = (int)OrderStatus.Created,
                Receiver = receiver,
                PhoneNumber = phoneValue.Value, // 存儲為字串（EF Core 映射）
                ShippingAddress = shippingAddress,
                RecieveWay = recieveWay,
                RecieveStore = recieveStore,
                Email = emailValue.Value, // 存儲為字串（EF Core 映射）
                ShippingPrice = shippingPrice,
                OrderPrice = 0, // 初始為 0，待添加商品後計算
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // 添加初始訂單步驟
            order.AddOrderStep(OrderStatus.Created);
            
            // 添加初始物流狀態
            order.AddShipment(ShipmentStatus.Pending);

            return order;
        }

        // 只讀屬性或私有 setter
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string RecordCode { get; private set; }
        public int? AddressId { get; private set; }
        public string Receiver { get; private set; }
        public string PhoneNumber { get; private set; }
        public string ShippingAddress { get; private set; }
        public string RecieveWay { get; private set; }
        public string RecieveStore { get; private set; }
        public string Email { get; private set; }
        public int OrderPrice { get; private set; }
        public int Status { get; private set; }
        public int PayWay { get; private set; }
        public int ShippingPrice { get; private set; }

        // ============ 值對象屬性（用於業務邏輯） ============

        /// <summary>
        /// Email 值對象
        /// </summary>
        public ValueObjects.Email EmailValue => ValueObjects.Email.Create(Email);

        /// <summary>
        /// 電話號碼值對象
        /// </summary>
        public ValueObjects.PhoneNumber PhoneNumberValue => ValueObjects.PhoneNumber.Create(PhoneNumber);

        /// <summary>
        /// 收貨地址值對象
        /// </summary>
        public ValueObjects.ShippingAddress ShippingAddressValue => ValueObjects.ShippingAddress.Create(
            Receiver, PhoneNumber, ShippingAddress, RecieveWay, RecieveStore);

        /// <summary>
        /// 訂單總價值對象
        /// </summary>
        public ValueObjects.Money OrderPriceValue => ValueObjects.Money.Create(OrderPrice);

        /// <summary>
        /// 運費值對象
        /// </summary>
        public ValueObjects.Money ShippingPriceValue => ValueObjects.Money.Create(ShippingPrice);
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // ============ 關鍵狀態時間戳（用於快速查詢，提升性能） ============
        /// <summary>
        /// 支付時間（用於前端快速顯示，避免 JOIN OrderStep 查詢）
        /// </summary>
        public DateTime? PaidAt { get; private set; }

        /// <summary>
        /// 送貨時間（用於前端快速顯示）
        /// </summary>
        public DateTime? ShippedAt { get; private set; }

        /// <summary>
        /// 取貨時間（用於前端快速顯示）
        /// </summary>
        public DateTime? PickedUpAt { get; private set; }

        /// <summary>
        /// 訂單完成時間（用於前端快速顯示）
        /// </summary>
        public DateTime? CompletedAt { get; private set; }

        // 導航屬性
        public User User { get; private set; }
        public UserShipAddress Address { get; private set; }
        public Payment Payment { get; private set; }
        public ICollection<OrderProduct> OrderProducts { get; private set; }
        public ICollection<OrderStep> OrderSteps { get; private set; }
        public ICollection<Shipment> Shipments { get; private set; }

        // ============ 業務邏輯方法 ============

        /// <summary>
        /// 添加訂單商品
        /// 注意：為避免 EF Core 追蹤整個物件圖（導致主鍵衝突），只接受必要的資料，不接受完整的 ProductVariant 實體
        /// </summary>
        public void AddOrderProduct(int productVariantId, int productPrice, int quantity)
        {
            if (Status != (int)OrderStatus.Created)
                throw new InvalidOperationException("只能在訂單創建狀態時添加商品");

            if (productVariantId <= 0)
                throw new ArgumentException("商品變體 ID 必須大於 0", nameof(productVariantId));

            if (productPrice < 0)
                throw new ArgumentException("商品價格不能為負數", nameof(productPrice));

            if (quantity <= 0)
                throw new ArgumentException("商品數量必須大於 0", nameof(quantity));

            var orderProduct = OrderProduct.Create(
                productVariantId,
                productPrice,
                quantity,
                null // 不傳入 ProductVariant 實體，避免 EF Core 追蹤整個物件圖
            );

            OrderProducts.Add(orderProduct);
            UpdatedAt = DateTime.UtcNow;
        }
        
        /// <summary>
        /// 添加訂單商品（舊版本，已棄用）
        /// 保留此方法以維持向後相容性，但建議使用新版本
        /// </summary>
        [Obsolete("請使用 AddOrderProduct(int productVariantId, int productPrice, int quantity) 以避免 EF Core 追蹤整個物件圖")]
        public void AddOrderProduct(ProductVariant productVariant, int quantity)
        {
            if (productVariant == null)
                throw new ArgumentNullException(nameof(productVariant));
                
            AddOrderProduct(productVariant.Id, productVariant.VariantPrice, quantity);
        }

        /// <summary>
        /// 計算訂單總價（使用 Money 值對象）
        /// 業務邏輯：計算所有訂單商品的總價（含折扣）+ 運費
        /// </summary>
        /// <param name="orderDomainService">訂單領域服務</param>
        /// <param name="productVariants">可選的商品變體字典，用於折扣計算（key: ProductVariantId, value: ProductVariant）</param>
        public void CalculateTotalPrice(Domain.Interfaces.Services.IOrderDomainService orderDomainService, Dictionary<int, ProductVariant>? productVariants = null)
        {
            if (orderDomainService == null)
                throw new ArgumentNullException(nameof(orderDomainService));

            // 使用 Domain Service 計算訂單總價（包含折扣計算）
            var total = orderDomainService.CalculateOrderTotal(OrderProducts.ToList(), ShippingPrice, productVariants);

            OrderPrice = total;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// 取消訂單
        /// </summary>
        public void Cancel()
        {
            if (Status != (int)OrderStatus.Created && Status != (int)OrderStatus.WaitingForPayment)
                throw new InvalidOperationException($"訂單狀態 {(OrderStatus)Status} 無法取消");

            Status = (int)OrderStatus.Canceled;
            UpdatedAt = DateTime.UtcNow;
            
            AddOrderStep(OrderStatus.Canceled);
        }

        /// <summary>
        /// 標記為已付款
        /// </summary>
        public void MarkAsPaid(int paymentMethod)
        {
            if (Status != (int)OrderStatus.Created)
                throw new InvalidOperationException("只能標記創建狀態的訂單為已付款");

            // 支付完成後，訂單狀態應該轉換為等待出貨
            Status = (int)OrderStatus.WaitingForShipment;
            PayWay = paymentMethod;
            UpdatedAt = DateTime.UtcNow;
            PaidAt = DateTime.UtcNow;  // 同步記錄支付時間，用於前端快速查詢
            
            // 添加支付已接收的訂單步驟（使用 OrderStepStatus.PaymentReceived）
            AddOrderStepStatus(Domain.Enums.OrderStepStatus.PaymentReceived);
            // 添加等待出貨的訂單步驟
            AddOrderStepStatus(Domain.Enums.OrderStepStatus.WaitingForShipment);
        }

        /// <summary>
        /// 標記為已付款（不創建 OrderStep，用於縮小事務範圍）
        /// OrderStep 將在背景異步創建，減少事務時間和數據庫鎖競爭
        /// 同時更新 PaidAt 時間戳，用於前端快速查詢
        /// </summary>
        public void MarkAsPaidWithoutSteps(int paymentMethod)
        {
            if (Status != (int)OrderStatus.Created)
                throw new InvalidOperationException("只能標記創建狀態的訂單為已付款");

            // 支付完成後，訂單狀態應該轉換為等待出貨
            Status = (int)OrderStatus.WaitingForShipment;
            PayWay = paymentMethod;
            UpdatedAt = DateTime.UtcNow;
            PaidAt = DateTime.UtcNow;  // 同步記錄支付時間，用於前端快速查詢
            
            // 不創建 OrderStep，將在背景異步創建（作為審計日誌）
        }

        /// <summary>
        /// 更新訂單狀態
        /// </summary>
        public void UpdateStatus(OrderStatus newStatus)
        {
            // 狀態轉換驗證
            if (!CanTransitionTo(newStatus))
                throw new InvalidOperationException($"無法從 {(OrderStatus)Status} 轉換到 {newStatus}");

            Status = (int)newStatus;
            UpdatedAt = DateTime.UtcNow;
            
            // 根據狀態更新對應的時間戳（用於前端快速查詢）
            var now = DateTime.UtcNow;
            switch (newStatus)
            {
                case OrderStatus.InTransit:
                    // 出貨時間（當訂單開始運送時）
                    if (ShippedAt == null)
                    {
                        ShippedAt = now;
                    }
                    // 添加已出貨的訂單步驟
                    AddOrderStepStatus(Domain.Enums.OrderStepStatus.ShipmentCompleted);
                    break;
                case OrderStatus.WaitPickup:
                    // 取貨時間（當訂單送達等待取貨時）
                    if (PickedUpAt == null)
                    {
                        PickedUpAt = now;
                    }
                    // 不需要添加 OrderStep，因為這只是物流狀態
                    break;
                case OrderStatus.Completed:
                    // 完成時間
                    CompletedAt = now;
                    // 添加訂單已完成的訂單步驟（使用 OrderStepStatus.OrderCompleted）
                    AddOrderStepStatus(Domain.Enums.OrderStepStatus.OrderCompleted);
                    break;
                default:
                    // 其他狀態轉換使用 OrderStatus 值作為 OrderStep 的狀態
                    // 注意：這只適用於 OrderStatus 和 OrderStepStatus 值相同的狀態
                    AddOrderStep(newStatus);
                    break;
            }
        }

        /// <summary>
        /// 設置收貨地址
        /// </summary>
        public void SetAddress(int addressId)
        {
            AddressId = addressId;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// 完成訂單
        /// </summary>
        public void Complete()
        {
            if (Status != (int)OrderStatus.WaitPickup)
                throw new InvalidOperationException("只能完成待取貨狀態的訂單");

            Status = (int)OrderStatus.Completed;
            UpdatedAt = DateTime.UtcNow;
            CompletedAt = DateTime.UtcNow;  // 同步記錄完成時間，用於前端快速查詢
            
            // 添加訂單已完成的訂單步驟（使用 OrderStepStatus.OrderCompleted）
            AddOrderStepStatus(Domain.Enums.OrderStepStatus.OrderCompleted);
        }

        // ============ 私有輔助方法 ============

        private void AddOrderStep(OrderStatus status)
        {
            var orderStep = OrderStep.Create((int)status);
            OrderSteps.Add(orderStep);
        }

        /// <summary>
        /// 添加訂單步驟（使用 OrderStepStatus）
        /// </summary>
        private void AddOrderStepStatus(Domain.Enums.OrderStepStatus stepStatus)
        {
            var orderStep = OrderStep.Create((int)stepStatus);
            OrderSteps.Add(orderStep);
        }

        private void AddShipment(ShipmentStatus status)
        {
            var shipment = Shipment.Create((int)status);
            Shipments.Add(shipment);
        }

        private bool CanTransitionTo(OrderStatus newStatus)
        {
            var currentStatus = (OrderStatus)Status;
            
            // 定義允許的狀態轉換
            return (currentStatus, newStatus) switch
            {
                (OrderStatus.Created, OrderStatus.WaitingForPayment) => true,
                (OrderStatus.Created, OrderStatus.WaitingForShipment) => true, // 支付完成後直接轉換為等待出貨
                (OrderStatus.Created, OrderStatus.Canceled) => true,
                (OrderStatus.WaitingForPayment, OrderStatus.WaitingForShipment) => true, // 從待付款轉換為待出貨
                (OrderStatus.WaitingForPayment, OrderStatus.Completed) => true,
                (OrderStatus.WaitingForPayment, OrderStatus.Canceled) => true,
                (OrderStatus.WaitingForShipment, OrderStatus.InTransit) => true, // 從待出貨轉換為運送中
                (OrderStatus.WaitingForShipment, OrderStatus.Canceled) => true,
                (OrderStatus.InTransit, OrderStatus.WaitPickup) => true, // 從運送中轉換為待取貨
                (OrderStatus.InTransit, OrderStatus.Canceled) => true,
                (OrderStatus.WaitPickup, OrderStatus.Completed) => true, // 從待取貨轉換為已完成
                _ => false
            };
        }

        private static string GenerateRecordCode()
        {
            return $"EC{Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper()}";
        }
    }
}
