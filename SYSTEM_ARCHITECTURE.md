# 電商系統架構圖

本文檔使用 Mermaid 圖表展示電商系統中三個主要服務的架構關係、職責分工以及透過 HTTP 和 MQ 的交互方式。

## 系統概覽

本系統由三個主要服務組成：
1. **EcommerceBackend** (C# .NET) - 主後端服務
2. **ec-payment-service** (Go) - 支付服務（使用模擬支付，預設支付成功）
3. **ec-order-state-service** (Go) - 訂單狀態管理服務

> **注意**：本系統使用模擬支付機制，不涉及真實的第三方支付（如綠界支付）。支付處理會模擬 3-5 秒的延遲後預設標記為成功，用於開發和測試環境。

## 系統架構圖

```mermaid
graph TB
    subgraph "外部系統"
        User[用戶/前端]
    end

    subgraph "EcommerceBackend (C# .NET)"
        EB_Order[訂單服務<br/>OrderService]
        EB_Payment[支付服務<br/>PaymentService]
        EB_Product[商品服務]
        EB_User[用戶服務]
        EB_Inventory[庫存服務<br/>InventoryService]
    end

    subgraph "ec-payment-service (Go)"
        PS_Handler[支付處理器<br/>PaymentHandler]
        PS_Service[支付服務<br/>PaymentService]
        PS_Scheduler[支付排程器]
    end

    subgraph "ec-order-state-service (Go)"
        OS_Handler[狀態事件處理器<br/>OrderStateEventHandler]
        OS_Service[狀態服務<br/>OrderStateService]
        OS_Scheduler[狀態排程器]
    end

    subgraph "基礎設施"
        RabbitMQ[RabbitMQ<br/>訊息佇列]
        Redis[Redis<br/>快取與狀態管理]
        PostgreSQL[(PostgreSQL<br/>資料庫)]
    end

    %% 用戶請求流程
    User -->|HTTP| EB_Order
    User -->|HTTP| EB_Product
    User -->|HTTP| EB_User

    %% 訂單創建流程
    EB_Order -->|檢查庫存| Redis
    EB_Order -->|保存訂單| PostgreSQL
    EB_Order -->|發送延遲消息| RabbitMQ
    EB_Order -->|HTTP POST| PS_Handler

    %% 支付流程
    PS_Handler -->|保存支付記錄| PostgreSQL
    PS_Scheduler -->|處理支付| PS_Service
    PS_Service -->|HTTP POST 回調<br/>模擬支付成功| EB_Payment

    %% 支付完成後流程
    EB_Payment -->|發送事件| RabbitMQ
    RabbitMQ -->|PaymentCompleted| OS_Handler
    OS_Handler -->|處理狀態轉換| OS_Service
    OS_Service -->|更新狀態| PostgreSQL
    OS_Service -->|發送狀態變更| RabbitMQ
    RabbitMQ -->|OrderStatusChanged| EB_Order

    %% 訂單超時處理
    RabbitMQ -->|訂單超時消息| EB_Order
    EB_Order -->|回補庫存| Redis

    %% 庫存管理
    EB_Inventory -->|預扣/回補庫存| Redis
    EB_Order -->|確認庫存| EB_Inventory

    %% 資料庫連接
    EB_Order -.->|讀寫| PostgreSQL
    EB_Payment -.->|讀寫| PostgreSQL
    PS_Service -.->|讀寫| PostgreSQL
    OS_Service -.->|讀寫| PostgreSQL

    style EB_Order fill:#e1f5ff
    style EB_Payment fill:#e1f5ff
    style PS_Handler fill:#fff4e1
    style OS_Handler fill:#fff4e1
    style RabbitMQ fill:#ffe1f5
    style Redis fill:#ffe1f5
    style PostgreSQL fill:#e1ffe1
```

## 服務職責說明

### EcommerceBackend (C# .NET)

**主要職責：**
- 📦 **訂單管理**：創建訂單、查詢訂單、訂單狀態同步
- 💰 **支付整合**：接收模擬支付回調、處理支付完成事件
- 📚 **商品管理**：商品資訊、商品變體管理
- 👤 **用戶管理**：用戶認證、會話管理
- 📊 **庫存管理**：透過 Redis 進行庫存預扣、回補、確認

**關鍵功能：**
- 訂單創建時預扣庫存（Redis）
- 發送訂單超時延遲消息（RabbitMQ TTL + DLX）
- 處理支付完成事件並發送狀態更新消息
- 接收訂單狀態變更通知並同步本地狀態

### ec-payment-service (Go)

**主要職責：**
- 💳 **支付處理**：接收支付請求、模擬支付處理邏輯
- 📝 **支付記錄**：保存支付記錄、查詢支付狀態
- 🔄 **支付回調**：處理支付完成後的回調通知

**關鍵功能：**
- 接收來自 EcommerceBackend 的支付請求（HTTP POST `/api/payment/process`）
- 保存支付記錄到資料庫
- 透過排程器模擬支付處理（延遲 3-5 秒，預設支付成功）
- 支付完成後透過 HTTP POST 回調 EcommerceBackend（SimulatePaid = "1" 標記為模擬支付）

**模擬支付說明：**
- 系統使用模擬支付而非真實的第三方支付（如綠界支付）
- 支付處理器會模擬支付延遲（3-5 秒隨機），然後預設標記為支付成功
- 回調資料中的 `SimulatePaid = "1"` 表示這是模擬支付，不會產生實際的資金流動
- 端點名稱 `/Payment/ECPayReturn` 為歷史遺留命名，實際功能為接收模擬支付回調

### ec-order-state-service (Go)

**主要職責：**
- 🔄 **狀態管理**：訂單狀態機管理、狀態轉換驗證
- 📋 **狀態追蹤**：訂單步驟記錄、狀態歷史
- ⚡ **事件處理**：處理支付完成等事件並更新狀態
- 🕐 **自動推進**：透過排程器自動推進訂單狀態

**關鍵功能：**
- 接收 PaymentCompleted 事件（RabbitMQ）
- 驗證狀態轉換合法性（狀態機）
- 更新訂單狀態並記錄步驟
- 發送狀態變更通知回 EcommerceBackend
- 提供 HTTP API 進行手動狀態更新（`PUT /orders/:orderId/status`）

## 交互方式詳解

### HTTP 交互

```mermaid
sequenceDiagram
    participant User as 用戶/前端
    participant EB as EcommerceBackend
    participant PS as ec-payment-service
    participant OS as ec-order-state-service

    Note over User,OS: 訂單創建與支付流程

    User->>EB: POST /Order/GenerateOrder
    EB->>PS: POST /api/payment/process<br/>(支付請求)
    PS-->>EB: 返回 PaymentID 和狀態
    EB-->>User: 返回訂單資訊

    Note over PS,EB: 支付處理完成後（模擬支付）

    PS->>PS: 排程器模擬支付處理<br/>(延遲 3-5 秒，預設成功)
    PS->>EB: POST /Payment/ECPayReturn<br/>(支付回調，SimulatePaid="1")
    EB-->>PS: 返回處理結果

    Note over EB,OS: 狀態查詢（可選）

    User->>OS: PUT /orders/:orderId/status<br/>(手動更新狀態)
    OS-->>User: 返回更新結果
```

### RabbitMQ 消息交互

```mermaid
graph LR
    subgraph "EcommerceBackend 發送"
        EB1[訂單超時消息<br/>order.timeout.exchange]
        EB2[支付完成事件<br/>order.state.update<br/>routingKey: order.state.payment.completed]
    end

    subgraph "RabbitMQ"
        MQ1[order_timeout_delay_queue<br/>TTL + DLX]
        MQ2[order_timeout_queue]
        MQ3[order_state_queue]
        MQ4[order_state_changed_queue]
    end

    subgraph "EcommerceBackend 接收"
        EB3[訂單超時消費者<br/>OrderTimeoutConsumer]
        EB4[狀態變更消費者<br/>OrderStatusChangedConsumer]
    end

    subgraph "ec-order-state-service 接收"
        OS1[狀態事件消費者<br/>OrderStateConsumer]
    end

    subgraph "ec-order-state-service 發送"
        OS2[狀態變更通知<br/>order.state.update<br/>routingKey: order.state.status.changed]
    end

    EB1 -->|延遲消息| MQ1
    MQ1 -->|TTL 到期| MQ2
    MQ2 --> EB3

    EB2 --> MQ3
    MQ3 --> OS1
    OS1 -->|處理後| OS2
    OS2 --> MQ4
    MQ4 --> EB4
```

### 消息流程詳解

#### 1. 訂單超時消息流程

```mermaid
sequenceDiagram
    participant EB as EcommerceBackend
    participant MQ as RabbitMQ
    participant Consumer as OrderTimeoutConsumer

    Note over EB,Consumer: 訂單創建時發送延遲消息

    EB->>MQ: 發送到 order.timeout.exchange<br/>(TTL: 2分鐘)
    MQ->>MQ: 消息進入 order_timeout_delay_queue<br/>(等待 TTL 到期)
    
    Note over MQ: 2分鐘後 TTL 到期

    MQ->>MQ: 消息轉移到 order.timeout.dlx
    MQ->>MQ: 消息進入 order_timeout_queue
    MQ->>Consumer: 消費消息
    Consumer->>EB: 處理訂單超時<br/>(回補庫存、取消訂單)
```

#### 2. 支付完成事件流程

```mermaid
sequenceDiagram
    participant PS as ec-payment-service
    participant EB as EcommerceBackend
    participant MQ as RabbitMQ
    participant OS as ec-order-state-service

    PS->>PS: 排程器模擬支付處理<br/>(延遲 3-5 秒，預設成功)
    PS->>EB: HTTP POST /Payment/ECPayReturn<br/>(支付回調，SimulatePaid="1")
    EB->>EB: 標記支付為已完成
    EB->>MQ: 發送 PaymentCompleted 事件<br/>(exchange: order.state.update)
    MQ->>OS: 消費消息<br/>(OrderStateConsumer)
    OS->>OS: 處理狀態轉換<br/>(Created → WaitingForShipment)
    OS->>MQ: 發送狀態變更通知<br/>(routingKey: order.state.status.changed)
    MQ->>EB: 消費狀態變更消息<br/>(OrderStatusChangedConsumer)
    EB->>EB: 同步本地訂單狀態
```

## Redis 職責說明

```mermaid
graph TB
    subgraph "Redis 使用場景"
        R1[庫存管理<br/>product:stock<br/>Hash 結構]
        R2[庫存預扣<br/>stock:hold:orderId<br/>Hash 結構，TTL: 150秒]
        R3[用戶會話<br/>user:sessionId<br/>String，TTL: 2小時]
        R4[消息冪等性<br/>message:processed:key<br/>String，TTL: 24小時]
        R5[分散式鎖<br/>lock:payment:recordCode<br/>String，TTL: 5秒]
    end

    subgraph "EcommerceBackend 操作"
        EB1[訂單創建時<br/>預扣庫存]
        EB2[支付完成時<br/>確認庫存]
        EB3[訂單超時時<br/>回補庫存]
        EB4[用戶登入時<br/>保存會話]
        EB5[支付回調時<br/>分散式鎖]
        EB6[消息處理時<br/>冪等性檢查]
    end

    EB1 --> R1
    EB1 --> R2
    EB2 --> R1
    EB3 --> R1
    EB3 --> R2
    EB4 --> R3
    EB5 --> R5
    EB6 --> R4

    style R1 fill:#ffcccc
    style R2 fill:#ffcccc
    style R3 fill:#ccffcc
    style R4 fill:#ccccff
    style R5 fill:#ffffcc
```

**Redis 主要用途：**

1. **庫存管理** (`product:stock`)
   - 使用 Hash 結構存儲商品變體庫存
   - 支援原子性操作（HINCRBY）
   - 訂單創建時預扣，支付完成時確認，超時時回補

2. **庫存預扣記錄** (`stock:hold:orderId`)
   - 記錄每個訂單預扣的庫存數量
   - 設定 TTL（150秒）自動過期
   - 用於訂單超時時回補庫存

3. **用戶會話管理** (`user:sessionId`)
   - 存儲用戶登入資訊
   - TTL 設定為 2 小時
   - 用於認證和授權

4. **消息冪等性檢查** (`message:processed:key`)
   - 防止重複處理相同的消息
   - TTL 設定為 24 小時
   - 用於 RabbitMQ 消息消費的冪等性保證

5. **分散式鎖** (`lock:payment:recordCode`)
   - 防止同一訂單的支付回調被重複處理
   - TTL 設定為 5 秒
   - 使用 SET NX EX 實現

## RabbitMQ 職責說明

```mermaid
graph TB
    subgraph "RabbitMQ Exchange 與 Queue"
        E1[order.timeout.exchange<br/>Direct]
        E2[order.timeout.dlx<br/>Dead Letter Exchange]
        E3[order.state.update<br/>Direct]
        E4[dead.letter.exchange<br/>通用 DLX]

        Q1[order_timeout_delay_queue<br/>TTL + DLX]
        Q2[order_timeout_queue<br/>處理隊列]
        Q3[order_state_queue<br/>狀態事件隊列]
        Q4[order_state_changed_queue<br/>狀態變更隊列]
        Q5[payment_completed_queue<br/>支付完成隊列]
        Q6[各種 DLQ<br/>死信隊列]
    end

    subgraph "消息類型"
        M1[延遲消息<br/>訂單超時]
        M2[事件消息<br/>PaymentCompleted]
        M3[通知消息<br/>OrderStatusChanged]
        M4[事件消息<br/>PaymentCompleted<br/>內部使用]
    end

    M1 --> E1
    E1 --> Q1
    Q1 -->|TTL 到期| E2
    E2 --> Q2

    M2 --> E3
    E3 --> Q3

    M3 --> E3
    E3 --> Q4

    M4 --> E4
    E4 --> Q5

    Q1 -.->|失敗| Q6
    Q2 -.->|失敗| Q6
    Q3 -.->|失敗| Q6
    Q4 -.->|失敗| Q6
    Q5 -.->|失敗| Q6

    style E1 fill:#ffe1f5
    style E2 fill:#ffe1f5
    style E3 fill:#ffe1f5
    style E4 fill:#ffe1f5
    style Q6 fill:#ffcccc
```

**RabbitMQ 主要用途：**

1. **異步消息傳遞**
   - 解耦服務之間的直接依賴
   - 提高系統的可擴展性和可靠性

2. **延遲消息處理**
   - 使用 TTL + DLX 實現訂單超時檢查
   - 訂單創建 2 分鐘後自動檢查支付狀態

3. **事件驅動架構**
   - PaymentCompleted 事件觸發訂單狀態更新
   - OrderStatusChanged 通知同步狀態變更

4. **消息可靠性保證**
   - 使用 DLQ（Dead Letter Queue）處理失敗消息
   - 支援消息重試機制
   - 確保消息不丟失

5. **負載均衡**
   - 多個消費者可以並行處理消息
   - 使用 Prefetch 控制消息分發

## 完整訂單生命週期流程

```mermaid
sequenceDiagram
    participant User as 用戶
    participant EB as EcommerceBackend
    participant Redis as Redis
    participant MQ as RabbitMQ
    participant PS as ec-payment-service
    participant OS as ec-order-state-service
    participant DB as PostgreSQL

    Note over User,DB: 1. 訂單創建階段

    User->>EB: 創建訂單請求
    EB->>Redis: 檢查並預扣庫存
    Redis-->>EB: 庫存預扣成功
    EB->>DB: 保存訂單和支付記錄
    EB->>MQ: 發送訂單超時延遲消息（2分鐘）
    EB->>PS: HTTP POST 支付請求
    PS-->>EB: 返回 PaymentID
    EB-->>User: 返回訂單資訊

    Note over MQ,EB: 2. 支付處理階段（模擬支付）

    PS->>PS: 排程器模擬支付處理<br/>(延遲 3-5 秒，預設支付成功)
    PS->>EB: HTTP POST 支付回調<br/>(SimulatePaid="1" 標記模擬支付)
    EB->>Redis: 獲取分散式鎖
    EB->>DB: 標記支付為已完成
    EB->>Redis: 確認庫存（正式扣除）
    EB->>MQ: 發送 PaymentCompleted 事件

    Note over MQ,OS: 3. 狀態更新階段

    MQ->>OS: 消費 PaymentCompleted 事件
    OS->>DB: 更新訂單狀態<br/>(Created → WaitingForShipment)
    OS->>MQ: 發送 OrderStatusChanged 通知
    MQ->>EB: 消費狀態變更消息
    EB->>DB: 同步本地訂單狀態

    Note over MQ,EB: 4. 訂單超時處理（如果未支付）

    MQ->>EB: 訂單超時消息（2分鐘後）
    EB->>DB: 檢查訂單狀態
    alt 訂單未支付
        EB->>Redis: 回補庫存
        EB->>DB: 取消訂單
    end
```

## 技術棧總結

| 服務 | 技術棧 | 主要職責 |
|------|--------|----------|
| **EcommerceBackend** | C# .NET, Entity Framework Core | 訂單管理、商品管理、用戶管理、庫存管理 |
| **ec-payment-service** | Go, Gin, PostgreSQL | 支付處理、支付記錄管理 |
| **ec-order-state-service** | Go, Gin, PostgreSQL | 訂單狀態管理、狀態機驗證 |
| **RabbitMQ** | AMQP 0.9.1 | 異步消息傳遞、延遲消息、事件驅動 |
| **Redis** | StackExchange.Redis | 庫存管理、會話管理、分散式鎖、冪等性 |
| **PostgreSQL** | Entity Framework Core / pgx | 資料持久化 |

## 關鍵設計模式

1. **事件驅動架構 (EDA)**
   - 使用 RabbitMQ 實現服務間的事件通信
   - 解耦服務依賴，提高系統彈性

2. **狀態機模式**
   - ec-order-state-service 實現訂單狀態機
   - 確保狀態轉換的合法性和一致性

3. **分散式鎖模式**
   - 使用 Redis 實現分散式鎖
   - 防止支付回調重複處理

4. **冪等性保證**
   - 使用 Redis 記錄已處理的消息
   - 確保消息處理的冪等性

5. **延遲消息模式**
   - 使用 RabbitMQ TTL + DLX 實現延遲消息
   - 用於訂單超時檢查

6. **庫存預扣模式**
   - 使用 Redis 原子操作預扣庫存
   - 支付完成後確認，超時後回補

