using Mapster;
using Messages.Core;
using Messages.Core.Extensions;
using RedisExample.Registration.Domain.Commands;
using RedisExample.Registration.Domain.Enums;
using RedisExample.Registration.Domain.Models;
using RedisExample.Registration.Messaging.RequestMessages;
using RedisExample.Registration.Messaging.ResponseMessages;
using Valuables.Utils;

namespace RedisExample.Registration.CrossCutting.DI.Mappings
{
    public static class MappingConfiguration
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Response<Human>, HumanResponseMessage>()
                .IgnoreNullValues(true)
                .ConstructUsing(response => new HumanResponseMessage
                {
                    Code = response.Data.Value.Code,
                    Name = response.Data.Value.Name,
                    BirthDate = response.Data.Value.BirthDate,
                    CPF = response.Data.Value.CPF.Text,
                    Email = response.Data.Value.Email.Address,
                    Gender = (Messaging.Enums.GenderType)response.Data.Value.Gender,
                    PhoneNumber = response.Data.Value.PhoneNumber,
                    Address = response.Data.Value.Address.Adapt<AddressResponseMessage>(),
                    Pets = response.Data.Value.Pets.Adapt<List<PetResponseMessage>>()
                });

            config.NewConfig<CreateHumanRequestMesssage, CreateHumanCommand>()
                .IgnoreNullValues(true)
                .ConstructUsing(requestMessage => new CreateHumanCommand
                {
                    Name = requestMessage.Name,
                    BirthDate = requestMessage.BirthDate ?? default,
                    PhoneNumber = requestMessage.PhoneNumber ?? string.Empty,
                    Gender = (GenderType)requestMessage.Gender,
                    Cpf = CPF.Create(requestMessage.CPF),
                    Email = Email.Create(requestMessage.Email),
                    Address = AddressMapping(requestMessage.Address)
                });

            config.ForType<string, Response<CPF>>()
                .IgnoreNullValues(true)
                .MapWith(value => CPF.Create(value));

            config.NewConfig<string, Response<Email>>()
                .IgnoreNullValues(true)
                .MapWith(value => Email.Create(value));

            config.NewConfig<AddressRequestMessage, Response<Address>>()
                .IgnoreNullValues(true)
                .ConstructUsing(src => Address.Create(src.Cep, src.Street, src.Neighborhood, src.Number, src.City, src.UF, src.Complement));

            config.NewConfig<(CreatePetRequestMessage requestMessage, Guid humanId), CreatePetCommand>()
                .IgnoreNullValues(true)
                .MapWith(src => new CreatePetCommand
                {
                    BirthDate = src.requestMessage.BirthDate,
                    Breed = src.requestMessage.Breed,
                    Color = src.requestMessage.Color,
                    Name = src.requestMessage.Name,
                    Species = (SpeciesType)src.requestMessage.Species,
                    HumanId = src.humanId
                });

            config.NewConfig<(VaccineRequestMessage requestMessage, Guid humanId, Guid petId), AddVaccineCommand>()
                .IgnoreNullValues(true)
                .MapWith(src => new AddVaccineCommand
                {
                    HumanId = src.humanId,
                    PetId = src.petId,
                    Name = src.requestMessage.Name,
                    ActiveIngredient = src.requestMessage.ActiveIngredient,
                    Batch = src.requestMessage.Batch,
                    Date = src.requestMessage.Date,
                    Producer = src.requestMessage.Producer,
                    Registration = src.requestMessage.Registration
                });

            config.Compile();

            return config;
        }

        private static Response<Address> AddressMapping(AddressRequestMessage? requestMessage)
            => requestMessage == null
                        ? Response<Address>.Create().WithBusinessError(nameof(Address), $"{nameof(Address)} is invalid")
                        : Address.Create(requestMessage.Cep, requestMessage.Street, requestMessage.Neighborhood,
                            requestMessage.Number, requestMessage.City, requestMessage.UF, requestMessage.Complement);
    }
}
