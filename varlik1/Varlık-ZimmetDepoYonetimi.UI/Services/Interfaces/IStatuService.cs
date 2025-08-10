using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;

namespace Services.Interfaces
{
    public interface IStatuService
    {
        Task<IEnumerable<Statu>> GetAllAsync();
        Task<Statu?> GetByIdAsync(int id);
        Task AddAsync(Statu statu);
        Task UpdateAsync(Statu statu);
        Task DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetStatuSelectListAsync();
        Task<IEnumerable<UrunStatuListesiViewModel>> GetUrunStatuListesiAsync();
    }
}