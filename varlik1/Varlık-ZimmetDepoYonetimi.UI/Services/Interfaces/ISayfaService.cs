using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface ISayfaService
    {
        Task<IEnumerable<Sayfa>> GetAllAsync();
        Task<Sayfa> GetByIdAsync(int id);
        Task AddAsync(Sayfa sayfa);
        Task UpdateAsync(Sayfa sayfa);
        Task DeleteAsync(int id);
        Task<IEnumerable<Sayfa>> GetAktifSayfalarAsync();
    }
}