using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System.Security.Claims;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Services.Implementations;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class KayipCalintiController : Controller
    {
        private readonly IKayipCalintiService _kayipCalintiService;
        private readonly IZimmetService _zimmetService;
        private readonly ILogger<KayipCalintiController> _logger;
        private readonly ICalisanService _calisanService;
        public KayipCalintiController(
            IKayipCalintiService kayipCalintiService,
            IZimmetService zimmetService,
            ILogger<KayipCalintiController> logger,
            ICalisanService calisanService)
        {
            _kayipCalintiService = kayipCalintiService;
            _zimmetService = zimmetService;
            _logger = logger;
            _calisanService = calisanService;
        }


        [Authorize(Policy = Permissions.KayıpCalıntı.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            //USER VEYA ÇALIŞAN KENDİ KAYIPÇALINTILARINI GÖRSÜN
            if (!User.IsInRole("Admin") || User.IsInRole(UserRoles.Calisan) || User.IsInRole(UserRoles.User))
            {
                // Sadece kendi kayıpcalıntılarını görsün
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var calisan = await _calisanService.GetCalisanByUserIdAsync(userId!);
                var zimmetler = await _kayipCalintiService.GetKayıpCalıntıByCalisanIdAsync(calisan!.CalisanId);
                return View(zimmetler);
            }
            var kayiplar = await _kayipCalintiService.GetAllAsync();
            return View(kayiplar);
        }


        [Authorize(Policy = Permissions.KayıpCalıntı.Ekleme)]
        public async Task<IActionResult> Create()
        {
            KayipCalintiViewModel model = new KayipCalintiViewModel();
            if (!User.IsInRole("Admin") || User.IsInRole(UserRoles.Calisan) || User.IsInRole(UserRoles.User))
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var calisan = await _calisanService.GetCalisanByUserIdAsync(userId!);
                model = new KayipCalintiViewModel
                {
                    AktifZimmetler = (_zimmetService.GetAktifZimmetlerAsync().Result.Where(x => x.AtananCalisanId == calisan?.CalisanId))
                       .Select(z => new SelectListItem
                       {
                           Value = z.ZimmetId.ToString(),
                           Text = z.Urun?.Model?.ModelAdi + " " + z.AtananCalisan?.AdSoyad
                       }).ToList()
                };
            }
            else
            {
                model = new KayipCalintiViewModel
                {
                    AktifZimmetler = (await _zimmetService.GetAktifZimmetlerAsync())
                        .Select(z => new SelectListItem
                        {
                            Value = z.ZimmetId.ToString(),
                            Text = z.Urun?.Model?.ModelAdi + " " + z.AtananCalisan?.AdSoyad
                        }).ToList()
                };
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.KayıpCalıntı.Ekleme)]
        public async Task<IActionResult> Create(KayipCalintiViewModel model)
        {
            if (ModelState.IsValid)
            {
                var zimmet = await _zimmetService.GetZimmetByIdAsync(model.ZimmetId);
                if (zimmet == null)
                {
                    ModelState.AddModelError("ZimmetId", "Geçersiz zimmet seçimi.");
                    model.AktifZimmetler = (await _zimmetService.GetAktifZimmetlerAsync())
                        .Select(z => new SelectListItem
                        {
                            Value = z.ZimmetId.ToString(),
                            Text = z.ZimmetNumarasi
                        }).ToList();
                    return View(model);
                }

                var kayipCalinti = new KayipCalinti
                {
                    ZimmetId = model.ZimmetId,
                    UrunId = zimmet.UrunId,
                    Aciklama = model.Aciklama,
                    Tarih = model.KayipTarihi,
                    KayipMi = model.KayipMi,
                    AktifMi = true
                };

                await _kayipCalintiService.AddAsync(kayipCalinti);
                return RedirectToAction(nameof(Index));
            }

            model.AktifZimmetler = (await _zimmetService.GetAktifZimmetlerAsync())
                .Select(z => new SelectListItem
                {
                    Value = z.ZimmetId.ToString(),
                    Text = z.ZimmetNumarasi
                }).ToList();
            return View(model);
        }

        [Authorize(Policy = Permissions.KayıpCalıntı.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var kayipCalinti = await _kayipCalintiService.GetByIdAsync(id);
            if (kayipCalinti == null)
            {
                return NotFound();
            }
            return View(kayipCalinti);
        }

        [HttpPost, ActionName("Delete")] //ActionName burada kullanılmış çünkü aynı isimde iki metot olamaz.
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.KayıpCalıntı.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _kayipCalintiService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = Permissions.KayıpCalıntı.Goruntuleme)]
        public async Task<IActionResult> Details(int id)
        {
            var kayipCalinti = await _kayipCalintiService.GetByIdAsync(id);
            using (var context = new VarlikZimmetEnginContext())
            {
                var kayıpcalıntı = context.KayipCalintis.Where(x => x.KayipCalintiId == id).Include(x => x.Urun).ThenInclude(x => x.Model).FirstOrDefault();
                ViewBag.Name = kayıpcalıntı.Urun.Model.ModelAdi;
            }

            if (kayipCalinti == null)
            {
                return NotFound();
            }
            return View(kayipCalinti);
        }
    }
}