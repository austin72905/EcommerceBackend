using Domain.Interfaces;

namespace Domain.Interfaces.Repositories
{
    /// <summary>
    /// 聚合根儲存庫介面
    /// 專門用於操作聚合根，確保聚合內的一致性
    /// </summary>
    /// <typeparam name="T">聚合根類型，必須實作 IAggregateRoot</typeparam>
    public interface IAggregateRootRepository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        /// <summary>
        /// 根據 ID 獲取聚合根（包含所有聚合內實體）
        /// </summary>
        Task<T?> GetByIdWithAggregateAsync(int id);
        
        /// <summary>
        /// 保存聚合根及其所有變更（確保聚合內一致性）
        /// </summary>
        Task SaveAggregateAsync(T aggregateRoot);
    }
}

