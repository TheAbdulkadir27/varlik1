using Microsoft.AspNetCore.Mvc.Rendering;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class CustomerContactViewModel
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public string? JobTitle { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Description { get; set; }
        public int? CustomerId { get; set; }
        public List<SelectListItem> Customers { get; set; }
    }
}
