

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        public Task<ServiceResult<PaymentInfomation>> PayRedirect(PaymentRequestData requestData);

        public Task<ServiceResult<object>> PayReturn();
    }
}
