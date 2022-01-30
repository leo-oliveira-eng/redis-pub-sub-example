using BaseEntity.Domain.Events;
using Newtonsoft.Json;
using RedisExample.Registration.Application.EventHandlers.Contracts;
using RedisExample.Registration.Common.Settings.Contracts;
using RedisExample.Registration.CrossCutting.Redis.Contracts;

namespace RedisExample.Registration.CrossCutting.Redis
{
    public class EventPublisher : IEventPublisher
    {
        IBrokerService BrokerService { get; }

        ISettings Settings { get; }

        public EventPublisher(IBrokerService brokerService, ISettings settings)
        {
            BrokerService = brokerService ?? throw new ArgumentNullException(nameof(brokerService));
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task Publish(Event? @event)
        {
            ArgumentNullException.ThrowIfNull(@event);

            var topic = Settings.RedisSettings.Topics[@event.GetType().Name];

            var message = JsonConvert.SerializeObject(@event);

            await BrokerService.PublishAsync(topic, message);
        }
    }
}
