using Microsoft.AspNetCore.Mvc;

namespace MSOC.Backend.Controller;

[ApiController]
[Route("api")]
public class TestController : ControllerBase
{
    [HttpGet("healthcheck")]
    public IActionResult HealthCheck()
    {
        return Ok("I am OK");
    }
}