using BaseEntity.Domain.Mediator.Contracts;
using Messages.Core;
using Messages.Core.Extensions;
using RedisExample.Registration.Application.Services.Contracts;
using RedisExample.Registration.Application.Services.Mappers;
using RedisExample.Registration.Domain.Commands;
using RedisExample.Registration.Domain.Models;
using RedisExample.Registration.Messaging.RequestMessages;
using RedisExample.Registration.Messaging.ResponseMessages;

namespace RedisExample.Registration.Application.Services
{
    public class HumanApplicationService : IHumanApplicationService
    {
        IMediatorHandler Mediator { get; }

        public HumanApplicationService(IMediatorHandler mediatorHandler)
        {
            Mediator = mediatorHandler ?? throw new ArgumentNullException(nameof(mediatorHandler));
        }

        public async Task<Response<HumanResponseMessage>> CreateAsync(CreateHumanRequestMesssage? requestMessage)
        {
            var response = Response<HumanResponseMessage>.Create();

            if (requestMessage is null)
                return response.WithBusinessError("Request data is invalid");

            var createHumanResponse = await Mediator.SendCommand<CreateHumanCommand, Response<Human>>(requestMessage.ToCreateHumanCommand());

            if (createHumanResponse.HasError)
                return response.WithMessages(createHumanResponse.Messages);

            return response.SetValue(createHumanResponse.Data.Value.ToHumanResponseMessage());
        }
    }
}
