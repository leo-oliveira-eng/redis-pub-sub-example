using Microsoft.Extensions.DependencyInjection;

namespace RedisExample.VaccinationCard.CrossCutting.DI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDependencyInjector(this IServiceCollection services)
            => DependencyInjector.Configure(services);
    }
}
