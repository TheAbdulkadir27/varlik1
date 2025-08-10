using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface ITaxRepository : IGenericRepository<Tax>
    {
        Task<string> Delete(Tax entity);
    }
}
