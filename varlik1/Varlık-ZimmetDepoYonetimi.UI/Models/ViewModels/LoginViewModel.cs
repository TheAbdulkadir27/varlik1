using System.ComponentModel.DataAnnotations;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class LoginViewModel
    {
        public string? Login { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        [DataType(DataType.Password)]
        public string? Sifre { get; set; }

        public bool BeniHatirla { get; set; }

        public LoginViewModel()
        {
            Login = string.Empty;
            Sifre = string.Empty;
        }
    }
}
