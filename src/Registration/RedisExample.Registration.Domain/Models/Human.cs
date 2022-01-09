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

        public List<Pet> Pets { get; set; } = new List<Pet>();

        #endregion

        #region Constructors

        [Obsolete(ConstructorObsoleteMessage, true)]
        private Human() : base(Guid.NewGuid()) { }

        public Human(CPF cPF, string name, Email email, DateTime birthDate, GenderType gender, Address address, List<Pet> pets)
            : base(Guid.NewGuid())
        {
            CPF = cPF;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Gender = gender;
            Address = address;
            Pets = pets;
        }

        #endregion

        #region Conversion Operators

        public static implicit operator Human(Maybe<Human> entity) => entity.Value;

        public static implicit operator Human(Response<Human> entity) => entity.Data;

        #endregion
    }
}
