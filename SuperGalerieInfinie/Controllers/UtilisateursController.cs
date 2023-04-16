using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.IdentityModel.Tokens;
using SuperGalerieInfinie.Data.Services;
using SuperGalerieInfinie.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperGalerieInfinie.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        //readonly UserManager<Utilisateur> UtilisateurManager;
        readonly UtilisateurService _utilisateurService;
        IConfiguration Config;

        public UtilisateursController(/*UserManager<Utilisateur>  utilisateurManager ,*/ IConfiguration configuration , UtilisateurService mutilisateurService)
        {
            //this.UtilisateurManager = utilisateurManager;
            this.Config = configuration;
            this._utilisateurService= mutilisateurService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            // Vérifier si les mots de passe correspondent
            if (register.Password != register.PasswordConfirm)
            {
                return BadRequest("Les mots de passe ne correspondent pas.");
            }
            return await _utilisateurService.RegisterAsync(register);

            //// Créer un nouvel utilisateur
            //var user = new Utilisateur
            //{
            //    UserName = register.Username,
            //    Email = register.Email,
            //};

            //IdentityResult identityResult = await _utilisateurService.AddUserAsync(user, register.Password);

            //if (identityResult.Succeeded)
            //{
            //    return Ok( new {Message = " wooohooo 😎😎"});
            //}
            //else
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError,
            //            new {Message =   "La création de l'utilisateur a échoué." });
            //}
            

        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO login)
        {
            
            //ehhh...we return un bool et un objet psk jsp comment send un actionresult avec l'erreur si ca fail
            var (isSuccessful, result) = await _utilisateurService.LoginAsync(login);

            if (isSuccessful)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }
        }
        [HttpPost]
        public async Task<IActionResult> hello()
        {
            return Ok(new {Message= "OK"});
        }
    }

    
}
