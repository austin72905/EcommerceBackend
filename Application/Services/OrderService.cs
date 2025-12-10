using Application.DTOs;
using Application.DummyData;
using Application.Extensions;
using Application.Interfaces;
using Common.Interfaces.Application.Services;
using Common.Interfaces.Infrastructure;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;
using System;
using System.Threading.Tasks;
using DomainOrder = Domain.Entities.Order; // 使用別名避免命名空間衝突

namespace Application.Services
{
    public class OrderService : BaseService<OrderService>, IOrderService, IOrderStatusSyncService
    {
        private readonly IOrderRepostory _orderRepostory;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IOrderTimeoutProducer _orderTimeoutProducer;

        private readonly IOrderDomainService _orderDomainService;

        private readonly IConfiguration _configuration;

        /// <summary>
        /// 預定義的狀態轉換路徑（靜態只讀，避免每次調用時重複建立）
        /// </summary>
        private static readonly ImmutableDictionary<(OrderStatus, OrderStatus), ImmutableArray<OrderStatus>> StatusPaths =
            new Dictionary<(OrderStatus, OrderStatus), OrderStatus[]>
            {
                // WaitingForShipment -> Completed 的路徑
                { (OrderStatus.WaitingForShipment, OrderStatus.Completed), 
                  new[] { OrderStatus.InTransit, OrderStatus.WaitPickup, OrderStatus.Completed } },
                // WaitingForShipment -> WaitPickup 的路徑
                { (OrderStatus.WaitingForShipment, OrderStatus.WaitPickup), 
                  new[] { OrderStatus.InTransit, OrderStatus.WaitPickup } },
                // InTransit -> Completed 的路徑
                { (OrderStatus.InTransit, OrderStatus.Completed), 
                  new[] { OrderStatus.WaitPickup, OrderStatus.Completed } },
            }.ToImmutableDictionary(kvp => kvp.Key, kvp => kvp.Value.ToImmutableArray());

        public OrderService(
            IInventoryService inventoryService,
            IOrderRepostory orderRepostory, 
            IProductRepository productRepository, 
            IPaymentRepository paymentRepository, 
            IOrderDomainService orderDomainService, 
            IOrderTimeoutProducer orderTimeoutProducer,
            IConfiguration configuration, 
            ILogger<OrderService> logger) : base(logger)
        {
            _orderRepostory = orderRepostory;
            _productRepository = productRepository;
            _paymentRepository = paymentRepository;
            _orderDomainService = orderDomainService;
            _configuration = configuration;
            _inventoryService = inventoryService;
            _orderTimeoutProducer = orderTimeoutProducer;
        }

        public async Task<ServiceResult<OrderInfomationDTO>> GetOrderInfo(int userid, string recordCode)
        {
            try
            {
                var order = await _orderRepostory.GetOrderInfoByUserId(userid, recordCode);
                if (order == null)
                {
                    return Success<OrderInfomationDTO>();

                }

                var orderDto = order.ToOrderDTO();

                return Success<OrderInfomationDTO>(orderDto);

            }
            catch (Exception ex)
            {

                return Error<OrderInfomationDTO>(ex.Message);

            }

        }

        public async Task<ServiceResult<List<OrderInfomationDTO>>> GetOrders(int userid, string query)
        {
            try
            {
                // 直接在資料庫層過濾和排序，避免載入所有訂單到記憶體
                var orderList = await _orderRepostory.GetOrdersByUserId(userid, query);

                var ordersDto = orderList.ToOrderDTOList();
                
                return Success<List<OrderInfomationDTO>>(ordersDto);                
            }
            catch (Exception ex)
            {
                return Error<List<OrderInfomationDTO>>(ex.Message);
            }
        }

