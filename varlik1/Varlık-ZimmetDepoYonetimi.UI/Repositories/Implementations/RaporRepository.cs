using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class RaporRepository : GenericRepository<Rapor>, IRaporRepository
    {
        public RaporRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Rapor>> GetRaporlarByTarihAsync(DateTime baslangic, DateTime bitis)
        {
            return await _dbSet
                .Where(r => r.Tarih >= baslangic && r.Tarih <= bitis)
                .ToListAsync();
        }
    }
}