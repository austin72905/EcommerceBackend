namespace Common.Interfaces.Application.Services
{
    public interface IOrderStatusSyncService
    {
        /// <summary>
        /// 從訂單狀態服務同步訂單狀態
        /// </summary>
        /// <param name="recordCode">訂單編號</param>
        /// <param name="fromStatus">來源狀態（Go 服務的狀態字串）</param>
        /// <param name="toStatus">目標狀態（Go 服務的狀態字串）</param>
        /// <returns></returns>
        Task SyncOrderStatusFromStateServiceAsync(string recordCode, string fromStatus, string toStatus);
    }
}


