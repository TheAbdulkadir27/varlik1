using System.ComponentModel.DataAnnotations;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class YetkiListeViewModel
    {
        [Display(Name = "Kategori")]
        public string Kategori { get; set; }

        [Display(Name = "Yetki Adı")]
        public string YetkiAdi { get; set; }

        [Display(Name = "Tam Adı")]
        public string TamAdi { get; set; }

        public YetkiListeViewModel()
        {
            Kategori = string.Empty;
            YetkiAdi = string.Empty;
            TamAdi = string.Empty;
        }
    }

    public class YetkiEkleViewModel
    {
        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [Display(Name = "Kategori")]
        public string Kategori { get; set; }

        [Required(ErrorMessage = "Yetki adı zorunludur.")]
        [Display(Name = "Yetki Adı")]
        public string YetkiAdi { get; set; }

        public YetkiEkleViewModel()
        {
            Kategori = string.Empty;
            YetkiAdi = string.Empty;
        }
    }
}