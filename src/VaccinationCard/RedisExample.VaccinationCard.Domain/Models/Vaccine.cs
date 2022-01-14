namespace RedisExample.VaccinationCard.Domain.Models
{
    public class Vaccine
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Producer { get; set; } = null!;

        public DateTime Date { get; set; }

        public string Registration { get; set; } = null!;

        public string? ActiveIngredient { get; set; }

        public string Batch { get; set; } = null!;
    }
}
