using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IStatuRepository : IGenericRepository<Statu>
    {
        Task<IEnumerable<Urun>> GetAllUrunlerWithStatuAsync();
        Task ClearAllStatusAsync();
        Task ClearContextAsync();
        Task AddWithoutSaveAsync(Statu statu);
    }
}