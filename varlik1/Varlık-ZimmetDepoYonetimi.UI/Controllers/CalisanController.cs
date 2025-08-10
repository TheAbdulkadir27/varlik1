using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize] // Tüm işlemler için yetkilendirme gereklidir
    public class CalisanController : Controller
    {
        private readonly ICalisanService _calisanService;
        private readonly ISirketService _sirketService;
        private readonly IEkipService _ekipService;
        private readonly ILogger<CalisanController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public CalisanController(
            ICalisanService calisanService,
            ISirketService sirketService,
            IEkipService ekipService,
            ILogger<CalisanController> logger,
            UserManager<IdentityUser> userManager)
        {
            _calisanService = calisanService;
            _sirketService = sirketService;
            _ekipService = ekipService;
            _logger = logger;
            _userManager = userManager;

        }

        [Authorize(Policy = Permissions.Calisanlar.Goruntuleme)] //Policy Bazlı Yetkilendirme. Calisan.Goruntuleme yetkisi olanlar bu sayfayı görebilir.
        public async Task<IActionResult> Index()
        {
            var calisanlar = await _calisanService.GetAllCalisanlarAsync();
            return View(calisanlar);
        }

        [Authorize(Policy = Permissions.Calisanlar.Goruntuleme)]
        public async Task<IActionResult> Details(int id)
        {
            var calisan = await _calisanService.GetCalisanByIdAsync(id);
            if (calisan == null)
            {
                return NotFound();
            }
            return View(calisan);
        }

        [Authorize(Policy = Permissions.Calisanlar.Ekleme)]
        public async Task<IActionResult> Create()
        {
            var model = new CalisanViewModel();
            try
            {
                model.Sirketler = (await _sirketService.GetAllSirketlerAsync())
                    .Select(s => new SelectListItem
                    {
                        Value = s.SirketId.ToString(),
                        Text = s.SirketAdi
                    }).ToList() ?? new List<SelectListItem>();

                model.Ekipler = (await _ekipService.GetAllAsync())
                    .Select(e => new SelectListItem
                    {
                        Value = e.EkipId.ToString(),
                        Text = e.EkipAdi
                    }).ToList() ?? new List<SelectListItem>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Çalışan oluşturma sayfası yüklenirken hata oluştu");
                model.Sirketler = new List<SelectListItem>();
                model.Ekipler = new List<SelectListItem>();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Calisanlar.Ekleme)]
        public async Task<IActionResult> Create(CalisanViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (_calisanService.GetAllCalisanlarAsync().Result.Any(x => x.Mail == model.Mail))
                {
                    TempData["Message"] = "Böyle Bir Mail Adresi Zaten Mevcut! Lütfen Başka Bir Mail Adresi Girin";
                }
                else
                {

                    var user = new IdentityUser { UserName = model.AdSoyad, Email = model.Mail };
                    var result = await _userManager.CreateAsync(user, model.ŞifreTekrar);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.User);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    var calisan = new Calisan
                    {
                        AdSoyad = model.AdSoyad,
                        AboneNo = model.AboneNo,
                        Telefon = model.Telefon,
                        Mail = model.Mail,
                        EkipId = model.EkipId,
                        SirketId = model.SirketId,
                        AktifMi = true,
                        KullaniciAdi = user.Id
                    };
                    try
                    {
                        _calisanService.AddCalisanAsync(calisan).Wait();
                        TempData["Success"] = "Çalışan başarıyla eklendi.";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Çalışan eklenirken hata oluştu: {Message}", ex.Message);
                        ModelState.AddModelError("", "Çalışan eklenirken bir hata oluştu.");
                    }
                }
            }
            // Hata durumunda dropdown listelerini tekrar doldur. 
            model.Sirketler = (await _sirketService.GetAllSirketlerAsync())
                .Select(s => new SelectListItem
                {
                    Value = s.SirketId.ToString(),
                    Text = s.SirketAdi
                }).ToList();

            model.Ekipler = (await _ekipService.GetAllAsync())
                .Select(e => new SelectListItem
                {
                    Value = e.EkipId.ToString(),
                    Text = e.EkipAdi
                }).ToList();

            return View(model);
        }

        [Authorize(Policy = Permissions.Calisanlar.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var calisan = await _calisanService.GetCalisanByIdAsync(id);
            if (calisan == null)
            {
                return NotFound();
            }

            var model = new CalisanViewModel
            {
                CalisanId = calisan.CalisanId,
                AdSoyad = calisan.AdSoyad!,
                AboneNo = calisan.AboneNo!,
                Telefon = calisan.Telefon!,
                Mail = calisan.Mail!,
                EkipId = calisan.EkipId ?? 0,
                SirketId = calisan.SirketId ?? 0
            };

            // Dropdown listeleri doldur
            model.Sirketler = (await _sirketService.GetAllSirketlerAsync())
                .Select(s => new SelectListItem
                {
                    Value = s.SirketId.ToString(),
                    Text = s.SirketAdi
                }).ToList();

            model.Ekipler = (await _ekipService.GetAllAsync())
                .Select(e => new SelectListItem
                {
                    Value = e.EkipId.ToString(),
                    Text = e.EkipAdi
                }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Calisanlar.Duzenleme)]
        public async Task<IActionResult> Edit(int id, CalisanViewModel model)
        {
            if (id != model.CalisanId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var calisan = await _calisanService.GetCalisanByIdAsync(id);
                    if (calisan == null)
                    {
                        return NotFound();
                    }

                    calisan.AdSoyad = model.AdSoyad;
                    calisan.AboneNo = model.AboneNo;
                    calisan.Telefon = model.Telefon;
                    calisan.Mail = model.Mail;
                    calisan.EkipId = model.EkipId;
                    calisan.SirketId = model.SirketId;

                    await _calisanService.UpdateCalisanAsync(calisan);
                    TempData["Success"] = "Çalışan başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Çalışan güncellenirken hata oluştu: {Message}", ex.Message);
                    ModelState.AddModelError("", "Çalışan güncellenirken bir hata oluştu.");
                }
            }

            // Hata durumunda dropdown listeleri tekrar doldur
            model.Sirketler = (await _sirketService.GetAllSirketlerAsync())
                .Select(s => new SelectListItem
                {
                    Value = s.SirketId.ToString(),
                    Text = s.SirketAdi
                }).ToList();

            model.Ekipler = (await _ekipService.GetAllAsync())
                .Select(e => new SelectListItem
                {
                    Value = e.EkipId.ToString(),
                    Text = e.EkipAdi
                }).ToList();

            return View(model);
        }

        [Authorize(Policy = Permissions.Calisanlar.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var calisan = await _calisanService.GetCalisanByIdAsync(id);
            if (calisan == null)
            {
                return NotFound();
            }
            return View(calisan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Calisanlar.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _calisanService.DeleteCalisanAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}
