using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class CustomerCategoryController : Controller
    {
        private readonly ICustomerCategoryService _CustomerCategoryService;
        private readonly ILogger<CustomerCategoryController> _logger;
        public CustomerCategoryController(ICustomerCategoryService customercategoryservice, ILogger<CustomerCategoryController> logger)
        {
            _CustomerCategoryService = customercategoryservice;
            _logger = logger;
        }
        [Authorize(Policy = Permissions.CustomerCategory.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var customerCategories = await _CustomerCategoryService.GetAllCustomerCategoryAsync();
            return View(customerCategories);
        }

        [Authorize(Policy = Permissions.CustomerCategory.Ekleme)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerCategory.Ekleme)]
        public async Task<IActionResult> Create(CustomerCategory customercategory)
        {
            if (ModelState.IsValid)
            {
                await _CustomerCategoryService.AddCustomerCategoryAsync(customercategory);
                TempData["Success"] = "Müşteri Kategorisi başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(customercategory);
        }

        [Authorize(Policy = Permissions.CustomerCategory.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _CustomerCategoryService.GetCustomerCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerCategory.Duzenleme)]
        public async Task<IActionResult> Edit(CustomerCategory customercategory)
        {
            if (ModelState.IsValid)
            {
                await _CustomerCategoryService.UpdateCustomerCategoryAsync(customercategory);
                TempData["Success"] = "Müşteri Kategorisi başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(customercategory);
        }

        [Authorize(Policy = Permissions.CustomerCategory.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var customercategory = await _CustomerCategoryService.GetCustomerCategoryByIdAsync(id);
            if (customercategory == null)
            {
                return NotFound();
            }
            return View(customercategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerCategory.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _CustomerCategoryService.DeleteCustomerCategoryAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}
