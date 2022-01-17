using Mapster;
using MediatR;
using RedisExample.VaccinationCard.Common.Exceptions;
using RedisExample.VaccinationCard.Domain.Commands;
using RedisExample.VaccinationCard.Domain.Events;
using RedisExample.VaccinationCard.Domain.Models;
using RedisExample.VaccinationCard.Domain.Repositories;

namespace RedisExample.VaccinationCard.Domain.Handlers
{
    public class CreateHumanCommandHandler : IRequestHandler<CreateHumanCommand, Unit>
    {
        IHumanRepository HumanRepository { get; }

        public CreateHumanCommandHandler(IHumanRepository humanRepository)
        {
            HumanRepository = humanRepository ?? throw new ArgumentNullException(nameof(humanRepository));
        }

        public async Task<Unit> Handle(CreateHumanCommand? request, CancellationToken cancellationToken = default)
        {
            if (request is null)
                throw new InvalidCommandException(nameof(CreateHumanCommand), $"{nameof(request)} is invalid");

            var human = request.Adapt<Human>();

            human.AddDomainEvent(new HumanCreatedEvent(human));

            await HumanRepository.AddAsync(human);

            return Unit.Value;
        }
    }
}
