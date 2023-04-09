using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace SuperGalerieInfinie.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterDTO 
    {
        //nom d'utilisater , adresse courriel et mot de passe + conf
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordConfirm { get; set; } = null!;

    }
}
