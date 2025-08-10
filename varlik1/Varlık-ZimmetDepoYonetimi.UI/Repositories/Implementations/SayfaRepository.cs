using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories
{
    public class SayfaRepository : GenericRepository<Sayfa>, ISayfaRepository
    {
        public SayfaRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Sayfa>> GetAktifSayfalarAsync()
        {
            return await _dbSet.Where(s => s.AktifMi == true).ToListAsync();
        }
    }
}