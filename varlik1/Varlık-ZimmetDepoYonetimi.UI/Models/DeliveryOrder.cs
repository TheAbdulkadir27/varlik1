using System.ComponentModel;

namespace Varlık_ZimmetDepoYonetimi.UI.Models
{
    public class DeliveryOrder
    {
        public int ID { get; set; }
        public string? Number { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DeliveryOrderStatus? Status { get; set; }
        public string? Description { get; set; }
        public int? SalesOrderId { get; set; }
        public SalesOrder? SalesOrder { get; set; }
        public ICollection<DeliveryOrderitem> DeliveryOrderItemList { get; set; } = new List<DeliveryOrderitem>();
    }
    public enum DeliveryOrderStatus
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
