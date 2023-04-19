using Microsoft.Build.Framework;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace SuperGalerieInfinie.Models
{
    public class Galerie
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }

        public string? Description { get; set; }
        [Required]
        public bool EstPublique { get; set; }

        // Propriété de navigation pour la relation many-to-many avec la classe Utilisateur
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual List<Utilisateur>? Utilisateurs { get; set; }
    }
}
