using BaseEntity.Domain.Events;
using RedisExample.Registration.Domain.Models;

namespace RedisExample.Registration.Domain.Events
{
    public class VaccineEvent : DomainEvent
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Producer { get; set; } = null!;

        public DateTime Date { get; set; }

        public string Registration { get; set; } = null!;

        public string? ActiveIngredient { get; set; }

        public string Batch { get; set; } = null!;

        public VaccineEvent(Vaccine vaccine, Guid petId)
            : base(petId)
        {

        }
    }
}