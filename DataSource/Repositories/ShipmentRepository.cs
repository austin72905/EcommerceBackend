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
        public ShipmentRepository(EcommerceDBContext context, EcommerceReadOnlyDBContext? readContext = null) 
            : base(context, readContext)
        {
        }

        public async Task<IEnumerable<Shipment>> GetShipmentsByOrderId(int orderId)
        {
            // 讀取操作使用讀取 DbContext（從庫）
            return await ReadContext.Shipments
                .AsNoTracking()
                .Where(sp=>sp.OrderId== orderId)
                .ToListAsync();
                   
        }
       
    }
}
