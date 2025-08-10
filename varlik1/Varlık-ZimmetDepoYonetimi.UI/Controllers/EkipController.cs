using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class EkipController : Controller
    {
        private readonly IEkipService _ekipService;
        private readonly ISirketService _sirketService;
        private readonly ILogger<EkipController> _logger;

        public EkipController(
            IEkipService ekipService,
            ISirketService sirketService,
            ILogger<EkipController> logger)
        {
            _ekipService = ekipService;
            _sirketService = sirketService;
            _logger = logger;
        }

        [Authorize(Policy =Permissions.Ekipler.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var ekipler = await _ekipService.GetAllAsync();
            return View(ekipler);
        }

        [Authorize(Policy = Permissions.Ekipler.Goruntuleme)]
        public async Task<IActionResult> Details(int id)
        {
            var ekip = await _ekipService.GetEkipByIdAsync(id);
            if (ekip == null)
            {
                return NotFound();
            }
            return View(ekip);
        }

        [Authorize(Policy = Permissions.Ekipler.Ekleme)]
        public async Task<IActionResult> Create()
        {
            var model = new EkipViewModel
            {
                Sirketler = await GetSirketlerSelectList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Ekipler.Ekleme)]
        public async Task<IActionResult> Create(EkipViewModel model) // EkipViewModel model parametresi ile gelen verileri alır
        {
            if (ModelState.IsValid)
            {
                var ekip = new Ekip
                {
                    EkipAdi = model.EkipAdi,
                    AktifMi = true,
                    EkipId = model.EkipId,
                    SirketId = model.SeciliSirketler.ToList().First()
                };

                await _ekipService.AddEkipAsync(ekip);

                if (model.SeciliSirketler != null && model.SeciliSirketler.Any())
                {
                    foreach (var sirketId in model.SeciliSirketler)
                    {
                        var sirketEkip = new SirketEkip
                        {
                            SirketId = sirketId,
                            EkipId = ekip.EkipId,
                            AktifMi = true
                        };
                        using (var dbContext = new VarlikZimmetEnginContext())
                        {
                            dbContext.SirketEkips.Add(sirketEkip);
                            dbContext.SaveChanges();
                        }

                        await _ekipService.AddSirketEkipAsync(sirketEkip);
                    }
                }

                _logger.LogInformation($"Yeni ekip eklendi: {ekip.EkipId}");
                TempData["Success"] = "Ekip başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }

            model.Sirketler = await GetSirketlerSelectList();
            return View(model);
        }

        [Authorize(Policy = Permissions.Ekipler.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var ekip = await _ekipService.GetEkipByIdAsync(id);
            if (ekip == null)
            {
                return NotFound();
            }

            var model = new EkipViewModel
            {
                EkipId = ekip.EkipId,
                EkipAdi = ekip.EkipAdi,
                AktifMi = ekip.AktifMi ?? true,
                Sirketler = await GetSirketlerSelectList(),
                SeciliSirketler = ekip.SirketEkips?.Select(se => se.SirketId).ToList() ?? new List<int>()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Ekipler.Duzenleme)]
        public async Task<IActionResult> Edit(EkipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ekip = await _ekipService.GetEkipByIdAsync(model.EkipId);
                if (ekip == null)
                {
                    return NotFound();
                }

                ekip.EkipAdi = model.EkipAdi;
                ekip.AktifMi = model.AktifMi;
                ekip.SirketId = model.SeciliSirketler.First();
                ekip.EkipId = model.EkipId;

                await _ekipService.UpdateEkipAsync(ekip);


                using (var context = new VarlikZimmetEnginContext())
                {
                    var sirketekip = context.SirketEkips.Include(x => x.Ekip).Where(e => e.EkipId == ekip.EkipId).FirstOrDefault();
                    sirketekip.SirketId = model.SeciliSirketler.First();
                    var myentity = context.Entry(sirketekip);
                    myentity.State = EntityState.Modified;
                    context.SirketEkips.Add(sirketekip);
                    context.SaveChanges();
                }
                TempData["Success"] = "Ekip başarıyla güncellendi.";
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

        [HttpGet]
        [Authorize(Policy = Permissions.Ekipler.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var ekip = await _ekipService.GetEkipByIdAsync(id);
            if (ekip == null)
            {
                return NotFound();
            }
            return View(ekip);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Ekipler.Silme)]
        public async Task<IActionResult> Delete(Ekip ekip)
        {
            var message = await _ekipService.DeleteEkipAsync(ekip.EkipId);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}

