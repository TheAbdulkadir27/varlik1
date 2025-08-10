using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IMusteriService
    {
        Task<IEnumerable<Musteri>> GetAllMusteriAsync();
        Task<Musteri> GetMusteriByIdAsync(int id);
        Task AddMusteriAsync(Musteri musteri);
        Task UpdateMusteriAsync(Musteri musteri);
        Task DeleteMusteriAsync(int id);
    }
}
