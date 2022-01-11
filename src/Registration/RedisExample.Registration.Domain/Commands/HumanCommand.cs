using BaseEntity.Domain.Messaging;
using MediatR;
using Messages.Core;
using Messages.Core.Extensions;
using RedisExample.Registration.Domain.Commands.Dtos;
using RedisExample.Registration.Domain.Models;

namespace RedisExample.Registration.Domain.Commands
{
    public class HumanCommand : Command, IRequest<Response<Human>>
    {
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public AddressDto? Address { get; set; }

        public override Response Validate()
        {
            var response = Response.Create();

            if (!Valuables.Utils.Email.IsValid(Email))
                response.WithBusinessError(nameof(Email), $"{nameof(Email)} is invalid");

            if (string.IsNullOrWhiteSpace(PhoneNumber))
                response.WithBusinessError(nameof(PhoneNumber),$"{nameof(PhoneNumber)} is invalid");

            var addressIsValidResponse = Valuables.Utils.Address.IsValid(Address?.Cep, Address?.Street, Address?.Neighborhood, Address?.Number, Address?.City, Address?.UF);

            if (addressIsValidResponse.HasError)
                response.WithMessages(addressIsValidResponse.Messages);

            return response;
        }
    }
}
