using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface ISirketService
    {
        Task<IEnumerable<Sirket>> GetAllAsync();
        Task<Sirket?> GetByIdAsync(int id);
        Task AddAsync(Sirket sirket);
        Task UpdateAsync(Sirket sirket);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<Sirket>> GetAllSirketlerAsync();
    }
}