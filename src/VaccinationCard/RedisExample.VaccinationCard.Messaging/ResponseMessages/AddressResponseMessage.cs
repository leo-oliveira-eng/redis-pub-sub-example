using System.Runtime.Serialization;

namespace RedisExample.VaccinationCard.Messaging.ResponseMessages
{
    [DataContract]
    public class AddressResponseMessage
    {
        [DataMember]
        public string? Cep { get; set; }

        [DataMember]
        public string? Street { get; set; }

        [DataMember]
        public string? Complement { get; set; }

        [DataMember]
        public string? Neighborhood { get; set; }

        [DataMember]
        public string? Number { get; set; }

        [DataMember]
        public string? City { get; set; }

        [DataMember]
        public string? UF { get; set; }
    }
}
