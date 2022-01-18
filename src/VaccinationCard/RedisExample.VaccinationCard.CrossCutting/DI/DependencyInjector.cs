using Mapster;
using MediatR;
using Messages.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RedisExample.VaccinationCard.Application.Queries;
using RedisExample.VaccinationCard.Common.Settings;
using RedisExample.VaccinationCard.Common.Settings.Contracts;
using RedisExample.VaccinationCard.CrossCutting.Bus;
using RedisExample.VaccinationCard.Data.Context;
using RedisExample.VaccinationCard.Data.Context.Contracts;
using RedisExample.VaccinationCard.Data.Human.Repositories;
using RedisExample.VaccinationCard.Data.Mappings;
using RedisExample.VaccinationCard.Domain.Commands;
using RedisExample.VaccinationCard.Domain.Core.Mediator.Contracts;
using RedisExample.VaccinationCard.Domain.Handlers;
using RedisExample.VaccinationCard.Domain.Repositories;
using RedisExample.VaccinationCard.Messaging.RequestMessages;
using RedisExample.VaccinationCard.Messaging.ResponseMessages;

namespace RedisExample.VaccinationCard.CrossCutting.DI
{
    public static class DependencyInjector
    {
        public static void Configure(IServiceCollection services)
        {
            #region Settings

            TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);
            services.AddSingleton<ISettings>(service => service.GetRequiredService<IOptions<VaccinationCardSettings>>().Value);

            #endregion            

            #region Data

            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IHumanRepository, HumanRepository>();
            EntityMapping.MapEntity();

            #endregion

            #region Handlers

            services.AddScoped<IRequestHandler<CreateHumanCommand, Unit>, CreateHumanCommandHandler>();
            services.AddScoped<IMediatorHandler, InternalBus>();
            services.AddScoped<IRequestHandler<FindHumanByIdQuery, Response<HumanResponseMessage>>, FindHumanByIdQueryHandler>();

            #endregion
        }
    }
}
