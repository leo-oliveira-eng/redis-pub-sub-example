using BaseEntity.Domain.Entities;
using Messages.Core;
using RedisExample.Registration.Domain.Enums;
using Valuables.Utils;

namespace RedisExample.Registration.Domain.Models
{
    public class Human : Entity
    {
        #region Properties

        public CPF CPF { get; private set; } = null!;

        public string Name { get; private set; } = null!;

        public Email Email { get; private set; } = null!;

        public DateTime BirthDate { get; private set; }

        public GenderType Gender { get; private set; }

        public Address Address { get; private set; } = null!;

        public string PhoneNumber { get; private set; } = null!;

        public List<Pet> Pets { get; private set; } = new List<Pet>();

        #endregion

        #region Constructors

        [Obsolete(ConstructorObsoleteMessage, true)]
        private Human() : base(Guid.NewGuid()) { }

        internal Human(CPF cpf, string name, Email email, DateTime birthDate, GenderType gender, Address address, string phoneNumber)
            : base(Guid.NewGuid())
        {
            CPF = cpf;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Gender = gender;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        internal void AddPet(Pet pet)
            => Pets.Add(pet);

        #endregion

        #region Conversion Operators

        public static implicit operator Human(Maybe<Human> entity) => entity.Value;

        public static implicit operator Human(Response<Human> entity) => entity.Data;

        #endregion
    }
}
