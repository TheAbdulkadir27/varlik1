using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IStokHareketService
    {
        Task<List<StokHareket>> GetSonStokHareketleriAsync(int limit = 10);
        Task<List<StokHareket>> GetStokHareketleriByUrunIdAsync(int urunId);
        Task AddStokHareketAsync(StokHareket stokHareket);
        Task UpdateStokHareketAsync(StokHareket stokHareket);
        Task DeleteStokHareketAsync(int id);
    }
}