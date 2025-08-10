using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class ZimmetService : IZimmetService
    {
        private readonly IZimmetRepository _zimmetRepository;
        private readonly IKayipCalintiRepository _kayipCalintiRepository;
        private readonly ICacheService _cacheService;
        private const string ZIMMET_CACHE_KEY = "zimmetler";
        private const string ZIMMET_BY_ID_CACHE_KEY = "zimmet_{0}";
        private const string ZIMMET_BY_CALISAN_CACHE_KEY = "zimmet_calisan_{0}";
        private const string ZIMMET_BY_URUN_CACHE_KEY = "zimmet_urun_{0}";

        public ZimmetService(IZimmetRepository zimmetRepository, ICacheService cacheService, IKayipCalintiRepository kayipCalintiRepository)
        {
            _zimmetRepository = zimmetRepository;
            _cacheService = cacheService;
            _kayipCalintiRepository = kayipCalintiRepository;
        }

        public async Task<IEnumerable<Zimmet>> GetAllZimmetsAsync()
        {
            var zimmetler = _cacheService.Get<IEnumerable<Zimmet>>(ZIMMET_CACHE_KEY);
            if (zimmetler == null)
            {
                zimmetler = await _zimmetRepository.GetAllWithDetailsAsync();
                _cacheService.Set(ZIMMET_CACHE_KEY, zimmetler);
            }
            return zimmetler;
        }

        public async Task<Zimmet?> GetZimmetByIdAsync(int id)
        {
            var cacheKey = string.Format(ZIMMET_BY_ID_CACHE_KEY, id);
            var zimmet = _cacheService.Get<Zimmet>(cacheKey);
            if (zimmet == null)
            {
                zimmet = await _zimmetRepository.GetByIdAsync(id);
                if (zimmet != null)
                {
                    _cacheService.Set(cacheKey, zimmet);
                }
            }
            return zimmet;
        }

        public async Task AddZimmetAsync(Zimmet zimmet)
        {
            zimmet.ZimmetTarihi = DateTime.Now;
            zimmet.AktifMi = true;
            await _zimmetRepository.AddAsync(zimmet);
            await _zimmetRepository.SaveAsync();

            // Cache'i temizle
            _cacheService.Remove(ZIMMET_CACHE_KEY);
            if (zimmet.AtananCalisanId.HasValue)
            {
                _cacheService.Remove(string.Format(ZIMMET_BY_CALISAN_CACHE_KEY, zimmet.AtananCalisanId));
            }
        }

        public async Task UpdateZimmetAsync(Zimmet zimmet)
        {
            _zimmetRepository.Update(zimmet);
            await _zimmetRepository.SaveAsync();
        }

        public async Task DeleteZimmetAsync(int id)
        {
            var zimmet = await _zimmetRepository.GetByIdAsync(id);
            if (zimmet != null)
            {
                _zimmetRepository.Delete(zimmet);
                await _zimmetRepository.SaveAsync();
            }
        }

        public async Task<IEnumerable<Zimmet>> GetZimmetlerByCalisanIdAsync(int calisanId)
        {
            return await _zimmetRepository.GetZimmetlerByCalisanIdAsync(calisanId);
        }

        public async Task<bool> HasZimmetYetkisiAsync(int zimmetId, string userId)
        {
            var zimmet = await _zimmetRepository.GetByIdAsync(zimmetId);
            return zimmet?.AtananCalisanId.ToString() == userId;
        }

        public async Task<IEnumerable<Zimmet>> GetAktifZimmetlerAsync()
        {
            var zimmetler = await _zimmetRepository.GetAllWithDetailsAsync();
            return zimmetler.Where(z => z.AktifMi == true && !z.IadeTarihi.HasValue)
                          .OrderByDescending(z => z.ZimmetTarihi);
        }

        public async Task<int> GetToplamZimmetSayisiAsync()
        {
            var zimmetler = await _zimmetRepository.GetAllAsync();
            return zimmetler.Count();
        }

        public async Task<decimal> GetZimmetliUrunOraniAsync()
        {
            var toplamUrun = await _zimmetRepository.GetTotalUrunCountAsync();
            var zimmetliUrun = (await _zimmetRepository.GetAllAsync())
                .Where(z => z.AktifMi.GetValueOrDefault());

            if (toplamUrun == 0) return 0;
            return (decimal)zimmetliUrun.Count() / toplamUrun * 100;
        }

        public async Task<decimal> GetCalintiUrunOraniAsync()
        {
            var toplamUrun = await _zimmetRepository.GetTotalUrunCountAsync();
            var calintiUrun = (await _kayipCalintiRepository.GetAllAsync())
                .Where(z => z.AktifMi.GetValueOrDefault());

            if (toplamUrun == 0) return 0;
            return (decimal)calintiUrun.Count() / toplamUrun * 100;
        }

        public decimal GetCalintiUrunOrani(int calısanid)
        {
            var toplamUrun = _zimmetRepository.GetAllAsync().Result.Where(x => x.AtananCalisanId == calısanid).Count();
            var calintiUrun = _kayipCalintiRepository.GetAllAsync().Result.Where(x => x.Zimmet.AtananCalisanId == calısanid)
                .Where(z => z.AktifMi.GetValueOrDefault());
            if (toplamUrun == 0) return 0;
            return (decimal)calintiUrun.Count() / toplamUrun * 100;
        }


        public async Task<IEnumerable<Zimmet>> GetZimmetlerByUrunIdAsync(int urunId)
        {
            var cacheKey = string.Format(ZIMMET_BY_URUN_CACHE_KEY, urunId);
            var zimmetler = _cacheService.Get<IEnumerable<Zimmet>>(cacheKey);
            if (zimmetler == null)
            {
                zimmetler = await _zimmetRepository.GetAllAsync(z => z.UrunId == urunId);
                _cacheService.Set(cacheKey, zimmetler);
            }
            return zimmetler;
        }

        public async Task<Dictionary<string, int>> GetDepartmanBazliZimmetlerAsync()
        {
            var zimmetler = await _zimmetRepository.GetAllWithDetailsAsync();
            return zimmetler
                .Where(z => z.AktifMi == true && z.AtananCalisan?.Ekip != null)
                .GroupBy(z => z.AtananCalisan.Ekip.EkipAdi)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}