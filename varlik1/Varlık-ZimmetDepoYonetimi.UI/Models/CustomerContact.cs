namespace Varlık_ZimmetDepoYonetimi.UI.Models
{
    public class CustomerContact
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public string? JobTitle { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Description { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
