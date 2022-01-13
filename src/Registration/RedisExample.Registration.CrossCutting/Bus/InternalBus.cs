using BaseEntity.Domain.Events;
using BaseEntity.Domain.Mediator.Contracts;
using BaseEntity.Domain.Messaging;
using MediatR;

namespace RedisExample.Registration.CrossCutting.Bus
{
    public sealed class InternalBus : IMediatorHandler
    {
        IMediator Mediator { get; }

        public InternalBus(IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<object> SendCommand<T>(T command) where T : Command
            => await Mediator.Send(command);

        public async Task PublishEvent<T>(T @event) where T : Event
            => await Mediator.Publish(@event);

        public async Task<TResponse> SendCommand<TRequest, TResponse>(TRequest command)
            where TRequest : IRequest<TResponse>
            where TResponse : class
            => await Mediator.Send(command);
    }
}
