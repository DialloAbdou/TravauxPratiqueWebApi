
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder();

builder.Logging.ClearProviders(); // suppression de log par defaut

// ---- Configuration de logger ---

var loggerConfiguration = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt",rollingInterval:RollingInterval.Day);
var logger = loggerConfiguration.CreateLogger();
builder.Services.AddSerilog(logger);

var app = builder.Build();

app.MapGet("/hello", (ILogger<Program> loger) =>
{
    loger.LogInformation("Hello Abdou Bienvenu sur les loggers");
    return Results.Ok("Hello Abdou");
});

app.MapGet("/hello/{nom}", ([FromRoute] string nom, [FromServices] ILogger<Program> loger) =>
{
    loger.LogInformation("Hello {nom} Bienvenu sur les loggers", nom);
    return Results.Ok($"Hello {nom}");
});
app.Run();