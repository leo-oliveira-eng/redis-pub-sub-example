using FluentAssertions;
using Messages.Core;
using Messages.Core.Extensions;
using Moq;
using RedisExample.Registration.Domain.Commands;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RedisExample.Registration.Application.Tests.ServicesTests.HumanTests
{
    public class DeleteAsyncUnitTests : HumanAppServiceTests
    {
        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccess_DeleteCommandExecutedWithSuccess()
        {
            _mediatorHandler.Setup(handler => handler.SendCommand<DeleteHumanCommand, Response>(It.IsAny<DeleteHumanCommand>()))
                .ReturnsAsync(Response.Create())
                .Verifiable();

            var response = await HumanApplicationService.DeleteAsync(Guid.NewGuid());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeFalse();
            _mediatorHandler.Verify();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnBusinessError_DeleteCommandExecutedReturnedError()
        {
            var commandResponse = Response.Create().WithBusinessError("Any error message");
            _mediatorHandler.Setup(handler => handler.SendCommand<DeleteHumanCommand, Response>(It.IsAny<DeleteHumanCommand>()))
                .ReturnsAsync(commandResponse)
                .Verifiable();

            var response = await HumanApplicationService.DeleteAsync(Guid.NewGuid());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().BeEquivalentTo(commandResponse.Messages);
            response.Data.HasValue.Should().BeFalse();
            _mediatorHandler.Verify();
        }
    }
}
