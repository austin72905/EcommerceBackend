namespace Common.Interfaces.Infrastructure
{
    public interface IOrderStatusChangedConsumer
    {
        Task StartListening();
    }
}

