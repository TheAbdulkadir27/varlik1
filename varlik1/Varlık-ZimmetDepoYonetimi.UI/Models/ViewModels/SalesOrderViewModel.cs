using Microsoft.AspNetCore.Mvc.Rendering;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class SalesOrderViewModel
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Description { get; set; }
        public int? CustomerId { get; set; }
        public List<SelectListItem> Customers { get; set; }
        public int? TaxId { get; set; }
        public List<SelectListItem> taxes { get; set; }
        public decimal? BeforeTaxAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? AfterTaxAmount { get; set; }
        public int? SalesStateId { get; set; }
        public int? ProductId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public List<SelectListItem> SalesState { get; set; }
        public List<SelectListItem> Products { get; set; } = new();
        public List<SalesOrderItemViewModel> SalesOrderItems { get; set; } = new();
        public List<ProductJsonViewModel> ProductsJson { get; set; } = new();
        public List<Tax> AllTaxes { get; set; }
        public List<TaxJsonViewModel> AllTaxesJson { get; set; } = new();
    }
    public class ProductJsonViewModel
    {
        public int Id { get; set; }
        public string ModelAdi { get; set; } = string.Empty;
        public decimal UrunGuncelFiyat { get; set; }
    }
    public class SalesOrderItemViewModel
    {
        public int? UrunId { get; set; }
        public int? SalesOrderId { get; set; }
        public string? Summary { get; set; }
        public decimal? UnitPrice { get; set; }
        public double? Quantity { get; set; }
        public decimal? Total { get; set; }
    }
    public class TaxJsonViewModel
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public double? Percentage { get; set; }
    }
}
