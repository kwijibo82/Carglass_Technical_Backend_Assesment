using Microsoft.AspNetCore.Mvc;

namespace Carglass.TechnicalAssessment.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet("KeepAlive")]
        public IActionResult KeepAlive()
        {
            return Ok("API is running.");
        }
    }
}
