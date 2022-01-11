using BaseEntity.Domain.Entities;
using Messages.Core;

namespace RedisExample.Registration.Domain.Models
{
    public class Vaccine : Entity
    {
        #region Properties

        public string Name { get; set; } = null!;

        public string Producer { get; set; } = null!;

        public DateTime Date { get; set; }

        public string Registration { get; set; } = null!;

        public string? ActiveIngredient { get; set; }

        public string Batch { get; set; } = null!;

        public long PetId { get; set; }

        public Pet Pet { get; set; } = null!;

        #endregion

        #region Constructors

        [Obsolete(ConstructorObsoleteMessage, true)]
        private Vaccine() : base(Guid.NewGuid()) { }

        public Vaccine(string name, string producer, DateTime date, string registration, string? activeIngredient, string batch, Pet pet)
            : base(Guid.NewGuid())
        {
            Name = name;
            Producer = producer;
            Date = date;
            Registration = registration;
            ActiveIngredient = activeIngredient;
            Batch = batch;
            PetId = pet.Id;
            Pet = pet;
        }

        #endregion

        #region Conversion Operators

        public static implicit operator Vaccine(Maybe<Vaccine> entity) => entity.Value;

        public static implicit operator Vaccine(Response<Vaccine> entity) => entity.Data;

        #endregion
    }
}
