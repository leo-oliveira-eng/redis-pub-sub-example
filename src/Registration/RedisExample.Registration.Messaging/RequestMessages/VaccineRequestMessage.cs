using Http.Request.Service.Messages;
using System.Runtime.Serialization;

namespace RedisExample.Registration.Messaging.RequestMessages
{
    [DataContract]
    public class VaccineRequestMessage : RequestMessage
    {
        [DataMember]
        public string? Name { get; set; }

        [DataMember]
        public string? Producer { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string? Registration { get; set; }

        [DataMember]
        public string? ActiveIngredient { get; set; }

        [DataMember]
        public string? Batch { get; set; }
    }
}
