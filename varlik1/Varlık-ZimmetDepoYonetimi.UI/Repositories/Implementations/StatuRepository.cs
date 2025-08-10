using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories
{
    public class StatuRepository : GenericRepository<Statu>, IStatuRepository
    {
        public StatuRepository(VarlikZimmetEnginContext context) : base(context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override async Task<IEnumerable<Statu>> GetAllAsync()
        {
             return await _dbSet
                .Include(s => s.UrunStatus)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Statu?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(s => s.UrunStatus).AsNoTracking()
                .FirstOrDefaultAsync(s => s.StatuId == id);
        }

        public async Task<IEnumerable<Urun>> GetAllUrunlerWithStatuAsync()
        {
            return await _context.Urun
                .Include(u => u.Model)
                .Include(u => u.UrunStatus)
                    .ThenInclude(us => us.Statu)
                 .Include(us => us.KayipCalintis)
                .Where(u => u.AktifMi == true && !u.KayipCalintis.Any())
                .OrderByDescending(u => u.UrunStatus.Max(us => us.Tarih)).AsNoTracking()
                .ToListAsync();
        }

        public async Task ClearAllStatusAsync()
        {
            // Önce UrunStatu tablosunu temizle
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM UrunStatu");

            // Sonra Statu tablosunu temizle
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Statu");

            // Identity'yi sıfırla
            await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Statu', RESEED, 0)");

            await _context.SaveChangesAsync();
        }

        public async Task ClearContextAsync()
        {
            _context.ChangeTracker.Clear();
        }

        public async Task AddWithoutSaveAsync(Statu statu)
        {
            await _dbSet.AddAsync(statu);
        }
    }
}