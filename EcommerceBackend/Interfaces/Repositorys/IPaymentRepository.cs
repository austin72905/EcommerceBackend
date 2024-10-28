using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Interfaces.Repositorys
{
    public interface IPaymentRepository
    {
        public TenantConfigDTO GetTenantConfig();
    }
}
