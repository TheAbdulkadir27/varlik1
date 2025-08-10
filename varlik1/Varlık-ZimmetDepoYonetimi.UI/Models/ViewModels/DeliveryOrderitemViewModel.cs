using Microsoft.AspNetCore.Mvc.Rendering;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class DeliveryOrderViewModel
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTime? DeliveryOrderDate { get; set; }
        public string? Description { get; set; }
        public int? SalesStateId { get; set; }
        public int? ProductId { get; set; }
        public int? SalesOrderId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public List<SelectListItem> SalesState { get; set; }
        public List<SelectListItem> WareHouses { get; set; } = new();
        public List<SelectListItem> Products { get; set; }
        public List<DeliveryOrderItemViewModel> DeliveryOrderItems { get; set; } = new();
        public List<DeliveryProductViewModel> ProductsJson { get; set; } = new();
        public List<SelectListItem> SalesOrderList { get; set; }
    }
    public class DeliveryOrderItemViewModel
    {
        public int? UrunId { get; set; }
        public int? DepoID { get; set; }
        public int? DeliveryOrderId { get; set; }
        public string? Summary { get; set; }
        public decimal? UnitPrice { get; set; }
        public double? Quantity { get; set; }
        public decimal? Total { get; set; }
    }
    public class DeliveryProductViewModel
    {
        public int Id { get; set; }
        public string ModelAdi { get; set; } = string.Empty;
        public decimal UrunGuncelFiyat { get; set; }
        public int? StokMiktari { get; set; }
    }
}
