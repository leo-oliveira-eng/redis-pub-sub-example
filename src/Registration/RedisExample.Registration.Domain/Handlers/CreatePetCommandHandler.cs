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
    public class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, Response<Human>>
    {
        IHumanRepository HumanRepository { get; }

        IUnitOfWork UnitOfWork { get; }

        public CreatePetCommandHandler(IHumanRepository humanRepository, IUnitOfWork unitOfWork)
        {
            HumanRepository = humanRepository ?? throw new ArgumentNullException(nameof(humanRepository));
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Response<Human>> Handle(CreatePetCommand command, CancellationToken cancellationToken = default)
        {
            var response = Response<Human>.Create();

            var validateCommandResponse = command.Validate();

            if (validateCommandResponse.HasError)
                return response.WithMessages(validateCommandResponse.Messages);

            var human = await HumanRepository.FindAsync(command.HumanId);

            if (!human.HasValue)
                return response.WithBusinessError($"No human was found with id {command.HumanId}");

            var pet = new Pet(command.Name, command.BirthDate, command.Species, command.Color, command.Breed, human);

            human.Value.AddPet(pet);

            human.Value.AddDomainEvent(new HumanUpdatedEvent(human));

            await HumanRepository.UpdateAsync(human);

            if (!await UnitOfWork.CommitAsync())
                return response.WithCriticalError("Failed to add new pet");

            return response.SetValue(human);
        }
    }
}
