using RedisExample.VaccinationCard.Domain.Enums;

namespace RedisExample.VaccinationCard.Domain.Models
{
    public class Pet
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public SpeciesType Species { get; set; }

        public string Color { get; set; } = null!;

        public string Breed { get; set; } = null!;

        public List<Vaccine> Vaccines { get; set; } = new List<Vaccine>();
    }
}
