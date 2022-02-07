using Http.Request.Service.Messages;
using RedisExample.Registration.Messaging.Enums;
using System.Runtime.Serialization;

namespace RedisExample.Registration.Messaging.RequestMessages
{
    [DataContract]
    public class CreatePetRequestMessage : RequestMessage
    {
        [DataMember]
        public Guid HumanId { get; set; }

        [DataMember]
        public string Name { get; set; } = null!;

        [DataMember]
        public DateTime BirthDate { get; set; }

        [DataMember]
        public SpeciesType Species { get; set; }

        [DataMember]
        public string Color { get; set; } = null!;

        [DataMember]
        public string Breed { get; set; } = null!;
    }
}
