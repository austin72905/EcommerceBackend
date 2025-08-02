using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Infrastructure
{
    /// <summary>
    /// 訊息處理器基礎介面
    /// </summary>
    /// <typeparam name="TMessage">訊息類型</typeparam>
    public interface IMessageHandler<TMessage> where TMessage : class
    {
        Task HandleAsync(TMessage message);
    }

    /// <summary>
    /// 訊息消費者基礎介面
    /// </summary>
    public interface IMessageConsumer
    {
        Task StartListening();
        Task StopListening();
    }

    /// <summary>
    /// 訊息生產者基礎介面
    /// </summary>
    /// <typeparam name="TMessage">訊息類型</typeparam>
    public interface IMessageProducer<TMessage> where TMessage : class
    {
        Task SendAsync(TMessage message);
        Task SendDelayedAsync(TMessage message, TimeSpan delay);
    }
}