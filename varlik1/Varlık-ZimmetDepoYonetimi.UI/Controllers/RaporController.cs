using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class RaporController : Controller
    {
        private readonly IPurchaseService _purchaseOrderService;
        private readonly IVendorService _VendorService;
        private readonly ISalesOrderService _salesOrderService;
        private readonly ICustomerService _customerService;
        private readonly IRaporService _raporService;
        private readonly IZimmetService _zimmetService;
        private readonly IUrunService _urunService;
        private readonly ILogger<RaporController> _logger;

        public RaporController(
            IRaporService raporService,
            IZimmetService zimmetService,
            IUrunService urunService,
            ILogger<RaporController> logger,
            ISalesOrderService salesOrderService,
            ICustomerService customerService,
            IPurchaseService purchaseOrderService,
            IVendorService vendorService)
        {
            _raporService = raporService;
            _zimmetService = zimmetService;
            _urunService = urunService;
            _logger = logger;
            _salesOrderService = salesOrderService;
            _customerService = customerService;
            _purchaseOrderService = purchaseOrderService;
            _VendorService = vendorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = Permissions.Raporlar.ZimmetRaporu)]
        public async Task<IActionResult> ZimmetRaporu()
        {
            var model = new ZimmetRaporViewModel
            {
                ToplamZimmetSayisi = await _zimmetService.GetToplamZimmetSayisiAsync(),
                ZimmetliUrunOrani = await _zimmetService.GetZimmetliUrunOraniAsync(),
                AktifZimmetler = await _zimmetService.GetAktifZimmetlerAsync(),
                DepartmanBazliZimmetler = await _zimmetService.GetDepartmanBazliZimmetlerAsync(),
                calintiUrunOrani = await _zimmetService.GetCalintiUrunOraniAsync(),
            };

            return View(model);
        }

        [Authorize(Policy = Permissions.Raporlar.StokRaporu)]
        public async Task<IActionResult> StokRaporu()
        {
            var model = new StokRaporViewModel
            {
                ToplamUrunSayisi = await _urunService.GetToplamUrunSayisiAsync(),
                DusukStokluUrunler = await _urunService.GetDusukStokluUrunlerAsync(),
                StokDurumu = await _urunService.GetStokDurumuAsync(),
                SonStokHareketleri = await _raporService.GetSonStokHareketleriAsync()
            };
            return View(model);
        }
        public async Task<IActionResult> SalesOrderReport()
        {
            var salesOrders = await _salesOrderService.GetAllAsync();
            var viewModelList = new List<SalesOrderReportViewModel>();

            foreach (var order in salesOrders)
            {
                var customer = await _customerService.GetCustomerByIdAsync(order.CustomerId!.Value);

                foreach (var item in order.SalesOrderItemList)
                {
                    var product = await _urunService.GetUrunByIdAsync(item.UrunId!.Value);

                    viewModelList.Add(new SalesOrderReportViewModel
                    {
                        OrderNumber = order.Number,
                        CustomerName = customer.Name,
                        ProductNumber = product.Number,
                        ProductName = product.Model?.ModelAdi ?? "N/A",
                        TaxRate = Convert.ToDecimal(order.Tax!.Percentage),
                        UnitPrice = item.UnitPrice!.Value / 100m,
                        Quantity = Convert.ToInt32(item.Quantity!.Value),
                        CreatedAtUtc = DateTime.Now,
                    });
                }
            }
            return View(viewModelList);
        }
        public async Task<IActionResult> PurchaseOrderReport()
        {
            var PurchaseOrders = await _purchaseOrderService.GetAllAsync();
            var viewModelList = new List<PurchaseOrderReportViewModel>();

            foreach (var order in PurchaseOrders)
            {
                var vendor = await _VendorService.GetByIdAsync(order.VendorId!.Value);

                foreach (var item in order.PurchaseOrderItemList)
                {
                    var product = await _urunService.GetUrunByIdAsync(item.UrunId!.Value);

                    viewModelList.Add(new PurchaseOrderReportViewModel
                    {
                        OrderNumber = order.Number,
                        VendorName = vendor.Name,
                        ProductNumber = product.Number,
                        ProductName = product.Model?.ModelAdi ?? "N/A",
                        TaxRate = Convert.ToDecimal(order.Tax!.Percentage),
                        UnitPrice = item.UnitPrice!.Value / 100m,
                        Quantity = Convert.ToInt32(item.Quantity!.Value),
                        CreatedAtUtc = DateTime.Now,
                    });
                }
            }
            return View(viewModelList);
        }
    }
}