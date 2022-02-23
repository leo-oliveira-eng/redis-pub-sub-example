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
    public class HumanCreatedEventHandlerUnitTests : BaseMock
    {
        private readonly Mock<IEventPublisher> _eventPublisher = new();

        private HumanCreatedEventHandler Handler { get; }

        public HumanCreatedEventHandlerUnitTests()
        {
            Handler = new HumanCreatedEventHandler(_eventPublisher.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_EventIsNull()
        {
            _eventPublisher.Setup(publisher => publisher.Publish(It.IsAny<HumanCreatedEvent>()));

            HumanCreatedEvent? @event = null;

            var response = await Record.ExceptionAsync(() => Handler.Handle(@event!));

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ArgumentNullException));
            response.Message.Should().Be("Value cannot be null. (Parameter '@event')");
            _eventPublisher.Verify(publisher => publisher.Publish(It.IsAny<HumanCreatedEvent>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Success_ShouldPublishEvent()
        {
            _eventPublisher.Setup(publisher => publisher.Publish(It.IsAny<HumanCreatedEvent>()));

            var @event = new HumanCreatedEvent(HumanFake());

            await Handler.Handle(@event);

            _eventPublisher.Verify(publisher => publisher.Publish(@event), Times.Once);
        }
    }
}
