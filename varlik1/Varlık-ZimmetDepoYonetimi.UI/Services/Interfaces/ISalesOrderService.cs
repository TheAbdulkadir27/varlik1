using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface ISalesOrderService
    {
        Task<IEnumerable<SalesOrder>> GetAllAsync();
        Task<SalesOrder> GetByIdAsync(int id);
        Task AddAsync(SalesOrder entity);
        Task UpdateAsync(SalesOrder entity);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetSelectListAsync();
    }
}
