namespace Varlık_ZimmetDepoYonetimi.UI.Models
{
    public class Vendor
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FaxNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Website { get; set; }

        public int VendorGroupId { get; set; }
        public VendorGroup? VendorGroup { get; set; }

        public int VendorCategoryId { get; set; }
        public VendorCategory? VendorCategory { get; set; }
        public ICollection<VendorContact> VendorContactList { get; set; } = new List<VendorContact>();


    }
}
