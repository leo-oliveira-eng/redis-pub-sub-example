using System.Runtime.Serialization;

namespace RedisExample.VaccinationCard.Messaging.ResponseMessages
{
    [DataContract]
    public class PetResponseMessage
    {
        [DataMember]
        public Guid Id { get; set; }
    }
}
