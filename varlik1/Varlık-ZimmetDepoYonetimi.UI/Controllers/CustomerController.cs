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
    public class CustomerController : Controller
    {
        private readonly ICustomerService _CustomerService;
        private readonly ICustomerGroupService _customergroupservice;
        private readonly ICustomerCategoryService _customercategoryservice;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerService customerservice, ILogger<CustomerController> logger, ICustomerGroupService customergroupservice = null, ICustomerCategoryService customercategoryservice = null)
        {
            _CustomerService = customerservice;
            _logger = logger;
            _customergroupservice = customergroupservice;
            _customercategoryservice = customercategoryservice;
        }
        [Authorize(Policy = Permissions.Customer.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var customerCategories = await _CustomerService.GetAllCustomerAsync();
            return View(customerCategories);
        }

        [Authorize(Policy = Permissions.Customer.Ekleme)]
        public async Task<IActionResult> Create()
        {
            var model = new CustomerViewModel();
            var autocount = _CustomerService.GetAllCustomerAsync().Result.Count() + 1;
            try
            {
                model.CustomerGroup = (await _customergroupservice.GetAllCustomerGroupAsync())
                    .Select(s => new SelectListItem
                    {
                        Value = s.ID.ToString(),
                        Text = s.Name
                    }).ToList() ?? new List<SelectListItem>();

                model.CustomerCategory = (await _customercategoryservice.GetAllCustomerCategoryAsync())
                    .Select(e => new SelectListItem
                    {
                        Value = e.ID.ToString(),
                        Text = e.Name
                    }).ToList() ?? new List<SelectListItem>();
                model.Number = GenerateNumber.GenerateProductNumber(autocount, Models.Enums.NumberModels.Customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri oluşturma sayfası yüklenirken hata oluştu");
                model.CustomerGroup = new List<SelectListItem>();
                model.CustomerCategory = new List<SelectListItem>();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Customer.Ekleme)]
        public async Task<IActionResult> Create(CustomerViewModel customer)
        {
            var Customer = new Customer
            {
                Name = customer.Name,
                Number = customer.Number,
                Description = customer.Description,
                Street = customer.Street,
                City = customer.City,
                State = customer.State,
                ZipCode = customer.ZipCode,
                Country = customer.Country,
                PhoneNumber = customer.PhoneNumber,
                FaxNumber = customer.FaxNumber,
                EmailAddress = customer.EmailAddress,
                Website = customer.Website,
                CustomerCategoryId = customer.CustomerCategoryID!.Value,
                CustomerGroupId = customer.CustomerGroupID!.Value
            };
            try
            {
                _CustomerService.AddCustomerAsync(Customer).Wait();
                TempData["Success"] = "Müşteri başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri eklenirken hata oluştu: {Message}", ex.Message);
                ModelState.AddModelError("", "Müşteri eklenirken bir hata oluştu.");
            }
            return View(customer);
        }

        [Authorize(Policy = Permissions.Customer.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var Customer1 = await _CustomerService.GetCustomerByIdAsync(id);
            if (Customer1 == null)
            {
                return NotFound();
            }

            var customer = new CustomerViewModel
            {
                ID = Customer1.ID,
                City = Customer1.City,
                Country = Customer1.Country,
                ZipCode = Customer1.ZipCode,
                Name = Customer1.Name,
                Description = Customer1.Description,
                EmailAddress = Customer1.EmailAddress,
                Street = Customer1.Street,
                State = Customer1.State,
                Website = Customer1.Website,
                FaxNumber = Customer1.FaxNumber,
                Number = Customer1.Number,
                PhoneNumber = Customer1.PhoneNumber,
                CustomerCategoryID = Customer1.CustomerCategoryId == null ? 0 : Customer1.CustomerCategoryId,
                CustomerGroupID = Customer1.CustomerGroupId == null ? 0 : Customer1.CustomerGroupId,

            };


            customer.CustomerCategory = (await _customercategoryservice.GetAllCustomerCategoryAsync())
                .Select(s => new SelectListItem
                {
                    Value = s.ID.ToString(),
                    Text = s.Name
                }).ToList();

            customer.CustomerGroup = (await _customergroupservice.GetAllCustomerGroupAsync())
                .Select(e => new SelectListItem
                {
                    Value = e.ID.ToString(),
                    Text = e.Name
                }).ToList();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Customer.Duzenleme)]
        public async Task<IActionResult> Edit(int id, CustomerViewModel customer)
        {
            if (id != customer.ID)
            {
                return BadRequest();
            }
            try
            {
                var customer1 = await _CustomerService.GetCustomerByIdAsync(id);
                if (customer1 == null)
                {
                    return NotFound();
                }

                customer1.City = customer.City;
                customer1.Country = customer.Country;
                customer1.ZipCode = customer.ZipCode;
                customer1.Name = customer.Name;
                customer1.Description = customer.Description;
                customer1.EmailAddress = customer.EmailAddress;
                customer1.Street = customer.Street;
                customer1.State = customer.State;
                customer1.Website = customer.Website;
                customer1.FaxNumber = customer.FaxNumber;
                customer1.Number = customer.Number;
                customer1.PhoneNumber = customer.PhoneNumber;

                customer1.CustomerCategoryId = customer.CustomerCategoryID!.Value;
                customer1.CustomerGroupId = customer.CustomerGroupID!.Value;

                await _CustomerService.UpdateCustomerAsync(customer1);
                TempData["Success"] = "Müşteri başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri güncellenirken hata oluştu: {Message}", ex.Message);
                ModelState.AddModelError("", "Müşteri güncellenirken bir hata oluştu.");
            }

            // Hata durumunda dropdown listeleri tekrar doldur
            customer.CustomerCategory = (await _customercategoryservice.GetAllCustomerCategoryAsync())
                .Select(s => new SelectListItem
                {
                    Value = s.ID.ToString(),
                    Text = s.Name
                }).ToList();

            customer.CustomerGroup = (await _customergroupservice.GetAllCustomerGroupAsync())
                .Select(e => new SelectListItem
                {
                    Value = e.ID.ToString(),
                    Text = e.Name
                }).ToList();

            return View(customer);
        }

        [Authorize(Policy = Permissions.Customer.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _CustomerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Customer.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _CustomerService.DeleteCustomerAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}
