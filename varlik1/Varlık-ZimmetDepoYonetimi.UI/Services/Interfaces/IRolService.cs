using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface IRolService
    {
        Task<IEnumerable<Rol>> GetAllRolAsync();
        Task<Rol> GetRolByIdAsync(int id);
        Task AddRolAsync(Rol role);
        Task UpdateRolAsync(Rol role);
        Task DeleteRolAsync(int id);
    }
}