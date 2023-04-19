using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace SuperGalerieInfinie.Models
{
    public class Utilisateur : IdentityUser
    {


        // Propriété de navigation pour la relation many-to-many avec la classe Galerie
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual List<Galerie>? Galeries { get; set; }
    }
}
