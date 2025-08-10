using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IYetkiRepository : IGenericRepository<Yetki>
    {
        Task<IEnumerable<Yetki>> GetAktifYetkilerAsync();
    }
}