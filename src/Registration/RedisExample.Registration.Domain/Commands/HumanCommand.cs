using BaseEntity.Domain.Messaging;
using MediatR;
using Messages.Core;
using Messages.Core.Extensions;
using RedisExample.Registration.Domain.Models;
using Valuables.Utils;

namespace RedisExample.Registration.Domain.Commands
{
    public class HumanCommand : Command, IRequest<Response<Human>>
    {
        public Response<Email> Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public Response<Address> Address { get; set; } = null!;

        public override Response Validate()
        {
            var response = Response.Create();

            if (Email.HasError)
                response.WithBusinessError(nameof(Email), $"{nameof(Email)} is invalid");

            if (string.IsNullOrWhiteSpace(PhoneNumber))
                response.WithBusinessError(nameof(PhoneNumber), $"{nameof(PhoneNumber)} is invalid");

            if (Address.HasError)
                response.WithMessages(Address.Messages);

            return response;
        }
    }
}