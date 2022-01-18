using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RedisExample.VaccinationCard.Data.Context;
using RedisExample.VaccinationCard.Data.Context.Contracts;
using RedisExample.VaccinationCard.Data.Human.Repositories;
using RedisExample.VaccinationCard.Data.Mappings;
using RedisExample.VaccinationCard.Domain.Commands;
using RedisExample.VaccinationCard.Domain.Handlers;
using RedisExample.VaccinationCard.Domain.Repositories;

namespace RedisExample.VaccinationCard.CrossCutting.DI
{
    public static class DependencyInjector
    {
        public static void Configure(IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);

            #region Data

            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IHumanRepository, HumanRepository>();
            EntityMapping.MapEntity();

            #endregion

            #region Handlers

            services.AddScoped<IRequestHandler<CreateHumanCommand, Unit>, CreateHumanCommandHandler>();

            #endregion
        }
    }
}
