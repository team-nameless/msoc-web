using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Service;

var builder = WebApplication.CreateBuilder(args);

// Add config file
builder.Configuration
    .AddJsonFile("config.json")
    .Build();

// :thinking:
builder.Services.AddControllers();
builder.Services.AddSignalR();

// Add crapwares to the controller    
builder.Services
    .AddSingleton<MaimaiInquiryService>()
    .AddDbContext<GameDatabaseService>(o => o.UseSqlite(new SqliteConnection("Filename=MSOC.db;")))
    .AddDbContext<TrackDatabaseService>(o => o.UseSqlite(new SqliteConnection("Filename=tracks.db;")))
    .AddDbContext<SchoolDatabaseService>(o => o.UseSqlite(new SqliteConnection("Filename=schools.db;")))
    .AddRouting()
    .AddEndpointsApiExplorer()
    .AddHttpContextAccessor()
    .AddSwaggerGen();

// Enable services at startup time.
builder.Services
    .ActivateSingleton<IConfiguration>()
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
    .UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

app.MapControllers();
app.Run();

public partial class Program
{
}