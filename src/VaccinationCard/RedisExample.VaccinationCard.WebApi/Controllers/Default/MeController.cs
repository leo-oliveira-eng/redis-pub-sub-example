using Microsoft.AspNetCore.Mvc;

namespace RedisExample.VaccinationCard.WebApi.Controllers.Default
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { name = "Redis Example - Vaccination Card - Web API", version = "1.0.0" });
    }
}
