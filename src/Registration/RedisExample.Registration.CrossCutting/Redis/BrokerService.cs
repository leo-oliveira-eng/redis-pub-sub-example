using RedisExample.Registration.Common.Settings.Contracts;
using RedisExample.Registration.CrossCutting.Redis.Contracts;
using StackExchange.Redis;

namespace RedisExample.Registration.CrossCutting.Redis
{
    public class BrokerService : IBrokerService
    {
        ISettings Settings { get; }

        ISubscriber Publisher { get; }

        ConnectionMultiplexer Connection { get; }

        public BrokerService(ISettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            Connection = CreateRedisConnection();
            Publisher = CreatePublisher();
        }

        public async Task PublishAsync(string topic, string message)
            => await Publisher.PublishAsync(topic, message);

        private ISubscriber CreatePublisher()
            => Connection.GetSubscriber();

        private ConnectionMultiplexer CreateRedisConnection()
        {
            ConfigurationOptions options = new();

            options.AbortOnConnectFail = true;
            options.EndPoints.Add(Settings.RedisSettings.Endpoint, Settings.RedisSettings.Port);
            options.Password = Settings.RedisSettings.Password;
            options.AllowAdmin = true;
            options.KeepAlive = 30;
            options.ConnectTimeout = 15000;
            options.SyncTimeout = 15000;

            return ConnectionMultiplexer.Connect(options);
        }

    }
}
