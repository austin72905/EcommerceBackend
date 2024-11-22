using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IShipmentRepository
    {
        public Task<IEnumerable<Shipment>> GetShipmentsByOrderId(int orderId);

        public  Task AddAsync(Shipment shipment);

        public Task SaveChangesAsync();
    }
}
