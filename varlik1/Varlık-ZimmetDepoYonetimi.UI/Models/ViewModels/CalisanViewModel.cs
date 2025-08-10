using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class CalisanViewModel
    {
        public CalisanViewModel()
        {
            Sirketler = new List<SelectListItem>();
            Ekipler = new List<SelectListItem>();
        }

        public int CalisanId { get; set; }

        [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; } = null!;

        [Required(ErrorMessage = "Abone No alanı zorunludur.")]
        [Display(Name = "Abone No")]
        public string AboneNo { get; set; } = null!;

        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        public string Telefon { get; set; } = null!;

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta")]
        public string Mail { get; set; } = null!;


        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [Display(Name = "Şifre")]
        public string Şifre { get; set; } = null!;


        [Required(ErrorMessage = "Şifre Tekrar alanı zorunludur.")]
        [Compare(nameof(Şifre), ErrorMessage = "Şifreler Uyuşmuyor")]
        [Display(Name = "Şifre Tekrar")]
        public string ŞifreTekrar { get; set; } = null!;


        [Required(ErrorMessage = "Şirket seçimi zorunludur.")]
        [Display(Name = "Şirket")]
        public int SirketId { get; set; }

        [Required(ErrorMessage = "Ekip seçimi zorunludur.")]
        [Display(Name = "Ekip")]
        public int EkipId { get; set; }

        public List<SelectListItem> Sirketler { get; set; }
        public List<SelectListItem> Ekipler { get; set; }
    }
}