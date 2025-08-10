using System.ComponentModel;

namespace Varlık_ZimmetDepoYonetimi.UI.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTime? OrderDate { get; set; }
        public PurchaseOrderStatus? PurchaseStatus { get; set; }
        public string? Description { get; set; }
        public int? VendorId { get; set; }
        public Vendor? Vendor { get; set; }
        public int? TaxId { get; set; }
        public Tax? Tax { get; set; }
        public decimal? BeforeTaxAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? AfterTaxAmount { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public ICollection<PurchaseOrderItem> PurchaseOrderItemList { get; set; } = new List<PurchaseOrderItem>();
    }
    public enum PurchaseOrderStatus
    {
        [Description("Taslak")]
        Taslak = 0,
        [Description("İptal Edildi")]
        İptalEdildi = 1,
        [Description("Onaylandı")]
        Onaylandı = 2,
        [Description("Arşivlendi")]
        Arşivlendi = 3
    }
}
