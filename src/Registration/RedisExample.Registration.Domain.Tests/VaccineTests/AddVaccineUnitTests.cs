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
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RedisExample.Registration.Domain.Tests.VaccineTests
{
    public class AddVaccineUnitTests : BaseMock
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        private readonly Mock<IHumanRepository> _humanRepository = new();

        private AddVaccineCommandHandler Handler { get; }

        public AddVaccineUnitTests()
        {
            Handler = new AddVaccineCommandHandler(_humanRepository.Object, _unitOfWork.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_ValidParameters()
        {
            _unitOfWork.Setup(uow => uow.CommitAsync())
                .ReturnsAsync(true)
                .Verifiable();

            _humanRepository.Setup(repositoy => repositoy.UpdateAsync(It.IsAny<Human>())).Verifiable();

            var petId = Guid.NewGuid();
            _humanRepository.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(HumanFake(pets: new List<Pet> { PetFake(id: petId) }))
                .Verifiable();

            var response = await Handler.Handle(AddVaccineCommandFake(petId: petId));

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(Human));
            response.Data.Value.DomainEvents.Should().NotBeEmpty();
            response.Data.Value.Pets.Should().Contain(pet => pet.Code.Equals(petId));
            _humanRepository.VerifyAll();
            _unitOfWork.Verify();
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_NameIsEmpty()
        {
            _unitOfWork.Setup(uow => uow.CommitAsync());
            _humanRepository.Setup(repository => repository.FindAsync(It.IsAny<Guid>()));
            _humanRepository.Setup(repositoy => repositoy.UpdateAsync(It.IsAny<Human>()));

            var response = await Handler.Handle(AddVaccineCommandFake(name: string.Empty));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("Name"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(repository => repository.FindAsync(It.IsAny<Guid>()), Times.Never);
            _humanRepository.Verify(x => x.UpdateAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_ProducerIsEmpty()
        {
            _unitOfWork.Setup(uow => uow.CommitAsync());
            _humanRepository.Setup(repository => repository.FindAsync(It.IsAny<Guid>()));
            _humanRepository.Setup(repositoy => repositoy.UpdateAsync(It.IsAny<Human>()));

            var response = await Handler.Handle(AddVaccineCommandFake(producer: string.Empty));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("Producer"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(repository => repository.FindAsync(It.IsAny<Guid>()), Times.Never);
            _humanRepository.Verify(x => x.UpdateAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_RegistrationIsEmpty()
        {
            _unitOfWork.Setup(uow => uow.CommitAsync());
            _humanRepository.Setup(repository => repository.FindAsync(It.IsAny<Guid>()));
            _humanRepository.Setup(repositoy => repositoy.UpdateAsync(It.IsAny<Human>()));

            var response = await Handler.Handle(AddVaccineCommandFake(registration: string.Empty));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("Registration"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(repository => repository.FindAsync(It.IsAny<Guid>()), Times.Never);
            _humanRepository.Verify(x => x.UpdateAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_BatchIsEmpty()
        {
            _unitOfWork.Setup(uow => uow.CommitAsync());
            _humanRepository.Setup(repository => repository.FindAsync(It.IsAny<Guid>()));
            _humanRepository.Setup(repositoy => repositoy.UpdateAsync(It.IsAny<Human>()));

            var response = await Handler.Handle(AddVaccineCommandFake(batch: string.Empty));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("Batch"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(repository => repository.FindAsync(It.IsAny<Guid>()), Times.Never);
            _humanRepository.Verify(x => x.UpdateAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidVaccineDates))]
        public async Task Handle_ShouldReturnBusinessError_BirthDateIsInvalid(DateTime date)
        {
            _unitOfWork.Setup(uow => uow.CommitAsync());
            _humanRepository.Setup(repository => repository.FindAsync(It.IsAny<Guid>()));
            _humanRepository.Setup(repositoy => repositoy.UpdateAsync(It.IsAny<Human>()));

            var response = await Handler.Handle(AddVaccineCommandFake(date: date));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("Date"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(repository => repository.FindAsync(It.IsAny<Guid>()), Times.Never);
            _humanRepository.Verify(x => x.UpdateAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_HumanNotFound()
        {
            _unitOfWork.Setup(uow => uow.CommitAsync());
            _humanRepository.Setup(repositoy => repositoy.UpdateAsync(It.IsAny<Human>()));
            _humanRepository.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(Maybe<Human>.Create())
                .Verifiable();

            var command = AddVaccineCommandFake();

            var response = await Handler.Handle(command);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Text.Equals($"Human ID {command.HumanId} not found"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify();
            _humanRepository.Verify(x => x.UpdateAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_PetNotFound()
        {
            _unitOfWork.Setup(uow => uow.CommitAsync());
            _humanRepository.Setup(repositoy => repositoy.UpdateAsync(It.IsAny<Human>()));
            _humanRepository.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(HumanFake(pets: new()))
                .Verifiable();

            var command = AddVaccineCommandFake();

            var response = await Handler.Handle(command);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Text.Equals($"Pet ID {command.PetId} not found"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify();
            _humanRepository.Verify(x => x.UpdateAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnCriticalError_CommitAsyncFailed()
        {
            _unitOfWork.Setup(uow => uow.CommitAsync())
                .ReturnsAsync(false)
                .Verifiable();

            _humanRepository.Setup(repositoy => repositoy.UpdateAsync(It.IsAny<Human>())).Verifiable();

            var petId = Guid.NewGuid();
            _humanRepository.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(HumanFake(pets: new List<Pet>{ PetFake(id: petId) }))
                .Verifiable();

            var response = await Handler.Handle(AddVaccineCommandFake(petId: petId));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.CriticalError));
            response.Messages.Should().Contain(message => message.Text.Equals("Failed to add vaccine"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.VerifyAll();
            _unitOfWork.Verify();
        }
    }
}
