# 富領域模型重構總結

## 📋 概述

本次重構將專案從**貧血領域模型**（Anemic Domain Model）改進為**富領域模型**（Rich Domain Model），更符合 DDD（領域驅動設計）原則。

## 🎯 重構目標

1. ✅ 將業務邏輯從應用層移至領域層
2. ✅ 實體擁有行為，不僅僅是數據容器
3. ✅ 使用工廠方法控制實體創建
4. ✅ 保護實體狀態，使用私有 setter
5. ✅ 明確聚合根的概念

## 🏗️ 重構的實體

### 1. Order（訂單聚合根）

**改進前：**
```csharp
public class Order
{
    public int Id { get; set; }
    public int Status { get; set; }
    // ... 只有屬性，無業務邏輯
}
```

**改進後：**
```csharp
public class Order
{
    // 私有 setter 保護狀態
    public int Status { get; private set; }
    
    // 工廠方法創建
    public static Order Create(int userId, string receiver, ...) { }
    
    // 業務邏輯方法
    public void Cancel() { }
    public void MarkAsPaid(int paymentMethod) { }
    public void AddOrderProduct(ProductVariant productVariant, int quantity) { }
    public void CalculateTotalPrice(Func<OrderProduct, int> getDiscountedPrice) { }
    public void UpdateStatus(OrderStatus newStatus) { }
    public void Complete() { }
}
```

**新增功能：**
- ✅ 訂單創建時自動生成訂單編號
- ✅ 自動添加初始訂單步驟和物流狀態
- ✅ 狀態轉換驗證（確保合法的狀態變更）
- ✅ 業務規則封裝（如：只能取消未付款訂單）

### 2. Cart（購物車聚合根）

**改進前：**
```csharp
public class Cart
{
    public int UserId { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
}
```

**改進後：**
```csharp
public class Cart
{
    // 工廠方法
    public static Cart CreateForUser(int userId) { }
    
    // 業務邏輯方法
    public void AddItem(ProductVariant productVariant, int quantity) { }
    public void UpdateItemQuantity(int productVariantId, int newQuantity) { }
    public void RemoveItem(int productVariantId) { }
    public void Clear() { }
    public void MergeItems(List<CartItem> itemsToMerge, ...) { }
    public void Rebuild(List<CartItem> newItems, ...) { }
    public int CalculateTotalAmount(Func<CartItem, int> getPriceForItem) { }
    public int GetTotalItemCount() { }
}
```

**新增功能：**
- ✅ 購物車項目自動去重與數量合併
- ✅ 驗證數量必須大於 0
- ✅ 提供合併與重建功能（登入後合併前端購物車）

### 3. User（用戶聚合根）

**改進前：**
```csharp
public class User
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime LastLogin { get; set; }
}
```

**改進後：**
```csharp
public class User
{
    // 私有 setter
    public string Email { get; private set; }
    
    // 工廠方法
    public static User CreateWithPassword(string email, string username, string passwordHash) { }
    public static User CreateWithGoogle(string email, string googleId, ...) { }
    
    // 業務邏輯方法
    public void UpdateProfile(string nickName, string phoneNumber, ...) { }
    public void UpdatePassword(string newPasswordHash) { }
    public void UpdatePicture(string pictureUrl) { }
    public void RecordLogin() { }
    public void AddShippingAddress(UserShipAddress address) { }
    public void RemoveShippingAddress(int addressId) { }
    public void AddFavoriteProduct(int productId) { }
    public void RemoveFavoriteProduct(int productId) { }
    public bool IsAdmin() { }
    public void SetAsAdmin() { }
}
```

**新增功能：**
- ✅ 區分帳號密碼註冊與 Google 登入
- ✅ Google 用戶無法修改密碼的驗證
- ✅ 自動管理登入時間
- ✅ 封裝個人資料更新邏輯

### 4. CartItem（購物車項目實體）

**改進後：**
```csharp
public class CartItem
{
    // 工廠方法
    public static CartItem Create(int productVariantId, int quantity, ...) { }
    
    // 業務邏輯方法
    public void UpdateQuantity(int newQuantity) { }
    public void IncreaseQuantity(int amount) { }
    public void DecreaseQuantity(int amount) { }
}
```

### 5. OrderProduct（訂單商品實體）

**改進後：**
```csharp
public class OrderProduct
{
    // 內部工廠方法（僅供 Order 使用）
    internal static OrderProduct Create(int productVariantId, int productPrice, int count, ...) { }
    
    // 業務邏輯方法
    public int CalculateSubtotal() { }
    internal void UpdateCount(int newCount) { }
}
```

### 6. OrderStep & Shipment

**改進後：**
- 使用內部工廠方法（`internal static Create`）
- 僅允許 Order 聚合根創建和管理

## 🔄 應用層服務更新

### OrderService

**改進前：**
```csharp
var order = new Order
{
    RecordCode = $"EC{Guid.NewGuid()...}",
    UserId = info.UserId,
    Status = (int)OrderStatus.Created,
    // ... 手動設置所有屬性
};
order.OrderProducts.Add(new OrderProduct { ... });
order.OrderPrice = _orderDomainService.CalculateOrderTotal(...);
```

