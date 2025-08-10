using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class SirketRepository : ISirketRepository
    {
        private readonly VarlikZimmetEnginContext _context;

        public SirketRepository(VarlikZimmetEnginContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sirket>> GetAllAsync()
        {
            return await _context.Sirkets.ToListAsync();
        }

        public Task<IEnumerable<Sirket>> GetAllAsync(Expression<Func<Sirket, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Sirket?> GetByIdAsync(int id)
        {
            return await _context.Sirkets.FindAsync(id);
        }

        public async Task AddAsync(Sirket sirket)
        {
            await _context.Sirkets.AddAsync(sirket);
        }

        public void Update(Sirket sirket)
        {
            _context.Sirkets.Update(sirket);
        }

        public void Delete(Sirket sirket)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Delete(int sirketid)
        {
            var sirketekip = await _context.Sirkets.Include(x => x.SirketEkips).Include(x=>x.Depos).Include(x=>x.Calisans).FirstOrDefaultAsync(x => x.SirketId == sirketid);
            if (sirketekip!.SirketEkips != null && sirketekip.SirketEkips.Any())
            {
                return "Şirket silinemez çünkü bağlı Ekipler var.";
            }
            else if(sirketekip!.Depos != null && sirketekip.Depos.Any())
            {
                return "Şirket silinemez çünkü bağlı Depo var.";
            }
            else if(sirketekip.Calisans != null && sirketekip.Calisans.Any())
            {
                return "Şirket silinemez çünkü bağlı Çalışanlar var.";
            }
            else
            {
                _context.Sirkets.Remove(sirketekip);
                return "Şirket Başarıyla Silindi";
            }

        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public VarlikZimmetEnginContext GetContext()
        {
            throw new NotImplementedException();
        }
    }
}

