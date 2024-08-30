using Microsoft.AspNetCore.Mvc;

namespace MSOC.Backend.Controller;

[ApiController]
[Route("api")]
public class TestController : ControllerBase
{
    /// <summary>
    ///     Send a GET to this. If it returns nothing, pray.
    /// </summary>
    [HttpGet("healthcheck")]
    public IActionResult HealthCheck()
    {
        return Ok("I am OK");
    }
}