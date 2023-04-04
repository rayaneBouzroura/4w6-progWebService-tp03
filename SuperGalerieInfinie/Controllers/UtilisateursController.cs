using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using SuperGalerieInfinie.Models;

namespace SuperGalerieInfinie.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        readonly UserManager<Utilisateur> UtilisateurManager;

        public UtilisateursController(UserManager<Utilisateur> utilisateurManager)
        {
            this.UtilisateurManager = utilisateurManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            // Vérifier si les mots de passe correspondent
            if (register.Password != register.PasswordConfirm)
            {
                return BadRequest("Les mots de passe ne correspondent pas.");
            }

            // Créer un nouvel utilisateur
            var user = new Utilisateur
            {
                UserName = register.Username,
                Email = register.Email
            };

            // Ajouter l'utilisateur à la base de données avec le mot de passe
            var result = await UtilisateurManager.CreateAsync(user, register.Password);

            // Vérifier si la création a réussi
            if (result.Succeeded)
            {
                return Ok( new {Message = " wooohooo 😎😎"});
            }
            else
            {
                // La création a échoué, renvoyer un message d'erreur
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new {Message =   "La création de l'utilisateur a échoué." });
            }
        }
    }

    
}
