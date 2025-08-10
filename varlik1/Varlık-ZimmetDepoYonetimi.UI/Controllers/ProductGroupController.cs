using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class ProductGroupController : Controller
    {
        private readonly IProductGroupService _repository;
        private readonly ILogger<ProductGroupController> _logger;
        public ProductGroupController(IProductGroupService repository, ILogger<ProductGroupController> logger)
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
        public async Task<IActionResult> Create(ProductGroup productgroup)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(productgroup);
                TempData["Success"] = "Ürün Grubu başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(productgroup);
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
        public async Task<IActionResult> Edit(ProductGroup productgroup)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(productgroup);
                TempData["Success"] = "Ürün Grubu başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(productgroup);
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
