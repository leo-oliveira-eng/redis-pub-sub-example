using MediatR;
using RedisExample.Registration.Application.EventHandlers.Contracts;
using RedisExample.Registration.Domain.Events;

namespace RedisExample.Registration.Application.EventHandlers
{
    public class HumanDeletedEventHandler : INotificationHandler<HumanDeletedEvent>
    {
        private IEventPublisher EventPublisher { get; }

        public HumanDeletedEventHandler(IEventPublisher eventPublisher)
        {
            EventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
        }

        public virtual Task Handle(HumanDeletedEvent @event, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(@event);

            return EventPublisher.PublishAsync(@event);
        }
    }
}
