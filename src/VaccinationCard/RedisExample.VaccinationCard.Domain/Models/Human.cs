using RedisExample.VaccinationCard.Domain.Enums;
using Valuables.Utils;

namespace RedisExample.VaccinationCard.Domain.Models
{
    public class Human
    {
        public Guid Id { get; set; }

        public string CPF { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public GenderType Gender { get; set; }

        public Address Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public List<Pet> Pets { get; set; } = new List<Pet>();
    }
}
