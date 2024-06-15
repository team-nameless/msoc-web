using Microsoft.AspNetCore.Mvc;

namespace MSOC.Backend.Api;

[ApiController]
[Route("[controller]")]
public class TestApi : ControllerBase
{
    [HttpGet("/healthcheck")]
    public IActionResult HealthCheck()
    {
        return Ok("I am OK");
    }
}