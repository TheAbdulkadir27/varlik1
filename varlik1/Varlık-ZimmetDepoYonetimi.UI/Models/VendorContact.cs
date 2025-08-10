namespace Varlık_ZimmetDepoYonetimi.UI.Models
{
    public class VendorContact
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public string? JobTitle { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Description { get; set; }
        public int? VendorId { get; set; }
        public Vendor? Vendor { get; set; }
    }
}
