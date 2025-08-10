using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class StokHareketService : IStokHareketService
    {
        private readonly IStokHareketRepository _stokHareketRepository;
        private readonly ICacheService _cacheService;
        private const string STOK_HAREKET_CACHE_KEY = "stok_hareket_listesi";
        private const string STOK_HAREKET_BY_URUN_CACHE_KEY = "stok_hareket_urun_{0}";

        public StokHareketService(IStokHareketRepository stokHareketRepository, ICacheService cacheService)
        {
            _stokHareketRepository = stokHareketRepository;
            _cacheService = cacheService;
        }

        public async Task<List<StokHareket>> GetSonStokHareketleriAsync(int limit = 10)
        {
            var cacheKey = $"{STOK_HAREKET_CACHE_KEY}_{limit}";
            var stokHareketleri = _cacheService.Get<List<StokHareket>>(cacheKey);

            if (stokHareketleri == null)
            {
                stokHareketleri = await _stokHareketRepository.GetSonStokHareketleriAsync(limit);
                _cacheService.Set(cacheKey, stokHareketleri);
            }

            return stokHareketleri;
        }

        public async Task<List<StokHareket>> GetStokHareketleriByUrunIdAsync(int urunId)
        {
            var cacheKey = string.Format(STOK_HAREKET_BY_URUN_CACHE_KEY, urunId);
            var stokHareketleri = _cacheService.Get<List<StokHareket>>(cacheKey);

            if (stokHareketleri == null)
            {
                stokHareketleri = await _stokHareketRepository.GetStokHareketleriByUrunIdAsync(urunId);
                _cacheService.Set(cacheKey, stokHareketleri);
            }

            return stokHareketleri;
        }

        public async Task AddStokHareketAsync(StokHareket stokHareket)
        {
            stokHareket.Tarih = DateTime.Now;
            stokHareket.AktifMi = true;
            await _stokHareketRepository.AddAsync(stokHareket);
            await _stokHareketRepository.SaveAsync();

            _cacheService.Remove(STOK_HAREKET_CACHE_KEY);
            _cacheService.Remove(string.Format(STOK_HAREKET_BY_URUN_CACHE_KEY, stokHareket.UrunId));
        }

        public async Task UpdateStokHareketAsync(StokHareket stokHareket)
        {
            _stokHareketRepository.Update(stokHareket);
            await _stokHareketRepository.SaveAsync();

            _cacheService.Remove(STOK_HAREKET_CACHE_KEY);
            _cacheService.Remove(string.Format(STOK_HAREKET_BY_URUN_CACHE_KEY, stokHareket.UrunId));
        }

        public async Task DeleteStokHareketAsync(int id)
        {
            var stokHareket = await _stokHareketRepository.GetByIdAsync(id);
            if (stokHareket != null)
            {
                stokHareket.AktifMi = false;
                _stokHareketRepository.Update(stokHareket);
                await _stokHareketRepository.SaveAsync();

                _cacheService.Remove(STOK_HAREKET_CACHE_KEY);
                _cacheService.Remove(string.Format(STOK_HAREKET_BY_URUN_CACHE_KEY, stokHareket.UrunId));
            }
        }
    }
}