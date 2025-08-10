using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IEkipRepository : IGenericRepository<Ekip>
    {
        public Task<string> Delete(int ekipId);
    }
}