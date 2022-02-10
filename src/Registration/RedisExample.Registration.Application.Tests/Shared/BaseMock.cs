using FizzWare.NBuilder;
using RedisExample.Registration.Domain.Models;
using RedisExample.Registration.Messaging.RequestMessages;
using Valuables.Utils;

namespace RedisExample.Registration.Application.Tests.Shared
{
    public class BaseMock
    {
        public CreateHumanRequestMesssage CreateHumanRequestMesssageFake()
            => Builder<CreateHumanRequestMesssage>.CreateNew().Build();

        public Human HumanFake()
            => Builder<Human>.CreateNew()
                .With(x => x.Address, AddressFake())
                .With(x => x.Email, Email.Create("any@nothing.com").Data.Value)
                .With(x => x.CPF, CPF.Create("98765432100").Data.Value)                
                .Build();

        public Address AddressFake()
            => Builder<Address>.CreateNew().Build();

        public CreatePetRequestMessage CreatePetRequestMessageFake()
            => Builder<CreatePetRequestMessage>.CreateNew().Build();
    }
}
