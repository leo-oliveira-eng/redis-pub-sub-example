﻿using Microsoft.AspNetCore.Mvc;
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
    }
}