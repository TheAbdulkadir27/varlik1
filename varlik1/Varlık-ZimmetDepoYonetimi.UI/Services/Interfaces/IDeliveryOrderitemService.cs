using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IDeliveryOrderitemService
    {
        Task<IEnumerable<DeliveryOrderitem>> GetAllAsync();
        Task<DeliveryOrderitem> GetByIdAsync(int id);
        Task AddAsync(DeliveryOrderitem entity);
        Task UpdateAsync(DeliveryOrderitem entity);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetSelectListAsync();
        Task DeleteAllByOrderIdAsync(int orderId);
    }
}
