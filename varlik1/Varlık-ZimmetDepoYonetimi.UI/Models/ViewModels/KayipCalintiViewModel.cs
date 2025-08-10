using Microsoft.AspNetCore.Mvc.Rendering;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class KayipCalintiViewModel
    {
        public int ZimmetId { get; set; }
        public string? Aciklama { get; set; }
        public DateTime KayipTarihi { get; set; } = DateTime.Now;
        public bool KayipMi { get; set; }
        public List<SelectListItem> AktifZimmetler { get; set; } = new List<SelectListItem>();
    }
}