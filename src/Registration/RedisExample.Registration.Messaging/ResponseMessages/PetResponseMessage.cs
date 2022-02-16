using Http.Request.Service.Messages;
using RedisExample.Registration.Messaging.Enums;
using System.Runtime.Serialization;

namespace RedisExample.Registration.Messaging.ResponseMessages
{
    [DataContract]
    public class PetResponseMessage : ResponseMessage
    {
        [DataMember]
        public Guid Code { get; set; }

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

        [DataMember]
        public List<VaccineResponseMessage> Vaccines { get; set; } = new();
    }
}
