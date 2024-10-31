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
                .Include(p => p.Order)
                .Where(p => p.Order.RecordCode == recordCode)
                .FirstOrDefaultAsync();
        }
    }
}
