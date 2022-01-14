using MediatR;
using RedisExample.VaccinationCard.Domain.Core.Commands;
using RedisExample.VaccinationCard.Domain.Core.Events;

namespace RedisExample.VaccinationCard.Domain.Core.Mediator.Contracts
{
    public interface IMediatorHandler
    {
        Task<object> SendCommand<T>(T command) where T : Command;

        Task<TResponse> SendCommand<TRequest, TResponse>(TRequest command)
            where TRequest : IRequest<TResponse>
            where TResponse : class;

        Task PublishEvent<T>(T @event) where T : Event;
    }
}
