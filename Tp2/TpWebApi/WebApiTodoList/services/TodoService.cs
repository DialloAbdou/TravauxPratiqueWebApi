using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using WebApiTodoList.Data;
using WebApiTodoList.Data.Models;
using WebApiTodoList.Dto;

namespace WebApiTodoList.services
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;

        public TodoService(TodoDbContext context)
        {
            _context = context;
        }
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

        private Todo GetTodo(TodoInput input, string token)
        {
            return new Todo
            {

                Titre = input.Titre,
                DateDebut = DateTime.Today,
                DateFin = input.DateFin,
                // ajouter le token
                UtilisateurToken = token

            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<TodoOutput>> GetAllTodoAsync()
        {
            return (await _context.Todos.ToListAsync()).ConvertAll(GetTodoOutput);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TodoOutput> GetTodoByIDAsync(int id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
            if (todo is not null) return GetTodoOutput(todo);
            return null!;
        }

        public async Task<TodoOutput> AddTodoAsync(TodoInput input, string token)
        {
            var todo = GetTodo(input, token);
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return GetTodoOutput(todo);
        }

        public async  Task<bool> UpdateTodoAsync(int id, TodoInput input)
        {
            var result = await _context.Todos.Where(t => t.Id == id)
                .ExecuteUpdateAsync(t => t
                .SetProperty(t => t.Titre , input.Titre)
                .SetProperty(t=>t.DateFin, input.DateFin));
            return result > 0;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            var result = await _context.Todos.Where(t=>t.Id==id).ExecuteDeleteAsync();
            return result > 0;
        }
    }
}
