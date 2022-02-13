using BaseEntity.Domain.Messaging;
using MediatR;
using Messages.Core;
using Messages.Core.Extensions;
using RedisExample.Registration.Domain.Models;

namespace RedisExample.Registration.Domain.Commands
{
    public class AddVaccineCommand : Command, IRequest<Response<Human>>
    {
        public Guid HumanId { get; set; }

        public Guid PetId { get; set; }

        public string? Name { get; set; }

        public string? Producer { get; set; }

        public DateTime Date { get; set; }

        public string? Registration { get; set; }

        public string? ActiveIngredient { get; set; }

        public string? Batch { get; set; }

        public override Response Validate()
        {
            var response = Response.Create();

            if (string.IsNullOrWhiteSpace(Name))
                response.WithBusinessError(nameof(Name), $"{nameof(Name)} is invalid");

            if (string.IsNullOrWhiteSpace(Producer))
                response.WithBusinessError(nameof(Producer), $"{nameof(Producer)} is invalid");

            if (!DateIsValid())
                response.WithBusinessError(nameof(Date), $"{nameof(Date)} is invalid");

            if (string.IsNullOrWhiteSpace(Registration))
                response.WithBusinessError(nameof(Registration), $"{nameof(Registration)} is invalid");

            if (string.IsNullOrWhiteSpace(Batch))
                response.WithBusinessError(nameof(Batch), $"{nameof(Batch)} is invalid");

            return response;
        }

        private bool DateIsValid()
            => Date <= DateTime.Today && Date >= DateTime.Today.AddYears(-5);
    }
}
