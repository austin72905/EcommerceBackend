using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Infrastructure
{
    public interface IOrderTimeoutConsumer
    {
        /// <summary>
        /// 開始監聽訂單超時訊息
        /// </summary>
        /// <returns></returns>
        Task StartListening();
    }
}