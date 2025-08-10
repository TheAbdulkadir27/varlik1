using System.ComponentModel.DataAnnotations;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Display(Name = "Mevcut Roller")]
        public List<string> CurrentRoles { get; set; }

        [Display(Name = "Tüm Roller")]
        public List<string> AllRoles { get; set; }

        [Display(Name = "Seçilen Roller")]
        public List<string> SelectedRoles { get; set; }

        public UserEditViewModel()
        {
            Id = string.Empty;
            UserName = string.Empty;
            Email = string.Empty;
            CurrentRoles = new List<string>();
            AllRoles = new List<string>();
            SelectedRoles = new List<string>();
        }
    }
}