using Microsoft.AspNetCore.Mvc;

namespace RedisExample.Registration.WebApi.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { name = "Redis Example - Registration - Web API", version = "1.0.0" });
    }
}
