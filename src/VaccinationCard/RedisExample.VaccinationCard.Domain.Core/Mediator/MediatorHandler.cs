using MediatR;
using RedisExample.VaccinationCard.Domain.Core.Commands;
using RedisExample.VaccinationCard.Domain.Core.Events;
using RedisExample.VaccinationCard.Domain.Core.Mediator.Contracts;
using System.Runtime.CompilerServices;

namespace RedisExample.VaccinationCard.Domain.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        IMediator Mediator { get; }

        public MediatorHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async virtual Task<object> SendCommand<T>(T command) where T : Command
            => await Mediator.Send(command);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async virtual Task<TResponse> SendCommand<TRequest, TResponse>(TRequest command)
            where TRequest : IRequest<TResponse>
            where TResponse : class
            => await Mediator.Send(command);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async virtual Task PublishEvent<T>(T @event) where T : Event
            => await Mediator.Publish(@event);
    }
}
