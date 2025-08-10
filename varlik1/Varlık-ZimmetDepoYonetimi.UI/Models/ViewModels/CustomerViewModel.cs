using Microsoft.AspNetCore.Mvc.Rendering;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class CustomerViewModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string Number { get; set; }
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
        public int? CustomerGroupID { get; set; }
        public List<SelectListItem> CustomerGroup { get; set; }
        public int? CustomerCategoryID { get; set; }
        public List<SelectListItem> CustomerCategory { get; set; }

    }
}
