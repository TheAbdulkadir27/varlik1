using System.Linq.Expressions;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IKayipCalintiRepository : IGenericRepository<KayipCalinti>
    {
        Task<IEnumerable<KayipCalinti>> GetAllAsync();
        Task<IEnumerable<KayipCalinti>> GetAllAsync(Expression<Func<KayipCalinti, bool>> predicate);
        Task<IEnumerable<KayipCalinti>> GetAllWithDetailsAsync();
        Task<IEnumerable<KayipCalinti>> GetKayıpCalıntıByCalisanIdAsync(int calisanId);
    }
}