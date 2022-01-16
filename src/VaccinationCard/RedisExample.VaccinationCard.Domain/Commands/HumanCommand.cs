using MediatR;
using Messages.Core;
using RedisExample.Registration.Domain.Models;
using RedisExample.VaccinationCard.Domain.Core.Commands;
using RedisExample.VaccinationCard.Domain.Models;

namespace RedisExample.VaccinationCard.Domain.Commands
{
    public class HumanCommand : Command, IRequest<Response<Human>>
    {
        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public Address Address { get; set; } = null!;
    }
}
