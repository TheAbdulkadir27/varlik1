using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IKayipCalintiService
    {
        Task<IEnumerable<KayipCalinti>> GetAllAsync();
        Task<KayipCalinti?> GetByIdAsync(int id);
        Task AddAsync(KayipCalinti kayipCalinti);
        Task UpdateAsync(KayipCalinti kayipCalinti);
        Task DeleteAsync(int id);
        Task<IEnumerable<KayipCalinti>> GetKayipCalintiByUrunIdAsync(int urunId);
        Task<IEnumerable<KayipCalinti?>> GetKayıpCalıntıByCalisanIdAsync(int calisanId);
    }
}