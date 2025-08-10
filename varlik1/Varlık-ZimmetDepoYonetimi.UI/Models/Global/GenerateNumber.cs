using Varlık_ZimmetDepoYonetimi.UI.Models.Enums;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.Global
{
    public static class GenerateNumber
    {
        public static string GenerateProductNumber(int orderNumber = 1, NumberModels Models = NumberModels.Product)
        {
            string prefix = orderNumber.ToString("D4");
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            string suffix = string.Empty;
            if (Models == NumberModels.Product)
            {
                suffix = "ART";
            }
            if (Models == NumberModels.Customer)
            {
                suffix = "CTS";
            }
            if (Models == NumberModels.SalesOrder)
            {
                suffix = "SO";
            }
            if (Models == NumberModels.Vendor)
            {
                suffix = "VND";
            }
            if (Models == NumberModels.PurchaseOrder)
            {
                suffix = "PO";
            }
            if (Models == NumberModels.DeliveryOrder)
            {
                suffix = "DO";
            }
            return $"{prefix}{datePart}{suffix}";
        }
    }
}
