using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Services.Interfaces
{
    public interface IModelService
    {
        Task<IEnumerable<Model>> GetAllModelsAsync();
        Task<Model> GetModelByIdAsync(int id);
        Task AddModelAsync(Model model);
        Task UpdateModelAsync(Model model);
        Task<string> DeleteModelAsync(int id);
        Task<IEnumerable<SelectListItem>> GetModelSelectListAsync();
    }
}