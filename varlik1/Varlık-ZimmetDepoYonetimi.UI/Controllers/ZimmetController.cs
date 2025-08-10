using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System.Security.Claims;
using System.Security.Permissions;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Implementations;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class ZimmetController : Controller
    {
        private readonly IZimmetService _zimmetService;
        private readonly VarlikZimmetEnginContext _db;
        private readonly ILogger<ZimmetController> _logger;
        private readonly IUrunService _urunService;
        private readonly ICalisanService _calisanService;
        private readonly IStokHareketService _stokHareketService;
        private readonly IModelService _modelService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ZimmetController(IZimmetService zimmetService, VarlikZimmetEnginContext db, ILogger<ZimmetController> logger, IUrunService urunService, ICalisanService calisanService, IStokHareketService stokHareketService, IHttpContextAccessor httpContextAccessor, IModelService modelService)
        {
            _zimmetService = zimmetService;
            _db = db;
            _logger = logger;
            _urunService = urunService;
            _calisanService = calisanService;
            _stokHareketService = stokHareketService;
            _httpContextAccessor = httpContextAccessor;
            _modelService = modelService;
        }
        [Authorize(Policy = Permissions.Zimmetler.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            try
            {
                //sadece çalışanlar kendi malzemelerini görebilir şeklinde dizayn edebilir
                if (!User.IsInRole("Admin") || User.IsInRole(UserRoles.Calisan) || User.IsInRole(UserRoles.User))
                {
                    // Sadece kendi zimmetlerini görsün
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (userId == null)
                        return NotFound();

                    var calisan = await _calisanService.GetCalisanByUserIdAsync(userId);
                    if (calisan == null)
                        return NotFound();

                    var zimmetler = await _zimmetService.GetZimmetlerByCalisanIdAsync(calisan.CalisanId);
                    return View(zimmetler);
                }

                // Admin ve ZimmetSorumlusu tüm zimmetleri görebilir
                var Tumzimmetler = await _zimmetService.GetAllZimmetsAsync();
                return View(Tumzimmetler);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Zimmet listesi getirilirken hata oluştu: {Message}", ex.Message);
                TempData["Error"] = "Zimmet listesi getirilirken bir hata oluştu.";
                return View(new List<Zimmet>());
            }
        }
        [Authorize(Policy = Permissions.Zimmetler.Atama)]
        public async Task<IActionResult> ZimmetAta(int id)
        {
            try
            {
                ZimmetAtaViewModel viewModel = new ZimmetAtaViewModel();
                var calisanlar = await _calisanService.GetAllCalisanlarAsync();
                var zimmet1 = await _zimmetService.GetZimmetByIdAsync(id);
                if (zimmet1 == null)
                {
                    return NotFound();
                }

                if (!zimmet1.Urun.KayipCalintis.Any())
                {
                    viewModel = new ZimmetAtaViewModel
                    {
                        Zimmet = zimmet1,
                        Calisanlar = calisanlar
                    };
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Zimmet Atama alınırken hata oluştu: {Message}", ex.Message);
                TempData["Error"] = "Zimmet Atama alınırken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Zimmetler.Atama)]
        public async Task<IActionResult> ZimmetAta(ZimmetAtaViewModel model)
        {
            //atayan çalışanı da al
            if (model.Zimmet.AtananCalisanId > 0)
            {
                using (var context = new VarlikZimmetEnginContext())
                {
                    var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Sistem";
                    var zimmet = await context.Zimmet.Where(x => x.ZimmetId == model.Zimmet.ZimmetId && x.UrunId == model.Zimmet.UrunId).FirstOrDefaultAsync();
                    zimmet.ZimmetBaslangicTarihi = model.Zimmet.ZimmetBaslangicTarihi;
                    zimmet.AtananCalisan = context.Calisan.Where(x => x.CalisanId == model.Zimmet.AtananCalisanId).Single();
                    zimmet.AtayanCalisanId = context.Calisan.Where(x => x.AdSoyad == userName).SingleOrDefault().CalisanId;
                    zimmet.Aciklama = model.Aciklama;
                    context.Zimmet.Update(zimmet);
                    context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = Permissions.Zimmetler.Goruntuleme)]
        public async Task<IActionResult> Details(int id)
        {
            var zimmet = await _zimmetService.GetZimmetByIdAsync(id);
            if (zimmet == null)
            {
                return NotFound();
            }
            using (var context = new VarlikZimmetEnginContext())
            {
                var kayıpcalıntı = context.Zimmet.Where(x => x.ZimmetId == id)
                    .Include(x => x.Urun)
                    .ThenInclude(x => x.Model)
                    .FirstOrDefault();
                ViewBag.Name = kayıpcalıntı?.Urun?.Model?.ModelAdi;
            }
            return View(zimmet);
        }
        [Authorize(Policy =Permissions.Zimmetler.Atama)]
        public async Task<IActionResult> Create()
        {
            try
            {
                var model = new ZimmetViewModel
                {
                    //Urunler = (await _urunService.GetZimmetsizUrunSelectListAsync()).ToList(),
                    Calisanlar = (await _calisanService.GetCalisanSelectListAsync()).ToList(),
                    Modeller = (await _modelService.GetModelSelectListAsync()).ToList(),
                    ZimmetBaslangicTarihi = DateTime.Today
                };
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Zimmet oluşturma sayfası yüklenirken hata oluştu: {Message}", ex.Message);
                TempData["Error"] = "Zimmet oluşturma sayfası yüklenirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetUrunlerByModelId(int modelId)
        {
            try
            {
                // Ürünleri modelId'ye göre al
                var urunler =  await _urunService.GetZimmetsizUrunSelectListAsync(modelId);

                // Ürünleri Json formatına dönüştür
                var result = urunler.Select(u => new {
                    value = u.Value,
                    text = u.Text
                }).ToList();

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = "Veri alınamadı", message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Zimmetler.Atama)]
        public async Task<IActionResult> Create(ZimmetViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    _logger.LogInformation("Giriş yapan kullanıcı ID: {UserId}", userId);

                    if (string.IsNullOrEmpty(userId))
                    {
                        ModelState.AddModelError("", "Kullanıcı bilgisi bulunamadı.");
                        await YenileLookupListelerini(model);
                        return View(model);
                    }

                    var atayanCalisan = await _calisanService.GetCalisanByUserIdAsync(userId);
                    _logger.LogInformation("Atayan çalışan bilgisi: {Calisan}",
                        atayanCalisan != null ? $"ID: {atayanCalisan.CalisanId}, Ad: {atayanCalisan.AdSoyad}" : "Bulunamadı");

                    if (atayanCalisan == null)
                    {
                        ModelState.AddModelError("", $"Zimmet atayan çalışan bilgisi bulunamadı. (UserID: {userId})");
                        await YenileLookupListelerini(model);
                        return View(model);
                    }

                    var zimmet = new Zimmet
                    {
                        UrunId = model.UrunId,
                        AtananCalisanId = model.AtananCalisanId,
                        AtayanCalisanId = atayanCalisan.CalisanId,
                        ZimmetBaslangicTarihi = model.ZimmetBaslangicTarihi,
                        Aciklama = model.Aciklama,
                        ZimmetNumarasi = Guid.NewGuid().ToString().Substring(0, 8).ToUpper()
                    };

                    await _zimmetService.AddZimmetAsync(zimmet);
                    TempData["Success"] = "Zimmet başarıyla oluşturuldu.";

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Zimmet oluşturulurken hata oluştu: {Message}", ex.Message);
                ModelState.AddModelError("", "Zimmet oluşturulurken bir hata oluştu.");
            }

            await YenileLookupListelerini(model);
            return View(model);
        }

        private async Task YenileLookupListelerini(ZimmetViewModel model)
        {
            model.Urunler = (await _urunService.GetUrunSelectListAsync()).ToList();
            model.Calisanlar = (await _calisanService.GetCalisanSelectListAsync()).ToList();
        }

        [Authorize(Policy =Permissions.Zimmetler.Düzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var zimmet = await _zimmetService.GetZimmetByIdAsync(id);
            if (zimmet == null)
            {
                return NotFound();
            }
            return View(zimmet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Zimmetler.Düzenleme)]
        public async Task<IActionResult> Edit(int id, Zimmet zimmet)
        {
            if (id != zimmet.ZimmetId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _zimmetService.UpdateZimmetAsync(zimmet);
                return RedirectToAction(nameof(Index));
            }
            return View(zimmet);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Zimmetler.IptalEtme)]
        public async Task<IActionResult> Delete(int id)
        {
            var zimmet = await _zimmetService.GetZimmetByIdAsync(id);
            if (zimmet == null)
            {
                return NotFound();
            }
            return View(zimmet);
        }

       
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = Permissions.Zimmetler.IptalEtme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            await _zimmetService.DeleteZimmetAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Zimmetler.IptalEtme)]
        public async Task<IActionResult> IadeAl(int id)
        {
            var zimmet = await _zimmetService.GetZimmetByIdAsync(id);
            if (zimmet == null)
            {
                return NotFound();
            }
            if (!zimmet.Urun.KayipCalintis.Any())
            {
                return View(zimmet);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Zimmetler.IptalEtme)]
        public async Task<IActionResult> IadeAl(Zimmet zimmet)
        {
            try
            {
                await _zimmetService.DeleteZimmetAsync(zimmet.ZimmetId);
                //var zimmet1 = await _zimmetService.GetZimmetByIdAsync(zimmet.ZimmetId);
                //if (zimmet1 == null)
                //{
                //    return NotFound();
                //}

                //zimmet1.IadeTarihi = DateTime.Now;
                //zimmet1.AktifMi = false;
                //zimmet.UrunId = null;
                TempData["Success"] = "Zimmet iadesi başarıyla alındı.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Zimmet iadesi alınırken hata oluştu: {Message}", ex.Message);
                TempData["Error"] = "Zimmet iadesi alınırken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

