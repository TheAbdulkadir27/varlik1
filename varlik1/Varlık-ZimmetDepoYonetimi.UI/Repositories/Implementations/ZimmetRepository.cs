using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class ZimmetRepository : IZimmetRepository
    {
        private readonly VarlikZimmetEnginContext _context;

        public ZimmetRepository(VarlikZimmetEnginContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalUrunCountAsync()
        {
            return await _context.Zimmet.SumAsync(z => z.Urun != null ? 1 : 0);
        }

        public async Task<IEnumerable<Zimmet>> GetAllAsync()
        {
            return await _context.Zimmet
                .Include(z => z.Urun)
                    .ThenInclude(u => u.Model)
                .Include(z => z.AtananCalisan)
                .ThenInclude(x => x.Ekip)
                .Include(z => z.AtayanCalisan)
                .Include(z => z.Urun)
                .ThenInclude(z => z.KayipCalintis)
                .Where(z => z.AktifMi == true)
                .OrderByDescending(z => z.ZimmetTarihi)
                .ToListAsync();
        }

        public async Task<IEnumerable<Zimmet>> GetAllAsync(Expression<Func<Zimmet, bool>> predicate)
        {
            return await _context.Zimmet.Where(predicate).ToListAsync();
        }

        public async Task<Zimmet> GetByIdAsync(int id)
        {
            return await _context.Zimmet
                .Include(z => z.Urun)
                .ThenInclude(x => x.Model)
                .Include(z => z.AtananCalisan)
                .Include(z => z.AtayanCalisan)
                .Include(z => z.Urun)
                .ThenInclude(x => x.KayipCalintis)
                .AsNoTracking()
                .FirstOrDefaultAsync(z => z.ZimmetId == id);
        }

        public async Task AddAsync(Zimmet entity)
        {
            await _context.Zimmet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(Zimmet entity)
        {
            _context.Zimmet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Zimmet entity)
        {
            _context.Zimmet.Remove(entity);
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public VarlikZimmetEnginContext GetContext()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Zimmet>> GetZimmetlerByCalisanIdAsync(int calisanId)
        {
            return await _context.Zimmet
                .Include(z => z.Urun)
                    .ThenInclude(u => u.Model)
                .Include(z => z.AtananCalisan)
                .Include(z => z.AtayanCalisan)
                .Include(z => z.Urun).ThenInclude(x => x.KayipCalintis)
                .Where(z => z.AtananCalisanId == calisanId && z.AktifMi == true)
                .OrderByDescending(z => z.ZimmetTarihi)
                .ToListAsync();
        }

        public async Task<IEnumerable<Zimmet>> GetAllWithDetailsAsync()
        {
            return await _context.Zimmet
                .Include(z => z.Urun)
                    .ThenInclude(u => u.Model)
                .Include(z => z.AtananCalisan)
                .ThenInclude(x => x.Ekip)
                .Include(z => z.AtayanCalisan)
                .Include(z=>z.Urun)
                .ThenInclude(z=>z.KayipCalintis)
                .Where(z => z.AktifMi == true)
                .OrderByDescending(z => z.ZimmetTarihi)
                .ToListAsync();
        }
    }
}