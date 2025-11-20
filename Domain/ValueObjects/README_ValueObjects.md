# 值對象（Value Objects）實作說明

> 📅 最後更新：2024年
> 
> 本文檔說明系統中實作的值對象及其使用方式。

## 📋 目錄

- [值對象概述](#值對象概述)
- [已實作的值對象](#已實作的值對象)
- [使用範例](#使用範例)
- [EF Core 映射說明](#ef-core-映射說明)

---

## 值對象概述

### 什麼是值對象？

值對象（Value Object）是 DDD 中的一個重要概念，具有以下特點：

1. **沒有唯一標識（Identity）**
   - 值對象通過值相等性比較，而非引用相等性
   - 兩個值對象如果值相同，則視為相等

2. **不可變（Immutable）**
   - 一旦創建，值對象的屬性不能改變
   - 如果需要修改，應該創建一個新的值對象

3. **封裝驗證邏輯**
   - 值對象在創建時進行驗證
   - 確保只有有效的值才能被創建

4. **封裝業務邏輯**
   - 值對象可以包含相關的業務邏輯方法
   - 例如：金額計算、地址格式化等

### 值對象基類

所有值對象都繼承自 `ValueObject` 基類，該基類提供了：
- 值相等性比較（重寫 `Equals` 和 `GetHashCode`）
- 運算符重載（`==` 和 `!=`）

---

## 已實作的值對象

### 1. Email（電子郵件）

**位置：** `Domain/ValueObjects/Email.cs`

**功能：**
- Email 格式驗證
- 自動轉換為小寫並去除空白
- 隱式轉換為字串

**使用範例：**
```csharp
// 創建 Email 值對象（會自動驗證格式）
var email = Email.Create("User@Example.COM");
// email.Value = "user@example.com"（自動轉換為小寫）

// 驗證 Email 格式
if (Email.IsValid("test@example.com"))
{
    // 格式有效
}

// 隱式轉換為字串
string emailString = email; // "user@example.com"
```

**驗證規則：**
- 必須符合標準 Email 格式（使用正則表達式驗證）
- 不能為空或空白

---

### 2. Money（金額）

**位置：** `Domain/ValueObjects/Money.cs`

**功能：**
- 金額計算（相加、相減、相乘）
- 金額比較（大於、小於）
- 貨幣單位支援（預設為 TWD）
- 格式化為顯示字串

**使用範例：**
```csharp
// 創建金額值對象
var price1 = Money.Create(1000); // 1000 TWD
var price2 = Money.Create(500, "USD"); // 500 USD

// 金額計算
var total = price1.Add(price2); // 需要相同貨幣
var discount = price1.Multiply(0.9m); // 打 9 折

// 金額比較
if (price1.IsGreaterThan(price2))
{
    // price1 大於 price2
}

// 格式化顯示
string display = price1.ToDisplayString(); // "TWD 10.00"

// 隱式轉換為整數
int amount = price1; // 1000
```

**驗證規則：**
- 金額不能為負數
- 貨幣單位不能為空
- 不同貨幣之間不能進行運算

---

### 3. ShippingAddress（收貨地址）

**位置：** `Domain/ValueObjects/ShippingAddress.cs`

**功能：**
- 收貨地址驗證
- 格式化為完整地址字串
- 支援超商取貨門市

**使用範例：**
```csharp
// 創建收貨地址值對象
var address = ShippingAddress.Create(
    recipientName: "張三",
    phoneNumber: "0912345678",
    addressLine: "台北市信義區信義路五段7號",
    receiveWay: "宅配",
    receiveStore: "7-11 信義門市" // 可選
);

// 格式化為完整地址字串
string fullAddress = address.ToFullAddressString();
// "收件人：張三，電話：0912345678，地址：台北市信義區信義路五段7號，收貨方式：宅配，收貨門市：7-11 信義門市"
```

**驗證規則：**
- 收件人姓名不能為空
- 電話號碼不能為空
- 地址不能為空
- 收貨方式不能為空

---

### 4. PhoneNumber（電話號碼）

**位置：** `Domain/ValueObjects/PhoneNumber.cs`

**功能：**
- 台灣手機號碼和市話格式驗證
- 自動格式化為顯示字串
- 支援多種輸入格式

**使用範例：**
```csharp
// 創建電話號碼值對象（支援多種格式）
var phone1 = PhoneNumber.Create("0912-345-678"); // 手機
var phone2 = PhoneNumber.Create("02-1234-5678"); // 市話
var phone3 = PhoneNumber.Create("0912345678"); // 無連字號

// 格式化為顯示字串
string formatted = phone1.ToFormattedString(); // "0912-345-678"

// 驗證電話號碼格式
if (PhoneNumber.IsValid("0912345678"))
{
    // 格式有效
}

// 隱式轉換為字串
string phoneString = phone1; // "0912345678"
```

**驗證規則：**
- 支援台灣手機號碼：09XX-XXX-XXX 或 09XXXXXXXX
- 支援台灣市話：0X-XXXX-XXXX 或 0XXXXXXXXX
- 自動移除所有非數字字元後驗證

---

## 使用範例

### 在聚合根中使用值對象

#### Order 聚合根

```csharp
// 創建訂單時，值對象會自動驗證
var order = Order.Create(
    userId: 1,
    receiver: "張三",
    phoneNumber: "0912345678", // 會透過 PhoneNumber 值對象驗證
    shippingAddress: "台北市信義區...",
    recieveWay: "宅配",
    email: "user@example.com", // 會透過 Email 值對象驗證
    shippingPrice: 100
);

// 使用值對象屬性進行業務邏輯
var emailValue = order.EmailValue; // Email 值對象
var phoneValue = order.PhoneNumberValue; // PhoneNumber 值對象
var addressValue = order.ShippingAddressValue; // ShippingAddress 值對象
var totalPrice = order.OrderPriceValue; // Money 值對象

// 使用 Money 值對象進行金額計算
order.CalculateTotalPrice(getDiscountedPrice); // 內部使用 Money 值對象
```

#### User 聚合根

```csharp
// 創建用戶時，Email 會自動驗證
var user = User.CreateWithPassword(
    email: "user@example.com", // 會透過 Email 值對象驗證
    username: "testuser",
    passwordHash: "hashed_password"
);

// 使用 Email 值對象
var emailValue = user.EmailValue; // Email 值對象
```

---

## EF Core 映射說明

### 當前實作方式

為了保持向後兼容性和簡化 EF Core 映射，目前採用以下策略：

1. **實體中保留字串屬性**（用於 EF Core 資料庫映射）
   ```csharp
   public string Email { get; private set; } // 存儲在資料庫中
   ```

2. **值對象用於驗證**（在工廠方法中）
   ```csharp
   var emailValue = Email.Create(email); // 驗證格式
   Email = emailValue.Value; // 存儲為字串
   ```

3. **值對象屬性**（用於業務邏輯，不存儲在資料庫）
   ```csharp
   public Email EmailValue => Email.Create(Email); // 從字串轉換為值對象
   ```

### 優點

- ✅ **向後兼容**：不需要修改資料庫結構
- ✅ **簡化映射**：EF Core 不需要特殊配置
- ✅ **驗證保證**：創建時透過值對象驗證
- ✅ **業務邏輯**：可以透過值對象屬性使用業務方法

### 未來改進（可選）

如果需要更完整的值對象支援，可以：

1. **使用 EF Core Owned Entity Types**
   ```csharp
   // 在 DBContext 中配置
   modelBuilder.Entity<Order>(entity =>
   {
       entity.OwnsOne(o => o.EmailValue);
       entity.OwnsOne(o => o.ShippingAddressValue);
   });
   ```

2. **使用值轉換器（Value Converters）**
   ```csharp
   // 在 DBContext 中配置
   modelBuilder.Entity<Order>()
       .Property(o => o.Email)
       .HasConversion(
           v => v.Value,
           v => Email.Create(v));
   ```

---

## 值對象設計原則

### ✅ 應該使用值對象的場景

1. **需要驗證的簡單值**
   - Email、電話號碼、URL 等

2. **需要封裝業務邏輯的值**
   - 金額（計算、比較）
   - 地址（格式化、驗證）

3. **多個屬性組成的概念**
   - 地址（包含收件人、電話、地址等）
   - 座標（經度、緯度）

### ❌ 不應該使用值對象的場景

1. **需要唯一標識的實體**
   - 這些應該是 Entity，而不是 Value Object

2. **需要變更追蹤的對象**
   - 如果對象需要追蹤變更歷史，應該是 Entity

3. **過於簡單的值**
   - 如果只是簡單的字串或數字，不需要值對象

---

## 相關文件

- [聚合邊界與不變性定義](../README_AggregateBoundaries.md)
- [富領域模型重構總結](../../README_RichDomainModel.md)
- [ValueObject 基類](./ValueObject.cs)

---

## 未來改進建議

1. **引入更多值對象**
   - `RecordCode` - 訂單編號（封裝生成規則）
   - `Password` - 密碼（封裝強度驗證）
   - `Coordinate` - 座標（經度、緯度）

2. **值對象序列化支援**
   - 支援 JSON 序列化/反序列化
   - 支援 XML 序列化

3. **值對象快取**
   - 對於常用的值對象實例進行快取
   - 減少重複創建

4. **值對象比較優化**
   - 優化 `GetEqualityComponents` 的效能
   - 支援自訂比較邏輯

