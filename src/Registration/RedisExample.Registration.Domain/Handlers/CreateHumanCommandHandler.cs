using BaseEntity.Domain.UnitOfWork;
using MediatR;
using Messages.Core;
using Messages.Core.Extensions;
using RedisExample.Registration.Domain.Commands;
using RedisExample.Registration.Domain.Events;
using RedisExample.Registration.Domain.Models;
using RedisExample.Registration.Domain.Repositories;

namespace RedisExample.Registration.Domain.Handlers
{
    public class CreateHumanCommandHandler : IRequestHandler<CreateHumanCommand, Response<Human>>
    {
        IHumanRepository HumanRepository { get; }

        IUnitOfWork UnitOfWork { get; }

        public CreateHumanCommandHandler(IHumanRepository humanRepository, IUnitOfWork unitOfWork)
        {
            HumanRepository = humanRepository ?? throw new ArgumentNullException(nameof(humanRepository));
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Response<Human>> Handle(CreateHumanCommand command, CancellationToken cancellationToken = default)
        {
            var response = Response<Human>.Create();

            var validateCommandResponse = command.Validate();

            if (validateCommandResponse.HasError)
                return response.WithMessages(validateCommandResponse.Messages);

            var human = new Human(command.Cpf, command.Name!, command.Email, command.BirthDate, command.Gender, command.Address, command.PhoneNumber!);

            human.AddDomainEvent(new HumanCreatedEvent(human));

            await HumanRepository.AddAsync(human);

            if (!await UnitOfWork.CommitAsync())
                return response.WithCriticalError("Failed to save human");

            return response.SetValue(human);
        }
    }
}
