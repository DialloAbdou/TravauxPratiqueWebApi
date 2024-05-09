using Microsoft.AspNetCore.Components.Web;
using WebApiTodoList.Data.Models;
using WebApiTodoList.Dto;

namespace WebApiTodoList.services
{
    public class TodoService
    {
        private TodoOutput GetTodoOutput(Todo todo)
        {
            return new TodoOutput
                 (
                 Id: todo.Id,
                 Titre: todo.Titre,
                 DateDebut: todo.DateDebut,
                 DateFin: todo.DateFin
                );

        }

        private Todo GetTodo(TodoInput input)
        {
            var id = _todoList.Count + 1;
            return new Todo
            {
                Id = id,
                Titre = input.Titre,
                DateDebut = DateTime.Today,
                DateFin = input.DateFin,

            };
        }
        List<Todo> _todoList = new List<Todo>()
        {
            new Todo{ Id = 1, Titre = "Formation WebApi", DateDebut = DateTime.Today },
            new Todo{ Id = 2, Titre = "Formation Fondamenteau", DateDebut = DateTime.Today.AddDays(1) },
            new Todo{ Id = 3, Titre = "Formation WebApi", DateDebut = DateTime.Today .AddDays(2)},
            new Todo{ Id = 4, Titre = "Formation WebApi", DateDebut = DateTime.Today, DateFin = DateTime.Today.AddDays(2)},
        };
        public List<TodoOutput> GetAllTodo()
        {
            return _todoList.ConvertAll(GetTodoOutput);
        }

        public TodoOutput GetTodoById(int id)
        {
            var todo = _todoList.Find(x => x.Id == id);
            if (todo is not null)
            {
                var _todo = GetTodoOutput(todo);
                return _todo;
            }
            return null!;
        }

        public TodoOutput AddTodo(TodoInput input)
        {
            var _todo = GetTodo(input);
            _todoList.Add(_todo);
            return GetTodoOutput(_todo);

        }

        public TodoOutput Update(TodoInput input, int id)
        {
            var todo = _todoList.Find(t => t.Id == id);
            if (todo is not null)
            {
                todo.Titre = input.Titre;
                todo.DateFin = input.DateFin;
                var _todoUpdate = GetTodoOutput(todo);
                return _todoUpdate;
            };
            return null!;
        }

        public bool Delete(int id)
        {
            var todo = _todoList.Find(t => t.Id == id);
            if (todo is not null)
            {
                _todoList.Remove(todo);
                return true;
            }
            return false;
        }
    }
}
