namespace WebApiTodoList.Dto
{
    public record TodoOutput
    (
        int Id,
        string Titre,
        DateTime DateDebut,
        DateTime? DateFin
    );
}
