using MediatR;
using RedisExample.VaccinationCard.Domain.Models;
using RedisExample.VaccinationCard.Domain.Core.Commands;

namespace RedisExample.VaccinationCard.Domain.Commands
{
    public class HumanCommand : Command, IRequest<Unit>
    {
        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public Address Address { get; set; } = null!;
    }
}
