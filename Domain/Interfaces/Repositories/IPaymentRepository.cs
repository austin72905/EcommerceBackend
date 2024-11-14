using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        public Task<Payment?> GetTenantConfig(string recordCode);

        public  Task GeneratePaymentRecord(Payment payment);

        public Task<Payment?> GetPaymentRecord(string recordCode);

        public Task SaveChangesAsync();
    }
}
