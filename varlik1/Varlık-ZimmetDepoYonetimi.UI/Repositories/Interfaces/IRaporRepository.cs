using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IRaporRepository : IGenericRepository<Rapor>
    {
        Task<IEnumerable<Rapor>> GetRaporlarByTarihAsync(DateTime baslangic, DateTime bitis);
    }
}