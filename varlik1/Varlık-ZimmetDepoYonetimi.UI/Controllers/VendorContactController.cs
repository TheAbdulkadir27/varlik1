using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;
namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class VendorContactController : Controller
    {
        private readonly IVendorContactService _VendorContactService;
        private readonly IVendorService _vendorservice;
        private readonly ILogger<VendorContactController> _logger;
        public VendorContactController(IVendorContactService vendorContactService, IVendorService vendorservice, ILogger<VendorContactController> logger)
        {
            _VendorContactService = vendorContactService;
            _vendorservice = vendorservice;
            _logger = logger;
        }

        [Authorize(Policy = Permissions.CustomerContact.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var values = await _VendorContactService.GetAllAsync();
            return View(values);
        }

        [Authorize(Policy = Permissions.CustomerContact.Ekleme)]
        public async Task<IActionResult> Create()
        {
            var VendorContactView = new VendorContactViewModel();
            try
            {
                VendorContactView.Vendors = (await _vendorservice.GetAllAsync())
                    .Select(e => new SelectListItem
                    {
                        Value = e.ID.ToString(),
                        Text = e.Name
                    }).ToList() ?? new List<SelectListItem>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikci İletişimi oluşturma sayfası yüklenirken hata oluştu");
                VendorContactView.Vendors = new List<SelectListItem>();
            }
            return View(VendorContactView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerContact.Ekleme)]
        public async Task<IActionResult> Create(VendorContactViewModel vendorcontact)
        {
            var vendorContact1 = new VendorContact()
            {
                Name = vendorcontact.Name,
                Number = vendorcontact.Number,
                JobTitle = vendorcontact.JobTitle,
                PhoneNumber = vendorcontact.PhoneNumber,
                EmailAddress = vendorcontact.EmailAddress,
                Description = vendorcontact.Description,
                VendorId = vendorcontact.VendorId
            };

            try
            {
                await _VendorContactService.AddAsync(vendorContact1);
                TempData["Success"] = "Tedarikci İletişimi başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikci İletişimi eklenirken hata oluştu: {Message}", ex.Message);
                ModelState.AddModelError("", "Tedarikci İletişimi eklenirken bir hata oluştu.");
            }
            return View(vendorContact1);
        }

        [Authorize(Policy = Permissions.CustomerContact.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var VendorContact1 = await _VendorContactService.GetByIdAsync(id);
            if (VendorContact1 == null)
            {
                return NotFound();
            }

            var Vendorcontact = new VendorContactViewModel
            {
                ID = VendorContact1.ID,
                Name = VendorContact1.Name,
                Number = VendorContact1.Number,
                JobTitle = VendorContact1.JobTitle,
                PhoneNumber = VendorContact1.PhoneNumber,
                EmailAddress = VendorContact1.EmailAddress,
                Description = VendorContact1.Description,
                VendorId = VendorContact1.VendorId == null ? 0 : VendorContact1.VendorId,
            };
            Vendorcontact.Vendors = (await _vendorservice.GetAllAsync())
                .Select(s => new SelectListItem
                {
                    Value = s.ID.ToString(),
                    Text = s.Name
                }).ToList();
            return View(Vendorcontact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerContact.Duzenleme)]
        public async Task<IActionResult> Edit(int id, VendorContactViewModel vendorcontact)
        {
            if (id != vendorcontact.ID)
            {
                return BadRequest();
            }
            try
            {
                var vendorcontact1 = await _VendorContactService.GetByIdAsync(id);
                if (vendorcontact1 == null)
                {
                    return NotFound();
                }
                vendorcontact1.Name = vendorcontact.Name;
                vendorcontact1.Number = vendorcontact.Number;
                vendorcontact1.JobTitle = vendorcontact.JobTitle;
                vendorcontact1.PhoneNumber = vendorcontact.PhoneNumber;
                vendorcontact1.EmailAddress = vendorcontact.EmailAddress;
                vendorcontact1.Description = vendorcontact.Description;


                vendorcontact1.VendorId = vendorcontact.VendorId!.Value;

                await _VendorContactService.UpdateAsync(vendorcontact1);
                TempData["Success"] = "Tedarikci İletişimi başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikci İletişimi güncellenirken hata oluştu: {Message}", ex.Message);
                ModelState.AddModelError("", "Tedarikci İletişimi güncellenirken bir hata oluştu.");
            }

            // Hata durumunda dropdown listeleri tekrar doldur
            vendorcontact.Vendors = (await _vendorservice.GetAllAsync())
                .Select(s => new SelectListItem
                {
                    Value = s.ID.ToString(),
                    Text = s.Name
                }).ToList();
            return View(vendorcontact);
        }

        [Authorize(Policy = Permissions.CustomerContact.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var vendorContact = await _VendorContactService.GetByIdAsync(id);
            if (vendorContact == null)
            {
                return NotFound();
            }
            return View(vendorContact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerContact.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _VendorContactService.DeleteAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}
