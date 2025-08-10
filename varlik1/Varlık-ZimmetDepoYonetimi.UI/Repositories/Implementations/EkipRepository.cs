using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories
{
    public class EkipRepository : GenericRepository<Ekip>, IEkipRepository
    {
        public EkipRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Ekip>> GetAllAsync()
        {
            var values = await _dbSet
                .Include(e => e.SirketEkips)
                .ThenInclude(e => e.Sirket)
                .Include(e => e.Calisans)
                .ToListAsync();

            return values;
        }

        public override async Task<Ekip?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(e => e.Sirket)
                .ThenInclude(e => e.SirketEkips)
                .Include(e => e.Calisans)
                .FirstOrDefaultAsync(e => e.EkipId == id);
        }

        public override async Task AddAsync(Ekip entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public override void Update(Ekip entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public override void Delete(Ekip entity)
        {
            //_dbSet.Include(x => x.Calisans).FirstOrDefault(x => x.EkipId == entity.EkipId);
            _dbSet.Remove(entity);
        }

        public async Task<string> Delete(int ekipId)
        {
            var calısanlar = await _context.Ekips.Include(x=>x.SirketEkips).Include(x=>x.Calisans).FirstOrDefaultAsync(x => x.EkipId == ekipId);
            if (calısanlar?.Calisans != null && calısanlar.Calisans.Any())
            {
                return "Bu Ekip Silinemez Çünkü Bağlı Çalışanlar Mevcut";
            }
            else
            {
                _dbSet.Remove(calısanlar!);
                return "Bu Ekip Başarıyla Silinmiştir";
            }
        }
    }
}