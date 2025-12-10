namespace Common.Interfaces.Application.Services
{
    public interface IOrderStatusSyncService
    {
        /// <summary>
        /// 從訂單狀態服務同步訂單狀態（統一使用數字 enum）
        /// </summary>
        /// <param name="recordCode">訂單編號</param>
        /// <param name="fromStatus">來源狀態（數字 enum 值）</param>
        /// <param name="toStatus">目標狀態（數字 enum 值）</param>
        /// <returns></returns>
        Task SyncOrderStatusFromStateServiceAsync(string recordCode, int fromStatus, int toStatus);
    }
}


