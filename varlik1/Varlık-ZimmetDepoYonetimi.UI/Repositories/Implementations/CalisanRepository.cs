using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories
{
    public class CalisanRepository : GenericRepository<Calisan>, ICalisanRepository
    {
        public CalisanRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }

        public async override Task<IEnumerable<Calisan>> GetAllAsync()
        {
           return await _dbSet.AsNoTracking().OrderBy(x => x.AdSoyad).ToListAsync();
        }


        public override async Task<Calisan> GetByIdAsync(int id)
        {
            return await _dbSet.Include(x => x.Zimmetler).SingleAsync(x => x.CalisanId == id);
        }


        public async Task<IEnumerable<Calisan>> GetAllWithDetailsAsync()
        {
            return await _context.Calisan
                .Include(c => c.Sirket)
                .Include(c => c.Ekip)
                .Where(c => c.AktifMi == true)
                .OrderBy(c => c.AdSoyad)
                .ToListAsync();
        }

        public async Task<Calisan?> GetCalisanByUserIdAsync(string userId)
        {
            var calisan = await _context.Calisan
                .FirstOrDefaultAsync(c => c.KullaniciAdi == userId);

            if (calisan == null)
            {
                // Eğer çalışan kaydı yoksa, yeni bir kayıt oluştur
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null)
                {
                    calisan = new Calisan
                    {
                        AdSoyad = user.UserName ?? "Belirtilmemiş",
                        KullaniciAdi = userId,
                        Mail = user.Email,
                        AktifMi = true
                    };
                    await _context.Calisan.AddAsync(calisan);
                    await _context.SaveChangesAsync();
                }
            }

            return calisan;
        }
    }
}