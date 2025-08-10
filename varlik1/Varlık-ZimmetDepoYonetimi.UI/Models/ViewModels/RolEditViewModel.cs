using System.ComponentModel.DataAnnotations;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class RolEditViewModel
    {
        public string RolId { get; set; }

        [Required(ErrorMessage = "Rol adı zorunludur.")]
        [Display(Name = "Rol Adı")]
        public string RolAdi { get; set; }

        public List<YetkiViewModel> TumYetkiler { get; set; }
        public List<string> SeciliYetkiler { get; set; }

        public RolEditViewModel()
        {
            RolId = string.Empty;
            RolAdi = string.Empty;
            TumYetkiler = new List<YetkiViewModel>();
            SeciliYetkiler = new List<string>();
        }
    }

    public class YetkiTalepAdminViewModel
    {
        public int TalepId { get; set; }
        public string? KullaniciAdi { get; set; }
        public string? Yetki { get; set; }
        public string? Aciklama { get; set; }
        public DateTime TalepTarihi { get; set; }
        public TalepDurumu Durum { get; set; }
    }
    public class YetkiViewModel
    {
        public string Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }

        public YetkiViewModel()
        {
            Id = string.Empty;
            Ad = string.Empty;
            Aciklama = string.Empty;
        }
    }
    //Yetki Talep
    public enum TalepDurumu
    {
        Beklemede = 0,
        Onaylandi = 1,
        Reddedildi = 2
    }
    public class YetkiTalepViewModel
    {
        public List<string> SecilenYetkiler { get; set; } = new();
        public string Aciklama { get; set; }
        public List<YetkiViewModel> TumYetkiler { get; set; } = new();
    }

}