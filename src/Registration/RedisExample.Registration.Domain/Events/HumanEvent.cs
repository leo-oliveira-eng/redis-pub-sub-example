using BaseEntity.Domain.Events;
using RedisExample.Registration.Domain.Enums;
using Valuables.Utils;

namespace RedisExample.Registration.Domain.Events
{
    public class HumanEvent : DomainEvent
    {
        public Guid Id { get; set; }

        public string CPF { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public GenderType Gender { get; set; }

        public Address Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public HumanEvent(Guid id, string cpf, string name, string email, DateTime birthDate, GenderType gender, Address address, string phoneNumber)
            : base(id)
        {
            Id = id;
            CPF = cpf;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Gender = gender;
            Address = address;
            PhoneNumber = phoneNumber;
        }
    }
}
