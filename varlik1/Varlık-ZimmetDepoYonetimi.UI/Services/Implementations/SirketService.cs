using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class SirketService : ISirketService
    {
        private readonly ISirketRepository _sirketRepository;

        public SirketService(ISirketRepository sirketRepository)
        {
            _sirketRepository = sirketRepository;
        }

        public async Task<IEnumerable<Sirket>> GetAllAsync()
        {
            return await _sirketRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Sirket>> GetAllSirketlerAsync()
        {
            return await _sirketRepository.GetAllAsync();
        }

        public async Task<Sirket?> GetByIdAsync(int id)
        {
            return await _sirketRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Sirket sirket)
        {
            await _sirketRepository.AddAsync(sirket);
            await _sirketRepository.SaveAsync();
        }

        public async Task UpdateAsync(Sirket sirket)
        {
            _sirketRepository.Update(sirket);
            await _sirketRepository.SaveAsync();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var sirket = await _sirketRepository.GetByIdAsync(id);
            if (sirket != null)
            {
                var message = await _sirketRepository.Delete(sirket.SirketId);
                await _sirketRepository.SaveAsync();
                return message;
            }
            return string.Empty;
        }
    }
}