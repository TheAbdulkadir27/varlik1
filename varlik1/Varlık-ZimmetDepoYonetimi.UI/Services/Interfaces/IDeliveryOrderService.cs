using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IDeliveryOrderService
    {
        Task<IEnumerable<DeliveryOrder>> GetAllAsync();
        Task<DeliveryOrder> GetByIdAsync(int id);
        Task AddAsync(DeliveryOrder entity);
        Task UpdateAsync(DeliveryOrder entity);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetSelectListAsync();
    }
}
