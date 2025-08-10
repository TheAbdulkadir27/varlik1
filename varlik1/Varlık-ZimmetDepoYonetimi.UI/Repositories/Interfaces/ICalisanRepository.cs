using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface ICalisanRepository : IGenericRepository<Calisan>
    {
        Task<IEnumerable<Calisan>> GetAllWithDetailsAsync();
        Task<Calisan?> GetCalisanByUserIdAsync(string userId);
    }
}