using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IProductGroupService
    {
        Task<IEnumerable<ProductGroup>> GetAllAsync();
        Task<ProductGroup> GetByIdAsync(int id);
        Task AddAsync(ProductGroup entity);
        Task UpdateAsync(ProductGroup entity);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetSelectListAsync();
    }
}
