using FizzWare.NBuilder;
using Messages.Core;
using RedisExample.Registration.Domain.Commands;
using RedisExample.Registration.Domain.Enums;
using RedisExample.Registration.Domain.Models;
using System;
using System.Collections.Generic;
using Valuables.Utils;

namespace RedisExample.Registration.Domain.Tests.Shared
{
    public class BaseMock
    {
        public CreateHumanCommand CreateHumanCommandFake(Response<CPF>? cpf = null, string? name = null, DateTime? birthDate = null, GenderType genderType = default, 
            Response<Email>? email = null, string? phoneNumber = null, Response<Address>? address = null)
            => Builder<CreateHumanCommand>.CreateNew()
                .With(x => x.Cpf, cpf ?? CPF.Create("98765432100"))
                .With(x => x.Name, name ?? "Any Name")
                .With(x => x.BirthDate, birthDate ?? DateTime.Now.AddYears(-20))
                .With(x => x.Gender, genderType == default ? GenderType.Man : genderType)
                .With(x => x.Email, email ?? Email.Create("any@nothing.com"))
                .With(x => x.PhoneNumber, phoneNumber ?? "+5521999991234")
                .With(x => x.Address, address ?? AddressFake())
                .Build();

        public Response<Address> AddressFake(string? cep = null, string? street = null, string? complement = null, string? neighborhood = null, string? number = null,
            string? city = null, string? uf = null)
            => Address.Create(cep ?? "12345-123",
                    street ?? "Any street",
                    neighborhood ?? "Any Place",
                    number ?? "S/N",
                    city ?? "Anywhere",
                    uf ?? "RJ",
                    complement);

        public CreatePetCommand CreatePetCommandFake(Guid? humanId = null, string? name = null, DateTime? birthDate = null, SpeciesType species = default, 
            string? color = null, string? breed = null)
            => Builder<CreatePetCommand>.CreateNew()
                .With(x => x.HumanId, humanId ?? Guid.NewGuid())
                .With(x => x.Name, name ?? "Any Name")
                .With(x => x.BirthDate, birthDate ?? DateTime.Now.AddMonths(-2))
                .With(x => x.Species, species == default ? SpeciesType.Dog : species)
                .With(x => x.Color, color ?? "caramel")
                .With(x => x.Breed, breed ?? "SRD")
                .Build();

        public static IEnumerable<object[]> InvalidHumanBirthDates()
        {
            yield return new object[] { DateTime.Now };
            yield return new object[] { DateTime.Now.AddYears(-15) };
            yield return new object[] { DateTime.Now.AddYears(-150) };
            yield return new object[] { DateTime.Now.AddYears(+18) };
            yield return new object[] { (DateTime)default };
        }

        public static IEnumerable<object[]> InvalidPetBirthDates()
        {
            yield return new object[] { DateTime.Now };
            yield return new object[] { DateTime.Now.AddYears(-150) };
            yield return new object[] { DateTime.Now.AddYears(+18) };
            yield return new object[] { (DateTime)default };
        }

        public Human HumanFake()
            => Builder<Human>.CreateNew().Build();
    }
}
