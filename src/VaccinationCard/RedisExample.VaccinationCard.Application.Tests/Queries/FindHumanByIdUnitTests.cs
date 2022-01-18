using FluentAssertions;
using Mapster;
using Messages.Core;
using Messages.Core.Enums;
using Moq;
using RedisExample.VaccinationCard.Application.Queries;
using RedisExample.VaccinationCard.Application.Tests.Shared;
using RedisExample.VaccinationCard.Domain.Models;
using RedisExample.VaccinationCard.Domain.Repositories;
using RedisExample.VaccinationCard.Messaging.RequestMessages;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RedisExample.VaccinationCard.Application.Tests.Queries
{
    public class FindHumanByIdUnitTests : BaseMock
    {
        private Mock<IHumanRepository> _humanRepositoryMock = new();
        
        private FindHumanByIdQueryHandler Handler { get; }

        public FindHumanByIdUnitTests()
        {
            TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);
            Handler = new FindHumanByIdQueryHandler(_humanRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnHuman_RepositoryFindHumanById()
        {
            var human = HumanFake();
            _humanRepositoryMock.Setup(x => x.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(human)
                .Verifiable();

            var request = new FindHumanByIdQuery { Id = Guid.NewGuid() };

            var response = await Handler.Handle(request);

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Id.Should().Be(human.Id);
            _humanRepositoryMock.Verify();
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_HumanNotFound()
        {
            _humanRepositoryMock.Setup(x => x.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(Maybe<Human>.Create())
                .Verifiable();

            var request = new FindHumanByIdQuery { Id = Guid.NewGuid() };

            var response = await Handler.Handle(request);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Text.Equals("Not found"));
            response.Data.HasValue.Should().BeFalse();
            _humanRepositoryMock.Verify();
        }
    }
}
