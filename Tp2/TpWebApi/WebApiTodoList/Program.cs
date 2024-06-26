﻿
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApiTodoList.Data;
using WebApiTodoList.Dto;
using WebApiTodoList.EndPoinds;
using WebApiTodoList.Mappages;
using WebApiTodoList.services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders(); // suppression de log par defaut

// ---- Configuration de logger ---

var loggerConfiguration = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt",rollingInterval:RollingInterval.Day);
var logger = loggerConfiguration.CreateLogger();
builder.Services.AddSerilog(logger);
//------------Mappage-------------------

builder.Services.AddAutoMapper(cf=>cf.AddProfile<AutoMappingConfiguration>());
//-----------Configuration DbContext For DB -----------------

builder.Services.AddDbContext<TodoDbContext>(op => op.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));
//builder.Services.AddDbContext<TodoDbContext>();
//-----Configuration Validator -------------

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddScoped< ITodoService, TodoService> ();
//builder.Services.AddMemoryCache();
//builder.Services.AddOutputCache(opt =>
//{
//    // les differents strategie des caches output
//    opt.AddBasePolicy(b=>b.Expire(TimeSpan.FromSeconds(2)));
//    opt.AddPolicy("Expire2mn", b => b.Expire(TimeSpan.FromMinutes(2)));
//    opt.AddPolicy("ById", b => b.SetVaryByRouteValue("id"));
//});

// Utilisation du Cache Distribuer
builder.Services.AddDistributedMemoryCache();
var app = builder.Build();
app.UseOutputCache();

app.MapGroup("/todo")
    .MapTodoEndPoint();
app.MapGroup("/user")
    .MapUserEndPoint();

//await app.Services
//    .CreateAsyncScope()
//    .ServiceProvider
//    .GetRequiredService<TodoDbContext>()
//    .Database.EnsureCreatedAsync();
await app.Services
    .CreateAsyncScope()
    .ServiceProvider
    .GetRequiredService<TodoDbContext>()
    .Database.MigrateAsync();

//app.MapGet("/todo",  async( [FromServices] ITodoService _todoservice,ILogger<Program> loger) =>
//{
//    var  _todos = await _todoservice.GetAllTodoAsync();
//    loger.LogInformation("Hello Abdou Bienvenu sur les loggers");
//    return Results.Ok(_todos);
//});

//app.MapGet("/todo/{id:int}", async ([FromRoute] int id, [FromServices] ITodoService _todoservice) =>
//{
//    var _todo = await _todoservice.GetTodoByIDAsync(id);
//    if( _todo is not null) return  Results.Ok(_todo);
//    return Results.NotFound();
//});


//app.MapPost("/todo", (  
//    [FromBody]TodoInput input,
//    [FromServices] ITodoService service,
//    [FromServices] IValidator<TodoInput> validator ) =>
//{
//    var result = validator.Validate(input);
//    if(!result.IsValid)
//    {
//        return Results.BadRequest(result.Errors.Select(e => new
//        {
//            e.ErrorMessage,
//            e.PropertyName
//        }));
//    }
//    var _todo = service.AddTodoAsync(input);
//    return Results.Ok(_todo);   
//});

//app.MapPut("/todo/{id:int}", async (
//    [FromRoute]int id ,
//    [FromBody] TodoInput input,
//    [FromServices] IValidator<TodoInput> validator,
//    [FromServices] ITodoService service) =>
//{
//    var result = validator.Validate(input);
//    if(!result.IsValid)
//    {
//        return Results.BadRequest(result.Errors.Select(e => new
//        {
//            e.ErrorMessage,
//            e.PropertyName
//        }));
//    }
//    var _todo =  await service.UpdateTodoAsync( id,input);
//    return Results.Ok(_todo);
//});

//app.MapDelete("/todo/{id:int}", 
//   async ([FromRoute] int id,[FromServices] ITodoService service) =>
//{
//    var result =  await service.DeleteTodoAsync(id);
//    if (result) return Results.Content("Supprimé!");
//    return Results.NoContent();
//});
app.Run();