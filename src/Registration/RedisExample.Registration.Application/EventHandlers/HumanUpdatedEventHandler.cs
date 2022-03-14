using RedisExample.Registration.Application.EventHandlers.Contracts;

namespace RedisExample.Registration.Application.EventHandlers
{
    public class HumanUpdatedEventHandler : HumanEventHandler
    {
        public HumanUpdatedEventHandler(IEventPublisher eventPublisher) : base(eventPublisher) { }
    }
}
