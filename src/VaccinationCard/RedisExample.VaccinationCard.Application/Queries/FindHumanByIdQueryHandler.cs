using Mapster;
using MediatR;
using Messages.Core;
using Messages.Core.Extensions;
using RedisExample.VaccinationCard.Domain.Repositories;
using RedisExample.VaccinationCard.Messaging.RequestMessages;
using RedisExample.VaccinationCard.Messaging.ResponseMessages;

namespace RedisExample.VaccinationCard.Application.Queries
{
    public class FindHumanByIdQueryHandler : IRequest<Response<HumanResponseMessage>>
    {
        IHumanRepository HumanRepository { get; }

        public FindHumanByIdQueryHandler(IHumanRepository humanRepository)
        {
            HumanRepository = humanRepository ?? throw new ArgumentNullException(nameof(humanRepository));
        }

        public async Task<Response<HumanResponseMessage>> Handle(FindHumanByIdQuery request)
        {
            var response = Response<HumanResponseMessage>.Create();

            var human = await HumanRepository.FindAsync(request.Id);

            if (!human.HasValue)
                return response.WithBusinessError("Not found");

            return response.SetValue(human.Value.Adapt<HumanResponseMessage>());
        }
    }
}
