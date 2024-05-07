
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApiTodoList.Dto;
using WebApiTodoList.services;

var builder = WebApplication.CreateBuilder();

builder.Logging.ClearProviders(); // suppression de log par defaut

// ---- Configuration de logger ---

var loggerConfiguration = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt",rollingInterval:RollingInterval.Day);
var logger = loggerConfiguration.CreateLogger();
builder.Services.AddSerilog(logger);

//-----Configuration Validator -------------

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddScoped<TodoService> ();
var app = builder.Build();

app.MapGet("/todo", ( [FromServices] TodoService _todoservice,ILogger<Program> loger) =>
{
    List<TodoOutput>? _todos = _todoservice.GetAllTodo();
    loger.LogInformation("Hello Abdou Bienvenu sur les loggers");
    return Results.Ok(_todos);
});

app.MapGet("/todo/{id:int}", ([FromRoute] int id, [FromServices] TodoService _todoservice) =>
{
    var _todo = _todoservice.GetTodoById(id);
    if( _todo is not null) return  Results.Ok(_todo);
    return Results.NotFound();
});


app.MapPost("/todo", (  
    [FromBody]TodoInput input,
    [FromServices] TodoService service,
    [FromServices] IValidator<TodoInput> validator ) =>
{
    var result = validator.Validate(input);
    if(!result.IsValid)
    {
        return Results.BadRequest(result.Errors.Select(e => new
        {
            e.ErrorMessage,
            e.PropertyName
        }));
    }
    var _todo = service.AddTodo(input);
    return Results.Ok(_todo);   
});

app.MapPut("/todo/{id:int}",(
    [FromRoute]int id ,
    [FromBody] TodoInput input,
    [FromServices] IValidator<TodoInput> validator,
       [FromServices] TodoService service) =>
{
    var result = validator.Validate(input);
    if(!result.IsValid)
    {
        return Results.BadRequest(result.Errors.Select(e => new
        {
            e.ErrorMessage,
            e.PropertyName
        }));
    }
    var _todo = service.Update(input, id);
    return Results.Ok(_todo);
});
app.Run();