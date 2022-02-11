using FluentAssertions;
using Messages.Core;
using Messages.Core.Enums;
using Messages.Core.Extensions;
using Moq;
using RedisExample.Registration.Domain.Commands;
using RedisExample.Registration.Domain.Models;
using RedisExample.Registration.Messaging.ResponseMessages;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RedisExample.Registration.Application.Tests.ServicesTests.HumanTests
{
    public class CreatePetAsyncUnitTests : HumanAppServiceTests
    {
        [Fact]
        public async Task CreatePetAsync_ShouldReturSuccess_CreatePetAndAddItToItsHuman()
        {
            _mediatorHandler.Setup(handler => handler.SendCommand<CreatePetCommand, Response<Human>>(It.IsAny<CreatePetCommand>()))
                .ReturnsAsync(HumanFake())
                .Verifiable();

            var response = await HumanApplicationService.CreatePetAsync(CreatePetRequestMessageFake(), Guid.NewGuid());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(HumanResponseMessage));
            _mediatorHandler.Verify();
        }

        [Fact]
        public async Task CreatePetAsync_ShouldReturnBusinessError_RequestIsNull()
        {
            _mediatorHandler.Setup(handler => handler.SendCommand<CreatePetCommand, Response<Human>>(It.IsAny<CreatePetCommand>()));

            var response = await HumanApplicationService.CreatePetAsync(null, Guid.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _mediatorHandler.Verify(handler => handler.SendCommand<CreatePetCommand, Response<Human>>(It.IsAny<CreatePetCommand>()), Times.Never);
        }

        [Fact]
        public async Task CreatePetAsync_ShouldReturnBusinessError_CommandFailed()
        {
            var errorMessage = "Any error message";

            _mediatorHandler.Setup(handler => handler.SendCommand<CreatePetCommand, Response<Human>>(It.IsAny<CreatePetCommand>()))
                .ReturnsAsync(Response<Human>.Create().WithBusinessError(errorMessage))
                .Verifiable();

            var response = await HumanApplicationService.CreatePetAsync(CreatePetRequestMessageFake(), Guid.NewGuid());

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
