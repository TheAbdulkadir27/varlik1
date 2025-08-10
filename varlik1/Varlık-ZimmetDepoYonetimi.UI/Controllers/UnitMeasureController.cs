using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class UnitMeasureController : Controller
    {
        private readonly IUnitMeasureService _IMeasureservice;
        private readonly ILogger<UnitMeasureController> _logger;
        public UnitMeasureController(IUnitMeasureService ıMeasureservice, ILogger<UnitMeasureController> logger)
        {
            _IMeasureservice = ıMeasureservice;
            _logger = logger;
        }

        [Authorize(Policy = Permissions.UnitMeasure.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var values = await _IMeasureservice.GetAllAsync();
            return View(values);
        }

        [Authorize(Policy = Permissions.UnitMeasure.Ekleme)]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.UnitMeasure.Ekleme)]
        public async Task<IActionResult> Create(UnitMeasure unitmeasure)
        {
            if (ModelState.IsValid)
            {
                await _IMeasureservice.AddAsync(unitmeasure);
                TempData["Success"] = "Birim Ölçüsü başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(unitmeasure);
        }

        [Authorize(Policy = Permissions.UnitMeasure.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var unitMeasure = await _IMeasureservice.GetByIdAsync(id);
            if (unitMeasure == null)
            {
                return NotFound();
            }
            return View(unitMeasure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.UnitMeasure.Duzenleme)]
        public async Task<IActionResult> Edit(UnitMeasure unitMeasure)
        {
            if (ModelState.IsValid)
            {
                await _IMeasureservice.UpdateAsync(unitMeasure);
                TempData["Success"] = "Birim Ölçüsü başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(unitMeasure);
        }

        [Authorize(Policy = Permissions.UnitMeasure.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var unitmeasure = await _IMeasureservice.GetByIdAsync(id);
            if (unitmeasure == null)
            {
                return NotFound();
            }
            return View(unitmeasure);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.UnitMeasure.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _IMeasureservice.DeleteAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}
