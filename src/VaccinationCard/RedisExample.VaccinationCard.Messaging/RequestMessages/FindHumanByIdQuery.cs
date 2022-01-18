using System.Runtime.Serialization;

namespace RedisExample.VaccinationCard.Messaging.RequestMessages
{
    [DataContract]
    public class FindHumanByIdQuery
    {
        [DataMember]
        public Guid Id { get; set; }
    }
}
