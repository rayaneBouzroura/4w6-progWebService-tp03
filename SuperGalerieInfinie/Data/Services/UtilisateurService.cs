using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SuperGalerieInfinie.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperGalerieInfinie.Data.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly UserManager<Utilisateur> _userManager;

        public UtilisateurService(UserManager<Utilisateur> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> AddUserAsync(Utilisateur utilisateur, string password)
        {
            return await _userManager.CreateAsync(utilisateur, password);

        }

        public async Task<bool> CheckPasswordAsync(Utilisateur utilisateur, string password)
        {
            return await _userManager.CheckPasswordAsync(utilisateur, password);
        }

        public async Task<IList<Utilisateur>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IList<string>> GetRolesAsync(Utilisateur utilisateur)
        {
            return await _userManager.GetRolesAsync(utilisateur);
        }

        public async Task<Utilisateur> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<Utilisateur> GetUserByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<(bool, object)> LoginAsync(LoginDTO login)
        {
            //recup du user
            Utilisateur utilisateur = await this.GetUserByNameAsync(login.Username);

            if (utilisateur != null && await this.CheckPasswordAsync(utilisateur, login.Password))
            {
                IList<string> roles = await this.GetRolesAsync(utilisateur);//recup les roles
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
                    audience: "https://localhost:4200",
                    claims: authClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                    );
                //alright so ont va return un true indiquant that tout va bien woohoo et le token
                return (true,new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    ValidTo = token.ValidTo

                });//return json avec le token et la validation
            }
            else
            {
                return (false, new {Message = "nom d'util et pw invalid ou bien pas pu recup le user"});
            }
        }

        public async Task<IActionResult> RegisterAsync(RegisterDTO register)
        {
            if (register.Password != register.PasswordConfirm)
            {
                return new BadRequestObjectResult("Les mots de passe ne correspondent pas.");
            }

            var user = new Utilisateur
            {
                UserName = register.Username,
                Email = register.Email,
            };
            // Ajouter l'utilisateur à la base de données avec le mot de passe
            IdentityResult identityResult = await _userManager.CreateAsync(user, register.Password);
            // Vérifier si la création a réussi

            if (identityResult.Succeeded)
            {
                return new OkObjectResult(new { Message = "wooohooo 😎😎" });
            }
            else
            {
                // La création a échoué, renvoyer un message d'erreur (mais pas un status code bizzarement)
                return new BadRequestObjectResult(new { Message = "La création du compte a échoué.", Errors = identityResult.Errors });
            }
        }
    }
}
