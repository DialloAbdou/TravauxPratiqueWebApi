using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApiTodoList.Data;
using WebApiTodoList.Data.Models;
using WebApiTodoList.Dto;

namespace WebApiTodoList.services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly TodoDbContext _contect;
        private readonly IMapper _mapper;

        public UtilisateurService(TodoDbContext context, IMapper mapper)
        {
            _contect = context;
            _mapper = mapper;
        }
        public async Task<UtilisateurOutput> CreateUserAsync(UtilisateurInput input)
        {
            Utilisateur? utilisateur = GetConvertUtilisateur(input);
            await _contect.AddAsync(utilisateur);
            await _contect.SaveChangesAsync();
            return _mapper.Map<Utilisateur,UtilisateurOutput>(utilisateur);
        }

        public async Task<IEnumerable<UtilisateurOutput>> GetAllUtilisateurAsync()
        {
           var utlisateurs = await _contect.Utilisateurs.ToListAsync();
            return _mapper.Map<IEnumerable<Utilisateur>, IEnumerable<UtilisateurOutput>>(utlisateurs);
        }
        private Utilisateur GetConvertUtilisateur(UtilisateurInput input)
        {
            return new Utilisateur
            {
                Nom = input.Nom,
                Token = GetAlphaNumerique()
            };
        }

        private string GetAlphaNumerique()
        {
            const string chaines = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] tabChar = new char[8];
            var random = new Random();
            for (int i = 0; i < chaines.Length; i++)
            {
                tabChar[i] = chaines[random.Next(chaines.Length)];
            }
            return new string(tabChar);
        }

      
    }
}
