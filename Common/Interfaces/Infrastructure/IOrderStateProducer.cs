using System.Threading.Tasks;

namespace Common.Interfaces.Infrastructure
{
    public interface IOrderStateProducer
    {
        public Task SendMessage(object message);
    }
}

