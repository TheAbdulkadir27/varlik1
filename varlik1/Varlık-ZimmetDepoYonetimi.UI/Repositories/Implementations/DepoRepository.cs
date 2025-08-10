using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories
{
    public class DepoRepository : GenericRepository<Depo>, IDepoRepository
    {
        public DepoRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }

        public override async Task<Depo> GetByIdAsync(int id)
        {
            return await _context.Depo.Include(d => d.DepoUruns)
                .ThenInclude(x => x.Depo)
                .FirstOrDefaultAsync(x => x.DepoId == id);
        }

        public override async Task<IEnumerable<Depo>> GetAllAsync()
        {
            return await _dbSet
                .Include(d => d.Sirket)
                .Include(d => d.DepoUruns)
                    .ThenInclude(du => du.Urun)
                    .ThenInclude(x => x.Zimmets)
                    .Include(x=>x.DepoUruns)
                    .ThenInclude(x=>x.Urun)
                        .ThenInclude(u => u.Model)
                .ToListAsync();
        }

        public async Task<Depo?> GetByIdWithUrunlerAsync(int id)
        {
            return await _dbSet
                .Include(d => d.Sirket)
                .Include(d => d.DepoUruns)
                    .ThenInclude(du => du.Urun)
                        .ThenInclude(u => u.Model)
                .FirstOrDefaultAsync(d => d.DepoId == id);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Depo depo)
        {
            throw new NotImplementedException();
        }
    }
}