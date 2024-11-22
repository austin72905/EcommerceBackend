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
        public void AddQueue(Queue<Shipment> queue);
    }
}
