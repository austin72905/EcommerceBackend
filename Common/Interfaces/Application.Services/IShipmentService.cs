using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Application.Services
{
    public interface IShipmentService
    {
        public Task UpdateShipmentStatus(int status,string recordCode,int orderId);
    }
}
