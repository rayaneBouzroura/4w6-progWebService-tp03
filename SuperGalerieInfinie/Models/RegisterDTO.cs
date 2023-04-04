using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace SuperGalerieInfinie.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterDTO : ControllerBase
    {
        //nom d'utilisater , adresse courriel et mot de passe + conf
        [System.ComponentModel.DataAnnotations.Required]
        public string Username { get; set; } = null!;

        [System.ComponentModel.DataAnnotations.Required]
        public string Password { get; set; } = null!;

        [System.ComponentModel.DataAnnotations.Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [System.ComponentModel.DataAnnotations.Required]
        public string PasswordConfirm { get; set; }

    }
}
