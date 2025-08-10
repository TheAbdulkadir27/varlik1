using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class EkipViewModel
    {
        public EkipViewModel()
        {
            SeciliSirketler = new List<int>();
            Sirketler = new List<SelectListItem>();
        }

        public int EkipId { get; set; }

        [Required(ErrorMessage = "Ekip adı zorunludur")]
        [StringLength(100)]
        public string? EkipAdi { get; set; }

        public IEnumerable<int> SeciliSirketler { get; set; }
        public IEnumerable<SelectListItem> Sirketler { get; set; }
        public bool AktifMi { get; set; } = true;
    }
}