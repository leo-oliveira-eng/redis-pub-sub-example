using RedisExample.VaccinationCard.Domain.Core.Events.Contracts;

namespace RedisExample.VaccinationCard.Domain.Core.Events
{
    public abstract class DomainEvent : Event, IDomainEvent
    {
        public bool IsPublished { get; set; }

        public Guid AggregateId { get; set; }

        protected DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
