using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IYetkiService
    {
        Task<IEnumerable<Yetki>> GetAllAsync();
        Task<Yetki?> GetByIdAsync(int id);
        Task AddAsync(Yetki yetki);
        Task UpdateAsync(Yetki yetki);
        Task DeleteAsync(int id);
        Task<IEnumerable<Yetki>> GetAktifYetkilerAsync();
    }
}