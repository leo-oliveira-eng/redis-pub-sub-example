using Microsoft.AspNetCore.Mvc;
using RedisExample.Registration.Application.Services.Contracts;
using RedisExample.Registration.Messaging.RequestMessages;
using Controller = RedisExample.Registration.WebApi.Controllers.Base.Controller;

namespace RedisExample.Registration.WebApi.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class HumanController : Controller
    {
        IHumanApplicationService HumanApplicationService { get; }

        public HumanController(IHumanApplicationService humanApplicationService)
        {
            HumanApplicationService = humanApplicationService ?? throw new ArgumentNullException(nameof(humanApplicationService));
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateHumanRequestMesssage requestMessage)
            => await WithResponseAsync(() => HumanApplicationService.CreateAsync(requestMessage));

        [HttpPut, Route("{humanId}/Pet")]
        public async Task<IActionResult> CreatePetAsync([FromBody] CreatePetRequestMessage requestMessage, Guid humanId)
            => await WithResponseAsync(() => HumanApplicationService.CreatePetAsync(requestMessage, humanId));

        [HttpPut, Route("{humanId}/pet/{petId}/vaccine")]
        public async Task<IActionResult> AddVaccineAsync([FromBody] VaccineRequestMessage requestMessage, Guid humanId, Guid petId)
            => await WithResponseAsync(() => HumanApplicationService.AddVaccineAsync(humanId, petId, requestMessage));

        [HttpDelete, Route("{humanId}")]
        public async Task<IActionResult> DeleteAsync(Guid humanId)
            => await WithResponseAsync(() => HumanApplicationService.DeleteAsync(humanId));
    }
}
