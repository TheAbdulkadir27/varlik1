using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Implementations;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class DepoController : Controller
    {
        private readonly IDepoService _depoService;
        private readonly ISirketService _sirketService;
        private readonly ILogger<DepoController> _logger;
        private readonly IUrunService _urunService;

        private readonly IDepoService _depoRepo;
        //private readonly IRepository<Urun> _urunRepo;
        //private readonly IRepository<DepoUrun> _depoUrunRepo;

        public DepoController(
            IDepoService depoService,
            ISirketService sirketService,
            ILogger<DepoController> logger,
            IUrunService urunService,
            IDepoService depoRepo)
        {
            _depoService = depoService;
            _sirketService = sirketService;
            _logger = logger;
            _urunService = urunService;
            _depoRepo = depoRepo;
        }

        [Authorize(Policy = Permissions.Depolar.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var depolar = await _depoService.GetAllDeposAsync();
            return View(depolar);
        }

        [Authorize(Policy = Permissions.Depolar.Ekleme)]
        public async Task<IActionResult> Create()
        {
            var model = new DepoViewModel
            {
                Sirketler = await GetSirketlerSelectList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Depolar.Ekleme)]
        public async Task<IActionResult> Create(DepoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var depo = new Depo
                {
                    DepoAdi = model.DepoAdi,
                    SirketId = model.SirketId,
                    AktifMi = true
                };

                await _depoService.AddDepoAsync(depo);
                _logger.LogInformation($"Yeni depo eklendi: {depo.DepoId}");
                return RedirectToAction(nameof(Index));
            }

            model.Sirketler = await GetSirketlerSelectList();
            return View(model);
        }

        private async Task<List<SelectListItem>> GetSirketlerSelectList()
        {
            var sirketler = await _sirketService.GetAllAsync();
            return sirketler.Select(s => new SelectListItem
            {
                Value = s.SirketId.ToString(),
                Text = s.SirketAdi
            }).ToList();
        }

        public async Task<IActionResult> Details(int id)
        {
            using (var dbcontext = new VarlikZimmetEnginContext())
            {
                var groupedUrunler = await dbcontext.Depo.Include(x=>x.Sirket)
               .Include(x => x.DepoUruns)
               .ThenInclude(x => x.Urun)
               .ThenInclude(x=>x.Model)
               .Include(x=>x.DepoUruns)
               .ThenInclude(x=>x.Urun)
               .ThenInclude(x=>x.UrunDetays)
               .Include(x=>x.DepoUruns)
               .ThenInclude(x=>x.Urun)
               .ThenInclude(x => x.Zimmets)
               .FirstOrDefaultAsync(x => x.DepoId == id);
                if (groupedUrunler == null)
                {
                    return NotFound();
                }
                return View(groupedUrunler);
            }
            
        }
        [Authorize(Policy =Permissions.Depolar.Goruntuleme)]
        public async Task<IActionResult> UrunDetay(int id)
        {
            var urun = await _urunService.GetUrunByIdAsync(id);
            if (urun == null)
            {
                return NotFound();
            }
            return View(urun);
        }

        [Authorize(Policy = Permissions.Depolar.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var depo = await _depoService.GetByIdAsync(id);
            if (depo == null)
            {
                return NotFound();
            }

            var model = new DepoViewModel
            {
                DepoId = depo.DepoId,
                DepoAdi = depo.DepoAdi,
                SirketId = depo.SirketId ?? 0,
                AktifMi = depo.AktifMi ?? true,
                Sirketler = (await _sirketService.GetAllSirketlerAsync())
                    .Select(s => new SelectListItem
                    {
                        Value = s.SirketId.ToString(),
                        Text = s.SirketAdi ?? "Belirtilmemiş"
                    }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Depolar.Duzenleme)]
        public async Task<IActionResult> Edit(int id, DepoViewModel model)
        {
            if (id != model.DepoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var depo = new Depo
                    {
                        DepoId = model.DepoId,
                        DepoAdi = model.DepoAdi,
                        SirketId = model.SirketId,
                        AktifMi = model.AktifMi
                    };

                    await _depoService.UpdateDepoAsync(depo);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Depo güncellenirken hata oluştu");
                    ModelState.AddModelError("", "Depo güncellenirken bir hata oluştu.");
                }
            }

            // Hata durumunda şirket listesini tekrar doldur
            model.Sirketler = (await _sirketService.GetAllSirketlerAsync())
                .Select(s => new SelectListItem
                {
                    Value = s.SirketId.ToString(),
                    Text = s.SirketAdi ?? "Belirtilmemiş"
                }).ToList();

            return View(model);
        }

        [Authorize(Policy = Permissions.Depolar.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var depo = await _depoService.GetDepoByIdAsync(id);
            if (depo == null)
            {
                return NotFound();
            }
            var message = await _depoService.DeleteDepoAsync(id);
            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _depoService.DeleteDepoAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

