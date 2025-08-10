using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IVendorContactService
    {
        Task<IEnumerable<VendorContact>> GetAllAsync();
        Task<VendorContact> GetByIdAsync(int id);
        Task AddAsync(VendorContact vendorcontact);
        Task UpdateAsync(VendorContact vendorcontact);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetSelectListAsync();
    }
}
