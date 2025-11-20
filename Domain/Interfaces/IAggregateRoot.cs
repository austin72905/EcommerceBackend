namespace Domain.Interfaces
{
    /// <summary>
    /// 聚合根介面
    /// 標示一個實體為聚合根，負責維護聚合內的一致性邊界和不變性
    /// </summary>
    public interface IAggregateRoot
    {
        /// <summary>
        /// 聚合根的唯一識別碼
        /// </summary>
        int Id { get; }
    }
}

