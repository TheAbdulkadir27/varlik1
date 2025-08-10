using System.ComponentModel.DataAnnotations;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class RolViewModel
    {
        public string RolId { get; set; }

        [Display(Name = "Rol Adı")]
        public string RolAdi { get; set; }

        [Display(Name = "Kullanıcı Sayısı")]
        public int KullaniciSayisi { get; set; }

        [Display(Name = "Yetkiler")]
        public List<string> Yetkiler { get; set; }

        public RolViewModel()
        {
            RolId = string.Empty;
            RolAdi = string.Empty;
            Yetkiler = new List<string>();
        }
    }
}