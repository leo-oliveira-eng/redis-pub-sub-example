using BaseEntity.Domain.Entities;
using Messages.Core;
using RedisExample.Registration.Domain.Enums;

namespace RedisExample.Registration.Domain.Models
{
    public class Pet : Entity
    {
        #region Properties

        public string Name { get; private set; } = null!;

        public DateTime BirthDate { get; private set; }

        public SpeciesType Species { get; private set; }

        public string Color { get; private set; } = null!;

        public string Breed { get; private set; } = null!;

        public List<Vaccine> Vaccines { get; private set; } = new List<Vaccine>();

        public long HumanId { get; private set; }

        public Human Human { get; private set; } = null!;

        #endregion

        #region Constructors

        [Obsolete(ConstructorObsoleteMessage, true)]
        private Pet() : base(Guid.NewGuid()) { }

        public Pet(string name, DateTime birthDate, SpeciesType species, string color, string breed, Human human)
            : base(Guid.NewGuid())
        {
            Name = name;
            BirthDate = birthDate;
            Species = species;
            Color = color;
            Breed = breed;
            HumanId = human.Id;
            Human = human;
        }

        #endregion

        #region Methods

        internal void AddVaccine(Vaccine vaccine)
            => Vaccines.Add(vaccine);

        #endregion

        #region Conversion Operators

        public static implicit operator Pet(Maybe<Pet> entity) => entity.Value;

        public static implicit operator Pet(Response<Pet> entity) => entity.Data;

        #endregion
    }
}
