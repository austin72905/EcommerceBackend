using EcommerceBackend.Models;

namespace EcommerceBackend.Interfaces.Services
{
    public interface IPaymentService
    {
        public ServiceResult<PaymentInfomation> PayRedirect(PaymentRequestData requestData);

        public ServiceResult<object> PayReturn();
    }
}
