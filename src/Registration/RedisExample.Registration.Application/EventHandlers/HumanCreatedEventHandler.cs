using RedisExample.Registration.Application.EventHandlers.Contracts;

namespace RedisExample.Registration.Application.EventHandlers
{
    public class HumanCreatedEventHandler : HumanEventHandler
    {
        public HumanCreatedEventHandler(IEventPublisher eventPublisher) : base(eventPublisher) { }
    }
}
