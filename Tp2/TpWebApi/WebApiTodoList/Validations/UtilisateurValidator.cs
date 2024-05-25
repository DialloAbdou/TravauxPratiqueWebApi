using FluentValidation;
using WebApiTodoList.Dto;

namespace WebApiTodoList.Validations
{
    public class UtilisateurValidator : AbstractValidator<UtilisateurInput>
    {
        public UtilisateurValidator()
        {
            RuleFor(u=>u.Nom).NotEmpty();
        }
    }
}
