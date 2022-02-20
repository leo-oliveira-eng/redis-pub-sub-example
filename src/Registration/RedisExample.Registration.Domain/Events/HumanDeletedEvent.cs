using BaseEntity.Domain.Events;

namespace RedisExample.Registration.Domain.Events
{
    public class HumanDeletedEvent : DomainEvent
    {
        public Guid HumanId { get; set; }

        public HumanDeletedEvent(Guid humanId)
            : base(humanId)
        {
            HumanId = humanId;
        }
    }
}
