namespace Varlık_ZimmetDepoYonetimi.UI.Models
{
    public class PurchaseOrderItem
    {
        public int Id { get; set; }
        public int? PurchaseOrderId { get; set; }
        public PurchaseOrder? PurchaseOrder { get; set; }
        public int? UrunId { get; set; }
        public Urun? Urun { get; set; }
        public string? Summary { get; set; }
        public decimal? UnitPrice { get; set; } = 0;
        public double? Quantity { get; set; } = 1;
        public decimal? Total => (decimal)(Quantity ?? 0) * (UnitPrice ?? 0);
    }
}
