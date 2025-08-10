using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IVendorCategoryService
    {
        Task<IEnumerable<VendorCategory>> GetAllAsync();
        Task<VendorCategory> GetByIdAsync(int id);
        Task AddAsync(VendorCategory entity);
        Task UpdateAsync(VendorCategory entity);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetSelectListAsync();
    }
}
