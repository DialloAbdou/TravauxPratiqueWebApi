using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApiTodoList.Data;
using WebApiTodoList.Dto;

namespace WebApiTodoList.services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly TodoDbContext _contect;

        public UtilisateurService( TodoDbContext context)
        {
            _contect = context;
        }
        public Task<UtilisateurOutput> CreateUserAsync(UtilisateurInput input)
        {
            throw new NotImplementedException();
        }

        public Task<UtilisateurOutput> GetAllUtilisateur()
        {
            throw new NotImplementedException();
        }
    }
}
