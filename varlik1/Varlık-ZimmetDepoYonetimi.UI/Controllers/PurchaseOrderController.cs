using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using Varlık_ZimmetDepoYonetimi.UI.Models.Global;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    public class PurchaseOrderController : Controller
    {

        private readonly IPurchaseService _repository;
        private readonly IVendorService _vendorService;
        private readonly ITaxService _taxservice;
        private readonly IUrunService _productservice;
        private readonly IPurchaseOrderitemService _purchaseorderitemservice;
        private readonly ILogger<PurchaseOrderController> _logger;
        public PurchaseOrderController(IPurchaseService repository, IVendorService vendorService, ITaxService taxservice, IUrunService productservice, IPurchaseOrderitemService purchaseorderitemservice, ILogger<PurchaseOrderController> logger)
        {
            _repository = repository;
            _vendorService = vendorService;
            _taxservice = taxservice;
            _productservice = productservice;
            _purchaseorderitemservice = purchaseorderitemservice;
            _logger = logger;
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
            var PurchaseOrder = new PurchaseOrderViewModel();
            try
            {
                var autonumber = _repository.GetAllAsync().Result.Count() + 1;
                PurchaseOrder.Number = GenerateNumber.GenerateProductNumber(autonumber, Models.Enums.NumberModels.PurchaseOrder);
                PurchaseOrder.Vendors = (await _vendorService.GetAllAsync())
                    .Select(e => new SelectListItem
                    {
                        Value = e.ID.ToString(),
                        Text = e.Name
                    }).ToList();

                PurchaseOrder.taxes = (await _taxservice.GetAllTaxAsync())
                    .Select(e => new SelectListItem
                    {
                        Value = e.ID.ToString(),
                        Text = $"{e.Name} %{e.Percentage}"
                    }).ToList();

                // AllTaxes listesi gerçek vergi nesneleri için
                PurchaseOrder.AllTaxes = (List<Tax>)await _taxservice.GetAllTaxAsync();

                PurchaseOrder.PurchaseState = list()
                    .Select(e => new SelectListItem
                    {
                        Value = e.Value.ToString(),
                        Text = e.Text
                    }).ToList();

                // Products listesi SelectListItem olarak
                PurchaseOrder.Products = (await _productservice.GetAllUrunsAsync())
                    .Select(e => new SelectListItem
                    {
                        Value = e.UrunId!.ToString(),
                        Text = e.Model!.ModelAdi
                    }).ToList();

                // ProductsJson olarak ürünlerin fiyat ve bilgileri (JS için)
                PurchaseOrder.ProductsJson = (await _productservice.GetAllUrunsAsync())
                    .Select(e => new ProductJsonViewModel1
                    {
                        Id = e.UrunId,
                        ModelAdi = e.Model.ModelAdi,
                        UrunGuncelFiyat = Convert.ToDecimal(e.UrunGuncelFiyat)
                    }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satın Alma Siparişi oluşturma sayfası yüklenirken hata oluştu");
                PurchaseOrder.Vendors = new List<SelectListItem>();
                PurchaseOrder.taxes = new List<SelectListItem>();
                PurchaseOrder.PurchaseState = new List<SelectListItem>();
                PurchaseOrder.Products = new List<SelectListItem>();
                PurchaseOrder.AllTaxes = new List<Tax>();
                PurchaseOrder.ProductsJson = new List<ProductJsonViewModel1>();
            }
            return View(PurchaseOrder);
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
        public async Task<IActionResult> Create(PurchaseOrderViewModel purchaseorderview)
        {
            var purchaseorder = new PurchaseOrder()
            {
                Id = purchaseorderview.Id,
                VendorId = purchaseorderview.VendorId,
                TaxId = purchaseorderview.TaxId,
                AfterTaxAmount = purchaseorderview.AfterTaxAmount,
                TaxAmount = purchaseorderview.TaxAmount,
                OrderDate = purchaseorderview.OrderDate,
                BeforeTaxAmount = purchaseorderview.BeforeTaxAmount,
                Number = purchaseorderview.Number,
                Description = purchaseorderview.Description,
                PurchaseStatus = (PurchaseOrderStatus)purchaseorderview.PurchaseStateId!,
                CreatedAtUtc = DateTime.Now
            };

            var firstItem = purchaseorderview.PurchaseOrderItems.SingleOrDefault();
            if (firstItem != null)
            {
                try
                {
                    await _repository.AddAsync(purchaseorder);
                    var purchaseorderitem = new PurchaseOrderItem()
                    {
                        Quantity = firstItem.Quantity,
                        UrunId = firstItem.UrunId,
                        PurchaseOrderId = purchaseorder.Id,
                        UnitPrice = firstItem.UnitPrice,
                        Summary = purchaseorder.Description
                    };
                    await _purchaseorderitemservice.AddAsync(purchaseorderitem);
                    TempData["Success"] = "Satın Alma Siparişi başarıyla eklendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, "Satın Alma Siparisi eklenirken hata oluştu: {Message}", ex.Message);
                    ModelState.AddModelError("", "Satın Alma Siparisi eklenirken bir hata oluştu.");
                }
            }
            return View(purchaseorder);
        }

        [Authorize(Policy = Permissions.CustomerContact.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {

            var order = await _repository.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            // Ürünler
            var allProducts = await _productservice.GetAllUrunsAsync();
            var productJsonList = allProducts.Select(p => new ProductJsonViewModel1
            {
                Id = p.UrunId,
                ModelAdi = p.Model?.ModelAdi ?? "",
                UrunGuncelFiyat = Convert.ToDecimal(p.UrunGuncelFiyat)
            }).ToList();

            // Vergiler
            var allTaxes = await _taxservice.GetAllTaxAsync();
            var taxJsonList = allTaxes.Select(t => new TaxJsonViewModel1
            {
                ID = t.ID,
                Name = t.Name,
                Percentage = t.Percentage
            }).ToList();


            var viewModel = new PurchaseOrderViewModel
            {
                Id = order.Id,
                Number = order.Number,
                OrderDate = order.OrderDate,
                VendorId = order.VendorId,
                TaxId = order.TaxId,
                PurchaseStateId = (int?)order.PurchaseStatus,
                Description = order.Description,
                CreatedAtUtc = DateTime.Now,


                PurchaseOrderItems = order.PurchaseOrderItemList.Select(i => new PurchaseOrderItemViewModel
                {
                    PurchaseOrderId = i.Id,
                    UrunId = i.UrunId,
                    UnitPrice = i.UnitPrice / 100m,
                    Quantity = i.Quantity,
                    Total = i.Total / 100m
                }).ToList(),

                Vendors = (await _vendorService.GetAllAsync()).Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name
                }).ToList(),

                PurchaseState = list().Select(s => new SelectListItem
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
        public async Task<IActionResult> Edit(int id, PurchaseOrderViewModel purchaseorderview)
        {
            if (id != purchaseorderview.Id)
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
                existingOrder.VendorId = purchaseorderview.VendorId!.Value;
                existingOrder.TaxId = purchaseorderview.TaxId!.Value;
                existingOrder.AfterTaxAmount = purchaseorderview.AfterTaxAmount;
                existingOrder.TaxAmount = purchaseorderview.TaxAmount;
                existingOrder.OrderDate = purchaseorderview.OrderDate;
                existingOrder.BeforeTaxAmount = purchaseorderview.BeforeTaxAmount;
                existingOrder.Number = purchaseorderview.Number;
                existingOrder.Description = purchaseorderview.Description;
                existingOrder.PurchaseStatus = (PurchaseOrderStatus)purchaseorderview.PurchaseStateId!;
                existingOrder.CreatedAtUtc = DateTime.Now;
                await _repository.UpdateAsync(existingOrder);

                // Mevcut kalemleri sil
                await _purchaseorderitemservice.DeleteAllByOrderIdAsync(existingOrder.Id);

                // Yeni kalemleri ekle
                if (purchaseorderview.PurchaseOrderItems != null)
                {
                    foreach (var item in purchaseorderview.PurchaseOrderItems)
                    {
                        var newItem = new PurchaseOrderItem
                        {
                            Quantity = item.Quantity,
                            UrunId = item.UrunId,
                            PurchaseOrderId = existingOrder.Id,
                            UnitPrice = item.UnitPrice,
                            Summary = existingOrder.Description
                        };
                        await _purchaseorderitemservice.AddAsync(newItem);
                    }
                }

                TempData["Success"] = "Satın Alma Siparişi başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satın Alma Siparişi güncellenirken hata oluştu: {Message}", ex.Message);
                ModelState.AddModelError("", "Satın Alma Siparişi güncellenirken bir hata oluştu.");
            }

            // Hata durumunda dropdown listeleri yeniden yükle
            await PrepareDropdowns(purchaseorderview);

            return View(purchaseorderview);
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
            await _purchaseorderitemservice.DeleteAllByOrderIdAsync(id);
            var message = await _repository.DeleteAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }
        private async Task PrepareDropdowns(PurchaseOrderViewModel model)
        {
            model.Vendors = (await _vendorService.GetAllAsync())
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

            model.PurchaseState = list()
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
