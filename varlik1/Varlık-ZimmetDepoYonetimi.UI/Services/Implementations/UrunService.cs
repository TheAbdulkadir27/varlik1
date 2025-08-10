using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class UrunService : IUrunService
    {
        private readonly IUrunRepository _urunRepository;
        private readonly IModelRepository _modelRepository;
        private readonly ICacheService _cacheService;
        private readonly IStokHareketService _stokHareketService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string URUN_CACHE_KEY = "urunler";
        private const string URUN_BY_ID_CACHE_KEY = "urun_{0}";
        private const string URUN_SELECT_LIST_CACHE_KEY = "urun_select_list";
        public UrunService(
            IUrunRepository urunRepository,
            ICacheService cacheService,
            IStokHareketService stokHareketService,
            IHttpContextAccessor httpContextAccessor,
            IModelRepository modelRepository)
        {
            _urunRepository = urunRepository;
            _cacheService = cacheService;
            _stokHareketService = stokHareketService;
            _httpContextAccessor = httpContextAccessor;
            _modelRepository = modelRepository;
        }

        public async Task<IEnumerable<Urun>> GetAllUrunsAsync()
        {
            var urunler = _cacheService.Get<IEnumerable<Urun>>(URUN_CACHE_KEY);
            if (urunler == null)
            {
                urunler = await _urunRepository.GetAllWithDetailsAsync();
                _cacheService.Set(URUN_CACHE_KEY, urunler);
            }
            return urunler;
        }

        public async Task<Urun?> GetUrunByIdAsync(int id)
        {
            var cacheKey = string.Format(URUN_BY_ID_CACHE_KEY, id);
            var urun = _cacheService.Get<Urun>(cacheKey);
            if (urun == null)
            {
                urun = await _urunRepository.GetByIdAsync(id);
                if (urun != null)
                {
                    _cacheService.Set(cacheKey, urun);
                }
            }
            return urun;
        }

        public async Task AddUrunAsync(Urun urun)
        {
            await _urunRepository.AddAsync(urun);
            await _urunRepository.SaveAsync();
            _cacheService.Remove(URUN_CACHE_KEY);
            _cacheService.Remove(URUN_SELECT_LIST_CACHE_KEY);
        }

        public async Task UpdateUrunAsync(Urun urun)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Sistem";
            var eskiUrun = await _urunRepository.GetByIdAsync(urun.UrunId);
            if (eskiUrun != null && eskiUrun.StokMiktari != urun.StokMiktari)
            {
                var stokHareket = new StokHareket
                {
                    UrunId = urun.UrunId,
                    Tarih = DateTime.Now,
                    IslemTipi = urun.StokMiktari > eskiUrun.StokMiktari ? "Stok Artışı" : "Stok Azalışı",
                    Miktar = Math.Abs(urun.StokMiktari - eskiUrun.StokMiktari),
                    Kullanici = userName
                };
                await _stokHareketService.AddStokHareketAsync(stokHareket);
            }

            _urunRepository.Update(urun);
            await _urunRepository.SaveAsync();

            _cacheService.Remove(URUN_CACHE_KEY);
            _cacheService.Remove(string.Format(URUN_BY_ID_CACHE_KEY, urun.UrunId));
            _cacheService.Remove(URUN_SELECT_LIST_CACHE_KEY);
        }

        public async Task DeleteUrunAsync(int id)
        {
            var urun = await _urunRepository.GetByIdAsync(id);
            if (urun != null)
            {
                try
                {
                    using var transaction = await _urunRepository.BeginTransactionAsync();
                    try
                    {
                        // İlişkili KayipCalinti kayıtlarını sil
                        foreach (var kayipCalinti in urun.KayipCalintis.ToList())
                        {
                            _urunRepository.DeleteKayipCalinti(kayipCalinti);
                        }

                        // İlişkili MusteriZimmet ve Zimmet kayıtlarını sil
                        foreach (var zimmet in urun.Zimmets.ToList())
                        {
                            foreach (var musteriZimmet in zimmet.MusteriZimmets.ToList())
                            {
                                _urunRepository.DeleteMusteriZimmet(musteriZimmet);
                            }
                            _urunRepository.DeleteZimmet(zimmet);
                        }

                        // İlişkili UrunStatu kayıtlarını sil
                        foreach (var urunStatu in urun.UrunStatus.ToList())
                        {
                            _urunRepository.DeleteUrunStatu(urunStatu);
                        }

                        // İlişkili DepoUrun kayıtlarını sil
                        foreach (var depoUrun in urun.DepoUruns.ToList())
                        {
                            _urunRepository.DeleteDepoUrun(depoUrun);
                        }

                        // İlişkili UrunBarkod kayıtlarını sil
                        foreach (var urunBarkod in urun.UrunBarkods.ToList())
                        {
                            _urunRepository.DeleteUrunBarkod(urunBarkod);
                        }

                        // İlişkili UrunDetay kayıtlarını sil
                        foreach (var urunDetay in urun.UrunDetays.ToList())
                        {
                            _urunRepository.DeleteUrunDetay(urunDetay);
                        }

                        // Ürünü sil
                        _urunRepository.Delete(urun);
                        await _urunRepository.SaveAsync();

                        await transaction.CommitAsync();

                        // Cache'i temizle
                        _cacheService.Remove(URUN_CACHE_KEY);
                        _cacheService.Remove(string.Format(URUN_BY_ID_CACHE_KEY, id));
                        _cacheService.Remove(URUN_SELECT_LIST_CACHE_KEY);
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ürün silinirken bir hata oluştu: {ex.Message}", ex);
                }
            }
        }

        public async Task<int> GetToplamUrunSayisiAsync()
        {
            var urunler = await _urunRepository.GetAllAsync();
            return urunler.Count();
        }

        public async Task<List<Urun>> GetDusukStokluUrunlerAsync()
        {
            const int MINIMUM_STOK = 10;
            var urunler = await _urunRepository.GetDusukStokluUrunlerAsync(MINIMUM_STOK);
            return urunler;
        }

        public async Task<Dictionary<string, int>> GetStokDurumuAsync()
        {
            //var urunler = await _urunRepository.GetAllAsync();
            var urunler = await _urunRepository.GetAllWithDetailsAsync();
            return await Task.Run(() => urunler.Where(x => x.Zimmets == null || !x.Zimmets.Any())
                .GroupBy(u => u.Model?.ModelAdi ?? "")
                .ToDictionary(g => g.Key, g => g.Sum(u => u.StokMiktari)));
        }

        public async Task<IEnumerable<SelectListItem>> GetUrunSelectListAsync()
        {
            var selectList = _cacheService.Get<IEnumerable<SelectListItem>>(URUN_SELECT_LIST_CACHE_KEY);
            if (selectList == null)
            {
                var urunler = await _urunRepository.GetAllWithModelAsync();
                selectList = urunler.Select(u => new SelectListItem
                {
                    Value = u.UrunId.ToString(),
                    Text = $"{u.Model?.ModelAdi ?? "Model Yok"} - ID: {u.UrunId}"
                });
                _cacheService.Set(URUN_SELECT_LIST_CACHE_KEY, selectList);
            }
            return selectList;
        }

        public async Task<IEnumerable<SelectListItem>> GetZimmetsizUrunSelectListAsync(int modelid)
        {
            var selectList = _cacheService.Get<IEnumerable<SelectListItem>>(URUN_SELECT_LIST_CACHE_KEY);
            var urunler = await _urunRepository.GetAllWithModelAsync();
            selectList = urunler.Where(x => x.ModelId == modelid && !x.Zimmets.Any(z => z.AktifMi == true))
            .Select(u => new SelectListItem
            {
                Value = u.UrunId.ToString(),
                Text = $"{u.Model?.ModelAdi ?? "Model Yok"} - {u.Aciklama}"
            });
            _cacheService.Set(URUN_SELECT_LIST_CACHE_KEY, selectList);
            return selectList;
        }

        public async Task AddUrunStatuAsync(UrunStatu urunStatu)
        {
            var urun = await _urunRepository.GetByIdAsync(urunStatu.UrunId);
            if (urun != null)
            {
                urun.UrunStatus.Add(urunStatu);
                await _urunRepository.SaveAsync();
            }
        }
    }
}
