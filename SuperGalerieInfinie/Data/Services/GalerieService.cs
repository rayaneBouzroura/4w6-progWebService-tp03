using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuperGalerieInfinie.Models;

namespace SuperGalerieInfinie.Data.Services
{
    public class GalerieService : IGalerieService
    {
        private readonly UserManager<Utilisateur> _userManager;
        private readonly SuperGalerieInfinieContext _context;
        public GalerieService(UserManager<Utilisateur> um , SuperGalerieInfinieContext context)
        {
            _userManager = um;
            _context = context;
        }
       

        public async Task DeleteGalerieAsync(Galerie galerie)
        {
            _context.Remove(galerie);
            await _context.SaveChangesAsync();
        }

        public bool doesBelong(Utilisateur utilisateur, Galerie galerie)
        {
            //check si utilisateur a galerie
            return utilisateur.Galeries.Contains(galerie);
        }

        public Task<bool> GalerieExistsAsync(int id)
        {
            throw new NotImplementedException();
        }
        //retourne toute les galerie  publique
        public async Task<List<Galerie>> GetAllPublicGaleriesAsync()
        {
             //filtrer la liste
             List<Galerie> galeriesFiltrer =await  _context.Galerie.Where(g => g.EstPublique).ToListAsync();
            return galeriesFiltrer;



        }

        public async Task<Galerie?> GetGalerieByIdAsync(int id)
        {
            return await _context.Galerie.FindAsync(id);
        }

        //public Task<IEnumerable<Galerie>> GetGaleriesAsync(Utilisateur utilisateur)
        //{
        //    return utilisateur.Galeries;
        //}

        public bool IsGalerieEmpty()
        {
            //verif si dbset galerie a etait init
            return _context.Galerie == null;
        }


        public async Task<Galerie?> UpdateGalerieAsync(int id, Galerie galerie)
        {
            // recup la galerie existante
            var existingGalerie = await _context.Galerie.FindAsync(id);
            if (existingGalerie == null)
            {
                return null;
            }
            //detach de cette entity
            _context.Entry(existingGalerie).State = EntityState.Detached;

            // Attach et update l'entite
            _context.Entry(galerie).State = EntityState.Modified;

            //try catch avec le save changes 
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if galerie exist pas
                if (!await _context.Galerie.AnyAsync(g => g.Id == id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return galerie;
        }

        public async Task CreateGalerieAsync(Utilisateur utilisateur, Galerie galerie)
        {

            // Check si liste utilisateurs ref non null dans galerie sinn pourra pas add
            if (galerie.Utilisateurs == null)
            {
                galerie.Utilisateurs = new List<Utilisateur>();
            }
            //fliperino les references des deux objets
            utilisateur.Galeries.Add(galerie);
            //la liste utilisateurs is null bizzarement , how do i init la liste...
            galerie.Utilisateurs.Add(utilisateur);            
            //add au dbcontext
            _context.Galerie.Add(galerie);
            //save
            await _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }

        public async Task PartagerGalerie(Galerie galerie, Utilisateur utilisateur)
        {
            // Update la ref des deux obj
            utilisateur.Galeries.Add(galerie);
            galerie.Utilisateurs.Add(utilisateur);
            // Save changes to la bd
            await _context.SaveChangesAsync();
      
        }
    }
}
