namespace MSOC.Backend.Middleware;

public class AdminControllerAuthentication
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    
    private readonly string _authKey;

    public AdminControllerAuthentication(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
        
        _authKey = _configuration.GetValue<string>("API:Authorization")!;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // EDGE CASE: if no authorization configuration, exit immediately.
        if (!string.IsNullOrWhiteSpace(_authKey))
        {
            var auth = context.Request.Headers.Authorization;

            if (
                !string.IsNullOrWhiteSpace(auth) &&
                auth == $"Basic {_authKey}"
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
}