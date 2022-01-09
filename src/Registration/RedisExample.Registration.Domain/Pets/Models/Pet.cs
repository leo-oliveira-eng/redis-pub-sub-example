using BaseEntity.Domain.Entities;
using Messages.Core;
using RedisExample.Registration.Domain.Humans.Models;
using RedisExample.Registration.Domain.Pets.Enums;
using RedisExample.Registration.Domain.Vaccines.Models;

namespace RedisExample.Registration.Domain.Pets.Models
{
    public class Pet : Entity
    {
        #region Properties

        public string Name { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public SpeciesType Species { get; set; }

        public string Color { get; set; } = null!;

        public string Breed { get; set; } = null!;

        public List<Vaccine> Vaccines { get; set; } = new List<Vaccine>();

        public Human Guardian { get; set; } = null!;

        #endregion

        #region Constructors

        [Obsolete(ConstructorObsoleteMessage, true)]
        private Pet() : base(Guid.NewGuid()) { }

        public Pet(string name, DateTime birthDate, SpeciesType species, string color, string breed, List<Vaccine> vaccines)
            : base(Guid.NewGuid())
        {
            Name = name;
            BirthDate = birthDate;
            Species = species;
            Color = color;
            Breed = breed;
            Vaccines = vaccines;
        }

        #endregion

        #region Conversion Operators

        public static implicit operator Pet(Maybe<Pet> entity) => entity.Value;

        public static implicit operator Pet(Response<Pet> entity) => entity.Data;

        #endregion
    }
}
