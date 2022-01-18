using MediatR;
using Messages.Core;
using RedisExample.VaccinationCard.Messaging.ResponseMessages;
using System.Runtime.Serialization;

namespace RedisExample.VaccinationCard.Messaging.RequestMessages
{
    [DataContract]
    public class FindHumanByIdQuery : IRequest<Response<HumanResponseMessage>>
    {
        [DataMember]
        public Guid Id { get; set; }
    }
}
