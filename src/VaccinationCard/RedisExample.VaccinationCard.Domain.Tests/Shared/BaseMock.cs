using FizzWare.NBuilder;
using RedisExample.VaccinationCard.Domain.Models;
using RedisExample.VaccinationCard.Domain.Commands;
using RedisExample.VaccinationCard.Domain.Enums;
using System;

namespace RedisExample.VaccinationCard.Domain.Tests.Shared
{
    public class BaseMock
    {
        public CreateHumanCommand CreateHumanCommandFake()
            => Builder<CreateHumanCommand>.CreateNew()
                .With(x => x.Address, Builder<Address>.CreateNew().Build())
                .With(x => x.BirthDate, DateTime.Now.AddYears(-20))
                .With(x => x.Gender, GenderType.Man)
                .Build();
    }
}
