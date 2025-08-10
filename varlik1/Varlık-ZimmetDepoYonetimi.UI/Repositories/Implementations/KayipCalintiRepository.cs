using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class KayipCalintiRepository : GenericRepository<KayipCalinti>, IKayipCalintiRepository
    {
        public KayipCalintiRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<KayipCalinti>> GetAllAsync()
        {
            return await _dbSet
                .Include(k => k.Urun)
                    .ThenInclude(u => u.Model)
                .Include(x=>x.Zimmet)
                .ToListAsync();
        }

        public override async Task<KayipCalinti?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(k => k.Urun)
                    .ThenInclude(u => u.Model)
                    .Include(u => u.Zimmet)
                    .ThenInclude(x => x.AtananCalisan)
                .FirstOrDefaultAsync(k => k.KayipCalintiId == id);
        }

        public override async Task<IEnumerable<KayipCalinti>> GetAllAsync(Expression<Func<KayipCalinti, bool>> predicate)
        {
            return await _dbSet
                .Include(k => k.Urun)
                .ThenInclude(u => u.Model)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<KayipCalinti>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .Include(k => k.Zimmet)
                    .ThenInclude(z => z.Urun)
                        .ThenInclude(u => u.Model)
                .Include(k => k.Zimmet.AtananCalisan)
                .OrderByDescending(k => k.Tarih)
                .ToListAsync();
        }
        public async Task<IEnumerable<KayipCalinti>> GetKayıpCalıntıByCalisanIdAsync(int calisanId)
        {
            return await _context.KayipCalintis
                .Include(z => z.Zimmet)
                    .ThenInclude(u => u.Urun)
                    .ThenInclude(x => x.Model)
                .Include(x => x.Zimmet)
                .ThenInclude(x => x.AtananCalisan)
                .Where(z => z.AktifMi == true && z.Zimmet.AtananCalisanId == calisanId)
                .OrderByDescending(z => z.Tarih)
                .ToListAsync();
        }
    }
}