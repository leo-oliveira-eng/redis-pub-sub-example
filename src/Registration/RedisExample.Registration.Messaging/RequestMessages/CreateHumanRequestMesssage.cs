using Http.Request.Service.Messages;
using RedisExample.Registration.Messaging.Enums;
using System.Runtime.Serialization;

namespace RedisExample.Registration.Messaging.RequestMessages
{
    [DataContract]
    public class CreateHumanRequestMesssage : RequestMessage
    {
        [DataMember]
        public string? CPF { get; set; }

        [DataMember]
        public string? Name { get; set; }

        [DataMember]
        public string? Email { get; set; }

        [DataMember]
        public DateTime? BirthDate { get; set; }

        [DataMember]
        public GenderType Gender { get; set; }

        [DataMember]
        public AddressRequestMessage? Address { get; set; }

        [DataMember]
        public string? PhoneNumber { get; set; }
    }
}
