using Microsoft.AspNetCore.Identity;
using SuperGalerieInfinie.Models;

namespace SuperGalerieInfinie.Data.Services
{
    public interface IGalerieService
    {
        /// <summary>
        /// retourne toutes les galeries du dbContext si galerie non vide et galerie publique slmnt
        /// </summary>
        /// <returns>toute les galleries publique</returns>
        Task<List<Galerie>> GetAllPublicGaleriesAsync();
        
        /// <summary>
        /// return une galerie specifique via id ensuite check si belong
        /// si belong ont la return niveau controller
        /// </summary>
        /// <param name="id">id de la galerie to return</param>
        /// <returns></returns>
        Task<Galerie?> GetGalerieByIdAsync(int id);


        //update galerie , check si belong a l'utilisateur
        Task<Galerie?>UpdateGalerieAsync( int id, Galerie galerie);
        /// <summary>
        /// Galerie qui va etre creer par un utilisteur
        /// </summary>
        /// <param name="galerie">galerie creer</param>
        /// /// <param name="utilisateur">utilisateur a qui ont atribue la galerie</param>
        /// <returns></returns>
        Task CreateGalerieAsync(Utilisateur utilisateur, Galerie galerie);
        //supp galerie si apartient a curr user...possiblement better
        //de simplement add un service dans galerie qui check si user proprio de gallerie..hmm
        Task DeleteGalerieAsync(Galerie galerie);
        Task<bool> GalerieExistsAsync(int id);
        bool IsGalerieEmpty();//check si dbset existe
        bool doesBelong(Utilisateur utilisateur,Galerie galerie);//check si la galerie apartient a l'utilisateur                      //
        
        
        //Task<IEnumerable<Galerie>> GetGaleriesAsync(Utilisateur utilisateur);
        
    
    
    }
}
