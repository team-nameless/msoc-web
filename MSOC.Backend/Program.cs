var builder = WebApplication.CreateBuilder(args);

// Add all API controller classes.
// Don't know if Microsuck spaghetti code can do?
builder.Services.AddControllers();

// Add crapwares to the controller
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddAuthentication()
    .AddDiscord(opt =>
    {
        opt.ClientId = "owo";
        opt.ClientSecret = "uwu";
    });

var app = builder.Build();

// The best testing is to send the request on browser.
// or, God knows.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .UseRouting()
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();
app.Run();
