using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class EkipService : IEkipService
    {
        private readonly IEkipRepository _ekipRepository;

        public EkipService(IEkipRepository ekipRepository)
        {
            _ekipRepository = ekipRepository;
        }

        public async Task<IEnumerable<Ekip>> GetAllAsync()
        {
            return await _ekipRepository.GetAllAsync();
        }

        public async Task<Ekip> GetEkipByIdAsync(int id)
        {
            return await _ekipRepository.GetByIdAsync(id);
        }

        public async Task AddEkipAsync(Ekip ekip)
        {
            await _ekipRepository.AddAsync(ekip);
            await _ekipRepository.SaveAsync();
        }

        public async Task UpdateEkipAsync(Ekip ekip)
        {
            _ekipRepository.Update(ekip);
            await _ekipRepository.SaveAsync();
        }

        public async Task<string> DeleteEkipAsync(int id)
        {
            var ekip = await _ekipRepository.GetByIdAsync(id);
            if (ekip != null)
            {
                var message = await _ekipRepository.Delete(ekip.EkipId);
                await _ekipRepository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task AddSirketEkipAsync(SirketEkip sirketEkip)
        {
            var ekip = await _ekipRepository.GetByIdAsync(sirketEkip.EkipId);
            if (ekip != null)
            {
                ekip.SirketEkips.Add(sirketEkip);
                await _ekipRepository.SaveAsync();
            }
        }
    }
}