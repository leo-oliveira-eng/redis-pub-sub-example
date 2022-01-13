using RedisExample.Registration.Domain.Commands;
using RedisExample.Registration.Domain.Enums;
using RedisExample.Registration.Domain.Models;
using RedisExample.Registration.Messaging.RequestMessages;
using RedisExample.Registration.Messaging.ResponseMessages;
using Valuables.Utils;

namespace RedisExample.Registration.Application.Services.Mappers
{
    public static class HumanMapper
    {
        public static CreateHumanCommand ToCreateHumanCommand(this CreateHumanRequestMesssage requestMessage)
        {
            var address = requestMessage.Address;

            return new CreateHumanCommand
            {
                Name = requestMessage.Name,
                BirthDate = requestMessage.BirthDate ?? default,
                PhoneNumber = requestMessage.PhoneNumber ?? string.Empty,
                Gender = (GenderType)requestMessage.Gender,
                Cpf = CPF.Create(requestMessage.CPF),
                Email = Email.Create(requestMessage.Email),
                Address = Address.Create(address?.Cep, address?.Street, address?.Neighborhood, address?.Number, address?.City, address?.UF, address?.Complement)
            };
        }

        public static HumanResponseMessage ToHumanResponseMessage(this Human human)
        {
            if (human is null)
                return new HumanResponseMessage();

            var address = human.Address;

            return new HumanResponseMessage
            {
                Code = human.Code,
                Name = human.Name,
                BirthDate = human.BirthDate,
                CPF = human.CPF.Text,
                Email = human.Email.Address,
                Gender = (Messaging.Enums.GenderType)human.Gender,
                PhoneNumber = human.PhoneNumber,
                Address = new AddressResponseMessage
                {
                    Cep = address.Cep,
                    Street = address.Street,
                    Neighborhood = address.Neighborhood,
                    Number = address.Number,
                    City = address.City,
                    UF = address.UF,
                    Complement = address.Complement
                },
                Pets = human.Pets.Select(pet => new PetResponseMessage
                {
                    // TODO: include mapping for pets
                }).ToList()
            };
        }
    }
}
