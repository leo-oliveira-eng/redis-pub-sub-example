using Messages.Core;
using Messages.Core.Extensions;
using RedisExample.Registration.Domain.Enums;
using Valuables.Utils;

namespace RedisExample.Registration.Domain.Commands
{
    public class CreateHumanCommand : HumanCommand
    {
        public Response<CPF> Cpf { get; set; } = null!;

        public string? Name { get; set; }

        public DateTime BirthDate { get; set; }

        public GenderType Gender { get; set; }

        public override Response Validate()
        {
            var response = base.Validate();

            if (Cpf.HasError)
                response.WithBusinessError(nameof(Cpf), $"{nameof(Cpf)} is invalid");

            if (!BirthDateIsValid(BirthDate))
                response.WithBusinessError(nameof(BirthDate), $"{nameof(BirthDate)} is invalid");

            if (string.IsNullOrEmpty(Name))
                response.WithBusinessError(nameof(Name), $"{nameof(Name)} is invalid");

            if (!Enum.IsDefined(typeof(GenderType), Gender))
                response.WithBusinessError(nameof(Gender), $"{nameof(Gender)} is invalid");

            return response;
        }

        private static bool BirthDateIsValid(DateTime birthDate)
            => birthDate.Date <= DateTime.Today.AddYears(-18) && birthDate > DateTime.Today.AddYears(-120);
    }
}
