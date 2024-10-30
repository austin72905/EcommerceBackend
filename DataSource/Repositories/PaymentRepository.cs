using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace DataSource.Repositories
{
    public class PaymentRepository: IPaymentRepository
    {
        public Payments? GetTenantConfig()
        {
            throw new NotImplementedException();
        }
    }
}
