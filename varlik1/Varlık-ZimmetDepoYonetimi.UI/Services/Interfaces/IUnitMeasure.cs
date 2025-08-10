using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IUnitMeasureService
    {
        Task<IEnumerable<UnitMeasure>> GetAllAsync();
        Task<UnitMeasure> GetByIdAsync(int id);
        Task AddAsync(UnitMeasure entity);
        Task UpdateAsync(UnitMeasure entity);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetSelectListAsync();
    }
}
