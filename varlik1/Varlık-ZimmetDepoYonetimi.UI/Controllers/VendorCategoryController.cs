using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class VendorCategoryController : Controller
    {
        private readonly IVendorCategoryService _repository;
        private readonly ILogger<VendorCategoryController> _logger;
        public VendorCategoryController(IVendorCategoryService repository, ILogger<VendorCategoryController> logger)
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
        public async Task<IActionResult> Create(VendorCategory vendorcategory)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(vendorcategory);
                TempData["Success"] = "Tedarikci Kategorisi başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(vendorcategory);
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
        public async Task<IActionResult> Edit(VendorCategory vendorcategory)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(vendorcategory);
                TempData["Success"] = "tedarikci kategorisi başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(vendorcategory);
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
