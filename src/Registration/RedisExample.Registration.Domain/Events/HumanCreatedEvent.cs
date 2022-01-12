using BaseEntity.Domain.Events;
using RedisExample.Registration.Domain.Models;

namespace RedisExample.Registration.Domain.Events
{
    public class HumanCreatedEvent : DomainEvent
    {
        public Human Human { get; set; }

        public HumanCreatedEvent(Human human)
            : base(human.Code)
        {
            Human = human;
        }
    }
}
