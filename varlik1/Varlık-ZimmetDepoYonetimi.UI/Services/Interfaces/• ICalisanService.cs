using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Services.Interfaces
{
    public interface ICalisanService
    {
        Task<IEnumerable<Calisan>> GetAllCalisanlarAsync();
        Task<Calisan> GetCalisanByIdAsync(int id);
        Task AddCalisanAsync(Calisan calisan);
        Task UpdateCalisanAsync(Calisan calisan);
        Task<string> DeleteCalisanAsync(int id);
        Task<IEnumerable<SelectListItem>> GetCalisanSelectListAsync();
        Task<Calisan?> GetCalisanByUserIdAsync(string userId);

    }
}