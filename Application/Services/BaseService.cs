using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public abstract class BaseService<T>
    {
        private readonly ILogger<T> _logger;
        protected BaseService(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected ServiceResult<E> Success<E>(E? data = null,string? message=null) where E : class
        {
            return new ServiceResult<E>
            {
                IsSuccess = true,
                Data = data,
                ErrorMessage = message
            };
        }

        protected ServiceResult<E> Error<E>(string exceptionMsg, string? message = null) where E : class
        {
            _logger.LogError($"an error occured : {exceptionMsg}");
            return new ServiceResult<E>
            {
                IsSuccess = false,
                ErrorMessage = string.IsNullOrEmpty(message) ? "系統錯誤，請聯繫管理員" : message
            };
        }

        protected ServiceResult<E> Fail<E>(string? message = null) where E : class
        {
            return new ServiceResult<E>
            {
                IsSuccess = false,
                ErrorMessage = string.IsNullOrEmpty(message) ? "請求資源不存在" : message
            };
        }
    }
}
