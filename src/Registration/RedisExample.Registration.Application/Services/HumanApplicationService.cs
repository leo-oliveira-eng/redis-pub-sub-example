using BaseEntity.Domain.Mediator.Contracts;
using MapsterMapper;
using Messages.Core;
using Messages.Core.Extensions;
using RedisExample.Registration.Application.Services.Contracts;
using RedisExample.Registration.Domain.Commands;
using RedisExample.Registration.Domain.Models;
using RedisExample.Registration.Messaging.RequestMessages;
using RedisExample.Registration.Messaging.ResponseMessages;

namespace RedisExample.Registration.Application.Services
{
    public class HumanApplicationService : IHumanApplicationService
    {
        IMapper Mapper { get; }

        IMediatorHandler Mediator { get; }

        public HumanApplicationService(IMediatorHandler mediatorHandler, IMapper mapper)
        {
            Mediator = mediatorHandler ?? throw new ArgumentNullException(nameof(mediatorHandler));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<HumanResponseMessage>> CreateAsync(CreateHumanRequestMesssage? requestMessage)
        {
            var response = Response<HumanResponseMessage>.Create();

            if (requestMessage is null)
                return response.WithBusinessError("Request data is invalid");

            var createHumanResponse = await Mediator.SendCommand<CreateHumanCommand, Response<Human>>(Mapper.Map<CreateHumanCommand>(requestMessage));

            if (createHumanResponse.HasError)
                return response.WithMessages(createHumanResponse.Messages);

            return response.SetValue(Mapper.Map<HumanResponseMessage>(createHumanResponse));
        }

        public async Task<Response<HumanResponseMessage>> CreatePetAsync(CreatePetRequestMessage? requestMessage, Guid humanId)
        {
            var response = Response<HumanResponseMessage>.Create();

            if (requestMessage is null)
                return response.WithBusinessError("Request data is invalid");

            var createPetResponse = await Mediator.SendCommand<CreatePetCommand, Response<Human>>(Mapper.Map<CreatePetCommand>((requestMessage, humanId)));

            if (createPetResponse.HasError)
                return response.WithMessages(createPetResponse.Messages);

            return response.SetValue(Mapper.Map<HumanResponseMessage>(createPetResponse));
        }
    }
}
