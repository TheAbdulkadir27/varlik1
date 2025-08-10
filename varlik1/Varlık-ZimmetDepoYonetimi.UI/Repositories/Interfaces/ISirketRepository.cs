using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface ISirketRepository : IGenericRepository<Sirket>
    {
        Task<string> Delete(int sirketid);
    }
}