using FluentAssertions;
using Moq;
using RedisExample.Registration.Application.EventHandlers;
using RedisExample.Registration.Application.EventHandlers.Contracts;
using RedisExample.Registration.Application.Tests.Shared;
using RedisExample.Registration.Domain.Events;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RedisExample.Registration.Application.Tests.HandlerTests
{
    public class HumanDeletedEventHandlerUnitTests : BaseMock
    {
        private readonly Mock<IEventPublisher> _eventPublisher = new();

        private HumanDeletedEventHandler Handler { get; }

        public HumanDeletedEventHandlerUnitTests()
        {
            Handler = new HumanDeletedEventHandler(_eventPublisher.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_EventIsNull()
        {
            _eventPublisher.Setup(publisher => publisher.PublishAsync(It.IsAny<HumanDeletedEvent>()));

            HumanDeletedEvent? @event = null;

            var response = await Record.ExceptionAsync(() => Handler.Handle(@event!));

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ArgumentNullException));
            response.Message.Should().Be("Value cannot be null. (Parameter '@event')");
            _eventPublisher.Verify(publisher => publisher.PublishAsync(It.IsAny<HumanDeletedEvent>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Success_ShouldPublishEvent()
        {
            _eventPublisher.Setup(publisher => publisher.PublishAsync(It.IsAny<HumanDeletedEvent>()));

            var @event = new HumanDeletedEvent(Guid.NewGuid());

            await Handler.Handle(@event);

            _eventPublisher.Verify(publisher => publisher.PublishAsync(@event), Times.Once);
        }
    }
}
