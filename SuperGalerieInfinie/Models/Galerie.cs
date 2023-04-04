﻿namespace SuperGalerieInfinie.Models
{
    public class Galerie
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string? Description { get; set; }
        public bool EstPublique { get; set; }

        // Propriété de navigation pour la relation many-to-many avec la classe Utilisateur
        public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = null!;
    }
}