# 聚合邊界與不變性定義

> 📅 最後更新：2024年
> 
> 本文檔明確定義了系統中的聚合根、聚合邊界和不變性規則，是 DDD 架構設計的核心文件。

## 📋 目錄

- [聚合定義](#-聚合定義)
  - [Order 聚合](#1-order-聚合訂單聚合)
  - [Cart 聚合](#2-cart-聚合購物車聚合)
  - [User 聚合](#3-user-聚合用戶聚合)
- [聚合一致性保證](#-聚合一致性保證)
- [使用指南](#-使用指南)
- [未來改進建議](#-未來改進建議)

## 📦 聚合定義

### 1. Order 聚合（訂單聚合）

**聚合根：** `Order`

**聚合邊界：**
- ✅ `Order` - 聚合根
- ✅ `OrderProduct` - 聚合內實體（訂單商品）
- ✅ `OrderStep` - 聚合內實體（訂單步驟）
- ✅ `Shipment` - 聚合內實體（物流記錄）

**外部引用（不屬於聚合）：**
- `User` - 訂單所屬用戶（引用其他聚合）
- `ProductVariant` - 商品變體（引用其他聚合）
- `UserShipAddress` - 收貨地址（引用 User 聚合）

**不變性（Invariants）：**

1. **訂單總價計算規則**
   - 訂單總價 = 所有訂單商品總價 + 運費
   - 必須透過 `CalculateTotalPrice()` 方法計算

2. **訂單狀態轉換規則**
   - 狀態轉換必須符合業務規則
   - 只能從特定狀態轉換到特定狀態（透過 `CanTransitionTo()` 驗證）
   - 允許的轉換：
     - `Created` → `WaitingForPayment`
     - `Created` → `Canceled`
     - `WaitingForPayment` → `Completed`
     - `WaitingForPayment` → `Canceled`

3. **訂單商品操作規則**
   - 只能在 `Created` 狀態時添加商品
   - 商品數量必須大於 0

4. **訂單取消規則**
   - 只能取消 `Created` 或 `WaitingForPayment` 狀態的訂單

5. **訂單記錄完整性**
   - 訂單必須有至少一個 `OrderStep` 記錄狀態變更
   - 訂單必須有至少一個 `Shipment` 記錄物流狀態

6. **訂單編號唯一性**
   - `RecordCode` 必須唯一且不可變
   - 創建時自動生成

---

### 2. Cart 聚合（購物車聚合）

**聚合根：** `Cart`

**聚合邊界：**
- ✅ `Cart` - 聚合根
- ✅ `CartItem` - 聚合內實體（購物車項目）

**外部引用（不屬於聚合）：**
- `User` - 購物車所屬用戶（引用其他聚合）
- `ProductVariant` - 商品變體（引用其他聚合）

**不變性（Invariants）：**

1. **用戶關聯規則**
   - 購物車必須屬於一個用戶（`UserId > 0`）

2. **購物車項目數量規則**
   - 購物車項目數量必須大於 0（清空後可為 0）
   - `CartItem` 的數量必須大於 0
   - 透過 `GetTotalItemCount()` 方法獲取總數量

3. **商品去重規則**
   - 同一商品變體（`ProductVariant`）在購物車中只能有一個 `CartItem`
   - 添加相同商品時，自動合併數量（透過 `AddItem()` 方法）

4. **數量更新規則**
   - 更新數量時，新數量必須大於 0
   - 移除商品時，必須確保商品存在於購物車中

---

### 3. User 聚合（用戶聚合）

**聚合根：** `User`

**聚合邊界：**
- ✅ `User` - 聚合根
- ✅ `UserShipAddress` - 聚合內實體（收貨地址）
- ✅ `FavoriteProduct` - 聚合內實體（收藏商品）

**外部引用（不屬於聚合）：**
- `Product` - 收藏的商品（引用其他聚合）

**不變性（Invariants）：**

1. **Email 唯一性規則**
   - `Email` 必須唯一且不可為空
   - 創建時必須驗證

2. **帳號類型規則**
   - **帳號密碼用戶：**
     - 必須有 `PasswordHash`
     - 必須有 `Username`
   - **Google 登入用戶：**
     - 必須有 `GoogleId`
     - 不能有 `PasswordHash`
     - 無法更新密碼

3. **密碼更新規則**
   - Google 登入用戶無法更新密碼
   - 帳號密碼用戶可以更新密碼

4. **用戶角色規則**
   - 用戶角色必須是有效的（預設為 "user"）
   - 可以透過 `SetAsAdmin()` 設置為管理員

5. **收藏商品規則**
   - 同一商品不能重複添加到收藏清單
   - 添加時自動檢查重複（透過 `AddFavoriteProduct()` 方法）

6. **收貨地址規則**
   - 用戶可以有多個收貨地址
   - 移除地址時，必須確保地址存在

---

## 🔒 聚合一致性保證

### 事務邊界
- 每個聚合根代表一個事務邊界
- 對聚合內實體的操作必須透過聚合根進行
- 跨聚合的操作需要透過應用服務協調

### 並發控制
- 聚合根應該支援樂觀鎖（透過 `UpdatedAt` 時間戳）
- 更新時檢查版本衝突

### 持久化規則
- 保存聚合根時，必須同時保存所有聚合內實體的變更
- 使用 Repository 的 `SaveAggregateAsync()` 方法確保一致性

---

## 📝 使用指南

### 創建聚合根
```csharp
// ✅ 正確：使用工廠方法
var order = Order.Create(userId, receiver, phoneNumber, ...);
var cart = Cart.CreateForUser(userId);
var user = User.CreateWithPassword(email, username, passwordHash);
```

### 操作聚合內實體
```csharp
// ✅ 正確：透過聚合根操作
order.AddOrderProduct(productVariant, quantity);
cart.AddItem(productVariant, quantity);
user.AddFavoriteProduct(productId);

// ❌ 錯誤：直接操作聚合內實體
order.OrderProducts.Add(new OrderProduct { ... }); // 不要這樣做！
```

### 保存聚合根
```csharp
// ✅ 正確：使用聚合根 Repository
await _orderRepository.SaveAggregateAsync(order);
await _cartRepository.SaveAggregateAsync(cart);
await _userRepository.SaveAggregateAsync(user);
```

---

## 🎯 未來改進建議

1. **引入領域事件（Domain Events）**
   - `OrderCreatedEvent` - 訂單創建事件
   - `OrderCanceledEvent` - 訂單取消事件
   - `OrderCompletedEvent` - 訂單完成事件
   - `CartItemAddedEvent` - 購物車商品添加事件
   - `UserRegisteredEvent` - 用戶註冊事件
   - `UserPasswordUpdatedEvent` - 用戶密碼更新事件

2. **引入值對象（Value Objects）**
   - `Email` - 封裝 Email 驗證邏輯
   - `Money` - 封裝金額計算與貨幣單位
   - `Address` - 封裝地址驗證與格式化
   - `PhoneNumber` - 封裝電話號碼驗證
   - `RecordCode` - 封裝訂單編號生成規則

3. **引入規格模式（Specification Pattern）**
   - `OrderCanBeCanceledSpecification` - 訂單可取消規格
   - `UserCanUpdatePasswordSpecification` - 用戶可更新密碼規格
   - `CartCanAddItemSpecification` - 購物車可添加商品規格

4. **引入快照模式（Snapshot Pattern）**
   - 用於大型聚合的持久化優化
   - 減少資料庫查詢負擔

5. **引入領域服務增強**
   - `OrderValidationService` - 訂單驗證服務
   - `CartCalculationService` - 購物車計算服務
   - `UserAuthenticationService` - 用戶認證服務

---

## 📌 重要提醒

### ⚠️ 違反不變性的後果

直接操作聚合內實體或繞過聚合根的操作會導致：
- ❌ 業務規則被破壞
- ❌ 資料不一致
- ❌ 難以追蹤的 Bug
- ❌ 違反 DDD 原則

### ✅ 正確做法

**永遠透過聚合根操作聚合內實體！**

```csharp
// ✅ 正確
order.AddOrderProduct(productVariant, quantity);
cart.AddItem(productVariant, quantity);
user.AddFavoriteProduct(productId);

// ❌ 錯誤 - 直接操作聚合內實體
order.OrderProducts.Add(new OrderProduct { ... }); // 違反封裝！
cart.CartItems.Clear(); // 應該使用 cart.Clear()
user.FavoriteProducts.Remove(...); // 應該使用 user.RemoveFavoriteProduct()
```

---

## 📚 相關文件

- [富領域模型重構總結](../README_RichDomainModel.md)
- [專案結構說明](../README_FolderIntroduce.md)
- [IAggregateRoot 介面定義](./Interfaces/IAggregateRoot.cs)
- [聚合根 Repository 介面](./Interfaces/Repositories/IAggregateRootRepository.cs)

