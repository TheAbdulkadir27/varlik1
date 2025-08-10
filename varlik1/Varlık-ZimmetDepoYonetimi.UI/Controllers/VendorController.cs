using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.Global;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class VendorController : Controller
    {
        private readonly IVendorService _VendorService;
        private readonly IVendorGroupService _Vendorgroupservice;
        private readonly IVendorCategoryService _Vendorcategoryservice;
        private readonly ILogger<VendorController> _logger;
        public VendorController(IVendorService vendorService, IVendorGroupService vendorgroupservice, IVendorCategoryService vendorcategoryservice, ILogger<VendorController> logger)
        {
            _VendorService = vendorService;
            _Vendorgroupservice = vendorgroupservice;
            _Vendorcategoryservice = vendorcategoryservice;
            _logger = logger;
        }

        [Authorize(Policy = Permissions.Customer.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var vendors = await _VendorService.GetAllAsync();
            return View(vendors);
        }

        [Authorize(Policy = Permissions.Customer.Ekleme)]
        public async Task<IActionResult> Create()
        {
            var model = new VendorViewModel();
            var VendorAutoNumber = _VendorService.GetAllAsync().Result.Count() + 1;
            try
            {
                model.VendorGroup = (await _Vendorgroupservice.GetAllAsync())
                    .Select(s => new SelectListItem
                    {
                        Value = s.ID.ToString(),
                        Text = s.Name
                    }).ToList() ?? new List<SelectListItem>();

                model.VendorCategory = (await _Vendorcategoryservice.GetAllAsync())
                    .Select(e => new SelectListItem
                    {
                        Value = e.ID.ToString(),
                        Text = e.Name
                    }).ToList() ?? new List<SelectListItem>();
                model.Number = GenerateNumber.GenerateProductNumber(VendorAutoNumber, Models.Enums.NumberModels.Vendor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikci oluşturma sayfası yüklenirken hata oluştu");
                model.VendorGroup = new List<SelectListItem>();
                model.VendorCategory = new List<SelectListItem>();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Customer.Ekleme)]
        public async Task<IActionResult> Create(VendorViewModel Vendor)
        {
            var Vendor1 = new Vendor
            {
                Name = Vendor.Name,
                Number = Vendor.Number,
                Description = Vendor.Description,
                Street = Vendor.Street,
                City = Vendor.City,
                State = Vendor.State,
                ZipCode = Vendor.ZipCode,
                Country = Vendor.Country,
                PhoneNumber = Vendor.PhoneNumber,
                FaxNumber = Vendor.FaxNumber,
                EmailAddress = Vendor.EmailAddress,
                Website = Vendor.Website,
                VendorCategoryId = Vendor.VendorCategoryID!.Value,
                VendorGroupId = Vendor.VendorGroupID!.Value
            };
            try
            {
                _VendorService.AddAsync(Vendor1).Wait();
                TempData["Success"] = "Tedarikci başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Tedarikci eklenirken hata oluştu: {Message}", ex.Message);
                ModelState.AddModelError("", "Tedarikci eklenirken bir hata oluştu.");
            }
            return View(Vendor);
        }

        [Authorize(Policy = Permissions.Customer.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var vendor1 = await _VendorService.GetByIdAsync(id);
            if (vendor1 == null)
            {
                return NotFound();
            }

            var Vendor = new VendorViewModel
            {
                ID = vendor1.ID,
                City = vendor1.City,
                Country = vendor1.Country,
                ZipCode = vendor1.ZipCode,
                Name = vendor1.Name,
                Description = vendor1.Description,
                EmailAddress = vendor1.EmailAddress,
                Street = vendor1.Street,
                State = vendor1.State,
                Website = vendor1.Website,
                FaxNumber = vendor1.FaxNumber,
                Number = vendor1.Number,
                PhoneNumber = vendor1.PhoneNumber,
                VendorCategoryID = vendor1.VendorCategoryId == null ? 0 : vendor1.VendorCategoryId,
                VendorGroupID = vendor1.VendorGroupId == null ? 0 : vendor1.VendorGroupId,

            };


            Vendor.VendorCategory = (await _Vendorcategoryservice.GetAllAsync())
            .Select(s => new SelectListItem
            {
                Value = s.ID.ToString(),
                Text = s.Name
            }).ToList();

            Vendor.VendorGroup = (await _Vendorgroupservice.GetAllAsync())
                .Select(e => new SelectListItem
                {
                    Value = e.ID.ToString(),
                    Text = e.Name
                }).ToList();
            return View(Vendor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Customer.Duzenleme)]
        public async Task<IActionResult> Edit(int id, VendorViewModel vendor)
        {
            if (id != vendor.ID)
            {
                return BadRequest();
            }
            try
            {
                var vendor1 = await _VendorService.GetByIdAsync(id);
                if (vendor1 == null)
                {
                    return NotFound();
                }

                vendor1.City = vendor.City;
                vendor1.Country = vendor.Country;
                vendor1.ZipCode = vendor.ZipCode;
                vendor1.Name = vendor.Name;
                vendor1.Description = vendor.Description;
                vendor1.EmailAddress = vendor.EmailAddress;
                vendor1.Street = vendor.Street;
                vendor1.State = vendor.State;
                vendor1.Website = vendor.Website;
                vendor1.FaxNumber = vendor.FaxNumber;
                vendor1.Number = vendor.Number;
                vendor1.PhoneNumber = vendor.PhoneNumber;

                vendor1.VendorCategoryId = vendor.VendorCategoryID!.Value;
                vendor1.VendorGroupId = vendor.VendorGroupID!.Value;

                await _VendorService.UpdateAsync(vendor1);
                TempData["Success"] = "tedarikci başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "tedarikci güncellenirken hata oluştu: {Message}", ex.Message);
                ModelState.AddModelError("", "tedarikci güncellenirken bir hata oluştu.");
            }

            // Hata durumunda dropdown listeleri tekrar doldur
            vendor.VendorCategory = (await _Vendorcategoryservice.GetAllAsync())
            .Select(s => new SelectListItem
            {
                Value = s.ID.ToString(),
                Text = s.Name
            }).ToList();

            vendor.VendorGroup = (await _Vendorgroupservice.GetAllAsync())
                .Select(e => new SelectListItem
                {
                    Value = e.ID.ToString(),
                    Text = e.Name
                }).ToList();
            return View(vendor);
        }

        [Authorize(Policy = Permissions.Customer.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var vendor = await _VendorService.GetByIdAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Customer.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _VendorService.DeleteAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}
