using BaseEntity.Domain.Entities;
using Messages.Core;
using RedisExample.Registration.Domain.Humans.Enums;
using Valuables.Utils;

namespace RedisExample.Registration.Domain.Humans.Models
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

        #endregion

        #region Constructors

        [Obsolete(ConstructorObsoleteMessage, true)]
        private Human() : base(Guid.NewGuid()) { }

        public Human(CPF cPF, string name, Email email, DateTime birthDate, GenderType gender, Address address)
            : base(Guid.NewGuid())
        {
            CPF = cPF;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Gender = gender;
            Address = address;
        }

        #endregion

        #region Conversion Operators

        public static implicit operator Human(Maybe<Human> entity) => entity.Value;

        public static implicit operator Human(Response<Human> entity) => entity.Data;

        #endregion
    }
}
