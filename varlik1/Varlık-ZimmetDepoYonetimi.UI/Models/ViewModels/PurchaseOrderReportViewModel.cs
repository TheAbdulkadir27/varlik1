namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class PurchaseOrderReportViewModel
    {
        public string OrderNumber { get; set; }
        public string VendorName { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }
        public int Quantity { get; set; }
        public decimal TaxAmount => UnitPrice * Quantity * TaxRate;
        public decimal Total => (UnitPrice * Quantity) + TaxAmount;
        public decimal NotaxTotal => UnitPrice * Quantity;
        public DateTime CreatedAtUtc { get; set; }
    }
}
