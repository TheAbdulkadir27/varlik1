using Microsoft.AspNetCore.Authorization;

namespace Varlık_ZimmetDepoYonetimi.UI.Attributes
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission) : base(permission)
        {
        }
    }
}