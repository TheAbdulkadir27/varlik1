using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System.Linq;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.Global;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Services.Implementations;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;
using static Permissions;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    public class DeliveryOrderController : Controller
    {
        private readonly IDeliveryOrderService _repository;
        private readonly IDeliveryOrderitemService _deliveryorderitemservice;
        private readonly IUrunService _urunservice;
        private readonly ISalesOrderService _salesorderservice;
        private readonly ILogger<DeliveryOrderController> _logger;
        private readonly VarlikZimmetEnginContext _context;
        public DeliveryOrderController(IDeliveryOrderService repository, IDeliveryOrderitemService deliveryorderitemservice, ILogger<DeliveryOrderController> logger, IUrunService urunservice, ISalesOrderService salesorderservice, VarlikZimmetEnginContext context)
        {
            _repository = repository;
            _deliveryorderitemservice = deliveryorderitemservice;
            _logger = logger;
            _urunservice = urunservice;
            _salesorderservice = salesorderservice;
            _context = context;
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
            var deliveryOrder = new DeliveryOrderViewModel();

            try
            {
                var count = (await _repository.GetAllAsync()).Count();
                deliveryOrder.Number = GenerateNumber.GenerateProductNumber(count + 1, Models.Enums.NumberModels.DeliveryOrder);

                // Onay Durumları
                deliveryOrder.SalesState = list()
                    .Select(e => new SelectListItem
                    {
                        Value = e.Value.ToString(),
                        Text = e.Text
                    }).ToList();

                // Ürünler
                var urunler = await _urunservice.GetAllUrunsAsync();

                // Depo listesi
                var depolar = urunler
                    .SelectMany(x => x.DepoUruns)
                    .Where(du => du.Depo != null)
                    .Select(du => du.Depo)
                    .Distinct()
                    .ToList();

                deliveryOrder.WareHouses = depolar
                    .Select(d => new SelectListItem
                    {
                        Value = d.DepoId.ToString(),
                        Text = d.DepoAdi
                    }).ToList();

                // Ürün dropdown
                deliveryOrder.Products = urunler
                    .Select(u => new SelectListItem
                    {
                        Value = u.UrunId.ToString(),
                        Text = u.Model?.ModelAdi ?? "Model Adı Yok"
                    }).ToList();

                deliveryOrder.SalesOrderList = (await _salesorderservice.GetAllAsync())
                    .Where(x => !_context.DeliveryOrders.Select(x => x.SalesOrderId).Contains(x.Id)) 
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Number?.ToString()
                    })
                    .ToList();

                deliveryOrder.ProductsJson = urunler
                    .Select(u => new DeliveryProductViewModel
                    {
                        Id = u.UrunId,
                        ModelAdi = u.Model?.ModelAdi ?? "Model Adı Yok",
                        UrunGuncelFiyat = Convert.ToDecimal(u.UrunGuncelFiyat),
                        StokMiktari = u.DepoUruns.Sum(x => x.Miktar)
                    }).ToList();



            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Teslimat Emri oluşturma sayfası yüklenirken hata oluştu");

                deliveryOrder.SalesState = new List<SelectListItem>();
                deliveryOrder.WareHouses = new List<SelectListItem>();
                deliveryOrder.Products = new List<SelectListItem>();
                deliveryOrder.ProductsJson = new List<DeliveryProductViewModel>();
            }

            return View(deliveryOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeliveryOrderViewModel model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var deliveryOrder = new DeliveryOrder
                {
                    Number = model.Number,
                    DeliveryDate = DateTime.Now.AddDays(3),
                    Description = model.Description,
                    SalesOrderId = model.SalesOrderId,
                    Status = (DeliveryOrderStatus)model.SalesStateId!
                };

                _context.DeliveryOrders.Add(deliveryOrder);
                await _context.SaveChangesAsync();

                foreach (var item in model.DeliveryOrderItems)
                {
                    // Stok kontrolü (DepoId property'si ViewModel'de DepoID olarak yazılmış, controllerda da aynı olmalı)
                    var depoStok = await _context.DepoUruns
                        .FirstOrDefaultAsync(x => x.UrunId == item.UrunId && x.DepoId == item.DepoID);

                    var Urun = await _context.Urun
                        .FirstOrDefaultAsync(x => x.UrunId == item.UrunId);

                    if (depoStok == null || depoStok.Miktar < item.Quantity)
                    {
                        ModelState.AddModelError(string.Empty, $"'{item.UrunId}' ürününün '{item.DepoID}' deposunda yeterli stok yok.");
                        await FillViewData(model);
                        return View(model);
                    }

                    depoStok.Miktar -= (short)item.Quantity;
                    Urun.StokMiktari -= (short)item.Quantity;

                    _context.DepoUruns.Update(depoStok);
                    _context.Urun.Update(Urun);

                    var deliveryItem = new DeliveryOrderitem
                    {
                        DeliveryOrderId = deliveryOrder.ID,
                        UrunId = item.UrunId,
                        DepoID = item.DepoID,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };

                    _context.DeliveryOrderitems.Add(deliveryItem);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Teslimat emri kaydedilirken hata oluştu.");
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "Kayıt sırasında hata oluştu.");
                await FillViewData(model);
                return View(model);
            }
        }
        [Authorize(Policy = Permissions.CustomerContact.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var deliveryOrder = await _repository.GetByIdAsync(id);
            if (deliveryOrder == null)
            {
                return NotFound();
            }
            return View(deliveryOrder);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.CustomerContact.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _deliveryorderitemservice.DeleteAllByOrderIdAsync(id);
            var message = await _repository.DeleteAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
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
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetSalesOrderItems(int salesOrderId)
        {
            var urunler = await _urunservice.GetAllUrunsAsync();

            var stoklar = urunler.ToDictionary(
                u => u.UrunId,
                u => u.DepoUruns.Sum(d => d.Miktar)
            );

            var items = await _context.SalesOrderItem
                .Include(x => x.Urun)
                    .ThenInclude(p => p.Model)
                .Where(x => x.SalesOrderId == salesOrderId)
                .Select(x => new
                {
                    x.UrunId,
                    ProductName = x.Urun!.Model!.ModelAdi,
                    UnitPrice = x.Urun.UrunGuncelFiyat,
                    RequestedQuantity = x.Quantity,
                    StockQuantity = stoklar.ContainsKey(Convert.ToInt32(x.UrunId)) ? stoklar[Convert.ToInt32(x.UrunId)] : 0
                })
                .ToListAsync();

            return Json(items);
        }
        private async Task FillViewData(DeliveryOrderViewModel model)
        {
            var urunler = await _urunservice.GetAllUrunsAsync();
            var depolar = urunler.SelectMany(x => x.DepoUruns).Where(du => du.Depo != null).Select(du => du.Depo).Distinct().ToList();

            model.SalesState = list()
                .Select(e => new SelectListItem { Value = e.Value.ToString(), Text = e.Text }).ToList();

            model.WareHouses = depolar
                .Select(d => new SelectListItem { Value = d.DepoId.ToString(), Text = d.DepoAdi }).ToList();

            model.Products = urunler
                .Select(u => new SelectListItem
                {
                    Value = u.UrunId.ToString(),
                    Text = u.Model?.ModelAdi ?? "Model Adı Yok"
                }).ToList();

            model.ProductsJson = urunler
                .Select(u => new DeliveryProductViewModel
                {
                    Id = u.UrunId,
                    ModelAdi = u.Model?.ModelAdi ?? "Model Adı Yok",
                    UrunGuncelFiyat = Convert.ToDecimal(u.UrunGuncelFiyat),
                    StokMiktari = u.DepoUruns.Sum(x => x.Miktar)
                }).ToList();

            model.SalesOrderList = (await _salesorderservice.GetAllAsync())
                .Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Number?.ToString()
                }).ToList();
        }
    }
}
