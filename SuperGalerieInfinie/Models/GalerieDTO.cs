using System.Text.Json.Serialization;

namespace SuperGalerieInfinie.Models
{
    public class GalerieDTO
    {
        //public int Id { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string? Description { get; set; } 
        public bool EstPublique { get; set; }
    }
}
