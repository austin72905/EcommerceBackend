

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        public ServiceResult<PaymentInfomation> PayRedirect(PaymentRequestData requestData);

        public ServiceResult<object> PayReturn();
    }
}
