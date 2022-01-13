using FluentAssertions;
using Messages.Core;
using Messages.Core.Enums;
using Messages.Core.Extensions;
using Moq;
using RedisExample.Registration.Domain.Commands;
using RedisExample.Registration.Domain.Models;
using RedisExample.Registration.Messaging.ResponseMessages;
using System.Threading.Tasks;
using Xunit;

namespace RedisExample.Registration.Application.Tests.ServicesTests.HumanTests
{
    public class CreateAsyncUnitTests : HumanAppServiceTests

    {
        [Fact]
        public async Task CreateAsync_ShouldReturnSuccess_ValidParameters()
        {
            _mediatorHandler.Setup(x => x.SendCommand<CreateHumanCommand, Response<Human>>(It.IsAny<CreateHumanCommand>()))
                .ReturnsAsync(HumanFake())
                .Verifiable();

            var response = await HumanApplicationService.CreateAsync(CreateHumanRequestMesssageFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(HumanResponseMessage));
            _mediatorHandler.Verify();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnBusinessError_RequestIsNull()
        {
            _mediatorHandler.Setup(x => x.SendCommand<CreateHumanCommand, Response<Human>>(It.IsAny<CreateHumanCommand>()));

            var response = await HumanApplicationService.CreateAsync(null);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _mediatorHandler.Verify(x => x.SendCommand<CreateHumanCommand, Response<Human>>(It.IsAny<CreateHumanCommand>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnBusinessError_CommandFailed()
        {
            var errorMessage = "Any error message";

            _mediatorHandler.Setup(x => x.SendCommand<CreateHumanCommand, Response<Human>>(It.IsAny<CreateHumanCommand>()))
                .ReturnsAsync(Response<Human>.Create().WithBusinessError(errorMessage))
                .Verifiable();

            var response = await HumanApplicationService.CreateAsync(CreateHumanRequestMesssageFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Text.Equals(errorMessage));
            response.Data.HasValue.Should().BeFalse();
            _mediatorHandler.Verify();
        }        
    }
}
