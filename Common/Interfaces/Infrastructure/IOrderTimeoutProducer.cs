using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Infrastructure
{
    public interface IOrderTimeoutProducer
    {
        /// <summary>
        /// 發送延遲訂單超時訊息
        /// </summary>
        /// <param name="userId">用戶ID</param>
        /// <param name="recordCode">訂單記錄代碼</param>
        /// <param name="delayMinutes">延遲分鐘數，默認10分鐘</param>
        /// <returns></returns>
        Task SendOrderTimeoutMessageAsync(int userId, string recordCode, int delayMinutes = 10);

        /// <summary>
        /// 發送以秒為單位的延遲訊息（用於測試）
        /// </summary>
        /// <param name="userId">用戶ID</param>
        /// <param name="recordCode">訂單記錄代碼</param>
        /// <param name="delaySeconds">延遲秒數</param>
        /// <returns></returns>
        Task SendOrderTimeoutMessageWithSecondsAsync(int userId, string recordCode, int delaySeconds);
    }
}