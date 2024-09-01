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
        var auth = context.Request.Headers.Authorization;

        if (
            !string.IsNullOrWhiteSpace(auth) &&
            auth == $"Basic {_configuration.GetValue<string>("API:Authorization")}"
        )
        {
            await _next(context);
        }
        else
        {
            context.Response.StatusCode = 401;
        }
    }
}