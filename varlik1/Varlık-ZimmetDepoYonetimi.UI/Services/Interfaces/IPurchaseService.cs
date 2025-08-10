using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseOrder>> GetAllAsync();
        Task<PurchaseOrder> GetByIdAsync(int id);
        Task AddAsync(PurchaseOrder entity);
        Task UpdateAsync(PurchaseOrder entity);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetSelectListAsync();
    }
}
