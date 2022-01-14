using BaseEntity.Domain.Mediator.Contracts;
using BaseEntity.Domain.UnitOfWork;
using Infrastructure.UnitOfWork;
using MediatR;
using Messages.Core;
using Microsoft.Extensions.DependencyInjection;
using RedisExample.Registration.Application.EventHandlers;
using RedisExample.Registration.Application.Services;
using RedisExample.Registration.Application.Services.Contracts;
using RedisExample.Registration.CrossCutting.Bus;
using RedisExample.Registration.Domain.Commands;
using RedisExample.Registration.Domain.Events;
using RedisExample.Registration.Domain.Handlers;
using RedisExample.Registration.Domain.Models;
using RedisExample.Registration.Domain.Repositories;
using RedisExample.Registration.Persistence.Context;
using RedisExample.Registration.Persistence.Repositories;

namespace RedisExample.Registration.CrossCutting.DI
{
    public static class DependencyInjector
    {
        public static void Configure(IServiceCollection services)
        {
            #region Data

            services.AddScoped<IHumanRepository, HumanRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork<RegistrationContext>>();

            #endregion

            #region Handlers

            services.AddScoped<IMediatorHandler, InternalBus>();
            services.AddScoped<IRequestHandler<CreateHumanCommand, Response<Human>>, CreateHumanCommandHandler>();
            services.AddScoped<INotificationHandler<HumanCreatedEvent>, HumanCreatedEventHandler>();

            #endregion

            #region Services

            services.AddScoped<IHumanApplicationService, HumanApplicationService>();

            #endregion
        }
    }
}
