using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Repositories
{
    public class ShipmentRepository : Repository<Shipment>, IShipmentRepository
    {
        public ShipmentRepository(EcommerceDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Shipment>> GetShipmentsByOrderId(int orderId)
        {
            return await _dbSet
                .Where(sp=>sp.OrderId== orderId)
                .ToListAsync();
                   
        }
       
    }
}
