using RedisExample.VaccinationCard.Domain.Core.Events;

namespace RedisExample.VaccinationCard.Domain.Models
{
    public abstract class Entity
    {
        #region Fields

        private readonly List<DomainEvent> _domainEvents = new();

        #endregion

        #region Properties

        public Guid Id { get; set; }

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        #endregion

        #region Methods

        public void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);

        public void RemoveDomainEvent(DomainEvent domainEvent) => _domainEvents.Remove(domainEvent);

        public void ClearDomainEvents() => _domainEvents?.Clear();

        #endregion
    }
}
