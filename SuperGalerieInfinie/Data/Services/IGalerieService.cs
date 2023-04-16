using Microsoft.AspNetCore.Identity;
using SuperGalerieInfinie.Models;

namespace SuperGalerieInfinie.Data.Services
{
    public interface IGalerieService
    {
        Task<IEnumerable<Galerie>> GetGalerieAsync(Utilisateur utilisateur);
        Task<Galerie> GetGalerieByIdAsync(int id);
        Task<bool> UpdateGalerieAsync(int id, Galerie galerie);
        Task<Galerie> CreateGalerieAsync(Galerie galerie);
        Task<bool> DeleteGalerieAsync(int id);
        Task<bool> GalerieExistsAsync(int id);
        bool IsGalerieEmpty();//check si dbset existe                                                                                                                                                               
    }
}
