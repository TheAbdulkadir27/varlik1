using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Services.Implementations;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class CustomerContactController : Controller
    {
        private readonly ICustomerContactService _CustomerContactService;
        private readonly ICustomerService _customerservice;
        private readonly ILogger<CustomerContactController> _logger;
        public CustomerContactController(ICustomerContactService customercontactservice, ILogger<CustomerContactController> logger, ICustomerService customerservice)
        {
            _CustomerContactService = customercontactservice;
            _logger = logger;
            _customerservice = customerservice;
        }
        [Authorize(Policy = Permissions.CustomerContact.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var values = await _CustomerContactService.GetAllCustomerContactAsync();
            return View(values);
        }

        [Authorize(Policy = Permissions.CustomerContact.Ekleme)]
        public async Task<IActionResult> Create()
        {
            var CustomerContactView = new CustomerContactViewModel();
            try
            {
                CustomerContactView.Customers = (await _customerservice.GetAllCustomerAsync())
                    .Select(e => new SelectListItem
                    {
                        Value = e.ID.ToString(),
                        Text = e.Name
                    }).ToList() ?? new List<SelectListItem>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri İletişimi oluşturma sayfası yüklenirken hata oluştu");
                CustomerContactView.Customers = new List<SelectListItem>();
            }
            return View(CustomerContactView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerContact.Ekleme)]
        public async Task<IActionResult> Create(CustomerContactViewModel customercontact)
        {
            var customerscontact1 = new CustomerContact()
            {
                Name = customercontact.Name,
                Number = customercontact.Number,
                JobTitle = customercontact.JobTitle,
                PhoneNumber = customercontact.PhoneNumber,
                EmailAddress = customercontact.EmailAddress,
                Description = customercontact.Description,
                CustomerId = customercontact.CustomerId
            };

            try
            {
                await _CustomerContactService.AddCustomerContactAsync(customerscontact1);
                TempData["Success"] = "Müşteri İletişimi başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri İletişimi eklenirken hata oluştu: {Message}", ex.Message);
                ModelState.AddModelError("", "Müşteri İletişimi eklenirken bir hata oluştu.");
            }
            return View(customerscontact1);
        }

        [Authorize(Policy = Permissions.CustomerContact.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var CustomerContact1 = await _CustomerContactService.GetCustomerContactByIdAsync(id);
            if (CustomerContact1 == null)
            {
                return NotFound();
            }

            var customercontact = new CustomerContactViewModel
            {
                ID = CustomerContact1.ID,
                Name = CustomerContact1.Name,
                Number = CustomerContact1.Number,
                JobTitle = CustomerContact1.JobTitle,
                PhoneNumber = CustomerContact1.PhoneNumber,
                EmailAddress = CustomerContact1.EmailAddress,
                Description = CustomerContact1.Description,
                CustomerId = CustomerContact1.CustomerId == null ? 0 : CustomerContact1.CustomerId,
            };
            customercontact.Customers = (await _customerservice.GetAllCustomerAsync())
                .Select(s => new SelectListItem
                {
                    Value = s.ID.ToString(),
                    Text = s.Name
                }).ToList();
            return View(customercontact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerContact.Duzenleme)]
        public async Task<IActionResult> Edit(int id, CustomerContactViewModel customercontact)
        {
            if (id != customercontact.ID)
            {
                return BadRequest();
            }
            try
            {
                var customercontact1 = await _CustomerContactService.GetCustomerContactByIdAsync(id);
                if (customercontact1 == null)
                {
                    return NotFound();
                }
                customercontact1.Name = customercontact.Name;
                customercontact1.Number = customercontact.Number;
                customercontact1.JobTitle = customercontact.JobTitle;
                customercontact1.PhoneNumber = customercontact.PhoneNumber;
                customercontact1.EmailAddress = customercontact.EmailAddress;
                customercontact1.Description = customercontact.Description;


                customercontact1.CustomerId = customercontact.CustomerId!.Value;

                await _CustomerContactService.UpdateCustomerContactAsync(customercontact1);
                TempData["Success"] = "Müşteri İletişimi başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri İletişimi güncellenirken hata oluştu: {Message}", ex.Message);
                ModelState.AddModelError("", "Müşteri İletişimi güncellenirken bir hata oluştu.");
            }

            // Hata durumunda dropdown listeleri tekrar doldur
            customercontact.Customers = (await _customerservice.GetAllCustomerAsync())
                .Select(s => new SelectListItem
                {
                    Value = s.ID.ToString(),
                    Text = s.Name
                }).ToList();
            return View(customercontact);
        }

        [Authorize(Policy = Permissions.CustomerContact.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var customerContact = await _CustomerContactService.GetCustomerContactByIdAsync(id);
            if (customerContact == null)
            {
                return NotFound();
            }
            return View(customerContact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerContact.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _CustomerContactService.DeleteCustomerContactAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}
