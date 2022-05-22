using Newtonsoft.Json;
using RedisExample.VaccinationCard.Domain.Enums;
using RedisExample.VaccinationCard.Domain.Models;

namespace RedisExample.VaccinationCard.Domain.Events
{
    public class HumanCreatedEvent : HumanEvent
    {
        [JsonConstructor]
        public HumanCreatedEvent(Guid id, string cpf, string name, string email, DateTime birthDate, GenderType gender, Address address, string phoneNumber)
            : base(id, cpf, name, email, birthDate, gender, address, phoneNumber) { }

        public HumanCreatedEvent(Human human)
            : base(human.Id, human.CPF, human.Name, human.Email, human.BirthDate, human.Gender, human.Address, human.PhoneNumber) { }
    }
}
