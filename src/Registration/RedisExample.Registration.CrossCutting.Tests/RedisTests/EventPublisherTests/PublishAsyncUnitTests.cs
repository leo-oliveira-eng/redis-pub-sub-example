using Moq;
using RedisExample.Registration.CrossCutting.Redis;
using System;
using System.Threading.Tasks;
using RedisExample.Registration.Common.Settings.Contracts;
using Xunit;
using RedisExample.Registration.Domain.Events;
using FluentAssertions;
using RedisExample.Registration.CrossCutting.Redis.Contracts;
using System.Collections.Generic;
using RedisExample.Registration.Common.Settings;
using RedisExample.Registration.Domain.Models;
using FizzWare.NBuilder;

namespace RedisExample.Registration.CrossCutting.Tests.RedisTests.EventPublisherTests
{
    public class PublishAsyncUnitTests
    {
        private readonly Mock<IBrokerService> _brokerService = new();

        private readonly Mock<ISettings> _settings = new();

        private EventPublisher EventPublisher { get; }

        public PublishAsyncUnitTests()
        {
            _settings.Setup(setting => setting.RedisSettings).Returns(FakeSettings().RedisSettings).Verifiable();
            EventPublisher = new EventPublisher(_brokerService.Object, _settings.Object);            
        }

        [Fact]
        public async Task PublishAsync_ShouldThrowArgumentNullException_EventIsNull()
        {
            _brokerService.Setup(service => service.PublishAsync(It.IsAny<string>(), It.IsAny<string>()));

            HumanCreatedEvent? @event = null;

            var response = await Record.ExceptionAsync(() => EventPublisher.PublishAsync(@event!));

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ArgumentNullException));
            response.Message.Should().Be("Value cannot be null. (Parameter '@event')");
            _brokerService.Verify(service => service.PublishAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task PublishAsync_ShouldPublishEvent_ValidEvent()
        {
            _brokerService.Setup(service => service.PublishAsync(It.IsAny<string>(), It.IsAny<string>()));

            var @event = new HumanCreatedEvent(Builder<Human>.CreateNew().Build());

            await EventPublisher.PublishAsync(@event);

            _brokerService.Verify(service => service.PublishAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        private static IDictionary<string, string> TopicsFake()
            => new Dictionary<string, string>() { { "HumanCreatedEvent", "HumanCreatedEvent" } };

        private static RegistrationSettings FakeSettings()
            => new RegistrationSettings
            {
                RedisSettings = new RedisSettings
                {
                    Topics = TopicsFake()
                }
            };

    }
}