        /// <summary>
        /// 生成訂單 - 使用富領域模型方法
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<ServiceResult<PaymentRequestDataWithUrl>> GenerateOrder(OrderInfo info)
        {
            try
            {
                // 獲取商品變體資料
                var variantIds = info.Items.Select(i => i.VariantId).ToList();
                var productVariants = await _productRepository.GetProductVariants(variantIds);
                var productVariantDict = productVariants.ToDictionary(pv => pv.Id);

                // 使用富領域模型的工廠方法創建訂單
                var order = DomainOrder.Create(
                    userId: info.UserId,
                    receiver: info.ReceiverName,
                    phoneNumber: info.ReceiverPhone,
                    shippingAddress: info.ShippingAddress,
                    recieveWay: info.RecieveWay,
                    email: info.Email,
                    shippingPrice: (int)info.ShippingFee,
                    recieveStore: info.RecieveStore
                );

                // 用來給庫存服務檢查庫存的
                var variantInventoryPair = new Dictionary<int, int>();

                // 添加訂單商品（使用領域方法）
                foreach (var item in info.Items)
                {
                    if (!productVariantDict.TryGetValue(item.VariantId, out var productVariant))
                    {
                        return new ServiceResult<PaymentRequestDataWithUrl>()
                        {
                            IsSuccess = false,
                            ErrorMessage = "品項不存在"
                        };
                    }

                    variantInventoryPair.Add(productVariant.Id, item.Quantity);

                    // 使用領域方法添加商品（只傳入必要資料，避免 EF Core 追蹤整個物件圖）
                    order.AddOrderProduct(productVariant.Id, productVariant.VariantPrice, item.Quantity);
                }

                // 檢查並預扣庫存（使用庫存服務）
                var inventoryResult = await ExecuteWithTimeoutRetryAsync(
                    () => _inventoryService.CheckAndHoldInventoryAsync(order.RecordCode, variantInventoryPair),
                    timeout: TimeSpan.FromSeconds(3),
                    maxRetry: 1);

                if (inventoryResult == null || !inventoryResult.IsSuccess)
                {
                    return Error<PaymentRequestDataWithUrl>(inventoryResult?.ErrorMessage ?? "庫存檢查逾時或失敗");
                }

                // 發送延遲超時訊息 (2分鐘後執行)
                _ = _orderTimeoutProducer.SendOrderTimeoutMessageAsync(info.UserId, order.RecordCode, 2)
                    .ContinueWith(t =>
                    {
                        if (t.IsFaulted)
                        {
                            _logger.LogError(t.Exception, "發送訂單超時訊息失敗 RecordCode={RecordCode}", order.RecordCode);
                        }
                    });

                // 使用領域方法計算總金額（業務邏輯在 Domain 層）
                // 傳入已載入的 productVariants 用於折扣計算
                order.CalculateTotalPrice(_orderDomainService, productVariantDict);

                // 保存訂單與付款記錄於同一交易
                await _orderRepostory.ExecuteInTransactionAsync(async () =>
                {
                    // 保存訂單（領域模型已經自動添加了 OrderStep 和 Shipment）
                    await _orderRepostory.GenerateOrder(order);

                    // 使用領域模型的工廠方法創建付款記錄
                    var payment = Payment.Create(
                        orderId: order.Id, // 此時 order.Id 已經有值 
                        paymentAmount: (int)order.OrderPrice,
                        tenantConfigId: 1 // 假設預設值
                    );

                    await _paymentRepository.GeneratePaymentRecord(payment);
                });

                return Success<PaymentRequestDataWithUrl>
                    (
                        new PaymentRequestDataWithUrl()
                        {
                            Amount = order.OrderPrice.ToString(), // 訂單金額
                            RecordNo = order.RecordCode, // 後端生成的訂單號
                            PaymentUrl = _configuration["AppSettings:PaymentRedirectUrl"],
                            PayType = "ECPAY" // 支付類型 (銀行、綠界第三方支付)
                        }
                    );
            }
            catch (Exception ex)
            {
                return Error<PaymentRequestDataWithUrl>(ex.Message);              
            }
        }

        /// <summary>
        /// 簡易逾時 + 重試包裝，避免外部服務長時間阻塞主流程
        /// </summary>
        private async Task<ServiceResult<InventoryCheckResult>?> ExecuteWithTimeoutRetryAsync(
            Func<Task<ServiceResult<InventoryCheckResult>>> action,
            TimeSpan timeout,
            int maxRetry)
        {
            ServiceResult<InventoryCheckResult>? last = null;
            for (var attempt = 0; attempt <= maxRetry; attempt++)
            {
                var task = action();
                var completed = await Task.WhenAny(task, Task.Delay(timeout));
                if (completed == task)
                {
                    last = await task;
                    if (last.IsSuccess || attempt == maxRetry)
                    {
                        return last;
                    }
                }
                else
                {
                    _logger.LogWarning("庫存檢查逾時，attempt={Attempt}", attempt + 1);
                    if (attempt == maxRetry)
                    {
                        return null;
                    }
                }
            }

            return last;
        }

