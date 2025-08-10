using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.X509;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.Global;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;
using static Permissions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Tax = Varlık_ZimmetDepoYonetimi.UI.Models.Tax;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    public class SalesOrderController : Controller
    {

        private readonly ISalesOrderService _repository;
        private readonly ICustomerService _customerService;
        private readonly ITaxService _taxservice;
        private readonly IUrunService _productservice;
        private readonly ISalesOrderItemService _salesorderitemservice;
        private readonly ILogger<SalesOrderController> _logger;
        public SalesOrderController(ISalesOrderService repository, ICustomerService customerService, ITaxService taxservice, ILogger<SalesOrderController> logger, IUrunService productservice, ISalesOrderItemService salesorderitemservice)
        {
            _repository = repository;
            _customerService = customerService;
            _taxservice = taxservice;
            _logger = logger;
            _productservice = productservice;
            _salesorderitemservice = salesorderitemservice;
        }

        [Authorize(Policy = Permissions.CustomerContact.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var values = await _repository.GetAllAsync();
            return View(values);
        }

        [Authorize(Policy = Permissions.CustomerContact.Ekleme)]
        public async Task<IActionResult> Create()
        {
            var salesOrder = new SalesOrderViewModel();
            try
            {
                var autonumber = _repository.GetAllAsync().Result.Count() + 1;
                salesOrder.Number = GenerateNumber.GenerateProductNumber(autonumber, Models.Enums.NumberModels.SalesOrder);
                salesOrder.Customers = (await _customerService.GetAllCustomerAsync())
                    .Select(e => new SelectListItem
                    {
                        Value = e.ID.ToString(),
                        Text = e.Name
                    }).ToList();

                salesOrder.taxes = (await _taxservice.GetAllTaxAsync())
                    .Select(e => new SelectListItem
                    {
                        Value = e.ID.ToString(),
                        Text = $"{e.Name} %{e.Percentage}"
                    }).ToList();

                // AllTaxes listesi gerçek vergi nesneleri için
                salesOrder.AllTaxes = (List<Tax>)await _taxservice.GetAllTaxAsync();

                salesOrder.SalesState = list()
                    .Select(e => new SelectListItem
                    {
                        Value = e.Value.ToString(),
                        Text = e.Text
                    }).ToList();

                // Products listesi SelectListItem olarak
                salesOrder.Products = (await _productservice.GetAllUrunsAsync())
                    .Select(e => new SelectListItem
                    {
                        Value = e.UrunId!.ToString(),
                        Text = e.Model!.ModelAdi
                    }).ToList();





                // ProductsJson olarak ürünlerin fiyat ve bilgileri (JS için)
                salesOrder.ProductsJson = (await _productservice.GetAllUrunsAsync())
                    .Select(e => new ProductJsonViewModel
                    {
                        Id = e.UrunId,
                        ModelAdi = e.Model.ModelAdi,
                        UrunGuncelFiyat = Convert.ToDecimal(e.UrunGuncelFiyat)
                    }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satış Siparişi oluşturma sayfası yüklenirken hata oluştu");
                salesOrder.Customers = new List<SelectListItem>();
                salesOrder.taxes = new List<SelectListItem>();
                salesOrder.SalesState = new List<SelectListItem>();
                salesOrder.Products = new List<SelectListItem>();
                salesOrder.AllTaxes = new List<Tax>();
                salesOrder.ProductsJson = new List<ProductJsonViewModel>();
            }
            return View(salesOrder);
        }
        private List<SelectListItem> list()
        {
            List<SelectListItem> ıtems = new List<SelectListItem>();
            ıtems.Add(new SelectListItem() { Text = "Taslak", Value = "0" });
            ıtems.Add(new SelectListItem() { Text = "İptal Edildi", Value = "1" });
            ıtems.Add(new SelectListItem() { Text = "Onaylandı", Value = "2" });
            ıtems.Add(new SelectListItem() { Text = "Arşivlendi", Value = "3" });
            return ıtems;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerContact.Ekleme)]
        public async Task<IActionResult> Create(SalesOrderViewModel salesorderview)
        {
            var salesorder = new SalesOrder()
            {
                Id = salesorderview.Id,
                CustomerId = salesorderview.CustomerId,
                TaxId = salesorderview.TaxId,
                AfterTaxAmount = salesorderview.AfterTaxAmount,
                TaxAmount = salesorderview.TaxAmount,
                OrderDate = salesorderview.OrderDate == null ? DateTime.Now : salesorderview.OrderDate,
                BeforeTaxAmount = salesorderview.BeforeTaxAmount,
                Number = salesorderview.Number,
                Description = salesorderview.Description,
                OrderStatus = (SalesOrderStatus)salesorderview.SalesStateId!,
                CreatedAtUtc = DateTime.Now
            };

            var firstItem = salesorderview.SalesOrderItems.FirstOrDefault();
            if (firstItem != null)
            {
                try
                {
                    await _repository.AddAsync(salesorder);
                    var salesorderitem = new SalesOrderItem()
                    {
                        Quantity = firstItem.Quantity,
                        UrunId = firstItem.UrunId,
                        SalesOrderId = salesorder.Id,
                        UnitPrice = firstItem.UnitPrice,
                        Summary = salesorder.Description
                    };
                    _salesorderitemservice.AddAsync(salesorderitem).Wait();
                    TempData["Success"] = "Satış Siparişi başarıyla eklendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, "Satış Siparisi eklenirken hata oluştu: {Message}", ex.Message);
                    ModelState.AddModelError("", "Satış Siparisi eklenirken bir hata oluştu.");
                }
            }
            return View(salesorder);
        }

        [Authorize(Policy = Permissions.CustomerContact.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {

            var order = await _repository.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            // Ürünler
            var allProducts = await _productservice.GetAllUrunsAsync();
            var productJsonList = allProducts.Select(p => new ProductJsonViewModel
            {
                Id = p.UrunId,
                ModelAdi = p.Model?.ModelAdi ?? "",
                UrunGuncelFiyat = Convert.ToDecimal(p.UrunGuncelFiyat)
            }).ToList();

            // Vergiler
            var allTaxes = await _taxservice.GetAllTaxAsync();
            var taxJsonList = allTaxes.Select(t => new TaxJsonViewModel
            {
                ID = t.ID,
                Name = t.Name,
                Percentage = t.Percentage
            }).ToList();


            var viewModel = new SalesOrderViewModel
            {
                Id = order.Id,
                Number = order.Number,
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
                TaxId = order.TaxId,
                SalesStateId = (int?)order.OrderStatus,
                Description = order.Description,
                CreatedAtUtc = DateTime.Now,


                SalesOrderItems = order.SalesOrderItemList.Select(i => new SalesOrderItemViewModel
                {
                    SalesOrderId = i.Id,
                    UrunId = i.UrunId,
                    UnitPrice = i.UnitPrice / 100m,
                    Quantity = i.Quantity,
                    Total = i.Total / 100m
                }).ToList(),

                Customers = (await _customerService.GetAllCustomerAsync()).Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name
                }).ToList(),

                SalesState = list().Select(s => new SelectListItem
                {
                    Value = s.Value.ToString(),
                    Text = s.Text
                }).ToList(),

                AllTaxes = allTaxes.ToList(),
                AllTaxesJson = taxJsonList,
                ProductsJson = productJsonList,

                Products = allProducts.Select(p => new SelectListItem
                {
                    Value = p.ModelId?.ToString(),
                    Text = p.Model?.ModelAdi ?? "Bilinmeyen"
                }).ToList()
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerContact.Duzenleme)]
        public async Task<IActionResult> Edit(int id, SalesOrderViewModel salesorderview)
        {
            if (id != salesorderview.Id)
            {
                return BadRequest();
            }

            try
            {
                var existingOrder = await _repository.GetByIdAsync(id);
                if (existingOrder == null)
                {
                    return NotFound();
                }

                // Sipariş ana bilgilerini güncelle
                existingOrder.CustomerId = salesorderview.CustomerId!.Value;
                existingOrder.TaxId = salesorderview.TaxId!.Value;
                existingOrder.AfterTaxAmount = salesorderview.AfterTaxAmount;
                existingOrder.TaxAmount = salesorderview.TaxAmount;
                existingOrder.OrderDate = salesorderview.OrderDate;
                existingOrder.BeforeTaxAmount = salesorderview.BeforeTaxAmount;
                existingOrder.Number = salesorderview.Number;
                existingOrder.Description = salesorderview.Description;
                existingOrder.OrderStatus = (SalesOrderStatus)salesorderview.SalesStateId!;
                existingOrder.CreatedAtUtc = DateTime.Now;
                await _repository.UpdateAsync(existingOrder);

                // Mevcut kalemleri sil
                await _salesorderitemservice.DeleteAllByOrderIdAsync(existingOrder.Id);

                // Yeni kalemleri ekle
                if (salesorderview.SalesOrderItems != null)
                {
                    foreach (var item in salesorderview.SalesOrderItems)
                    {
                        var newItem = new SalesOrderItem
                        {
                            Quantity = item.Quantity,
                            UrunId = item.UrunId,
                            SalesOrderId = existingOrder.Id,
                            UnitPrice = item.UnitPrice,
                            Summary = existingOrder.Description
                        };
                        await _salesorderitemservice.AddAsync(newItem);
                    }
                }

                TempData["Success"] = "Satış Siparişi başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satış Siparişi güncellenirken hata oluştu: {Message}", ex.Message);
                ModelState.AddModelError("", "Satış Siparişi güncellenirken bir hata oluştu.");
            }

            // Hata durumunda dropdown listeleri yeniden yükle
            await PrepareDropdowns(salesorderview);

            return View(salesorderview);
        }

        [Authorize(Policy = Permissions.CustomerContact.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var salesorder = await _repository.GetByIdAsync(id);
            if (salesorder == null)
            {
                return NotFound();
            }
            return View(salesorder);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerContact.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _salesorderitemservice.DeleteAllByOrderIdAsync(id);
            var message = await _repository.DeleteAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }
        private async Task PrepareDropdowns(SalesOrderViewModel model)
        {
            model.Customers = (await _customerService.GetAllCustomerAsync())
                .Select(s => new SelectListItem
                {
                    Value = s.ID.ToString(),
                    Text = s.Name
                }).ToList();

            model.taxes = (await _taxservice.GetAllTaxAsync())
                .Select(e => new SelectListItem
                {
                    Value = e.ID.ToString(),
                    Text = $"{e.Name} %{e.Percentage}"
                }).ToList();

            model.SalesState = list()
                .Select(e => new SelectListItem
                {
                    Value = e.Value.ToString(),
                    Text = e.Text
                }).ToList();

            model.Products = (await _productservice.GetAllUrunsAsync())
                .Select(e => new SelectListItem
                {
                    Value = e.ModelId!.Value.ToString(),
                    Text = e.Model!.ModelAdi
                }).ToList();
        }

    }
}
