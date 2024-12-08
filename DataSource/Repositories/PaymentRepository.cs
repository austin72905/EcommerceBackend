using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataSource.Repositories
{
    public class PaymentRepository: Repository<Payment>,IPaymentRepository
    {
        public PaymentRepository(EcommerceDBContext context) : base(context)
        {
        }

        public  async Task<Payment?> GetTenantConfig(string recordCode)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Order)
                .Include(p=>p.TenantConfig)
                .Where(p => p.Order.RecordCode == recordCode)
                .FirstOrDefaultAsync();
        }

        public async Task GeneratePaymentRecord(Payment payment)
        {
            await _dbSet.AddAsync(payment);
            await SaveChangesAsync();
        }

        public async Task<Payment?> GetPaymentRecord(string recordCode)
        {
            return await _dbSet
                .Include(p => p.Order)
                    .ThenInclude(p => p.OrderSteps)
                .Include(p => p.Order)
                    .ThenInclude(p => p.Shipments)
                .Where(p => p.Order.RecordCode == recordCode)
                .FirstOrDefaultAsync();
        }
    }
}
