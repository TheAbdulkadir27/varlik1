using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class CalisanService : ICalisanService
    {
        private readonly ICalisanRepository _calisanRepository;
        private readonly ICacheService _cacheService;
        private const string CALISAN_CACHE_KEY = "calisanlar";
        private const string CALISAN_BY_ID_CACHE_KEY = "calisan_{0}";

        public CalisanService(ICalisanRepository calisanRepository, ICacheService cacheService)
        {
            _calisanRepository = calisanRepository;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<Calisan>> GetAllCalisanlarAsync()
        {
            return await _calisanRepository.GetAllWithDetailsAsync();
        }

        public async Task<Calisan?> GetCalisanByIdAsync(int id)
        {
            var cacheKey = string.Format(CALISAN_BY_ID_CACHE_KEY, id);
            var calisan = _cacheService.Get<Calisan>(cacheKey);
            if (calisan == null)
            {
                calisan = await _calisanRepository.GetByIdAsync(id);
                if (calisan != null)
                {
                    _cacheService.Set(cacheKey, calisan);
                }
            }
            return calisan;
        }

        public async Task AddCalisanAsync(Calisan calisan)
        {
            await _calisanRepository.AddAsync(calisan);
            await _calisanRepository.SaveAsync();

            // Cache'i temizle
            _cacheService.Remove(CALISAN_CACHE_KEY);
            _cacheService.Remove(string.Format(CALISAN_BY_ID_CACHE_KEY, calisan.CalisanId));
        }

        public async Task UpdateCalisanAsync(Calisan calisan)
        {
            _calisanRepository.Update(calisan);
            await _calisanRepository.SaveAsync();

            _cacheService.Remove(CALISAN_CACHE_KEY);
            _cacheService.Remove(string.Format(CALISAN_BY_ID_CACHE_KEY, calisan.CalisanId));
        }

        public async Task<string> DeleteCalisanAsync(int id)
        {
            var calisan = await _calisanRepository.GetByIdAsync(id);
            if (calisan != null)
            {
                if (calisan.Zimmetler.Count() == 0 && !calisan.Zimmetler.Any())
                {
                    _calisanRepository.Delete(calisan);
                    await _calisanRepository.SaveAsync();

                    _cacheService.Remove(CALISAN_CACHE_KEY);
                    _cacheService.Remove(string.Format(CALISAN_BY_ID_CACHE_KEY, id));
                    return "Bu Çalışan Başarıyla Silindi";
                }
                else
                {
                    return "Bu Çalışan Silinemez Çünkü Üstüne Zimmet Bulunmakta";
                }
            }
            return string.Empty;
        }

        public async Task<IEnumerable<SelectListItem>> GetCalisanSelectListAsync()
        {
            var calisanlar = await _calisanRepository.GetAllAsync();
            return calisanlar.Select(c => new SelectListItem
            {
                Value = c.CalisanId.ToString(),
                Text = c.AdSoyad
            }).ToList();
        }

        public async Task<Calisan?> GetCalisanByUserIdAsync(string userId)
        {
            return await _calisanRepository.GetCalisanByUserIdAsync(userId);
        }
    }
}