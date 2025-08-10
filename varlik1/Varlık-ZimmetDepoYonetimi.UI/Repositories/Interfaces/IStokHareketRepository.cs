using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IStokHareketRepository : IGenericRepository<StokHareket>
    {
        Task<List<StokHareket>> GetSonStokHareketleriAsync(int limit = 10);
        Task<List<StokHareket>> GetStokHareketleriByUrunIdAsync(int urunId);
    }
}