using BaseEntity.Domain.UnitOfWork;
using FluentAssertions;
using Messages.Core;
using Messages.Core.Enums;
using Moq;
using RedisExample.Registration.Domain.Commands;
using RedisExample.Registration.Domain.Events;
using RedisExample.Registration.Domain.Handlers;
using RedisExample.Registration.Domain.Models;
using RedisExample.Registration.Domain.Repositories;
using RedisExample.Registration.Domain.Tests.Shared;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RedisExample.Registration.Domain.Tests.HumanTests
{
    public class DeleteHumanUnitTests : BaseMock
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        private readonly Mock<IHumanRepository> _humanRepository = new();

        private DeleteHumanCommandHandler Handler { get; }

        public DeleteHumanUnitTests()
        {
            Handler = new DeleteHumanCommandHandler(_humanRepository.Object, _unitOfWork.Object);
        }

        [Fact]
        public async Task DeleteHuman_ShouldReturnSuccess_ValidHuman()
        {
            var human = HumanFake();
            _unitOfWork.Setup(uow => uow.CommitAsync()).ReturnsAsync(true).Verifiable();
            _humanRepository.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(human)
                .Verifiable();
            _humanRepository.Setup(repository => repository.Delete(It.IsAny<Human>()));

            var command = new DeleteHumanCommand(human.Code);

            var response = await Handler.Handle(command);

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            human.DomainEvents.Should().Contain(domainEvent => domainEvent.GetType() == typeof(HumanDeletedEvent));
            _humanRepository.VerifyAll();
            _unitOfWork.Verify();
        }

        [Fact]
        public async Task DeleteHuman_ShouldReturnBusinessError_HumanNotFound()
        {
            _unitOfWork.Setup(uow => uow.CommitAsync());
            _humanRepository.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(Maybe<Human>.Create())
                .Verifiable();
            _humanRepository.Setup(repository => repository.RemoveAsync(It.IsAny<Human>()));

            var command = new DeleteHumanCommand(Guid.NewGuid());

            var response = await Handler.Handle(command);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Text.Equals("Human not found"));
            _humanRepository.Verify(repository => repository.FindAsync(It.IsAny<Guid>()), Times.Once);
            _humanRepository.Verify(repository => repository.RemoveAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(uow => uow.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task DeleteHuman_ShouldReturnCriticalError_CommitAsyncFailed()
        {
            var human = HumanFake();
            _unitOfWork.Setup(uow => uow.CommitAsync()).ReturnsAsync(false).Verifiable();
            _humanRepository.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(human)
                .Verifiable();
            _humanRepository.Setup(repository => repository.Delete(It.IsAny<Human>()));

            var command = new DeleteHumanCommand(human.Code);

            var response = await Handler.Handle(command);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.CriticalError));
            response.Messages.Should().Contain(message => message.Text.Equals($"Failed to delete human id {command.HumanId}"));
            _humanRepository.VerifyAll();
            _unitOfWork.Verify();
        }
    }
}
