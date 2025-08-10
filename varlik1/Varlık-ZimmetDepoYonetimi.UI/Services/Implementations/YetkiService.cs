using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class YetkiService : IYetkiService
    {
        private readonly IYetkiRepository _yetkiRepository;

        public YetkiService(IYetkiRepository yetkiRepository)
        {
            _yetkiRepository = yetkiRepository;
        }

        public async Task<IEnumerable<Yetki>> GetAllAsync()
        {
            return await _yetkiRepository.GetAllAsync();
        }

        public async Task<Yetki?> GetByIdAsync(int id)
        {
            return await _yetkiRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Yetki yetki)
        {
            await _yetkiRepository.AddAsync(yetki);
            await _yetkiRepository.SaveAsync();
        }

        public async Task UpdateAsync(Yetki yetki)
        {
            _yetkiRepository.Update(yetki);
            await _yetkiRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var yetki = await _yetkiRepository.GetByIdAsync(id);
            if (yetki != null)
            {
                _yetkiRepository.Delete(yetki);
                await _yetkiRepository.SaveAsync();
            }
        }

        public Task<IEnumerable<Yetki>> GetAktifYetkilerAsync()
        {
            throw new NotImplementedException();
        }
    }
}