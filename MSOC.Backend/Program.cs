using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MSOC.Backend.Service;

var builder = WebApplication.CreateBuilder(args);

// Add config file
builder.Configuration
    .AddJsonFile("config.json")
    .Build();

// :thinking:
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddHealthChecks();

// Add crapwares to the controller    
builder.Services
    .AddSingleton<MaimaiInquiryService>()
    .AddDbContext<GameDatabaseService>(o => o.UseSqlite(new SqliteConnection("Filename=MSOC.db;")))
    .AddDbContext<TrackDatabaseService>(o => o.UseSqlite(new SqliteConnection("Filename=tracks.db;")))
    .AddDbContext<SchoolDatabaseService>(o => o.UseSqlite(new SqliteConnection("Filename=schools.db;")))
    .AddRouting()
    .AddEndpointsApiExplorer()
    .AddHttpContextAccessor()
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MSOC API", Version = "v1" });
        c.IncludeXmlComments(Assembly.GetExecutingAssembly());
    });

var app = builder.Build();

// The best testing is to send the request on browser.
// or, God knows.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1);
    });
    app.UseDeveloperExceptionPage();
}

app
    .UseHealthChecks("/api/healthcheck")
    .UseHsts()
    .UseRouting()
    .UseHttpsRedirection()
    .UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

app.MapControllers();
app.Run();

public partial class Program
{
}