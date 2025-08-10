using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class StatuService : IStatuService
    {
        private readonly IStatuRepository _statuRepository;
        private readonly ICacheService _cacheService;
        private const string STATU_CACHE_KEY = "statusler";
        private const string STATU_BY_ID_CACHE_KEY = "statu_{0}";

        public StatuService(IStatuRepository statuRepository, ICacheService cacheService)
        {
            _statuRepository = statuRepository;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<Statu>> GetAllAsync()
        {
            var statusler = _cacheService.Get<IEnumerable<Statu>>(STATU_CACHE_KEY);
            if (statusler == null)
            {
                statusler = await _statuRepository.GetAllAsync();
                _cacheService.Set(STATU_CACHE_KEY, statusler);
            }
            return statusler;
        }

        public async Task<Statu?> GetByIdAsync(int id)
        {
            var cacheKey = string.Format(STATU_BY_ID_CACHE_KEY, id);
            var statu = _cacheService.Get<Statu>(cacheKey);
            if (statu == null)
            {
                statu = await _statuRepository.GetByIdAsync(id);
                if (statu != null)
                {
                    _cacheService.Set(cacheKey, statu);
                }
            }
            return statu;
        }

        public async Task AddAsync(Statu statu)
        {
            await _statuRepository.AddAsync(statu);
            await _statuRepository.SaveAsync();
            _cacheService.Remove(STATU_CACHE_KEY);
        }

        public async Task UpdateAsync(Statu statu)
        {
            _statuRepository.Update(statu);
            await _statuRepository.SaveAsync();
            var cacheKey = string.Format(STATU_BY_ID_CACHE_KEY, statu.StatuId);
            _cacheService.Remove(cacheKey);
            _cacheService.Remove(STATU_CACHE_KEY);
        }

        public async Task DeleteAsync(int id)
        {
            var statu = await _statuRepository.GetByIdAsync(id);
            if (statu != null)
            {
                _statuRepository.Delete(statu);
                await _statuRepository.SaveAsync();
                _cacheService.Remove(STATU_CACHE_KEY);
                _cacheService.Remove(string.Format(STATU_BY_ID_CACHE_KEY, id));
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetStatuSelectListAsync()
        {
            _cacheService.Remove(STATU_CACHE_KEY);

            var statuler = await _statuRepository.GetAllAsync();

            if (!statuler.Any())
            {
                var varsayilanStatuler = new List<Statu>
                {
                    new Statu { StatuId = 1, StatuAdi = "Kullanımda", AktifMi = true },
                    new Statu { StatuId = 2, StatuAdi = "Arızalı", AktifMi = true },
                    new Statu { StatuId = 3, StatuAdi = "Tamirde", AktifMi = true },
                    new Statu { StatuId = 4, StatuAdi = "Kullanım Dışı", AktifMi = true }
                };

                foreach (var statu in varsayilanStatuler)
                {
                    await _statuRepository.AddAsync(statu);
                }

                await _statuRepository.SaveAsync();
                statuler = varsayilanStatuler;
            }

            return statuler.Select(s => new SelectListItem
            {
                Value = s.StatuId.ToString(),
                Text = s.StatuAdi ?? "Belirtilmemiş"
            }).ToList();
        }

        public async Task<IEnumerable<UrunStatuListesiViewModel>> GetUrunStatuListesiAsync()
        {
            var urunler = await _statuRepository.GetAllUrunlerWithStatuAsync();
            return urunler.Select(u => new UrunStatuListesiViewModel
            {
                UrunId = u.UrunId,
                UrunAdi = u.Model?.ModelAdi ?? "Belirtilmemiş",
                ModelAdi = u.Model?.ModelAdi ?? "Belirtilmemiş",
                StokMiktari = u.StokMiktari,
                UrunMaliyeti = u.UrunMaliyeti,
                StatuId = u.UrunStatus.OrderByDescending(us => us.Tarih).FirstOrDefault()?.StatuId ?? 0,
                StatuAdi = u.UrunStatus.OrderByDescending(us => us.Tarih).FirstOrDefault()?.Statu?.StatuAdi ?? "Belirtilmemiş",
                StatuTipi = GetStatuTipi(u.UrunStatus.OrderByDescending(us => us.Tarih).FirstOrDefault()?.Statu?.StatuAdi),
                SonGuncellemeTarihi = u.UrunStatus.OrderByDescending(us => us.Tarih).FirstOrDefault()?.Tarih ?? DateTime.Now,
                Açıklama = u.Aciklama,
                AktifMi = u.AktifMi ?? false
            })
            .OrderBy(u => u.StatuId)
            .ThenBy(u => u.UrunAdi)
            .ToList();
        }

        private StatuTipi GetStatuTipi(string? statuAdi)
        {
            if (string.IsNullOrEmpty(statuAdi)) return StatuTipi.KullanimDisi;

            return statuAdi.ToLower() switch
            {
                var s when s == "kullanım dışı" => StatuTipi.KullanimDisi,
                var s when s.Contains("kullanım") => StatuTipi.Kullanımda,
                var s when s.Contains("arıza") => StatuTipi.Arızalı,
                var s when s.Contains("tamir") => StatuTipi.Tamirde,
                _ => StatuTipi.KullanimDisi
            };
        }
    }
}