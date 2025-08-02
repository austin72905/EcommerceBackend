using Application.Interfaces;
using Common.Interfaces.Application.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    /// <summary>
    /// 訂單超時處理器
    /// </summary>
    public class OrderTimeoutHandler : IOrderTimeoutHandler
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderTimeoutHandler> _logger;

        public OrderTimeoutHandler(IOrderService orderService, ILogger<OrderTimeoutHandler> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task HandleOrderTimeoutAsync(int userId, string recordCode)
        {
            try
            {
                _logger.LogInformation("Processing order timeout for user {UserId}, order {RecordCode}", userId, recordCode);
                
                await _orderService.HandleOrderTimeoutAsync(userId, recordCode);
                
                _logger.LogInformation("Order timeout processed successfully for order {RecordCode}", recordCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing order timeout for order {RecordCode}", recordCode);
                throw;
            }
        }
    }
}