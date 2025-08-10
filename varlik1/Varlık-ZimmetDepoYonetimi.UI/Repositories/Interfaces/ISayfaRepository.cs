using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface ISayfaRepository : IGenericRepository<Sayfa>
    {
        Task<IEnumerable<Sayfa>> GetAktifSayfalarAsync();
    }
}