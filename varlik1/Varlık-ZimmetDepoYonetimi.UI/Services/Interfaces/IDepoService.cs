using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Services.Interfaces
{
    public interface IDepoService
    {
        Task<IEnumerable<Depo>> GetAllDeposAsync();
        Task<Depo?> GetDepoByIdAsync(int id);
        Task<IEnumerable<DepoUrun>> GetDepoUrunlerAsync(int depoId);
        Task AddDepoAsync(Depo depo);
        Task UpdateDepoAsync(Depo depo);
        Task<string> DeleteDepoAsync(int id);
        Task<Depo?> GetByIdAsync(int id);
    }
}