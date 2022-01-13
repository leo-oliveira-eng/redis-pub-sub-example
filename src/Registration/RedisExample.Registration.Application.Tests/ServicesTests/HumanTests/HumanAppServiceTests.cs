using BaseEntity.Domain.Mediator.Contracts;
using Moq;
using RedisExample.Registration.Application.Services;
using RedisExample.Registration.Application.Tests.Shared;

namespace RedisExample.Registration.Application.Tests.ServicesTests.HumanTests
{
    public class HumanAppServiceTests : BaseMock
    {
        protected readonly Mock<IMediatorHandler> _mediatorHandler = new();

        protected HumanApplicationService HumanApplicationService { get; }

        public HumanAppServiceTests()
        {
            HumanApplicationService = new HumanApplicationService(_mediatorHandler.Object);
        }
    }
}
