using Messages.Core;
using RedisExample.Registration.Messaging.RequestMessages;
using RedisExample.Registration.Messaging.ResponseMessages;

namespace RedisExample.Registration.Application.Services.Contracts
{
    public interface IHumanApplicationService
    {
        Task<Response<HumanResponseMessage>> CreateAsync(CreateHumanRequestMesssage requestMessage);
    }
}
