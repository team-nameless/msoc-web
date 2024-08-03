using Microsoft.AspNetCore.Mvc;
using MSOC.Backend.Controller;

namespace MSOC.Backend.Tests;

public class HealthCheckApiTest
{
    /// <summary>
    ///     Smoke test(n):
    ///     preliminary testing or sanity testing to reveal simple failures severe enough to,
    ///     for example, reject a prospective software release.
    /// </summary>
    [Fact]
    public void ApiSmokeTest()
    {
        var healthCheckController = new TestController();
        var result = healthCheckController.HealthCheck();

        Assert.NotNull(result);
        Assert.Multiple(() =>
        {
            Assert.IsType<OkObjectResult>(result);
            Assert.StrictEqual((result as OkObjectResult)?.Value, "I am OK");
        });
    }
}