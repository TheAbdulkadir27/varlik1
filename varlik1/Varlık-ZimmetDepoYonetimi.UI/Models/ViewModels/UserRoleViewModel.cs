namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        public UserRoleViewModel()
        {
            UserId = string.Empty;
            UserName = string.Empty;
            Email = string.Empty;
            Roles = new List<string>();
        }
    }

    public class UserRoleEditViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleViewModel> Roles { get; set; }

        public UserRoleEditViewModel()
        {
            UserId = string.Empty;
            UserName = string.Empty;
            Roles = new List<RoleViewModel>();
        }
    }

    public class RoleViewModel
    {
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }

        public RoleViewModel()
        {
            RoleName = string.Empty;
        }
    }
}