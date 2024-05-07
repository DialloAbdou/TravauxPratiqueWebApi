namespace WebApiTodoList.Dto
{
    public class TodoInput
    {
        public string Titre { get; set; } = string.Empty;
        public DateTime ? DateFin  { get; set; }

    }
}
