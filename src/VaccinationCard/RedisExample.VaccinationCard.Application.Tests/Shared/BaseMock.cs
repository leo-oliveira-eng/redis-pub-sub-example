using FizzWare.NBuilder;
using RedisExample.VaccinationCard.Domain.Models;
using System.Collections.Generic;

namespace RedisExample.VaccinationCard.Application.Tests.Shared
{
    public class BaseMock
    {
        public Human HumanFake()
            => Builder<Human>.CreateNew()
                .With(x => x.Address, Builder<Address>.CreateNew().Build())
                .With(x => x.Pets, new List<Pet>())
                .Build();
    }
}
