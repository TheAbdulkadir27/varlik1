using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class StokHareketRepository : GenericRepository<StokHareket>, IStokHareketRepository
    {
        public StokHareketRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }

        public async Task<List<StokHareket>> GetSonStokHareketleriAsync(int limit = 10)
        {
            return await _context.StokHareket
                .Include(sh => sh.Urun)
                    .ThenInclude(u => u.Model)
                .Where(sh => sh.AktifMi == true)
                .OrderByDescending(sh => sh.Tarih)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<List<StokHareket>> GetStokHareketleriByUrunIdAsync(int urunId)
        {
            return await _context.StokHareket
                .Include(sh => sh.Urun)
                    .ThenInclude(u => u.Model)
                .Where(sh => sh.UrunId == urunId && sh.AktifMi == true)
                .OrderByDescending(sh => sh.Tarih)
                .ToListAsync();
        }
    }
}