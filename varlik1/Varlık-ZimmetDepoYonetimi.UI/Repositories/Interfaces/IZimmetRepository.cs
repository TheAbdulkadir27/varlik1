using System.Linq.Expressions;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IZimmetRepository : IGenericRepository<Zimmet>
    {
        Task<int> GetTotalUrunCountAsync();
        new Task<IEnumerable<Zimmet>> GetAllAsync();
        Task<IEnumerable<Zimmet>> GetAllAsync(Expression<Func<Zimmet, bool>> predicate);
        Task<IEnumerable<Zimmet>> GetAllWithDetailsAsync();
        Task<IEnumerable<Zimmet>> GetZimmetlerByCalisanIdAsync(int calisanId);
    }
}