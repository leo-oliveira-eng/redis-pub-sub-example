using BaseEntity.Domain.Events;
using MediatR;
using RedisExample.Registration.Application.EventHandlers.Contracts;

namespace RedisExample.Registration.Application.EventHandlers
{
    public abstract class HumanEventHandler : INotificationHandler<DomainEvent>
    {
        protected IEventPublisher EventPublisher { get; }

        protected HumanEventHandler(IEventPublisher eventPublisher)
        {
            EventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
        }

        public virtual Task Handle(DomainEvent @event, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(@event);

            return EventPublisher.PublishAsync(@event);
        }
    }    
}
