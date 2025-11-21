using Application.DTOs;
using Application.Interfaces;
using Common.Interfaces.Infrastructure;
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

        public InventoryService(
            IRedisService redisService,
            ILogger<InventoryService> logger) : base(logger)
        {
            _redisService = redisService;
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
        public Task<ServiceResult<bool>> ConfirmInventoryAsync(string orderId)
        {
            try
            {
                if (string.IsNullOrEmpty(orderId))
                {
                    return Task.FromResult(new ServiceResult<bool>
                    {
                        IsSuccess = false,
                        ErrorMessage = "訂單編號不能為空"
                    });
                }

                // 付款成功後，刪除 holdKey，表示庫存正式確認扣除
                // 注意：目前 Redis 的 holdKey 會自動過期，這裡可以選擇：
                // 1. 提前刪除 holdKey（推薦，表示正式確認）
                // 2. 保留 holdKey 直到過期（用於審計）
                
                // 實作：刪除 holdKey
                var holdKey = $"stock:hold:{orderId}";
                // 由於 IRedisService 沒有直接刪除 key 的方法，我們可以：
                // 1. 在 IRedisService 中新增 DeleteKeyAsync 方法
                // 2. 或者使用 RollbackStockAsync 後再重新扣除（不推薦）
                // 3. 或者讓 holdKey 自然過期（目前實作）
                
                // 目前實作：由於 Redis 的 holdKey 會自動過期，付款成功後不需要特別處理
                // 庫存已經在 CheckAndHoldInventoryAsync 時從 Redis 扣除
                // holdKey 只是用於記錄，過期後會自動清理
                
                _logger.LogInformation("庫存確認成功，訂單編號: {OrderId}", orderId);
                
                return Task.FromResult(new ServiceResult<bool>
                {
                    IsSuccess = true,
                    Data = true
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "確認庫存時發生錯誤，訂單編號: {OrderId}", orderId);
                return Task.FromResult(new ServiceResult<bool>
                {
                    IsSuccess = false,
                    ErrorMessage = "系統錯誤，請聯繫管理員"
                });
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

