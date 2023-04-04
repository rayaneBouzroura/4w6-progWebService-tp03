using Microsoft.AspNetCore.Identity;

namespace SuperGalerieInfinie.Models
{
    public class Utilisateur : IdentityUser
    {


        // Propriété de navigation pour la relation many-to-many avec la classe Galerie
        public virtual ICollection<Galerie> Galeries { get; set; } = null;
    }
}
