using BaseEntity.Domain.Mediator.Contracts;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RedisExample.Registration.Application.Services;
using RedisExample.Registration.Application.Tests.Shared;
using RedisExample.Registration.CrossCutting.DI.Extensions;
using RedisExample.Registration.CrossCutting.DI.Mappings;

namespace RedisExample.Registration.Application.Tests.ServicesTests.HumanTests
{
    public class HumanAppServiceTests : BaseMock
    {
        protected readonly Mock<IMediatorHandler> _mediatorHandler = new();

        protected HumanApplicationService HumanApplicationService { get; }

        public HumanAppServiceTests()
        {
            var scope = CreateScope();

            HumanApplicationService = new HumanApplicationService(_mediatorHandler.Object, scope.ServiceProvider.GetService<IMapper>()!);
        }

        public static IServiceScope CreateScope()
        {
            var config = MappingConfiguration.GetConfiguredMappingConfig();

            IServiceCollection services = new ServiceCollection();

            services.AddSingleton(config);

            services.AddScoped<IMapper, ServiceMapper>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.CreateScope();
        }
    }
}
