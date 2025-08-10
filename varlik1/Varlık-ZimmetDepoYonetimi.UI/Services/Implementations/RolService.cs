using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<IEnumerable<Rol>> GetAllRolAsync()
        {
            return await _rolRepository.GetAllAsync();
        }

        public async Task<Rol> GetRolByIdAsync(int id)
        {
            return await _rolRepository.GetByIdAsync(id);
        }

        public async Task AddRolAsync(Rol role)
        {
            await _rolRepository.AddAsync(role);
            await _rolRepository.SaveAsync();
        }

        public async Task UpdateRolAsync(Rol role)
        {
            _rolRepository.Update(role);
            await _rolRepository.SaveAsync();
        }

        public async Task DeleteRolAsync(int id)
        {
            var role = await _rolRepository.GetByIdAsync(id);
            if (role != null)
            {
                _rolRepository.Delete(role);
                await _rolRepository.SaveAsync();
            }
        }
    }
}