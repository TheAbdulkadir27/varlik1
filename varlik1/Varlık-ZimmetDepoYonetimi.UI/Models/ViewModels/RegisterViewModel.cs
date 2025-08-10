using System.ComponentModel.DataAnnotations; //Validation işlemleri için gerekli kütüphane

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")] //Zorunlu ALAN
        [Display(Name = "Kullanıcı Adı")] // Kullanıcıya gösterilecek metin
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullanıcı adı en az 3, en fazla 50 karakter olmalıdır")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]

        public string Sifre { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı zorunludur")]
        [Display(Name = "Şifre Tekrar")]
        [DataType(DataType.Password)]
        [Compare("Sifre", ErrorMessage = "Şifreler eşleşmiyor")]
        public string SifreTekrar { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur")]
        [Display(Name = "E-posta")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; }

        public RegisterViewModel()
        {
            KullaniciAdi = string.Empty;
            Sifre = string.Empty;
            SifreTekrar = string.Empty;
            Email = string.Empty;
        }
    }
}
