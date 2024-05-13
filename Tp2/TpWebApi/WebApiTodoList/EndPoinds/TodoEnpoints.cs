using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebApiTodoList.Dto;
using WebApiTodoList.services;

namespace WebApiTodoList.EndPoinds
{
    public static class TodoEnpoints
    {
        // prefice: todo
        public static RouteGroupBuilder MapTodoEndPoint(this RouteGroupBuilder group)
        {
            group.MapGet("", GetAll);

            group.MapGet("/{id:int}", GetById);

            group.MapPost("", AddTodoAsync);

            group.MapPut("/{id:int}", UpdateAsync);

            group.MapDelete("/{id:int}", DeleteAsync);


            return group;
        }

        /// <summary>
        ///  elle renvoie la liste des elements 
        /// </summary>
        /// <param name="_todoservice"></param>
        /// <returns></returns>
        private static async Task<IResult> GetAll(
            [FromServices] ITodoService _todoservice)
        {
            var _todos = await _todoservice.GetAllTodoAsync();
            return Results.Ok(_todos);
        }

        /// <summary>
        /// elle renvoie un todo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        private static async Task<IResult> GetById(
            [FromRoute] int id,
            [FromServices] ITodoService service)
        {
            var _todo = await service.GetTodoByIDAsync(id);
            if (_todo is not null) return Results.Ok(_todo);
            return Results.NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <param name="validator"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        private static async Task<IResult> AddTodoAsync(
                [FromBody] TodoInput input,
                [FromServices] IValidator<TodoInput> validator,
                [FromServices] ITodoService service)
        {
            var result = validator.Validate(input);
            if (!result.IsValid)
            {
                return Results.BadRequest(result.Errors.Select(e => new
                {
                    e.ErrorMessage,
                    e.PropertyName
                }));
            }
            var _todo = await service.AddTodoAsync(input);

            return Results.Ok(_todo);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <param name="validator"></param>
        /// <param name="service"></param>
        /// <returns></returns>

        private static async Task<IResult> UpdateAsync(
            [FromRoute] int id,
            [FromBody] TodoInput input,
            [FromServices] IValidator<TodoInput> validator,
            [FromServices] ITodoService service)
        {
            var result = validator.Validate(input);
            if (!result.IsValid)
            {
                return Results.BadRequest(result.Errors.Select(e => new
                {
                    e.ErrorMessage,
                    e.PropertyName
                }));
            }
            var _todo = await service.UpdateTodoAsync(id, input);
            return Results.Ok(_todo);
        }

        private static async Task<IResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] ITodoService service)
        {
            var result = await service.DeleteTodoAsync(id);
            if (result) return Results.Content("Supprimé!");
            return Results.NoContent();
        }
    }
}
