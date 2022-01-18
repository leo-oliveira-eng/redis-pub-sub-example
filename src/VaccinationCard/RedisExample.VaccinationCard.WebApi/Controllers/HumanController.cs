using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedisExample.VaccinationCard.Messaging.RequestMessages;
using Controller = RedisExample.VaccinationCard.WebApi.Controllers.Default.Controller;

namespace RedisExample.VaccinationCard.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HumanController : Controller
    {
        public HumanController(ISender mediator) : base(mediator) { }

        [HttpGet, Route("id")]
        public async Task<IActionResult> CreateAsync([FromQuery] FindHumanByIdQuery query)
            => await SendAsync(query);
    }
}
