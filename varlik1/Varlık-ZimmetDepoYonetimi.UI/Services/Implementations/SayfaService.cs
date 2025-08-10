using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class SayfaService : ISayfaService
    {
        private readonly ISayfaRepository _sayfaRepository;

        public SayfaService(ISayfaRepository sayfaRepository)
        {
            _sayfaRepository = sayfaRepository;
        }

        public async Task<IEnumerable<Sayfa>> GetAllAsync()
        {
            return await _sayfaRepository.GetAllAsync();
        }

        public async Task<Sayfa> GetByIdAsync(int id)
        {
            return await _sayfaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Sayfa sayfa)
        {
            await _sayfaRepository.AddAsync(sayfa);
            await _sayfaRepository.SaveAsync();
        }

        public async Task UpdateAsync(Sayfa sayfa)
        {
            _sayfaRepository.Update(sayfa);
            await _sayfaRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sayfa = await _sayfaRepository.GetByIdAsync(id);
            if (sayfa != null)
            {
                _sayfaRepository.Delete(sayfa);
                await _sayfaRepository.SaveAsync();
            }
        }

        public async Task<IEnumerable<Sayfa>> GetAktifSayfalarAsync()
        {
            return await _sayfaRepository.GetAktifSayfalarAsync();
        }
    }
}