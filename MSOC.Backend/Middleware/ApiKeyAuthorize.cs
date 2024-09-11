namespace MSOC.Backend.Middleware;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ApiKeyAuthorize : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var extractedApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var apiKey = extractedApiKey.ToString().Replace("Basic ", "");
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var validApiKey = configuration.GetValue<string>("API:Authorization")!;

        if (apiKey != validApiKey)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        await Task.CompletedTask;
    }
}