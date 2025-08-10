using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class KayipCalintiService : IKayipCalintiService
    {
        private readonly IKayipCalintiRepository _kayipCalintiRepository;
        private readonly ICacheService _cacheService;
        private const string KAYIP_CACHE_KEY = "kayip_calinti_listesi";
        private const string KAYIP_BY_ID_CACHE_KEY = "kayip_calinti_{0}";
        private const string KAYIP_BY_URUN_CACHE_KEY = "kayip_calinti_urun_{0}";

        public KayipCalintiService(IKayipCalintiRepository kayipCalintiRepository, ICacheService cacheService)
        {
            _kayipCalintiRepository = kayipCalintiRepository;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<KayipCalinti>> GetAllKayipCalintiAsync()
        {
            var kayiplar = _cacheService.Get<IEnumerable<KayipCalinti>>(KAYIP_CACHE_KEY);
            if (kayiplar == null)
            {
                kayiplar = await _kayipCalintiRepository.GetAllAsync();
                _cacheService.Set(KAYIP_CACHE_KEY, kayiplar);
            }
            return kayiplar;
        }

        public async Task<KayipCalinti?> GetKayipCalintiByIdAsync(int id)
        {
            var cacheKey = string.Format(KAYIP_BY_ID_CACHE_KEY, id);
            var kayip = _cacheService.Get<KayipCalinti>(cacheKey);
            if (kayip == null)
            {
                kayip = await _kayipCalintiRepository.GetByIdAsync(id);
                if (kayip != null)
                {
                    _cacheService.Set(cacheKey, kayip);
                }
            }
            return kayip;
        }

        public async Task DeleteAsync(int id)
        {
            var kayipCalinti = await _kayipCalintiRepository.GetByIdAsync(id);
            if (kayipCalinti != null)
            {
                _kayipCalintiRepository.Delete(kayipCalinti);
                await _kayipCalintiRepository.SaveAsync();
                _cacheService.Remove(KAYIP_CACHE_KEY);
                _cacheService.Remove(string.Format(KAYIP_BY_ID_CACHE_KEY, id));
                if (kayipCalinti.UrunId.HasValue)
                {
                    _cacheService.Remove(string.Format(KAYIP_BY_URUN_CACHE_KEY, kayipCalinti.UrunId));
                }
            }
        }

        public async Task<IEnumerable<KayipCalinti>> GetKayipCalintiByUrunIdAsync(int urunId)
        {
            var cacheKey = string.Format(KAYIP_BY_URUN_CACHE_KEY, urunId);
            var kayiplar = _cacheService.Get<IEnumerable<KayipCalinti>>(cacheKey);
            if (kayiplar == null)
            {
                kayiplar = await _kayipCalintiRepository.GetAllAsync(k => k.UrunId == urunId);
                _cacheService.Set(cacheKey, kayiplar);
            }
            return kayiplar;
        }

        public async Task AddKayipCalintiAsync(KayipCalinti kayipCalinti)
        {
            await _kayipCalintiRepository.AddAsync(kayipCalinti);
            await _kayipCalintiRepository.SaveAsync();
            _cacheService.Remove(KAYIP_CACHE_KEY);
            if (kayipCalinti.UrunId.HasValue)
            {
                _cacheService.Remove(string.Format(KAYIP_BY_URUN_CACHE_KEY, kayipCalinti.UrunId));
            }
        }

        public async Task UpdateKayipCalintiAsync(KayipCalinti kayipCalinti)
        {
            _kayipCalintiRepository.Update(kayipCalinti);
            await _kayipCalintiRepository.SaveAsync();
            _cacheService.Remove(KAYIP_CACHE_KEY);
            _cacheService.Remove(string.Format(KAYIP_BY_ID_CACHE_KEY, kayipCalinti.KayipCalintiId));
            if (kayipCalinti.UrunId.HasValue)
            {
                _cacheService.Remove(string.Format(KAYIP_BY_URUN_CACHE_KEY, kayipCalinti.UrunId));
            }
        }

        public async Task DeleteKayipCalintiAsync(int id)
        {
            var kayipCalinti = await _kayipCalintiRepository.GetByIdAsync(id);
            if (kayipCalinti != null)
            {
                _kayipCalintiRepository.Delete(kayipCalinti);
                await _kayipCalintiRepository.SaveAsync();
                _cacheService.Remove(KAYIP_CACHE_KEY);
                _cacheService.Remove(string.Format(KAYIP_BY_ID_CACHE_KEY, id));
                if (kayipCalinti.UrunId.HasValue)
                {
                    _cacheService.Remove(string.Format(KAYIP_BY_URUN_CACHE_KEY, kayipCalinti.UrunId));
                }
            }
        }

        public async Task<IEnumerable<KayipCalinti>> GetAllAsync()
        {
            return await _kayipCalintiRepository.GetAllWithDetailsAsync();
        }

        public async Task<KayipCalinti?> GetByIdAsync(int id)
        {
            return await _kayipCalintiRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(KayipCalinti kayipCalinti)
        {
            await _kayipCalintiRepository.AddAsync(kayipCalinti);
            await _kayipCalintiRepository.SaveAsync();
            _cacheService.Remove(KAYIP_CACHE_KEY);
        }

        public async Task UpdateAsync(KayipCalinti kayipCalinti)
        {
            await UpdateKayipCalintiAsync(kayipCalinti);
        }

        public async Task<IEnumerable<KayipCalinti?>> GetKayıpCalıntıByCalisanIdAsync(int calisanId)
        {
            return await _kayipCalintiRepository.GetKayıpCalıntıByCalisanIdAsync(calisanId);
        }
    }
}