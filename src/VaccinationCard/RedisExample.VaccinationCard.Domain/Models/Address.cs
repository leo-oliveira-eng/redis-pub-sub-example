namespace RedisExample.VaccinationCard.Domain.Models
{
    public class Address
    {
        public string Cep { get; set; } = null!;

        public string Street { get; set; } = null!;

        public string? Complement { get; set; }

        public string Neighborhood { get; set; } = null!;

        public string Number { get; set; } = null!;

        public string City { get; set; } = null!;

        public string UF { get; set; } = null!;
    }
}
