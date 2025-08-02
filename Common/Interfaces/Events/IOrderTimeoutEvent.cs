using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Events
{
    /// <summary>
    /// 訂單超時事件
    /// </summary>
    public interface IOrderTimeoutEvent
    {
        int UserId { get; }
        string RecordCode { get; }
        DateTime CreatedAt { get; }
    }

    /// <summary>
    /// 事件處理器基礎介面
    /// </summary>
    /// <typeparam name="TEvent">事件類型</typeparam>
    public interface IEventHandler<TEvent> where TEvent : class
    {
        Task HandleAsync(TEvent eventData);
    }

    /// <summary>
    /// 訂單超時事件處理器
    /// </summary>
    public interface IOrderTimeoutEventHandler : IEventHandler<IOrderTimeoutEvent>
    {
    }
}