        /// <summary>
        /// 處理訂單超時 - 使用富領域模型方法
        /// </summary>
        public async Task HandleOrderTimeoutAsync(int userId, string recordcode)
        {
            try
            {
                _logger.LogInformation("Handling order timeout for user {UserId}, order {RecordCode}", userId, recordcode);
                
                var order = await _orderRepostory.GetOrderInfoByUserId(userId, recordcode);

                // 如果訂單不存在或狀態不是 Created（未付款），則不需要處理
                if (order == null || order.Status != (int)OrderStatus.Created)
                {
                    _logger.LogInformation("Order {RecordCode} not found or not in Created status. Current status: {Status}", 
                        recordcode, order?.Status);
                    return;
                }

                _logger.LogInformation("Processing timeout for order {RecordCode}, rolling back inventory", recordcode);
                
                // 回滾庫存（使用庫存服務）
                var rollbackResult = await _inventoryService.RollbackInventoryAsync(recordcode);
                
                if (!rollbackResult.IsSuccess)
                {
                    _logger.LogWarning("回滾庫存失敗，訂單編號: {RecordCode}, 錯誤: {Error}", 
                        recordcode, rollbackResult.ErrorMessage);
                    // 即使回滾失敗，也繼續處理訂單取消
                }
                
                // 使用領域方法取消訂單（這會自動添加訂單步驟）
                // 注意：由於富領域模型的 setter 是 private，我們需要保持原有的 repository 更新方式
                // 或者需要修改 repository 來支持直接保存領域模型的變更
                await _orderRepostory.UpdateOrderStatusAsync(recordcode, (int)OrderStatus.Canceled);
                
                _logger.LogInformation("Order {RecordCode} timeout processed successfully", recordcode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling order timeout for order {RecordCode}", recordcode);
                throw;
            }
        }

        /// <summary>
        /// 從訂單狀態服務同步訂單狀態（統一使用數字 enum）
        /// </summary>
        public async Task SyncOrderStatusFromStateServiceAsync(string recordCode, int fromStatus, int toStatus)
        {
            try
            {
                _logger.LogInformation("[訂單狀態同步] 開始同步訂單狀態: RecordCode={RecordCode}, FromStatus={FromStatus}, ToStatus={ToStatus}",
                    recordCode, fromStatus, toStatus);

                // 獲取訂單（帶追蹤，用於更新狀態）
                var order = await _orderRepostory.GetOrderInfoByRecordCodeForUpdate(recordCode);

                if (order == null)
                {
                    _logger.LogWarning("[訂單狀態同步] 訂單不存在: RecordCode={RecordCode}", recordCode);
                    return;
                }

                // 直接將數字轉換為 OrderStatus enum（統一使用 enum）
                if (!Enum.IsDefined(typeof(OrderStatus), toStatus))
                {
                    _logger.LogWarning("[訂單狀態同步] 無效的狀態值: ToStatus={ToStatus}, RecordCode={RecordCode}", toStatus, recordCode);
                    return;
                }

                var targetStatus = (OrderStatus)toStatus;

                // 檢查當前狀態是否與目標狀態一致（冪等性檢查）
                var currentStatus = (OrderStatus)order.Status;
                if (currentStatus == targetStatus)
                {
                    _logger.LogInformation("[訂單狀態同步] 訂單狀態已是最新: RecordCode={RecordCode}, Status={Status}", 
                        recordCode, targetStatus);
                    return;
                }

                // 檢查訂單是否處於終態（Completed、Canceled、Refund）
                if (currentStatus == OrderStatus.Completed || 
                    currentStatus == OrderStatus.Canceled || 
                    currentStatus == OrderStatus.Refund)
                {
                    _logger.LogWarning("[訂單狀態同步] 訂單已處於終態，忽略狀態轉換: RecordCode={RecordCode}, 當前狀態={CurrentStatus}, 目標狀態={ToStatus}", 
                        recordCode, currentStatus, targetStatus);
                    return; // 終態訂單不再處理狀態轉換
                }

                // 日誌記錄狀態不匹配情況（Go 服務的 fromStatus 可能與實際狀態不同）
                if (Enum.IsDefined(typeof(OrderStatus), fromStatus))
                {
                    var expectedFromStatus = (OrderStatus)fromStatus;
                    if (currentStatus != expectedFromStatus)
                    {
                        _logger.LogWarning("[訂單狀態同步] 狀態不一致: RecordCode={RecordCode}, 資料庫狀態={DbStatus}, Go服務認為的狀態={GoStatus}, 目標狀態={ToStatus}", 
                            recordCode, currentStatus, expectedFromStatus, targetStatus);
                    }
                }

                // 嘗試直接更新狀態
                bool updateSuccess = false;
                try
                {
                    order.UpdateStatus(targetStatus);
                    updateSuccess = true;
                }
                catch (InvalidOperationException ex)
                {
                    // 如果直接轉換失敗，嘗試逐步轉換狀態
                    _logger.LogInformation("[訂單狀態同步] 直接狀態轉換失敗，嘗試逐步轉換: RecordCode={RecordCode}, 從={FromStatus}, 到={ToStatus}, 錯誤={Error}", 
                        recordCode, currentStatus, targetStatus, ex.Message);
                    
                    // 逐步轉換狀態
                    updateSuccess = await TryProgressiveStatusUpdate(order, currentStatus, targetStatus);
                }

                if (!updateSuccess)
                {
                    _logger.LogWarning("[訂單狀態同步] 無法更新狀態: RecordCode={RecordCode}, 從={FromStatus}, 到={ToStatus}", 
                        recordCode, currentStatus, targetStatus);
                    return;
                }

                // 保存變更
                await _orderRepostory.SaveChangesAsync();

                _logger.LogInformation("[訂單狀態同步] 訂單狀態同步成功: RecordCode={RecordCode}, FromStatus={FromStatus}, ToStatus={ToStatus}",
                    recordCode, fromStatus, toStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[訂單狀態同步] 同步訂單狀態失敗: RecordCode={RecordCode}, FromStatus={FromStatus}, ToStatus={ToStatus}",
                    recordCode, fromStatus, toStatus);
                throw;
            }
        }

        /// <summary>
        /// 嘗試逐步轉換狀態（當直接轉換失敗時使用）
        /// </summary>
        private async Task<bool> TryProgressiveStatusUpdate(Order order, OrderStatus currentStatus, OrderStatus targetStatus)
        {
            // 定義狀態轉換路徑
            var statusPath = GetStatusTransitionPath(currentStatus, targetStatus);
            
            if (statusPath == null || statusPath.Count == 0)
            {
                _logger.LogWarning("[訂單狀態同步] 無法找到狀態轉換路徑: 從={FromStatus}, 到={ToStatus}", 
                    currentStatus, targetStatus);
                return false;
            }

            _logger.LogInformation("[訂單狀態同步] 找到狀態轉換路徑: {FromStatus} -> {Path}", 
                currentStatus, string.Join(" -> ", statusPath));

            // 逐步轉換狀態
            var current = currentStatus;
            foreach (var nextStatus in statusPath)
            {
                try
                {
                    order.UpdateStatus(nextStatus);
                    _logger.LogInformation("[訂單狀態同步] 狀態轉換成功: {FromStatus} -> {ToStatus}", 
                        current, nextStatus);
                    current = nextStatus;
                }
                catch (InvalidOperationException ex)
                {
                    _logger.LogWarning("[訂單狀態同步] 逐步轉換失敗: {FromStatus} -> {ToStatus}, 錯誤={Error}", 
                        current, nextStatus, ex.Message);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 獲取狀態轉換路徑（使用預定義的 StatusPaths，避免每次調用時重複建立 Dictionary）
        /// </summary>
        private List<OrderStatus>? GetStatusTransitionPath(OrderStatus from, OrderStatus to)
        {
            // 使用預定義的狀態轉換路徑
            if (StatusPaths.TryGetValue((from, to), out var path))
            {
                return path.ToList();
            }

            // 如果沒有預定義的路徑，檢查是否可以直接轉換
            if (CanTransitionTo(from, to))
            {
                return new List<OrderStatus> { to };
            }

            return null;
        }

        /// <summary>
        /// 檢查是否可以進行狀態轉換（與 Order 實體中的規則一致，統一使用 enum）
        /// 根據 ec-order-state-service 的狀態定義簡化轉換邏輯
        /// </summary>
        private bool CanTransitionTo(OrderStatus from, OrderStatus to)
        {
            return (from, to) switch
            {
                (OrderStatus.Created, OrderStatus.WaitingForShipment) => true,  // Created -> WaitingForShipment
                (OrderStatus.Created, OrderStatus.Canceled) => true,             // Created -> Canceled
                (OrderStatus.WaitingForShipment, OrderStatus.InTransit) => true, // WaitingForShipment -> InTransit
                (OrderStatus.WaitingForShipment, OrderStatus.Canceled) => true,  // WaitingForShipment -> Canceled
                (OrderStatus.InTransit, OrderStatus.WaitPickup) => true,         // InTransit -> WaitPickup
                (OrderStatus.InTransit, OrderStatus.Canceled) => true,           // InTransit -> Canceled
                (OrderStatus.WaitPickup, OrderStatus.Completed) => true,         // WaitPickup -> Completed
                _ => false
            };
        }


        
    }
}
