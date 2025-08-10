using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface ITaxService
    {
        Task<IEnumerable<Tax>> GetAllTaxAsync();
        Task<Tax> GetTaxByIdAsync(int id);
        Task AddTaxAsync(Tax tax);
        Task UpdateTaxAsync(Tax tax);
        Task<string> DeleteTaxAsync(int id);
        Task<IEnumerable<SelectListItem>> GetTaxSelectListAsync();
    }
}
