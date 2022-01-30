namespace RedisExample.Registration.CrossCutting.Redis.Contracts
{
    public interface IBrokerService
    {
        Task PublishAsync(string topic, string message);
    }
}