**改進後：**
```csharp
// 使用工廠方法創建
var order = Order.Create(
    userId: info.UserId,
    receiver: info.ReceiverName,
    phoneNumber: info.ReceiverPhone,
    shippingAddress: info.ShippingAddress,
    recieveWay: info.RecieveWay,
    email: info.Email,
    shippingPrice: (int)info.ShippingFee,
    recieveStore: info.RecieveStore
);

// 使用領域方法添加商品
order.AddOrderProduct(productVariant, item.Quantity);

// 使用領域方法計算總價
order.CalculateTotalPrice(...);
```

### CartService

**改進前：**
```csharp
cart = new Cart
{
    UserId = userid,
    CreatedAt = DateTime.Now,
    UpdatedAt = DateTime.Now,
    CartItems = new List<CartItem>()
};
_cartDomainService.MergeCartItems(cart, domainCartItems, productVariants);
```

**改進後：**
```csharp
// 使用工廠方法創建
cart = Cart.CreateForUser(userid);

// 直接使用領域方法
cart.MergeItems(domainCartItems, productVariants);
```

### UserService

**改進前：**
```csharp
var user = new User
{
    Email = signUpDto.Email,
    Username = signUpDto.Username,
    PasswordHash = encryptionService.HashPassword(signUpDto.Password),
    CreatedAt = DateTime.Now,
    // ...
};
```

**改進後：**
```csharp
// 使用工廠方法創建
var user = User.CreateWithPassword(
    email: signUpDto.Email,
    username: signUpDto.Username,
    passwordHash: encryptionService.HashPassword(signUpDto.Password)
);

// 登入時記錄
user.RecordLogin();

// 更新資料
user.UpdateProfile(nickName: userDto.NickName, ...);
```

## 📊 重構效益

### 1. 業務邏輯集中化
- ✅ 業務規則在領域層統一管理
- ✅ 避免邏輯散落在應用層各處

### 2. 狀態保護
- ✅ 私有 setter 防止外部隨意修改
- ✅ 只能通過業務方法變更狀態

### 3. 驗證集中
- ✅ 創建和修改時的驗證邏輯在實體內部
- ✅ 確保實體始終處於有效狀態

### 4. 可測試性提升
- ✅ 領域邏輯可獨立測試，無需依賴資料庫
- ✅ 業務規則測試更簡單

### 5. 可維護性提升
- ✅ 業務邏輯修改只需改領域層
- ✅ 減少重複代碼

### 6. 更符合 DDD 原則
- ✅ 明確的聚合根（Order, Cart, User）
- ✅ 實體擁有行為，不只是數據容器
- ✅ 封裝與不變性保護

## 🎨 設計模式運用

### 1. 工廠方法模式
```csharp
public static Order Create(...) { }
public static Cart CreateForUser(...) { }
public static User CreateWithPassword(...) { }
public static User CreateWithGoogle(...) { }
```

### 2. 策略模式
```csharp
// 允許外部傳入計算邏輯
order.CalculateTotalPrice(orderProduct => 
    _orderDomainService.CalculateOrderTotal(...)
);
```

### 3. 狀態模式（隱含）
```csharp
// 狀態轉換驗證
private bool CanTransitionTo(OrderStatus newStatus)
{
    return (currentStatus, newStatus) switch
    {
        (OrderStatus.Created, OrderStatus.WaitingForPayment) => true,
        (OrderStatus.Created, OrderStatus.Canceled) => true,
        // ...
    };
}
```

## 🔮 未來改進建議

### 1. 引入值對象（Value Objects）
```csharp
// 建議將這些改為值對象
public class Money { ... }
public class Email { ... }
public class Address { ... }
public class PhoneNumber { ... }
```

### 2. 引入領域事件（Domain Events）
```csharp
public class OrderCreatedEvent : IDomainEvent { }
public class OrderCanceledEvent : IDomainEvent { }
public class OrderCompletedEvent : IDomainEvent { }
```

### 3. 規格模式（Specification Pattern）
```csharp
public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
}

public class OrderCanBeCanceledSpecification : ISpecification<Order> { }
```

### 4. Repository 模式改進
```csharp
// 支持直接保存聚合根的變更
public interface IOrderRepository
{
    Task<Order> GetByIdAsync(int id);
    Task SaveAsync(Order order); // 支持追蹤變更
}
```

## 📝 注意事項

### 1. EF Core 限制
由於使用 EF Core 且實體屬性為 `private set`，某些場景下需要注意：
- ✅ EF Core 支援私有 setter
- ⚠️ 查詢時 EF Core 會使用反射設置屬性
- ⚠️ 導航屬性的集合初始化需在構造函數中

### 2. 向後兼容
現有的 Repository 實現仍然可以正常工作：
- ✅ `UpdateOrderStatusAsync` 等方法仍可使用
- 💡 未來可考慮改用領域方法 + `SaveChangesAsync`

### 3. 測試
所有重構後的代碼通過 Linter 檢查，無錯誤。

## 📚 參考資源

- [Domain-Driven Design (Eric Evans)](https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215)
- [Implementing Domain-Driven Design (Vaughn Vernon)](https://www.amazon.com/Implementing-Domain-Driven-Design-Vaughn-Vernon/dp/0321834577)
- [Anemic Domain Model (Martin Fowler)](https://martinfowler.com/bliki/AnemicDomainModel.html)

---

**重構完成日期：** 2025-11-19  
**影響範圍：** Domain 層、Application 層  
**測試狀態：** ✅ 無 Linter 錯誤

