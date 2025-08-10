namespace Varlık_ZimmetDepoYonetimi.UI.Models
{
    public class Customer
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

        public int CustomerGroupId { get; set; }
        public CustomerGroup? CustomerGroup { get; set; }
      
        public int CustomerCategoryId { get; set;}
        public CustomerCategory? CustomerCategory { get; set; }
        public ICollection<CustomerContact> CustomerContactList { get; set; } = new List<CustomerContact>();
    }
}
