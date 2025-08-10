using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Diagnostics;
using System.Security.Claims;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Services.Implementations;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize] // Bu controller'a erişim yetkisi olan kullanıcıların giriş yapması gerekiyor
    public class HomeController : Controller
    {
        private readonly VarlikZimmetEnginContext _db;
        private readonly ILogger<HomeController> _logger;
        private readonly IUrunService _urunService;
        private readonly ICalisanService _calisanService;
        private readonly IZimmetService _zimmetService;
        public HomeController(VarlikZimmetEnginContext db, ILogger<HomeController> logger, IUrunService urunService, ICalisanService calisanService, IZimmetService zimmetService)
        {
            _db = db;
            _logger = logger;
            _urunService = urunService;
            _calisanService = calisanService;
            _zimmetService = zimmetService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Ana sayfa görüntüleniyor"); // Loglama

                DashboardViewModel dashboardViewModel = new DashboardViewModel();

                //User VEYA ÇALIŞAN Kendi Zimmet sayısını görebilir
                if (!User.IsInRole("Admin"))
                {
                    // Sadece kendi zimmetleri ve Stok Raporu veya zimmetraporlarını olarak görebilir
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var calisan = await _calisanService.GetCalisanByUserIdAsync(userId!);
                    dashboardViewModel = new DashboardViewModel
                    {
                        ToplamCalisanSayisi = _db.Calisan.Count(),
                        ToplamDepoSayisi = _db.Depo.Count(),
                        AktifZimmetSayisi = _zimmetService.GetZimmetlerByCalisanIdAsync(calisan!.CalisanId).Result.Count(),
                        ToplamUrunSayisi = _zimmetService.GetZimmetlerByCalisanIdAsync(calisan!.CalisanId).Result.Count(),
                        StokDurumu = new Dictionary<string, int>() { },
                        calintiUrunOrani = _zimmetService.GetCalintiUrunOrani(calisan.CalisanId)
                    };
                }
                if (User.IsInRole(UserRoles.Admin) || User.HasClaim("Permission", Permissions.Raporlar.StokRaporu) || User.HasClaim("Permission", Permissions.Raporlar.ZimmetRaporu))
                {
                    dashboardViewModel = new DashboardViewModel
                    {
                        ToplamCalisanSayisi = _db.Calisan.Count(),
                        ToplamDepoSayisi = _db.Depo.Count(),
                        AktifZimmetSayisi = _db.Zimmet.Count(z => z.AktifMi == true),
                        ToplamUrunSayisi = _db.Urun.Count(),
                        DusukStokluUrunSayisi = await _urunService.GetDusukStokluUrunlerAsync(),
                        StokDurumu = await _urunService.GetStokDurumuAsync()
                    };

                }
                return View(dashboardViewModel); // Ana sayfayı görüntüle
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ana sayfa görüntülenirken hata oluştu");
                TempData["Error"] = "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.";
                return View(new DashboardViewModel());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] // Tarayıcı önbelleğini devre dışı bırakır ve her zaman güncel veri alınmasını sağlar
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
