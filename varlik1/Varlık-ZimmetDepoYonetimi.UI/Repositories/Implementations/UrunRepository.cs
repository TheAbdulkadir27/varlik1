using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories
{
    public class UrunRepository : GenericRepository<Urun>, IUrunRepository
    {
        public UrunRepository(VarlikZimmetEnginContext context) : base(context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public override async Task<Urun?> GetByIdAsync(int id)
        {
            var values = await _context.Urun
                .Include(u => u.Model)
                .Include(u => u.DepoUruns)
                .ThenInclude(u => u.Depo)
                .Include(u => u.UrunStatus)
                    .ThenInclude(us => us.Statu)
                .Include(u => u.Zimmets)
                    .ThenInclude(z => z.MusteriZimmets)
                .Include(u => u.KayipCalintis)
                .Include(u => u.UrunBarkods)
                .Include(u => u.UrunDetays)
                .FirstOrDefaultAsync(u => u.UrunId == id);
            return values;
        }

        public async Task<List<Urun>> GetDusukStokluUrunlerAsync(int minimumStok)
        {
            var groupedUrunler = await _context.Urun
     .Include(u => u.Model)
     .Include(u => u.DepoUruns)
     .ThenInclude(u => u.Depo)
     .Include(u => u.Zimmets)
     .Where(u => u.AktifMi == true)
     .GroupBy(u => u.Model)
     .Select(g => new Urun
     {
         Model = g.Key,
         StokMiktari = g
      .Where(u => !u.Zimmets.Any())
      .Sum(u => u.StokMiktari),
         AktifMi = true,
         DepoUruns = g.SelectMany(u => u.DepoUruns).ToList()
     }).Where(u => u.StokMiktari < minimumStok)
     .OrderBy(u => u.StokMiktari)
     .AsNoTracking()
     .ToListAsync();

            return groupedUrunler;
        }

        public async Task<IEnumerable<Urun>> GetAllWithModelAsync()
        {
            return await _context.Urun.Include(u => u.Model)
                .Include(x => x.Zimmets)
                .Include(x => x.UrunDetays)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Urun>> GetAllWithDetailsAsync()
        {
            return await _context.Urun
                .Include(u => u.Zimmets)
                .Include(u => u.Model)
                .Include(u => u.UrunStatus)
                    .ThenInclude(us => us.Statu)
                .Include(u => u.DepoUruns)
                .ThenInclude(u => u.Depo)
                .OrderBy(u => u.Model.ModelAdi).AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<UrunStatu>> GetUrunStatuListAsync(int urunId)
        {
            return await _context.UrunStatus
                .Where(us => us.UrunId == urunId).AsNoTracking()
                .ToListAsync();
        }

        public void DeleteUrunStatu(UrunStatu urunStatu)
        {
            _context.UrunStatus.Remove(urunStatu);
        }

        public async Task<List<Zimmet>> GetZimmetListByUrunIdAsync(int urunId)
        {
            return await _context.Zimmet
                .Where(z => z.UrunId == urunId).AsNoTracking()
                .ToListAsync();
        }

        public void DeleteZimmet(Zimmet zimmet)
        {
            _context.Zimmet.Remove(zimmet);
        }

        public async Task<List<DepoUrun>> GetDepoUrunListAsync(int urunId)
        {
            return await _context.DepoUruns
                .Where(du => du.UrunId == urunId).AsNoTracking()
                .ToListAsync();
        }

        public void DeleteDepoUrun(DepoUrun depoUrun)
        {
            _context.DepoUruns.Remove(depoUrun);
        }

        public void DeleteKayipCalinti(KayipCalinti kayipCalinti)
        {
            _context.KayipCalintis.Remove(kayipCalinti);
        }

        public void DeleteMusteriZimmet(MusteriZimmet musteriZimmet)
        {
            _context.MusteriZimmets.Remove(musteriZimmet);
        }

        public void DeleteUrunBarkod(UrunBarkod urunBarkod)
        {
            _context.UrunBarkods.Remove(urunBarkod);
        }

        public void DeleteUrunDetay(UrunDetay urunDetay)
        {
            _context.UrunDetays.Remove(urunDetay);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

    }
}