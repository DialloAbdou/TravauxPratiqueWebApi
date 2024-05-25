namespace WebApiTodoList.Data.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public required string Nom { get; set; } = string.Empty;
        public  required string Token { get; set; } = string.Empty;
        public List<Todo> ?Todos { get; set; }
    }
}
