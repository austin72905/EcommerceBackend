using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Infrastructure
{
    public interface IShipmentProducer
    {
        public Task SendMessage(object message);
    }
}
