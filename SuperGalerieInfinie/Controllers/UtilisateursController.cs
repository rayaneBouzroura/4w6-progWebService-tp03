using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.IdentityModel.Tokens;
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
                Email = register.Email,
            };

            // Ajouter l'utilisateur à la base de données avec le mot de passe
            IdentityResult identityResult = await UtilisateurManager.CreateAsync(user, register.Password);

            // Vérifier si la création a réussi
            if (identityResult.Succeeded)
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

        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO login)
        {
            //recup du user
            Utilisateur utilisateur = await UtilisateurManager.FindByNameAsync(login.Username);

            if(utilisateur != null && await UtilisateurManager.CheckPasswordAsync(utilisateur , login.Password))
            {
                IList<string> roles = await UtilisateurManager.GetRolesAsync(utilisateur);//recup les roles
                List<Claim> authClaims = new List<Claim>();
                foreach (string role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));//met le claim dans la liste authClaims tadaa
                }
                authClaims.Add(new Claim(ClaimTypes.NameIdentifier, utilisateur.Id));
                SymmetricSecurityKey key = new SymmetricSecurityKey(
                                           Encoding.UTF8.GetBytes("phrase suppppeeRR Long woooHoo Heck yeaaah je deprime."));
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: "https://localhost:7008",
                    audience : "https://localhost:4200",
                    claims: authClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    ValidTo = token.ValidTo

                });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = "nom d'utilisateur et pw invalid" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> hello()
        {
            return Ok(new {Message= "OK"});
        }
    }

    
}
