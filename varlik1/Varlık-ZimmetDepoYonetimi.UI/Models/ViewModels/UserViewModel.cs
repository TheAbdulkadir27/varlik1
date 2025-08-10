using System.ComponentModel.DataAnnotations;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Display(Name = "Roller")]
        public List<string> Roles { get; set; }

        public UserViewModel()
        {
            Id = string.Empty;
            UserName = string.Empty;
            Email = string.Empty;
            Roles = new List<string>();
        }
    }
}