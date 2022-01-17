using FluentAssertions;
using MediatR;
using Moq;
using RedisExample.VaccinationCard.Common.Exceptions;
using RedisExample.VaccinationCard.Domain.Commands;
using RedisExample.VaccinationCard.Domain.Handlers;
using RedisExample.VaccinationCard.Domain.Models;
using RedisExample.VaccinationCard.Domain.Repositories;
using RedisExample.VaccinationCard.Domain.Tests.Shared;
using System.Threading.Tasks;
using Xunit;

namespace RedisExample.VaccinationCard.Domain.Tests.Handlers
{
    public class CreateHumanUnitTests : BaseMock
    {
        private Mock<IHumanRepository> _humanRepository = new();

        private CreateHumanCommandHandler Handler { get; set; }

        public CreateHumanUnitTests()
        {
            Handler = new CreateHumanCommandHandler(_humanRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateHuman_ValidCommand()
        {
            _humanRepository.Setup(x => x.AddAsync(It.IsAny<Human>())).Verifiable();

            var response = await Handler.Handle(CreateHumanCommandFake());

            response.Should().NotBeNull();
            response.Should().Be(Unit.Value);
            _humanRepository.Verify();
        }

        [Fact]
        public async Task Handle_ShouldThrowInvalidCommandException_RequestIsNull()
        {
            _humanRepository.Setup(x => x.AddAsync(It.IsAny<Human>()));

            CreateHumanCommand? command = null;

            Task act() => Handler.Handle(command);

            var response = await Record.ExceptionAsync(act);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(InvalidCommandException));
            response.Source.Should().Be(nameof(CreateHumanCommand));
            response.Message.Should().Be("request is invalid");
        }
    }
}
