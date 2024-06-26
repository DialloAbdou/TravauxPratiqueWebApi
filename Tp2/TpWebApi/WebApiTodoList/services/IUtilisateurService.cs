﻿using WebApiTodoList.Dto;

namespace WebApiTodoList.services
{
    public interface IUtilisateurService
    {
        Task<UtilisateurOutput> CreateUserAsync(UtilisateurInput input);
        Task<IEnumerable<UtilisateurOutput>> GetAllUtilisateurAsync();
    }
}
