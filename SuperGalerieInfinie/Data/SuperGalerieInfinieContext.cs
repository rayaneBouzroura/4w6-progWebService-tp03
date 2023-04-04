using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperGalerieInfinie.Models;

namespace SuperGalerieInfinie.Data
{
    public class SuperGalerieInfinieContext : IdentityDbContext<Utilisateur>
    {
        public SuperGalerieInfinieContext (DbContextOptions<SuperGalerieInfinieContext> options)
            : base(options)
        {
        }

        public DbSet<SuperGalerieInfinie.Models.Galerie> Galerie { get; set; } = default!;
    }
}
