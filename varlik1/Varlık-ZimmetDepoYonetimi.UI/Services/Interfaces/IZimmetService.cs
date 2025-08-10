using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Services.Interfaces
{
    public interface IZimmetService
    {
        Task<IEnumerable<Zimmet>> GetAllZimmetsAsync();
        Task<Zimmet?> GetZimmetByIdAsync(int id);
        Task<IEnumerable<Zimmet>> GetZimmetlerByCalisanIdAsync(int calisanId);
        Task AddZimmetAsync(Zimmet zimmet);
        Task UpdateZimmetAsync(Zimmet zimmet);
        Task DeleteZimmetAsync(int id);
        Task<IEnumerable<Zimmet>> GetAktifZimmetlerAsync();
        Task<int> GetToplamZimmetSayisiAsync();
        Task<decimal> GetZimmetliUrunOraniAsync();
        Task<decimal> GetCalintiUrunOraniAsync();
        decimal GetCalintiUrunOrani(int calısanid);
        Task<IEnumerable<Zimmet>> GetZimmetlerByUrunIdAsync(int urunId);
        Task<Dictionary<string, int>> GetDepartmanBazliZimmetlerAsync();
    }
}