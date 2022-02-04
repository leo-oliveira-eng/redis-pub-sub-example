using BaseEntity.Domain.UnitOfWork;
using FluentAssertions;
using Messages.Core.Enums;
using Moq;
using RedisExample.Registration.Domain.Enums;
using RedisExample.Registration.Domain.Handlers;
using RedisExample.Registration.Domain.Models;
using RedisExample.Registration.Domain.Repositories;
using RedisExample.Registration.Domain.Tests.Shared;
using System;
using System.Threading.Tasks;
using Valuables.Utils;
using Xunit;

namespace RedisExample.Registration.Domain.Tests.HumanTests
{
    public class CreateHumanUnitTests : BaseMock
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        private readonly Mock<IHumanRepository> _humanRepository = new();

        private CreateHumanCommandHandler Handler { get; }

        public CreateHumanUnitTests()
        {
            Handler = new CreateHumanCommandHandler(_humanRepository.Object, _unitOfWork.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateNewHuman_ValidParameters()
        {
            _humanRepository.Setup(x => x.AddAsync(It.IsAny<Human>())).Verifiable();
            _unitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true).Verifiable();

            var response = await Handler.Handle(CreateHumanCommandFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(Human));
            response.Data.Value.DomainEvents.Should().NotBeEmpty();
            _humanRepository.Verify();
            _unitOfWork.Verify();
        }

        [Fact]
        public async Task Handle_ShouldReturnResponseWithBusinessError_InvalidEmail()
        {
            _humanRepository.Setup(x => x.AddAsync(It.IsAny<Human>())).Verifiable();
            _unitOfWork.Setup(x => x.CommitAsync());

            var response = await Handler.Handle(CreateHumanCommandFake(email: Email.Create("any.com.br")));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("Email"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(x => x.AddAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnResponseWithBusinessError_PhoneNumberIsEmpty()
        {
            _humanRepository.Setup(x => x.AddAsync(It.IsAny<Human>())).Verifiable();
            _unitOfWork.Setup(x => x.CommitAsync());

            var response = await Handler.Handle(CreateHumanCommandFake(phoneNumber: string.Empty));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("PhoneNumber"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(x => x.AddAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnResponseWithBusinessError_AddressIsInvalid()
        {
            _humanRepository.Setup(x => x.AddAsync(It.IsAny<Human>())).Verifiable();
            _unitOfWork.Setup(x => x.CommitAsync());

            var address = AddressFake(cep: string.Empty);

            var response = await Handler.Handle(CreateHumanCommandFake(address: address));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("cep"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(x => x.AddAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnResponseWithBusinessError_CpfIsInvalid()
        {
            _humanRepository.Setup(x => x.AddAsync(It.IsAny<Human>())).Verifiable();
            _unitOfWork.Setup(x => x.CommitAsync());

            var response = await Handler.Handle(CreateHumanCommandFake(cpf: CPF.Create("11111111111")));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("Cpf"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(x => x.AddAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnResponseWithBusinessError_NameIsEmpty()
        {
            _humanRepository.Setup(x => x.AddAsync(It.IsAny<Human>())).Verifiable();
            _unitOfWork.Setup(x => x.CommitAsync());

            var response = await Handler.Handle(CreateHumanCommandFake(name: string.Empty));

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
        [MemberData(nameof(InvalidHumanBirthDates))]
        public async Task Handle_ShouldReturnResponseWithBusinessError_BirthDateIsInvalid(DateTime birthDate)
        {
            _humanRepository.Setup(x => x.AddAsync(It.IsAny<Human>())).Verifiable();
            _unitOfWork.Setup(x => x.CommitAsync());

            var response = await Handler.Handle(CreateHumanCommandFake(birthDate: birthDate));

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
        public async Task Handle_ShouldReturnResponseWithBusinessError_GenderIsInvalid()
        {
            _humanRepository.Setup(x => x.AddAsync(It.IsAny<Human>())).Verifiable();
            _unitOfWork.Setup(x => x.CommitAsync());

            var response = await Handler.Handle(CreateHumanCommandFake(genderType: (GenderType)50));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("Gender"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify(x => x.AddAsync(It.IsAny<Human>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnResponseWithCriticalError_CommitAsyncFailed()
        {
            _humanRepository.Setup(x => x.AddAsync(It.IsAny<Human>())).Verifiable();
            _unitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(false).Verifiable();

            var response = await Handler.Handle(CreateHumanCommandFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.CriticalError));
            response.Messages.Should().Contain(message => message.Text.Equals("Failed to save human"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _humanRepository.Verify();
            _unitOfWork.Verify();
        }
    }
}
