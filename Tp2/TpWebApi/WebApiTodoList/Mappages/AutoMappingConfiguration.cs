using AutoMapper;
using WebApiTodoList.Data.Models;
using WebApiTodoList.Dto;

namespace WebApiTodoList.Mappages
{
    public class AutoMappingConfiguration: Profile
    {
        public AutoMappingConfiguration()
        {
            CreateMap<Utilisateur, UtilisateurOutput>();
                
        }
    }
}
