using RedisExample.Registration.Domain.Models;

namespace RedisExample.Registration.Domain.Events
{
    public class HumanCreatedEvent : HumanEvent
    {
        public HumanCreatedEvent(Human human)
            : base(human!.Code, human!.CPF.Text, human!.Name, human!.Email.Address, human!.BirthDate, human!.Gender, human!.Address, human!.PhoneNumber) { }
    }
}
