using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IDepoRepository : IGenericRepository<Depo>
    {
        Task<Depo?> GetByIdWithUrunlerAsync(int id);
    }
}