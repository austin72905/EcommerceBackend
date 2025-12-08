using Application.DTOs;
using Application.Interfaces;
using Common.Interfaces.Infrastructure;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Services
{
    /// <summary>
    /// 庫存管理服務
    /// </summary>
    public class InventoryService : BaseService<InventoryService>, IInventoryService
    {
        private readonly IRedisService _redisService;
        private readonly IOrderRepostory _orderRepository;
        private readonly IProductRepository _productRepository;

        public InventoryService(
            IRedisService redisService,
            IOrderRepostory orderRepository,
            IProductRepository productRepository,
            ILogger<InventoryService> logger) : base(logger)
        {
            _redisService = redisService;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// 檢查並預扣庫存
        /// </summary>
        public async Task<ServiceResult<InventoryCheckResult>> CheckAndHoldInventoryAsync(
            string orderId, 
            Dictionary<int, int> variantInventory)
        {
            try
            {
                if (string.IsNullOrEmpty(orderId))
                {
                    return Error<InventoryCheckResult>("訂單編號不能為空", "訂單編號不能為空");
                }

                if (variantInventory == null || !variantInventory.Any())
                {
                    return Error<InventoryCheckResult>("商品清單不能為空", "商品清單不能為空");
                }

                // 調用 Redis 服務檢查並預扣庫存
                var checkStockStatus = await _redisService.CheckAndHoldStockAsync(
                    orderId, 
                    variantInventory);

                if (checkStockStatus == null)
                {
                    _logger.LogWarning("Redis 沒有找到庫存資料，訂單編號: {OrderId}", orderId);
                    return Error<InventoryCheckResult>("Redis 沒有找到庫存資料", "系統錯誤，請聯繫管理員");
                }

                // 解析 Redis 返回的 JSON 結果
                StockCheckDTO? result;
                try
                {
                    var jsonString = checkStockStatus?.ToString() ?? string.Empty;
                    result = JsonSerializer.Deserialize<StockCheckDTO>(
                        jsonString,
                        new JsonSerializerOptions 
                        { 
                            PropertyNameCaseInsensitive = true  // 忽略大小寫
                        });
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "庫存檢查結果 JSON 解析失敗，訂單編號: {OrderId}, 原始資料: {RawData}", 
                        orderId, checkStockStatus);
                    return Error<InventoryCheckResult>("庫存檢查結果解析失敗", "系統錯誤，請聯繫管理員");
                }

                if (result == null)
                {
                    _logger.LogWarning("庫存檢查結果為空，訂單編號: {OrderId}", orderId);
                    return Error<InventoryCheckResult>("庫存檢查結果為空", "系統錯誤，請聯繫管理員");
                }

                // 檢查庫存是否充足
                if (result.Status == "error")
                {
                    var failedVariantIds = new List<int>();
                    
                    if (result.Failed != null && result.Failed.Any())
                    {
                        // 將字串 ID 轉換為整數
                        foreach (var failedId in result.Failed)
                        {
                            if (int.TryParse(failedId, out var variantId))
                            {
                                failedVariantIds.Add(variantId);
                            }
                        }
                    }

                    var failedItems = failedVariantIds.Any() 
                        ? string.Join(",", failedVariantIds) 
                        : "未知商品";

                    _logger.LogWarning("庫存不足，訂單編號: {OrderId}, 失敗的商品變體: {FailedItems}", 
                        orderId, failedItems);

                    return Fail<InventoryCheckResult>($"庫存不足: {failedItems}");
                }

                // 庫存檢查成功
                _logger.LogInformation("庫存檢查並預扣成功，訂單編號: {OrderId}", orderId);
                
                return Success<InventoryCheckResult>(new InventoryCheckResult
                {
                    IsSuccess = true,
                    OrderId = orderId,
                    FailedVariantIds = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "檢查庫存時發生錯誤，訂單編號: {OrderId}", orderId);
                return Error<InventoryCheckResult>(ex.Message, "系統錯誤，請聯繫管理員");
            }
        }

        /// <summary>
        /// 確認庫存（付款成功後，正式扣除庫存）
        /// </summary>
        public async Task<ServiceResult<bool>> ConfirmInventoryAsync(string orderId)
        {
            // 使用分散式鎖防止同一訂單的庫存確認操作並發執行
            string lockKey = $"inventory_confirm:{orderId}";
            string lockValue = Guid.NewGuid().ToString();
            bool lockAcquired = false;
            // 這個可以加stop watch
            try
            {
                if (string.IsNullOrEmpty(orderId))
                {
                    return new ServiceResult<bool>
                    {
                        IsSuccess = false,
                        ErrorMessage = "訂單編號不能為空"
                    };
                }

                // 嘗試獲取分散式鎖（鎖定時間 10 秒，足夠完成庫存確認）
                lockAcquired = await _redisService.TryAcquireLockAsync(lockKey, lockValue, TimeSpan.FromSeconds(10));
                
                if (!lockAcquired)
                {
                    _logger.LogWarning("無法獲取分散式鎖，訂單編號: {OrderId}，可能正在被其他請求處理，跳過此次庫存確認", orderId);
                    // 返回成功，避免重複處理（庫存已經在創建訂單時預扣）
                    return new ServiceResult<bool>
                    {
                        IsSuccess = true,
                        Data = true
                    };
                }

                // 1. 獲取訂單信息（僅載入 OrderProducts，減少資料庫查詢負載）
                // 使用 ConfigureAwait(false) 避免捕獲同步上下文，確保操作順序執行
                var order = await _orderRepository.GetOrderWithProductsOnlyForUpdate(orderId).ConfigureAwait(false);
                if (order == null)
                {
                    _logger.LogWarning("找不到訂單，訂單編號: {OrderId}", orderId);
                    return new ServiceResult<bool>
                    {
                        IsSuccess = false,
                        ErrorMessage = "找不到訂單"
                    };
                }

                // 2. 獲取訂單中的所有商品變體和數量（在記憶體中處理，不涉及資料庫）
                var variantQuantities = order.OrderProducts
                    .GroupBy(op => op.ProductVariantId)
                    .Select(g => new { VariantId = g.Key, Quantity = g.Sum(op => op.Count) })
                    .ToList();

                if (!variantQuantities.Any())
                {
                    _logger.LogWarning("訂單中沒有商品，訂單編號: {OrderId}", orderId);
                    return new ServiceResult<bool>
                    {
                        IsSuccess = false,
                        ErrorMessage = "訂單中沒有商品"
                    };
                }

                // 3. 批量扣除資料庫庫存（直接在 SQL 中計算，減少一次查詢）
                // 將訂單商品變體和數量轉換為字典
                var variantQuantitiesDict = variantQuantities
                    .ToDictionary(vq => vq.VariantId, vq => vq.Quantity);

                // 直接扣除庫存（SQL 中會使用 GREATEST(0, Stock - Quantity) 確保不會變成負數）
                // 使用 ConfigureAwait(false) 確保操作順序執行，避免並發問題
                await _productRepository.DeductProductVariantStocksAsync(variantQuantitiesDict).ConfigureAwait(false);

                // 記錄日誌（簡化版，因為不再需要查詢當前庫存）
                foreach (var item in variantQuantities)
                {
                    _logger.LogInformation("扣除商品變體庫存，變體 ID: {VariantId}, 扣除數量: {Quantity}, 訂單編號: {OrderId}",
                        item.VariantId, item.Quantity, orderId);
                }

                _logger.LogInformation("庫存確認成功，已更新資料庫庫存，訂單編號: {OrderId}", orderId);
                
                return new ServiceResult<bool>
                {
                    IsSuccess = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "確認庫存時發生錯誤，訂單編號: {OrderId}", orderId);
                return new ServiceResult<bool>
                {
                    IsSuccess = false,
                    ErrorMessage = "系統錯誤，請聯繫管理員"
                };
            }
            finally
            {
                // 釋放分散式鎖
                if (lockAcquired)
                {
                    try
                    {
                        await _redisService.ReleaseLockAsync(lockKey, lockValue).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "釋放分散式鎖失敗，訂單編號: {OrderId}，鎖會自動過期", orderId);
                    }
                }
            }
        }

        /// <summary>
        /// 回滾庫存（訂單取消或付款失敗時，將預扣的庫存回補）
        /// </summary>
        public async Task<ServiceResult<bool>> RollbackInventoryAsync(string orderId)
        {
            try
            {
                if (string.IsNullOrEmpty(orderId))
                {
                    return new ServiceResult<bool>
                    {
                        IsSuccess = false,
                        ErrorMessage = "訂單編號不能為空"
                    };
                }

                // 調用 Redis 服務回滾庫存
                var result = await _redisService.RollbackStockAsync(orderId);
                
                if (result == null)
                {
                    _logger.LogWarning("回滾庫存失敗，訂單編號: {OrderId}，可能該訂單沒有預扣庫存", orderId);
                    // 即使回滾失敗，也返回成功（可能是訂單已經處理過或沒有預扣庫存）
                    return new ServiceResult<bool>
                    {
                        IsSuccess = true,
                        Data = true
                    };
                }

                _logger.LogInformation("庫存回滾成功，訂單編號: {OrderId}", orderId);
                
                return new ServiceResult<bool>
                {
                    IsSuccess = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "回滾庫存時發生錯誤，訂單編號: {OrderId}", orderId);
                return new ServiceResult<bool>
                {
                    IsSuccess = false,
                    ErrorMessage = "系統錯誤，請聯繫管理員"
                };
            }
        }

        /// <summary>
        /// 獲取單個商品變體的庫存
        /// </summary>
        public async Task<ServiceResult<int?>> GetProductInventoryAsync(int variantId)
        {
            try
            {
                if (variantId <= 0)
                {
                    return new ServiceResult<int?>
                    {
                        IsSuccess = false,
                        ErrorMessage = "參數錯誤"
                    };
                }

                var stock = await _redisService.GetProductStockAsync(variantId);
                
                return new ServiceResult<int?>
                {
                    IsSuccess = true,
                    Data = stock
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "獲取商品庫存時發生錯誤，商品變體 ID: {VariantId}", variantId);
                return new ServiceResult<int?>
                {
                    IsSuccess = false,
                    ErrorMessage = "系統錯誤，請聯繫管理員"
                };
            }
        }

        /// <summary>
        /// 批量獲取商品變體的庫存
        /// </summary>
        public async Task<ServiceResult<Dictionary<int, int>>> GetProductInventoriesAsync(int[] variantIds)
        {
            try
            {
                if (variantIds == null || variantIds.Length == 0)
                {
                    return new ServiceResult<Dictionary<int, int>>
                    {
                        IsSuccess = false,
                        ErrorMessage = "參數錯誤"
                    };
                }

                var stocks = await _redisService.GetProductStocksAsync(variantIds);
                
                return new ServiceResult<Dictionary<int, int>>
                {
                    IsSuccess = true,
                    Data = stocks
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "批量獲取商品庫存時發生錯誤");
                return new ServiceResult<Dictionary<int, int>>
                {
                    IsSuccess = false,
                    ErrorMessage = "系統錯誤，請聯繫管理員"
                };
            }
        }
    }
}

