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
    public class AddVaccineCommandHandler : IRequestHandler<AddVaccineCommand, Response<Human>>
    {
        IHumanRepository HumanRepository { get; }

        IUnitOfWork UnitOfWork { get; }

        public AddVaccineCommandHandler(IHumanRepository humanRepository, IUnitOfWork unitOfWork)
        {
            HumanRepository = humanRepository ?? throw new ArgumentNullException(nameof(humanRepository));
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Response<Human>> Handle(AddVaccineCommand command, CancellationToken token = default)
        {
            var response = Response<Human>.Create();

            var validateCommandResponse = command.Validate();

            if (validateCommandResponse.HasError)
                return response.WithMessages(validateCommandResponse.Messages);

            var human = await HumanRepository.FindAsync(command.HumanId);

            if (!human.HasValue)
                return response.WithBusinessError($"Human ID {command.HumanId} not found");

            var targetPet = human.Value.Pets.SingleOrDefault(pet => pet.Code.Equals(command.PetId));

            if (targetPet is null)
                return response.WithBusinessError($"Pet ID {command.PetId} not found");

            var vaccine = new Vaccine(command.Name!, command.Producer!, command.Date, command.Registration!, command.ActiveIngredient, command.Batch!, targetPet);

            targetPet.AddVaccine(vaccine);

            human.Value.AddDomainEvent(new HumanUpdatedEvent(human));

            await HumanRepository.UpdateAsync(human);

            if (!await UnitOfWork.CommitAsync())
                return response.WithCriticalError("Failed to add vaccine");

            return response.SetValue(human);
        }
    }
}
