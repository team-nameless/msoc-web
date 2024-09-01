namespace MSOC.Backend.Middleware;

public class AdminControllerAuthentication
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public AdminControllerAuthentication(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authConfig = _configuration.GetValue<string>("API:Authorization");
        
        // EDGE CASE: if no authorization configuration, exit immediately.
        if (string.IsNullOrWhiteSpace(_configuration.GetValue<string>("API:Authorization")))
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return;
        }
        
        var auth = context.Request.Headers.Authorization;

        if (
            !string.IsNullOrWhiteSpace(auth) &&
            auth == $"Basic {authConfig}"
        )
        {
            await _next(context);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
    }
}