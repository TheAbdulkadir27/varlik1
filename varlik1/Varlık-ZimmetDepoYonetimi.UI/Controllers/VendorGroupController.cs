using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class VendorGroupController : Controller
    {
        private readonly IVendorGroupService _repository;
        private readonly ILogger<VendorGroupController> _logger;
        public VendorGroupController(IVendorGroupService repository, ILogger<VendorGroupController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [Authorize(Policy = Permissions.ProductGroup.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var values = await _repository.GetAllAsync();
            return View(values);
        }

        [Authorize(Policy = Permissions.ProductGroup.Ekleme)]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.ProductGroup.Ekleme)]
        public async Task<IActionResult> Create(VendorGroup vendorgroup)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(vendorgroup);
                TempData["Success"] = "Tedarikci Grubu başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(vendorgroup);
        }

        [Authorize(Policy = Permissions.ProductGroup.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var productgroup = await _repository.GetByIdAsync(id);
            if (productgroup == null)
            {
                return NotFound();
            }
            return View(productgroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.ProductGroup.Duzenleme)]
        public async Task<IActionResult> Edit(VendorGroup vendorgroup)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(vendorgroup);
                TempData["Success"] = "tedarikci Grubu başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(vendorgroup);
        }

        [Authorize(Policy = Permissions.ProductGroup.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var productgroup = await _repository.GetByIdAsync(id);
            if (productgroup == null)
            {
                return NotFound();
            }
            return View(productgroup);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.ProductGroup.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _repository.DeleteAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}
