using FluentValidation;
using WebApiTodoList.Dto;

namespace WebApiTodoList.Validations
{
    public class TodoValidator: AbstractValidator<TodoInput>
    {
        public TodoValidator()
        {
            RuleFor(t => t.Titre).NotEmpty();
            RuleFor(t => t.DateFin).GreaterThanOrEqualTo(DateTime.Now);
        }
    }
}
