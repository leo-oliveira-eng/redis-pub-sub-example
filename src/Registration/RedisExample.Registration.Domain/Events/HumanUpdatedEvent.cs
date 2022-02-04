using BaseEntity.Domain.Events;
using RedisExample.Registration.Domain.Models;

namespace RedisExample.Registration.Domain.Events
{
    public class HumanUpdatedEvent : DomainEvent
    {
        public Human Human { get; private set; }

        public HumanUpdatedEvent(Human human)
            : base(human.Code)
        {
            Human = human;
        }
    }
}
