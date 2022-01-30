using BaseEntity.Domain.Events;

namespace RedisExample.Registration.Application.EventHandlers.Contracts
{
    public interface IEventPublisher
    {
        Task Publish(Event? @event);
    }
}
