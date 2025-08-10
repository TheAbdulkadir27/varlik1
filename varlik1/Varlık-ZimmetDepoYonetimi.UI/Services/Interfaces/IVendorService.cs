using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<Vendor>> GetAllAsync();
        Task<Vendor> GetByIdAsync(int id);
        Task AddAsync(Vendor vendor);
        Task UpdateAsync(Vendor vendor);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetSelectListAsync();
    }
}
