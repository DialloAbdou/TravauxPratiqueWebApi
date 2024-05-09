using WebApiTodoList.Dto;

namespace WebApiTodoList.services
{
    public interface ITodoService
    {

        Task<List<TodoOutput>> GetAllTodoAsync();
        Task<TodoOutput> GetTodoByIDAsync(int id);
        Task<TodoOutput> AddTodoAsync(TodoInput input);
        Task<bool> UpdateTodoAsync( int id , TodoInput input);
        Task<bool> DeleteTodoAsync(int id);
    }
}
