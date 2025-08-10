using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class DepoService : IDepoService
    {
        private readonly IDepoRepository _depoRepository;
        private readonly ICacheService _cacheService;
        private const string DEPO_CACHE_KEY = "depolar";
        private const string DEPO_BY_ID_CACHE_KEY = "depo_{0}";
        private const string DEPO_URUNLER_CACHE_KEY = "depo_urunler_{0}";

        public DepoService(IDepoRepository depoRepository, ICacheService cacheService)
        {
            _depoRepository = depoRepository;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<Depo>> GetAllDeposAsync()
        {
            _cacheService.Remove(DEPO_CACHE_KEY);

            var depolar = _cacheService.Get<IEnumerable<Depo>>(DEPO_CACHE_KEY);
            if (depolar == null)
            {
                depolar = await _depoRepository.GetAllAsync();
                _cacheService.Set(DEPO_CACHE_KEY, depolar);
            }
            return depolar;
        }

        public async Task<Depo?> GetDepoByIdAsync(int id)
        {
            var cacheKey = string.Format(DEPO_BY_ID_CACHE_KEY, id);
            var depo = _cacheService.Get<Depo>(cacheKey);
            if (depo == null)
            {
                depo = await _depoRepository.GetByIdAsync(id);
                if (depo != null)
                {
                    _cacheService.Set(cacheKey, depo);
                }
            }
            return depo;
        }

        public async Task<IEnumerable<DepoUrun>> GetDepoUrunlerAsync(int depoId)
        {
            var cacheKey = string.Format(DEPO_URUNLER_CACHE_KEY, depoId);
            var depoUrunler = _cacheService.Get<IEnumerable<DepoUrun>>(cacheKey);
            if (depoUrunler == null)
            {
                var depo = await _depoRepository.GetByIdWithUrunlerAsync(depoId);
                if (depo != null)
                {
                    depoUrunler = depo.DepoUruns;
                    _cacheService.Set(cacheKey, depoUrunler);
                }
            }
            return depoUrunler ?? Enumerable.Empty<DepoUrun>();
        }

        public async Task AddDepoAsync(Depo depo)
        {

            await _depoRepository.AddAsync(depo);
            await _depoRepository.SaveAsync();
            _cacheService.Remove(DEPO_CACHE_KEY);
        }

        public async Task UpdateDepoAsync(Depo depo)
        {
            _depoRepository.Update(depo);
            await _depoRepository.SaveAsync();

            _cacheService.Remove(DEPO_CACHE_KEY);
            _cacheService.Remove(string.Format(DEPO_BY_ID_CACHE_KEY, depo.DepoId));
            _cacheService.Remove(string.Format(DEPO_URUNLER_CACHE_KEY, depo.DepoId));
        }

        public async Task<string> DeleteDepoAsync(int id)
        {
            var depo = await _depoRepository.GetByIdAsync(id);
            if (depo != null)
            {

                if (depo.DepoUruns != null && depo.DepoUruns.Any())
                {
                    return "Depo silinemez çünkü bağlı ürünler var.";
                }
                else
                {
                    _depoRepository.Delete(depo);
                    await _depoRepository.SaveAsync();

                    _cacheService.Remove(DEPO_CACHE_KEY);
                    _cacheService.Remove(string.Format(DEPO_BY_ID_CACHE_KEY, id));
                    _cacheService.Remove(string.Format(DEPO_URUNLER_CACHE_KEY, id));
                    return "Depo Başarıyla Silindi";
                }
            }
            return string.Empty;
        }
        public async Task<Depo?> GetByIdAsync(int id)
        {
            _cacheService.Remove(DEPO_CACHE_KEY);
            return await _depoRepository.GetByIdWithUrunlerAsync(id);
        }
    }
}