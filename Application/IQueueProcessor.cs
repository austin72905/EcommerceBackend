using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IQueueProcessor
    {
        /// <summary>
        /// 將要處理的物流狀態排入共用訊息佇列（非本機記憶體）
        /// </summary>
        /// <param name="queue">預計依序送出的物流狀態</param>
        /// <param name="recordCode">訂單編號，用於下游識別</param>
        /// <returns></returns>
        Task AddQueueAsync(Queue<Shipment> queue, string recordCode);
    }
}
