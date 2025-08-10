using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface ISalesOrderItemService
    {
        Task<IEnumerable<SalesOrderItem>> GetAllAsync();
        Task<SalesOrderItem> GetByIdAsync(int id);
        Task AddAsync(SalesOrderItem entity);
        Task UpdateAsync(SalesOrderItem entity);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetSelectListAsync();
        Task DeleteAllByOrderIdAsync(int orderId);
    }
}
