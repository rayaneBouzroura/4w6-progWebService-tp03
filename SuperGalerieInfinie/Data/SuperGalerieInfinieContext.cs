﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperGalerieInfinie.Models;

namespace SuperGalerieInfinie.Data
{
    public class SuperGalerieInfinieContext : IdentityDbContext<Utilisateur>
    {
        public IConfiguration cfg;
        public SuperGalerieInfinieContext (DbContextOptions<SuperGalerieInfinieContext> options , IConfiguration Config)
            : base(options)
        {
            cfg = Config;
        }

        public DbSet<SuperGalerieInfinie.Models.Galerie> Galerie { get; set; } = default!;

        //LETS SEED WOOHOOO
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Créer un password hasher pour utilisateur
            var hasher = new PasswordHasher<Utilisateur>();

            // Création des deux utilisateurs
            var u1 = new Utilisateur
            {
                Id = "11111111-1111-1111-1111-111111111111",
                UserName = "MisterFluffy",
                Email = "fluffy@gmail.com",
                NormalizedUserName = "MISTERFLUFFY",
                NormalizedEmail = "FLUFFY@GMAIL.COM",
            };
            u1.PasswordHash = hasher.HashPassword(u1, "pass1");

            var u2 = new Utilisateur
            {
                Id = "22222222-2222-2222-2222-222222222222",
                UserName = "CaptainNemo",
                Email = "nemo@gmail.com",
                NormalizedUserName = "CAPTAINNEMO",
                NormalizedEmail = "NEMO@GMAIL.COM",
               
            };
            u2.PasswordHash = hasher.HashPassword(u2, "pass1");
            //les galleries
            var g1 = new Galerie
            {
                Id = 1,
                Nom = "La galerie des chats",
                Description = "Une galerie remplie de photos de chats.",
                EstPublique = true
            };

            var g2 = new Galerie
            {
                Id = 2,
                Nom = "La galerie des chiens",
                Description = "Une galerie remplie de photos de chiens.",
                EstPublique = false
            };

            builder.Entity<Galerie>().HasData(g1, g2);

            // Mapping de la relation many-to-many entre Utilisateur et Galerie
            builder.Entity<Utilisateur>()
                   .HasMany(u => u.Galeries)
                   .WithMany(g => g.Utilisateurs)
                   .UsingEntity(e =>
                   {
                       e.HasData(new { UtilisateursId = u1.Id ,GalerieId = u1});
                   });

            //creer un password hasher pour utilisateur
            //creation des deux utilisateur avec ID format guid , un username , email et mot de passe rando et farfelu houhouhou
            //sans oublier de mettre le PasswordHash de l'utilisateur (clefs secrete : this.cfg["JWT:Secret"]; )
            //suivi par builder.entity<User>().has data et l'objet creer en parametre

            //suivit de la creation des deux galleries une pour chaque utilisateur une prive et une publique

            //suivit du mapping de entity many to many donc .hasmany .withmany et usingentity avec lambda fonction qui cree 4 hasdata entre u1 u2 et les deux galleries

        }
    }
}
