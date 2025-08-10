using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories
{
    public class YetkiRepository : GenericRepository<Yetki>, IYetkiRepository
    {
        public YetkiRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Yetki>> GetAktifYetkilerAsync()
        {
            return await _dbSet.Where(y => y.AktifMi == true).ToListAsync();
        }
    }
}