using System.Globalization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Service;

var builder = WebApplication.CreateBuilder(args);

// Add config file
builder.Configuration
    .AddIniFile("config.ini")
    .Build();

// Add all API controller classes.
// Don't know if Microsuck spaghetti code can do?
builder.Services.AddControllers();

// Add crapwares to the controller
builder.Services
    .AddSingleton<MaimaiInquiryService>()
    .AddDbContext<DatabaseService>(x =>
    {
        x
#if DEBUG
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
#endif
            .UseSqlite(new SqliteConnection(
                $"Data Source=MSOC.db;" +
                $"Password={builder.Configuration.GetValue<string>("Database:PASSWORD")};"
            ));
    })
    .AddRouting()
    .AddEndpointsApiExplorer()
    .AddHttpContextAccessor()
    .AddSwaggerGen()
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/api/user/login";
        opt.LogoutPath = "/api/user/logout";
    })
    .AddDiscord(opt =>
    {
        opt.ClientId = builder.Configuration.GetValue<string>("Discord:CLIENT_ID") ?? "";
        opt.ClientSecret = builder.Configuration.GetValue<string>("Discord:CLIENT_SECRET") ?? "";

        // https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers/issues/584
        opt.ClaimActions.MapCustomJson("urn:discord:avatar:url", user =>
            string.Format(
                CultureInfo.InvariantCulture,
                "https://cdn.discordapp.com/avatars/{0}/{1}.{2}",
                user.GetString("id"),
                user.GetString("avatar"),
                user.GetString("avatar")!.StartsWith("a_") ? "gif" : "png"));
    });

// Enable services at startup time.
builder.Services
    .ActivateSingleton<MaimaiInquiryService>();

var app = builder.Build();

// The best testing is to send the request on browser.
// or, God knows.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app
    .UseHsts()
    .UseRouting()
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

app.MapControllers();
app.Run();

public partial class Program
{
}