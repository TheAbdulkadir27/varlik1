using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IRaporService
    {
        Task<Dictionary<string, int>> GetDepartmanBazliZimmetlerAsync();
        Task<List<StokHareket>> GetSonStokHareketleriAsync();
    }
}