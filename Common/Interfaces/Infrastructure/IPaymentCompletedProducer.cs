using System.Threading.Tasks;

namespace Common.Interfaces.Infrastructure
{
    public interface IPaymentCompletedProducer
    {
        Task SendMessage(object message);
    }
}
