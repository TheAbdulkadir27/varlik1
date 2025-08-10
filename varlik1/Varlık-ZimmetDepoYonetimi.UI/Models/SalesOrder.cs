using System.ComponentModel;

namespace Varlık_ZimmetDepoYonetimi.UI.Models
{
    public class SalesOrder
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTime? OrderDate { get; set; }
        public SalesOrderStatus? OrderStatus { get; set; }
        public string? Description { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int? TaxId { get; set; }
        public Tax? Tax { get; set; }
        public decimal? BeforeTaxAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? AfterTaxAmount { get; set; }
        public DateTime CreatedAtUtc { get; set; } 
        public ICollection<SalesOrderItem> SalesOrderItemList { get; set; } = new List<SalesOrderItem>();
    }
    public enum SalesOrderStatus
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
