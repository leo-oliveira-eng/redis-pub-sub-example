using BaseEntity.Domain.Events;
using RedisExample.Registration.Domain.Enums;

namespace RedisExample.Registration.Domain.Events
{
    public class PetEvent : DomainEvent
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public SpeciesType Species { get; set; }

        public string Color { get; set; } = null!;

        public string Breed { get; set; } = null!;

        public List<VaccineEvent> Vaccines { get; set; } = new List<VaccineEvent>();

        public PetEvent(Guid id, string name, DateTime birthDate, SpeciesType species, string color, string breed, List<VaccineEvent> vaccines, Guid humanId)
            : base(humanId)
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
            Species = species;
            Color = color;
            Breed = breed;
            Vaccines = vaccines;
        }
    }
}