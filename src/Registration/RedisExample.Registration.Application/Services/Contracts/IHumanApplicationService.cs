using Messages.Core;
using RedisExample.Registration.Messaging.RequestMessages;
using RedisExample.Registration.Messaging.ResponseMessages;

namespace RedisExample.Registration.Application.Services.Contracts
{
    public interface IHumanApplicationService
    {
        Task<Response<HumanResponseMessage>> CreateAsync(CreateHumanRequestMesssage requestMessage);

        Task<Response<HumanResponseMessage>> CreatePetAsync(CreatePetRequestMessage? requestMessage, Guid humanId);

        Task<Response<HumanResponseMessage>> AddVaccineAsync(Guid humanId, Guid petId, VaccineRequestMessage? requestMessage);

        Task<Response<HumanDeletedResponseMessage>> DeleteAsync(Guid humanId);
    }
}
