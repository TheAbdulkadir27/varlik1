using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;
namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class CustomerGroupController : Controller
    {
        private readonly ICustomerGroupService _CustomerGroupService;
        private readonly ILogger<CustomerGroupController> _logger;
        public CustomerGroupController(ICustomerGroupService customergroupservice, ILogger<CustomerGroupController> logger)
        {
            _CustomerGroupService = customergroupservice;
            _logger = logger;
        }
        [Authorize(Policy = Permissions.CustomerGroup.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var modeller = await _CustomerGroupService.GetAllCustomerGroupAsync();
            return View(modeller);
        }

        [Authorize(Policy = Permissions.CustomerGroup.Ekleme)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerGroup.Ekleme)]
        public async Task<IActionResult> Create(CustomerGroup customergroup)
        {
            if (ModelState.IsValid)
            {
                await _CustomerGroupService.AddCustomerGroupAsync(customergroup);
                TempData["Success"] = "Müşteri Grubu başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(customergroup);
        }

        [Authorize(Policy = Permissions.CustomerGroup.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _CustomerGroupService.GetCustomerGroupByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerGroup.Duzenleme)]
        public async Task<IActionResult> Edit(CustomerGroup customergroup)
        {
            if (ModelState.IsValid)
            {
                await _CustomerGroupService.UpdateCustomerGroupAsync(customergroup);
                TempData["Success"] = "Müşteri Grubu başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(customergroup);
        }

        [Authorize(Policy = Permissions.CustomerGroup.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var customergroup = await _CustomerGroupService.GetCustomerGroupByIdAsync(id);
            if (customergroup == null)
            {
                return NotFound();
            }
            return View(customergroup);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerGroup.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _CustomerGroupService.DeleteCustomerGroupAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}
