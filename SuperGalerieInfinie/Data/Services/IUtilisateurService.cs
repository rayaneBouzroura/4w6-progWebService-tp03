using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperGalerieInfinie.Models;

namespace SuperGalerieInfinie.Data.Services
{
    public interface IUtilisateurService
    {
        //bigger badderier methods
        Task<IActionResult> RegisterAsync(RegisterDTO register);
        Task<(bool, object)> LoginAsync(LoginDTO login);
        //ehh service bidon 
        Task<Utilisateur> GetUserByIdAsync(string id);
        Task<IList<Utilisateur>> GetAllUsersAsync();
        Task<IdentityResult> AddUserAsync(Utilisateur utilisateur, string password);
        Task<Utilisateur> GetUserByNameAsync(string username);
        Task<bool> CheckPasswordAsync(Utilisateur utilisateur, string password);
        Task<IList<string>> GetRolesAsync(Utilisateur utilisateur);
    }
}
