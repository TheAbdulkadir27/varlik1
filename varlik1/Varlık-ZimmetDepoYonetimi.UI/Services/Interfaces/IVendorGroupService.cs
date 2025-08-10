using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IVendorGroupService
    {
        Task<IEnumerable<VendorGroup>> GetAllAsync();
        Task<VendorGroup> GetByIdAsync(int id);
        Task AddAsync(VendorGroup entity);
        Task UpdateAsync(VendorGroup entity);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetSelectListAsync();
    }
}
