using MediatR;
using Messages.Core;
using Messages.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace RedisExample.VaccinationCard.WebApi.Controllers.Default
{
    public abstract class Controller : ControllerBase
    {
        protected ISender Mediator { get; }

        public Controller(ISender mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected async Task<IActionResult> SendQueryAsync<T>(IRequest<Response<T>> request)
        {
            var response = await Mediator.Send(request);

            if (response.HasError)
                return Ok(response);

            if (response.Messages.Any(message => message.Type is MessageType.BusinessError))
                return BadRequest(response);

            return StatusCode(500, response);
        }
    }
}
