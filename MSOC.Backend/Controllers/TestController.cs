using Microsoft.AspNetCore.Mvc;

namespace MSOC.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("/healthcheck")]
    public IActionResult HealthCheck()
    {
        return Ok("I am OK");
    }
}