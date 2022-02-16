using Http.Request.Service.Messages;
using System.Runtime.Serialization;

namespace RedisExample.Registration.Messaging.ResponseMessages
{
    [DataContract]
    public class VaccineResponseMessage : ResponseMessage
    {
        [DataMember]
        public Guid Code { get; set; }

        public string Name { get; set; } = null!;

        public string Producer { get; set; } = null!;

        public DateTime Date { get; set; }

        public string Registration { get; set; } = null!;

        public string? ActiveIngredient { get; set; }

        public string Batch { get; set; } = null!;
    }
}
