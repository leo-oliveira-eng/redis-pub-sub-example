using BaseEntity.Domain.UnitOfWork;
using FluentAssertions;
using Messages.Core;
using Messages.Core.Enums;
using Moq;
using RedisExample.Registration.Domain.Handlers;
using RedisExample.Registration.Domain.Models;
using RedisExample.Registration.Domain.Repositories;
using RedisExample.Registration.Domain.Tests.Shared;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RedisExample.Registration.Domain.Tests.PetTests
{
    public class CreatePetUnitTests : BaseMock
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        private readonly Mock<IHumanRepository> _humanRepository = new();

        private CreatePetCommandHandler Handler { get; }

        public CreatePetUnitTests()
        {
            Handler = new CreatePetCommandHandler(_humanRepository.Object, _unitOfWork.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_NameIsEmpty()
        {
            _unitOfWork.Setup(x => x.CommitAsync());
            _humanRepository.Setup(x => x.UpdateAsync(It.IsAny<Human>()));

            var response = await Handler.Handle(CreatePetCommandFake(name: string.Empty));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("Name"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(x => x.AddAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidPetBirthDates))]
        public async Task Handle_ShouldReturnBusinessError_BirthDateIsInvalid(DateTime birthDate)
        {
            _unitOfWork.Setup(x => x.CommitAsync());
            _humanRepository.Setup(x => x.UpdateAsync(It.IsAny<Human>()));

            var response = await Handler.Handle(CreatePetCommandFake(birthDate: birthDate));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("BirthDate"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(x => x.AddAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_InvalidSpeciesType()
        {
            _unitOfWork.Setup(x => x.CommitAsync());
            _humanRepository.Setup(x => x.UpdateAsync(It.IsAny<Human>()));

            var response = await Handler.Handle(CreatePetCommandFake(species: (Enums.SpeciesType)100));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("Species"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(x => x.AddAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_ColorIsEmpty()
        {
            _unitOfWork.Setup(x => x.CommitAsync());
            _humanRepository.Setup(x => x.UpdateAsync(It.IsAny<Human>()));

            var response = await Handler.Handle(CreatePetCommandFake(color: string.Empty));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("Color"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(x => x.AddAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_HumanNotFound()
        {
            _unitOfWork.Setup(x => x.CommitAsync());
            _humanRepository.Setup(x => x.UpdateAsync(It.IsAny<Human>()));
            _humanRepository.Setup(x => x.FindAsync(It.IsAny<Guid>())).ReturnsAsync(Maybe<Human>.Create());

            var command = CreatePetCommandFake();

            var response = await Handler.Handle(command);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Text.Equals($"No human was found with id {command.HumanId}"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(x => x.AddAsync(It.IsAny<Human>()), Times.Never);
            _humanRepository.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Once);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }
    }
}
