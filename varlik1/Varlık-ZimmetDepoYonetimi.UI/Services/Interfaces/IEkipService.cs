using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Services.Interfaces
{
    public interface IEkipService
    {
        Task<IEnumerable<Ekip>> GetAllAsync();
        Task<Ekip> GetEkipByIdAsync(int id);
        Task AddEkipAsync(Ekip ekip);
        Task UpdateEkipAsync(Ekip ekip);
        Task<string> DeleteEkipAsync(int id);
        Task AddSirketEkipAsync(SirketEkip sirketEkip);
    }
}