using BaseEntity.Domain.Messaging;
using MediatR;
using Messages.Core;
using Messages.Core.Extensions;
using RedisExample.Registration.Domain.Enums;
using RedisExample.Registration.Domain.Models;

namespace RedisExample.Registration.Domain.Commands
{
    public class CreatePetCommand : Command, IRequest<Response<Human>>
    {
        public Guid HumanId { get; set; }

        public string Name { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public SpeciesType Species { get; set; }

        public string Color { get; set; } = null!;

        public string Breed { get; set; } = null!;

        public override Response Validate()
        {
            var response = Response.Create();

            if (string.IsNullOrWhiteSpace(Name))
                response.WithBusinessError(nameof(Name), $"{nameof(Name)} is invalid");

            if (!BirthDateIsValid())
                response.WithBusinessError(nameof(BirthDate), $"{nameof(BirthDate)} is invalid");

            if (!Enum.IsDefined(typeof(SpeciesType), Species))
                response.WithBusinessError(nameof(Species), $"{nameof(Species)} is invalid");

            if (string.IsNullOrWhiteSpace(Color))
                response.WithBusinessError(nameof(Color), $"{nameof(Color)} is invalid");

            return response;
        }

        private bool BirthDateIsValid()
            => BirthDate <= DateTime.Today && BirthDate >= DateTime.Today.AddYears(-60);
    }
}
