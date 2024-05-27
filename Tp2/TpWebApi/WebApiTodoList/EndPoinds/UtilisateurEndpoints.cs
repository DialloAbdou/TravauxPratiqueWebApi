using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebApiTodoList.Dto;
using WebApiTodoList.services;

namespace WebApiTodoList.EndPoinds
{
    public static class UtilisateurEndpoints
    {

        public static RouteGroupBuilder MapUserEndPoint(this RouteGroupBuilder group)
        {
            group.MapPost("", AddUserAsync);
            group.MapGet("", GetUSersAsync);
            return group;
        }

        private static async Task<IResult> AddUserAsync
            (
            [ FromBody] UtilisateurInput input,
            [FromServices] IUtilisateurService service,
            [FromServices] IValidator<UtilisateurInput> validator)
        {
            var resutl = validator.Validate(input);
            if(!resutl.IsValid )
            {
                return Results.BadRequest(resutl.Errors.Select(e => new
                {
                    e.ErrorMessage,
                    e.PropertyName
                }));

                
            }
            var utlisateur = await service.CreateUserAsync(input);
            return Results.Ok(utlisateur);
        }

        private static async Task<IResult> GetUSersAsync(
            [FromServices] IUtilisateurService service
            )
        {
            var utilisateurs = await service.GetAllUtilisateurAsync();
            return Results.Ok(utilisateurs);
        }
    }
}
