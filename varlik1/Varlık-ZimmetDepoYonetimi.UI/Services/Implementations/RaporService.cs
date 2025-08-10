using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class RaporService : IRaporService
    {
        private readonly IZimmetRepository _zimmetRepository;
        private readonly IUrunRepository _urunRepository;
        private readonly ICacheService _cacheService;
        private readonly IStokHareketService _stokHareketService;

        public RaporService(
            IZimmetRepository zimmetRepository,
            IUrunRepository urunRepository,
            ICacheService cacheService,
            IStokHareketService stokHareketService)
        {
            _zimmetRepository = zimmetRepository;
            _urunRepository = urunRepository;
            _cacheService = cacheService;
            _stokHareketService = stokHareketService;
        }

        public async Task<Dictionary<string, int>> GetDepartmanBazliZimmetlerAsync()
        {
            var cacheKey = "departman_bazli_zimmetler";
            var sonuc = _cacheService.Get<Dictionary<string, int>>(cacheKey);

            if (sonuc == null)
            {
                var zimmetler = await _zimmetRepository.GetAllAsync();
                sonuc = zimmetler
                    .Where(z => z.AtananCalisan?.Ekip != null)
                    .GroupBy(z => z.AtananCalisan!.Ekip.EkipAdi!)
                    .ToDictionary(g => g.Key, g => g.Count());

                _cacheService.Set(cacheKey, sonuc);
            }

            return sonuc;
        }

        public async Task<List<StokHareket>> GetSonStokHareketleriAsync()
        {
            return await _stokHareketService.GetSonStokHareketleriAsync();
        }
    }
}