using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Application.Services
{
    /// <summary>
    /// 訂單超時處理介面
    /// </summary>
    public interface IOrderTimeoutHandler
    {
        /// <summary>
        /// 處理訂單超時
        /// </summary>
        /// <param name="userId">用戶ID</param>
        /// <param name="recordCode">訂單記錄代碼</param>
        /// <returns></returns>
        Task HandleOrderTimeoutAsync(int userId, string recordCode);
    }
}