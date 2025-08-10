namespace Varlık_ZimmetDepoYonetimi.UI.Models
{
    public class DeliveryOrderitem
    {
        public int Id { get; set; }
        public int? DeliveryOrderId { get; set; }
        public DeliveryOrder? DeliveryOrder { get; set; }
        public int? UrunId { get; set; }
        public Urun? Urun { get; set; }
        public int? DepoID { get; set; }
        public Depo? Depo { get; set; }
        public string? Summary { get; set; }
        public decimal? UnitPrice { get; set; } = 0;
        public double? Quantity { get; set; } = 1;
        public decimal? Total => (decimal)(Quantity ?? 0) * (UnitPrice ?? 0);
    }
}
