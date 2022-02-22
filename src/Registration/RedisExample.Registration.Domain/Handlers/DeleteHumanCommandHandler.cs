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
    public class DeleteHumanCommandHandler : IRequestHandler<DeleteHumanCommand, Response>
    {
        IHumanRepository HumanRepository { get; }

        IUnitOfWork UnitOfWork { get; }

        public DeleteHumanCommandHandler(IHumanRepository humanRepository, IUnitOfWork unitOfWork)
        {
            HumanRepository = humanRepository ?? throw new ArgumentNullException(nameof(humanRepository));
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Response> Handle(DeleteHumanCommand command, CancellationToken cancellationToken = default)
        {
            var response = Response.Create();

            var human = await HumanRepository.FindAsync(command.HumanId);

            if (!human.HasValue)
                return response.WithBusinessError("Human not found");

            human.Value!.AddDomainEvent(new HumanDeletedEvent(human.Value.Code));

            await HumanRepository.Delete(human!);

            if (!await UnitOfWork.CommitAsync())
                return response.WithCriticalError($"Failed to delete human id {command.HumanId}");

            return response;
        }
    }
}
