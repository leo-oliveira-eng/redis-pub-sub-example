using RedisExample.VaccinationCard.Messaging.Enums;
using System.Runtime.Serialization;

namespace RedisExample.VaccinationCard.Messaging.ResponseMessages
{
    [DataContract]
    public class HumanResponseMessage
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string CPF { get; set; } = null!;

        [DataMember]
        public string Name { get; set; } = null!;

        [DataMember]
        public string Email { get; set; } = null!;

        [DataMember]
        public DateTime BirthDate { get; set; }

        [DataMember]
        public GenderType Gender { get; set; }

        [DataMember]
        public AddressResponseMessage Address { get; set; } = null!;

        [DataMember]
        public string PhoneNumber { get; set; } = null!;

        [DataMember]
        public List<PetResponseMessage> Pets { get; set; } = new List<PetResponseMessage>();
    }
}
