using RedisExample.VaccinationCard.Domain.Enums;

namespace RedisExample.VaccinationCard.Domain.Commands
{
    public class CreateHumanCommand : HumanCommand
    {
        public string Cpf { get; set; } = null!;

        public string? Name { get; set; }

        public DateTime BirthDate { get; set; }

        public GenderType Gender { get; set; }
    }
}
