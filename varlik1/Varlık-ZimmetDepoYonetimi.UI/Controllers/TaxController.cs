using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class TaxController : Controller
    {
        private readonly ITaxService _taxservice;
        private readonly ILogger<TaxController> _logger;
        public TaxController(ITaxService taxservice, ILogger<TaxController> logger)
        {
            _taxservice = taxservice;
            _logger = logger;
        }

        [Authorize(Policy = Permissions.Tax.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var values = await _taxservice.GetAllTaxAsync();
            return View(values);
        }

        [Authorize(Policy = Permissions.Tax.Ekleme)]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Tax.Ekleme)]
        public async Task<IActionResult> Create(Tax tax)
        {
            if (ModelState.IsValid)
            {
                await _taxservice.AddTaxAsync(tax);
                TempData["Success"] = "Vergi başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(tax);
        }

        [Authorize(Policy = Permissions.Tax.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var tax = await _taxservice.GetTaxByIdAsync(id);
            if (tax == null)
            {
                return NotFound();
            }
            return View(tax);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Tax.Duzenleme)]
        public async Task<IActionResult> Edit(Tax tax)
        {
            if (ModelState.IsValid)
            {
                await _taxservice.UpdateTaxAsync(tax);
                TempData["Success"] = "Vergi başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(tax);
        }

        [Authorize(Policy = Permissions.Tax.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var Tax = await _taxservice.GetTaxByIdAsync(id);
            if (Tax == null)
            {
                return NotFound();
            }
            return View(Tax);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Tax.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _taxservice.DeleteTaxAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}
