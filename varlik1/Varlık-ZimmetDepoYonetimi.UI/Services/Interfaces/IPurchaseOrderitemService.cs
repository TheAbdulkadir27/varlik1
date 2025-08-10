using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IPurchaseOrderitemService
    {
        Task<IEnumerable<PurchaseOrderItem>> GetAllAsync();
        Task<PurchaseOrderItem> GetByIdAsync(int id);
        Task AddAsync(PurchaseOrderItem entity);
        Task UpdateAsync(PurchaseOrderItem entity);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetSelectListAsync();
        Task DeleteAllByOrderIdAsync(int purchaseId);
    }
}
