using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperGalerieInfinie.Data;
using SuperGalerieInfinie.Data.Services;
using SuperGalerieInfinie.Models;

namespace SuperGalerieInfinie.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class GaleriesController : ControllerBase
    {

        UserManager<Utilisateur> _utilisateurManager;
        GalerieService _galerieService;
        UtilisateurService _utilisateurService;
        private readonly SuperGalerieInfinieContext _context;

        public GaleriesController(UtilisateurService mUtilisateurService, SuperGalerieInfinieContext context, UserManager<Utilisateur> utilisateurManager, GalerieService mgalerieService)
        {
            _context = context;
            //_utilisateurManager = utilisateurManager;
            _galerieService = mgalerieService;
            _utilisateurService = mUtilisateurService;
        }
        //recup tt les galerie du current user
        // GET: api/Galeries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Galerie>>> GetUserGaleries()
        {
            //recup du curent user (le mauvais current user is currently being sent jsp pk ca indique un autre user
            Utilisateur? utilisateurActuelle = await _utilisateurService.GetUserByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //check si liste de galerie n'est pas empty
            if (_galerieService.IsGalerieEmpty() || utilisateurActuelle == null)
            {
                return NotFound();
            }

            if (_context.Galerie == null)
            {
                return NotFound();
            }
            IEnumerable < Galerie > listTempo = await _context.Galerie.Where(g => g.Utilisateurs.Contains(utilisateurActuelle)).ToListAsync();


            return utilisateurActuelle.Galeries;
        }




        // GET: api/Galeries
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Galerie>>> GetAllPublicGaleries()
        {
            //check si galerie existe
            if (_galerieService.IsGalerieEmpty())
            {
                return NotFound();
            }
            //recup de tt les gal publique
            List<Galerie> galPublique = await _galerieService.GetAllPublicGaleriesAsync();
            //renvoi de tt les galeries publique
            return galPublique;

        }





        // GET: api/Galeries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Galerie>> GetGalerie(int id)
        {
          if (_context.Galerie == null)
          {
              return NotFound();
          }
            var galerie = await _context.Galerie.FindAsync(id);

            if (galerie == null)
            {
                return NotFound();
            }

            return galerie;
        }





        // PUT: api/Galeries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGalerie(int id, Galerie galerie)
        {
            //recup l'utilisateur connecter via token
            Utilisateur? utilisateur = await _utilisateurService.
                                             GetUserByIdAsync(
                                                User.FindFirstValue(ClaimTypes.NameIdentifier)
                                             );
            
            //check qu'ont modif la bonne galerie
            if (id != galerie.Id)
            {
                return BadRequest();
            }
            
            //recup l'ancien galerie
            Galerie? oldGalerie = await _galerieService.GetGalerieByIdAsync(id);
            
            //check que user et vieille galerie existe pi que la liste de gal
            //du db context existe
            if(utilisateur == null || _galerieService.IsGalerieEmpty() || oldGalerie == null)
            {
                return NotFound();
            }
            
            //check si user proprio du comm
            if(!_galerieService.doesBelong(utilisateur , oldGalerie))
            {
                return Unauthorized(new { Message = "tu peux pas modif vu qu'elle ne t'arpartient pas" });
            }
            //recup du new updated galerie
            Galerie? newGalerie = await _galerieService.UpdateGalerieAsync(id, galerie);
            if(newGalerie == null)
            {
                return NotFound();
            }
            return Ok(new { Message = "Galerie modifier" });
            
        }



        // POST: api/Galeries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Galerie>> PostGalerie(Galerie galerie)
        {
            //check si liste de galerie pas null
            if(_context.Galerie == null)
            {
                return Problem("Entity set Galerie is null");
            }
            //recup de l'id du current user
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //recup l'utilisateur connecter via token
            Utilisateur? utilisateur = await _utilisateurService.
                                             GetUserByIdAsync(
                                                User.FindFirstValue(
                                                    ClaimTypes.NameIdentifier)
                                             );

            if (utilisateur == null || _galerieService.IsGalerieEmpty() )
            {
                return NotFound();
            }
            //conv le dto en objet galerie
            //Galerie galerie = new Galerie
            //{
            //    Id = 0,
            //    Nom = galerieDTO.Nom,
            //    Description = galerieDTO.Description,
            //    EstPublique = galerieDTO.EstPublique,
            //    Utilisateurs = new List<Utilisateur>()
            //};
            await _galerieService.CreateGalerieAsync(utilisateur, galerie);
            return CreatedAtAction("GetGalerie", new { id = galerie.Id }, galerie);
        }



        // DELETE: api/Galeries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGalerie(int id)
        {
            //user qui fait la req
            Utilisateur? utilisateur =await  _utilisateurService.GetUserByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            //comm a supp(met var to make sure it nevers gets null )
            var galerie = await _galerieService.GetGalerieByIdAsync(id);
            //si dbset , user et galerie were not trouver return objet notfound();
            if(_galerieService.IsGalerieEmpty() || galerie == null || utilisateur == null)
            {
                return NotFound();
            }
            //check si utilisateur proprio
            bool isProprio = _galerieService.doesBelong(utilisateur, galerie);
            if (!isProprio)
            {
                return Unauthorized(new { Message = "la galerie do not belong a l'utilisateur actuel" });
            }
            //supp le comm via service
            await _galerieService.DeleteGalerieAsync(galerie);
            return Ok(new {Message = "Galerie supprime" });
        }

        private bool GalerieExists(int id)
        {
            return (_context.Galerie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
