using EcommerceBackend.Interfaces.Repositorys;
using EcommerceBackend.Models.DTOs;
using EcommerceBackend.Utils;

namespace EcommerceBackend.Repositorys
{
    public class PaymentRepository : IPaymentRepository
    {
        public TenantConfigDTO GetTenantConfig()
        {
            return new TenantConfigDTO()
            {
                MerchantId = "3002607",
                SecretKey = "pwFHCqoQZGmho4w6",
                HashIV = "EkRm7iFT261dpevs",
                RecordNo = $"RK{Tools.TimeStamp()}",
                Amount="100"
            };
        }
    }
}
