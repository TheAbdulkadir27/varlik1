using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class MusteriService : IMusteriService
    {
        private readonly IMusteriRepository _musteriRepository;

        public MusteriService(IMusteriRepository musteriRepository)
        {
            _musteriRepository = musteriRepository;
        }

        public async Task<IEnumerable<Musteri>> GetAllMusteriAsync()
        {
            return await _musteriRepository.GetAllAsync();
        }

        public async Task<Musteri> GetMusteriByIdAsync(int id)
        {
            return await _musteriRepository.GetByIdAsync(id);
        }

        public async Task AddMusteriAsync(Musteri musteri)
        {
            await _musteriRepository.AddAsync(musteri);
            await _musteriRepository.SaveAsync();
        }

        public async Task UpdateMusteriAsync(Musteri musteri)
        {
            _musteriRepository.Update(musteri);
            await _musteriRepository.SaveAsync();
        }

        public async Task DeleteMusteriAsync(int id)
        {
            var musteri = await _musteriRepository.GetByIdAsync(id);
            if (musteri != null)
            {
                _musteriRepository.Delete(musteri);
                await _musteriRepository.SaveAsync();
            }
        }
    }
}
