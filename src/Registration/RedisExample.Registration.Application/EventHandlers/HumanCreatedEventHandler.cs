using MediatR;
using RedisExample.Registration.Application.EventHandlers.Contracts;
using RedisExample.Registration.Domain.Events;

namespace RedisExample.Registration.Application.EventHandlers
{
    public class HumanCreatedEventHandler : INotificationHandler<HumanCreatedEvent>
    {
        IEventPublisher EventPublisher { get; }

        public HumanCreatedEventHandler(IEventPublisher eventPublisher)
        {
            EventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
        }

        public Task Handle(HumanCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(@event);

            return EventPublisher.Publish(@event);
        }
    }
}
