using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace SuperGalerieInfinie.Models
{
    public class Galerie
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string? Description { get; set; }
        public bool EstPublique { get; set; }

        // Propriété de navigation pour la relation many-to-many avec la classe Utilisateur
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = null!;
    }
}
