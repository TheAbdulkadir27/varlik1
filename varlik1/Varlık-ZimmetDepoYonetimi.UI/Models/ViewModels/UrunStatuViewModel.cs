using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class UrunStatuViewModel
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; } = string.Empty;
        public int StatuId { get; set; }
        public string StatuAdi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yeni statü seçimi zorunludur")]
        public int YeniStatuId { get; set; }

        public string? Açıklama { get; set; }

        public List<SelectListItem> Statuler { get; set; } = new List<SelectListItem>();
    }
}