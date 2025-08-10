using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Services.Interfaces
{
    public interface IUrunService
    {
        Task<IEnumerable<Urun>> GetAllUrunsAsync();
        Task<Urun?> GetUrunByIdAsync(int id);
        Task AddUrunAsync(Urun urun);
        Task UpdateUrunAsync(Urun urun);
        Task DeleteUrunAsync(int id);
        Task<int> GetToplamUrunSayisiAsync();
        Task<List<Urun>> GetDusukStokluUrunlerAsync();
        Task<Dictionary<string, int>> GetStokDurumuAsync();
        Task<IEnumerable<SelectListItem>> GetUrunSelectListAsync();
        Task<IEnumerable<SelectListItem>> GetZimmetsizUrunSelectListAsync(int modelid);
        Task AddUrunStatuAsync(UrunStatu urunStatu);
    }
}