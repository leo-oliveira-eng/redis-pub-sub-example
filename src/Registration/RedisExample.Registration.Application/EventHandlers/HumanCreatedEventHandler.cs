using MediatR;
using RedisExample.Registration.Domain.Events;

namespace RedisExample.Registration.Application.EventHandlers
{
    public class HumanCreatedEventHandler : INotificationHandler<HumanCreatedEvent>
    {
        public Task Handle(HumanCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
