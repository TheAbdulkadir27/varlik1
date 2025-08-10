using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;

namespace Varlık_ZimmetDepoYonetimi.UI.Models
{
    public class PurchaseOrderViewModel
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Description { get; set; }
        public int? VendorId { get; set; }
        public List<SelectListItem> Vendors { get; set; }
        public int? TaxId { get; set; }
        public List<SelectListItem> taxes { get; set; }
        public decimal? BeforeTaxAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? AfterTaxAmount { get; set; }
        public int? PurchaseStateId { get; set; }
        public int? ProductId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public List<SelectListItem> PurchaseState { get; set; }
        public List<SelectListItem> Products { get; set; } = new();
        public List<PurchaseOrderItemViewModel> PurchaseOrderItems { get; set; } = new();
        public List<ProductJsonViewModel1> ProductsJson { get; set; } = new();
        public List<Tax> AllTaxes { get; set; }
        public List<TaxJsonViewModel1> AllTaxesJson { get; set; } = new();
    }
    public class PurchaseOrderItemViewModel
    {
        public int? UrunId { get; set; }
        public int? PurchaseOrderId { get; set; }
        public string? Summary { get; set; }
        public decimal? UnitPrice { get; set; }
        public double? Quantity { get; set; }
        public decimal? Total { get; set; }
    }
    public class TaxJsonViewModel1
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public double? Percentage { get; set; }
    }
    public class ProductJsonViewModel1
    {
        public int Id { get; set; }
        public string ModelAdi { get; set; } = string.Empty;
        public decimal UrunGuncelFiyat { get; set; }
    }
}
