namespace WebApiTodoList.Data.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public required DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public int UtilisateurId { get; set; }
        public required Utilisateur Utilisateur { get; set; }
    }
}
